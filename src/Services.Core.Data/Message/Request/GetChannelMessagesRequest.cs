namespace Aether.Services.Core.Data.Message.Request
{
    public class GetChannelMessagesRequest
    {
        public Guid RealmId { get; set; }
        public Guid ChannelId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
