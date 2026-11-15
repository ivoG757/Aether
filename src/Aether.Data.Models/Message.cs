using Aether.Data.Models.Enums;
using Aether.GCommon.Validation;
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

        [MaxLength(MessageValidationConstants.TextMessageMaxLength)]
        public string? Text { get; set; }
        public string? AttachmentUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(TextChannel))]
        public Guid TextChannelId { get; set; }
        public TextChannel TextChannel { get; set; } = null!; 

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
