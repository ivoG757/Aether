namespace Aether.Services.Core.Data.Message.Response
{
    public class GetChannelMessagesResponse
    {
        public ResponseStatus Status { get; set; }
        public ICollection<MessageResponse> Messages { get; set; } = new List<MessageResponse>();
    }
}
