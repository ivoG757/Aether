using Aether.Web.ViewModels.Channels;

namespace Aether.Web.ViewModels.Realm
{
    public class RealmStructureViewModel
    {
        public Guid RealmId { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public ICollection<TextChannelNavigationItemViewModel> TextChannels { get; set; } = 
            new List<TextChannelNavigationItemViewModel>();
        public ICollection<VoiceChannelNavigationItemViewModel> VoiceChannels { get; set; } = 
            new List<VoiceChannelNavigationItemViewModel>();
    }
}
