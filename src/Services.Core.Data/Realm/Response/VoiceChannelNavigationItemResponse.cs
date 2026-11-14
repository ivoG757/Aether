namespace Aether.Services.Core.Data.Realm.Response
{
    public class VoiceChannelNavigationItemResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = null!;

        public int Position { get; init; }

        public string? Icon { get; init; }
    }
}
