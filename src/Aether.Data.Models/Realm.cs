using Aether.GCommon.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Data.Models
{
    public class Realm
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(RealmValidationConstants.NameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(RealmValidationConstants.DescriptionMaxLength)]
        public string? Description { get; set; }

        [MaxLength(RealmValidationConstants.IconUrlMaxLength)]
        public string? Icon { get; set; } // icon shouldnt be string, but for now it is
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;
        public ApplicationUser Owner { get; set; } = null!;

        public ICollection<RealmUser> Members { get; set; } = new HashSet<RealmUser>();
        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        public string InviteCode { get; set; } = null!;
        public bool IsPublic { get; set; }
    }
}
