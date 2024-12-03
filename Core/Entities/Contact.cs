using Core.Entities.Enumerations;
using Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Contact : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FullName { get; set; }

        public string ProfileImageUri { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public ContactType ContactType { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
