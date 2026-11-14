using Aether.Web.ViewModels.Channels;
namespace Aether.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Position { get; set; }
        public IEnumerable<INavigationChannelItem> ChannelsInOrder => 
        TextChannels.Concat<INavigationChannelItem>(VoiceChannels)
        .OrderBy(channel => channel.Position)
        .ThenBy(channel => channel.Name);

        public ICollection<TextChannelNavigationItemViewModel> TextChannels { get; set; } = new List<TextChannelNavigationItemViewModel>();
        public ICollection<VoiceChannelNavigationItemViewModel> VoiceChannels { get; set; } = new List<VoiceChannelNavigationItemViewModel>();
    }
}
