namespace Aether.Services.Core.Data.Channel.Response
{
    public class VoiceChannelNavigationItemResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = null!;

        public string? Category { get; init; }

        public string? Icon { get; init; }
    }
}
