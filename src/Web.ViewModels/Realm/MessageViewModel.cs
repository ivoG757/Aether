namespace Aether.Web.ViewModels.Realm
{
    public class MessageViewModel
    {
        public Guid Id { get; init; }
        public string Content { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public string SenderName { get; init; } = string.Empty;
        public string SenderAvatarUrl { get; init; } = string.Empty;
    }
}
