namespace Aether.Services.Core.Data.Channel.Response
{
    public class TextChannelNavigationItemResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = null!;

        public string? Category { get; init; }

        public string? Icon { get; init; }
        //public int UnreadMessageCount { get; init; }

        //public int Position { get; init; } TODO: add positioning later
    }
}
