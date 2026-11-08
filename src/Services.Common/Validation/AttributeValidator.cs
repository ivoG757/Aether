using System.ComponentModel.DataAnnotations;

namespace Aether.Services.Common.Validation
{
    public static class AttributeValidator
    {
        public static bool IsValid<T>(T model, out IReadOnlyCollection<ValidationResult> errors)
        where T : class
        {
            List<ValidationResult> results = [];

            bool isValid = Validator.TryValidateObject(
                model,
                new ValidationContext(model),
                results,
                validateAllProperties: true);

            errors = results;

            return isValid;
        }
    }
}
