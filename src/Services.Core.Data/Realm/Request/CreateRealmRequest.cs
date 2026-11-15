using Aether.GCommon.Validation;
using System.ComponentModel.DataAnnotations;

namespace Aether.Services.Core.Data.Realm.Request
{
    public class CreateRealmRequest
    {
        [Required]
        [MaxLength(RealmValidationConstants.NameMaxLength), MinLength(RealmValidationConstants.NameMinLength)]
        public string RealmName { get; set; } = null!;

        [MaxLength(RealmValidationConstants.DescriptionMaxLength)]
        public string? Description { get; set; }

        [MaxLength(RealmValidationConstants.IconUrlMaxLength)]
        public string? Icon { get; set; } // icon shouldnt be string, but for now it is

        public bool IsPublic { get; set; } = false;
    }
}
