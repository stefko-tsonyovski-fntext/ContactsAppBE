using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Contacts = new HashSet<Contact>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
