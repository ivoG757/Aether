using System.ComponentModel.DataAnnotations;

namespace Aether.Services.Core.DTOs.Realm.Request
{
    public class CreateRealmRequest
    {
        [Required]
        public string RealmName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Icon { get; set; } // icon shouldnt be string, but for now it is

        public bool IsPublic { get; set; } = false;
    }
}
