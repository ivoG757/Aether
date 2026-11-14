namespace Aether.Services.Core.Data.Realm.Response
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Position { get; set; }
        public ICollection<TextChannelNavigationItemResponse> TextChannelNavigationItems = 
            new List<TextChannelNavigationItemResponse>();
        public ICollection<VoiceChannelNavigationItemResponse> VoiceChannelNavigationItems =
            new List<VoiceChannelNavigationItemResponse>();
    }
}
