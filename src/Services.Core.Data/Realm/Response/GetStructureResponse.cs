using Aether.Services.Core.Data.Channel.Response;

namespace Aether.Services.Core.Data.Realm.Response
{
    public class GetStructureResponse
    {
        public Guid? RealmId { get; init; }
        public ResponseStatus Status { get; init; }
        public string? Name { get; init; }
        public string? Icon { get; init; }
        public IReadOnlyCollection<TextChannelNavigationItemResponse> TextChannels { get; init; } = new List<TextChannelNavigationItemResponse>();
        public IReadOnlyCollection<VoiceChannelNavigationItemResponse> VoiceChannels { get; init; } = new List<VoiceChannelNavigationItemResponse>();
    }
}
