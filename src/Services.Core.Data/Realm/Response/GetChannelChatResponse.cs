namespace Aether.Services.Core.Data.Realm.Response
{
    public class GetChannelChatResponse
    {
        public ResponseStatus Status { get; set; }
        public string? ChannelName { get; set; }
        public Guid? ChannelId { get; set; }
    }
}
