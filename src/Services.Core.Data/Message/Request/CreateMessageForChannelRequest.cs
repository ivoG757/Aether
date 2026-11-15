using System.ComponentModel.DataAnnotations;
using Aether.GCommon.Validation;
namespace Aether.Services.Core.Data.Message.Request
{
    public class CreateMessageForChannelRequest
    {
        [Required]
        [MaxLength(MessageValidationConstants.TextMessageMaxLength)]
        public string Text { get; set; } = null!;
        public Guid RealmId { get; set; }
        public Guid ChannelId { get; set; }
    }
}
