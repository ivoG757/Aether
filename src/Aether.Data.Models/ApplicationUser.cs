using Microsoft.AspNetCore.Identity;

namespace Aether.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfilePicture { get; set; } // profile pic shouldnt be string, but for now it is
        public string? Bio { get; set; }
        public string? CustomStatusMessage { get; set; }
        public DateTime AccountCreationDate { get; set; }

        public ICollection<RealmUser> Realms { get; set; } = new HashSet<RealmUser>();
        public bool IsDeleted { get; set; }
    }
}
