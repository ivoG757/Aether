using Aether.Web.ViewModels.Category;

namespace Aether.Web.ViewModels.Realm
{
    public class RealmStructureViewModel
    {
        public Guid RealmId { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public ICollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
