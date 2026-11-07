using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Data.Models
{
    public class TextChannel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public string? Category { get; set; }
        public string? Icon { get; set; }  // icon shouldnt be string, but for now it is
        public string? Description { get; set; }

        [ForeignKey(nameof(Realm))]
        public Guid RealmId { get; set; }
        public Realm Realm { get; set; } = null!;
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
