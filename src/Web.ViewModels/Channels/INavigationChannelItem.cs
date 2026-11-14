namespace Aether.Web.ViewModels.Channels
{
    public interface INavigationChannelItem
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Position { get; }
    }
}
