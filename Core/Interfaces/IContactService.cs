using Core.Dtos.Contact;

namespace Core.Interfaces
{
    public interface IContactService
    {
        Task<ContactDto> CreateAsync(CreateContactDto createContactDto);

        Task<ContactDto> UpdateAsync(EditContactDto editContactDto);

        Task DeleteAsync(string contactId);

        Task<bool> ExistsAsync(string contactId);

        Task<ContactDto> GetByIdAsync(string contactId);

        Task<ICollection<ContactDto>> GetAllByUserIdAsync(string userId);
    }
}
