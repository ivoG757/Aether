using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Data.Models
{
    public class Realm
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public string? Icon { get; set; } // icon shouldnt be string, but for now it is
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;
        public ApplicationUser Owner { get; set; } = null!;

        public ICollection<RealmUser> Members { get; set; } = new HashSet<RealmUser>();
        public ICollection<TextChannel> TextChannels { get; set; } = new HashSet<TextChannel>();
        public ICollection<VoiceChannel> VoiceChannels { get; set; } = new HashSet<VoiceChannel>();
        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        public string InviteCode { get; set; } = null!;
        public bool IsPublic { get; set; }
    }
}
