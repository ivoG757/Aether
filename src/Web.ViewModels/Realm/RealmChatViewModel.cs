namespace Aether.Web.ViewModels.Realm
{
    public class RealmChatViewModel : IRealmPageViewModels
    {
        public Guid RealmId { get; init; }
        public string ChannelName { get; init; } = string.Empty;
        public ICollection<MessageViewModel> Messages { get; init; } = new List<MessageViewModel>();
    }
}
