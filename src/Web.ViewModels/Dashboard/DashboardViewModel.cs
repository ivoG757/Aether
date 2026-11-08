namespace Aether.Web.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public IEnumerable<RealmInfoViewModel> Realms { get; set; } = new List<RealmInfoViewModel>();
    }
}
