using Microsoft.EntityFrameworkCore;

namespace Aether.Data.Models
{
    [PrimaryKey(nameof(RealmId), nameof(UserId), nameof(RoleId))]
    public class RealmUserRole
    {
        public Guid RealmId { get; set; }
        public string UserId { get; set; } = null!;
        public Guid RoleId { get; set; }

        public RealmUser Membership { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
