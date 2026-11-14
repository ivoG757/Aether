namespace Aether.Web.ViewModels.Channels
{
    public class VoiceChannelNavigationItemViewModel : INavigationChannelItem
    {
        public Guid Id { get; init; }

        public int Position { get; init; }

        public string Name { get; init; } = null!;

        public string? Category { get; init; }

        public string? Icon { get; init; }
    }
}
