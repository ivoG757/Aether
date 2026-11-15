namespace Aether.Services.Core.Data.Message.Response
{
    public class MessageResponse
    {
        public Guid Id { get; init; }

        public string SenderId { get; init; } = null!;

        public string SenderName { get; init; } = null!;

        public string? SenderIcon { get; init; }

        public string? Text { get; init; }

        public string? AttachmentUrl { get; init; }

        public DateTime CreatedAt { get; init; }
    }
}