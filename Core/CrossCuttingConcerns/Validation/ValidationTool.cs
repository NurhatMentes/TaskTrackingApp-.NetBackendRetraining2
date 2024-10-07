using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(string.Join(", ", errors)); 
            }
        }
    }
}
