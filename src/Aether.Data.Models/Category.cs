using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aether.GCommon.Validation;
namespace Aether.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(CategoryValidationConstants.NameMaxLength)]
        public string Name { get; set; } = null!;
        public ICollection<TextChannel> TextChannels { get; set; } = new HashSet<TextChannel>();
        public ICollection<VoiceChannel> VoiceChannels { get; set; } = new HashSet<VoiceChannel>();

        [ForeignKey(nameof(Realm))]
        public Guid RealmId { get; set; }
        public Realm Realm { get; set; } = null!;
        public int Position { get; set; }
    }
}
