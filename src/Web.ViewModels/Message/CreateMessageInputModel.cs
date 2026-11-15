using System.ComponentModel.DataAnnotations;
using Aether.GCommon.Validation;
namespace Aether.Web.ViewModels.Message
{
    public class CreateMessageInputModel
    {
        [Required]
        [MaxLength(MessageValidationConstants.TextMessageMaxLength)]
        public string Text { get; set; } = null!;
    }
}
