using API.Attributes;
using API.Errors;
using API.Extensions;
using Core.Dtos.Contact;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ContactController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IContactService contactService;

        public ContactController(
            UserManager<ApplicationUser> userManager,
            IContactService contactService)
        {
            this.userManager = userManager;
            this.contactService = contactService;
        }

        [CustomAuthorize]
        [HttpGet]
        public async Task<ActionResult> GetAllByCurrentUser()
        {
            var user = await this.userManager
                .FindByEmailFromClaimsPrincipalAsync(this.User);

            ICollection<ContactDto> contactDtos = await this.contactService
                .GetAllByUserIdAsync(user.Id);

            return this.Ok(contactDtos);
        }

        [CustomAuthorize]
        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetById([FromRoute] string contactId)
        {
            if (!await this.contactService.ExistsAsync(contactId))
            {
                return this.NotFound(new ApiResponse(StatusCodes.Status404NotFound, Constants.CONTACT_NOT_FOUND));
            }

            ContactDto contactDto = await this.contactService
                .GetByIdAsync(contactId);

            return this.Ok(contactDto);
        }

        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateContactDto createContactDto)
        {
            ContactDto contactDto = await this.contactService
                .CreateAsync(createContactDto);

            return this.Ok(contactDto);
        }

        [CustomAuthorize]
        [HttpPut("{contactId}")]
        public async Task<IActionResult> Edit([FromRoute] string contactId, [FromForm] EditContactDto editContactDto)
        {
            if (!await this.contactService.ExistsAsync(editContactDto.Id))
            {
                return this.NotFound(new ApiResponse(StatusCodes.Status404NotFound, Constants.CONTACT_NOT_FOUND));
            }

            ContactDto contactDto = await this.contactService
                .UpdateAsync(editContactDto);

            return this.Ok(contactDto);
        }

        [CustomAuthorize]
        [HttpDelete("{contactId}")]
        public async Task<IActionResult> Delete([FromRoute] string contactId)
        {
            if (!await this.contactService.ExistsAsync(contactId))
            {
                return this.NotFound(new ApiResponse(StatusCodes.Status404NotFound, Constants.CONTACT_NOT_FOUND));
            }

            await this.contactService
                .DeleteAsync(contactId);

            return this.NoContent();
        }
    }
}
