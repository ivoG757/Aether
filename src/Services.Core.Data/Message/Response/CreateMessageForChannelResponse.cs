namespace Aether.Services.Core.Data.Message.Response
{
    public class CreateMessageForChannelResponse
    {
        public ResponseStatus Status { get; set; }

        public Guid? MessageId { get; set; }

        public string? Text { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? ProfilePic { get; set; }

        public string Username { get; set; } = null!;
    }
}
