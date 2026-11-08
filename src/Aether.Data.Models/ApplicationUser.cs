using Aether.GCommon.Validation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Aether.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(UserValidationConstants.ProfilePictureUrlMaxLength)]
        public string? ProfilePicture { get; set; } // profile pic shouldnt be string, but for now it is

        [MaxLength(UserValidationConstants.BioMaxLength)]
        public string? Bio { get; set; }

        [MaxLength(UserValidationConstants.CustomStatusMessageMaxLength)]
        public string? CustomStatusMessage { get; set; }
        public DateTime AccountCreationDate { get; set; }

        public ICollection<RealmUser> Realms { get; set; } = new HashSet<RealmUser>();
        public bool IsDeleted { get; set; }
    }
}
