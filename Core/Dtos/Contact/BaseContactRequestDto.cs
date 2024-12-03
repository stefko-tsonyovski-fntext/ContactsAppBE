using Core.Entities.Enumerations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Contact
{
    public class BaseContactRequestDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FullName { get; set; }

        public IFormFile File { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string ContactType { get; set; }

        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
