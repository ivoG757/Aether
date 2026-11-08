using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Aether.Web.ViewModels.Dashboard
{
    public class CreateRealmViewModel
    {
        [Required]
        [DisplayName("Realm Name")]
        public string RealmName { get; set; } = null!;

        [DisplayName("Realm Description")]
        public string? RealmDescription { get; set; }
        
        [DisplayName("Icon")]
        public string? Icon { get; set; } // icon shouldnt be string, but for now it is

        [DisplayName("Should this realm be public?")]
        public bool IsPublic { get; set; } = false;

    }
}
