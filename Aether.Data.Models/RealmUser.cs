using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Data.Models
{
    [PrimaryKey(nameof(RealmId), nameof(UserId))]
    public class RealmUser
    {
        [ForeignKey(nameof(Realm))]
        public Guid RealmId { get; set; }
        public Realm Realm { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public ICollection<RealmUserRole> Roles { get; set; } = new HashSet<RealmUserRole>();
        public DateTime JoinedAt { get; set; }
    }
}
