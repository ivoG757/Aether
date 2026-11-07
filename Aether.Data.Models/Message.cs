using Aether.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Data.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public MessageType Type { get; set; }
        public string? Text { get; set; }
        public string? AttachmentUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(User))]
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
