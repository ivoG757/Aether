using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Data.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string HexColor { get; set; } = null!;
        public ICollection<RealmUserRole> RealmUsers { get; set; } = new HashSet<RealmUserRole>();

        [ForeignKey(nameof(Realm))]
        public Guid RealmId { get; set; }
        public Realm Realm { get; set; } = null!;
    }
}
