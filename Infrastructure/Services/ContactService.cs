using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Contact;
using Core.Entities;
using Core.Entities.Enumerations;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationContext dbContext;
        private readonly IMapper mapper;
        private readonly ICloudinaryService cloudinaryService;

        public ContactService(
            ApplicationContext dbContext,
            IMapper mapper,
            ICloudinaryService cloudinaryService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<ContactDto> CreateAsync(CreateContactDto createContactDto)
        {
            Contact contact = this.mapper.Map<CreateContactDto, Contact>(createContactDto);

            if (createContactDto.File != null)
            {
                contact.ProfileImageUri = await this.cloudinaryService
                    .UploadAsync(createContactDto.File);
            }

            contact.ContactType = this.GetContactType(createContactDto.ContactType);

            EntityEntry<Contact> contactEntity = await this.dbContext.Contacts
                .AddAsync(contact);

            await this.dbContext.SaveChangesAsync();

            return this.mapper.Map<Contact, ContactDto>(contactEntity.Entity);
        }

        public async Task DeleteAsync(string contactId)
        {
            Contact contact = await this.dbContext.Contacts
                .FirstOrDefaultAsync(c => c.Id == contactId);

            this.dbContext.Contacts.Remove(contact);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string contactId)
        {
            return await this.dbContext.Contacts
                .AnyAsync(c => c.Id == contactId);
        }

        public async Task<ICollection<ContactDto>> GetAllByUserIdAsync(string userId)
        {
            ICollection<ContactDto> contactDtos = await this.dbContext.Contacts
                .Where(c => c.UserId == userId)
                .ProjectTo<ContactDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return contactDtos;
        }

        public async Task<ContactDto> GetByIdAsync(string contactId)
        {
            ContactDto contactDto = await this.dbContext.Contacts
                .Where(c => c.Id == contactId)
                .ProjectTo<ContactDto>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return contactDto;
        }

        public async Task<ContactDto> UpdateAsync(EditContactDto editContactDto)
        {
            Contact contact = await this.dbContext.Contacts
                .FirstOrDefaultAsync(c => c.Id == editContactDto.Id);

            contact = this.mapper.Map(editContactDto, contact);

            if (editContactDto.File != null)
            {
                contact.ProfileImageUri = await this.cloudinaryService
                    .UploadAsync(editContactDto.File);
            }

            contact.ContactType = this.GetContactType(editContactDto.ContactType);

            EntityEntry<Contact> contactEntity = this.dbContext.Contacts
                .Update(contact);

            await this.dbContext.SaveChangesAsync();

            return this.mapper.Map<Contact, ContactDto>(contactEntity.Entity);
        }

        private ContactType GetContactType(string contactType)
        {
            ContactType result = ContactType.Work;

            switch (contactType)
            {
                case "BUSINESS":
                    result = ContactType.Business;
                    break;
                case "PERSONAL":
                    result = ContactType.Personal;
                    break;
                case "OTHER":
                    result = ContactType.Other;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
