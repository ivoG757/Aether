namespace Aether.Web.ViewModels.Dashboard
{
    public class RealmInfoViewModel
    {
        public Guid RealmId { get; init; }
        public string RealmName { get; init; } = null!;
        public string? RealmDescription { get; init; }
        public string? Icon { get; init; }
        public int MemberCount { get; init; }
    }
}
