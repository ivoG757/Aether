namespace Aether.Services.Core.Data.User.Response
{
    public class JoinedRealmsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public int MemberCount { get; set; }
    }
}
