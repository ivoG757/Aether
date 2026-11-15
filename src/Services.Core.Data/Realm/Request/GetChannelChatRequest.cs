namespace Aether.Services.Core.Data.Realm.Request
{
    public class GetChannelChatRequest
    {
        public Guid RealmId { get; set; }
        public Guid? ChannelId { get; set; }
    }
}
