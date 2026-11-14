namespace Aether.Services.Core.Data.Realm.Response
{
    public class TextChannelNavigationItemResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = null!;

        public string? Icon { get; init; }

        public int Position { get; init; }
    }
}
