using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.ComponentModel.DataAnnotations;

namespace MultiLingualWeb;

public class MyModelMetadataProvider : IValidationMetadataProvider
{
    private void AddRequiredAttributeIfValueType(Type type, IList<object> validators)
    {
        if (!type.IsValueType) return;
        if (Nullable.GetUnderlyingType(type) != null) return;
        if (validators.OfType<RequiredAttribute>().Any()) return;

        validators.Add(new RequiredAttribute());
    }

    public void CreateValidationMetadata(ValidationMetadataProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var validators = context.ValidationMetadata.ValidatorMetadata;

        // add [Required] for value-types (int/DateTime etc)
        // to set ErrorMessage before asp.net does it
        AddRequiredAttributeIfValueType(context.Key.ModelType, validators);

        foreach (var validationAttribute in validators.OfType<ValidationAttribute>())
        {
            switch (validationAttribute.GetType().Name)
            {
                case nameof(RequiredAttribute):
                    validationAttribute.ErrorMessage = "Please please please provide a value for {0}";
                    break;
                case nameof(MaxLengthAttribute):
                    validationAttribute.ErrorMessage = "{0} must not exceed {1} characters in length.";
                    break;
            }
            Console.WriteLine(new string('-', Console.WindowWidth));
        }
    }
}