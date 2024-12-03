using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Contact
{
    public class EditContactDto : BaseContactRequestDto
    {
        [Required]
        public string Id { get; set; }
    }
}
