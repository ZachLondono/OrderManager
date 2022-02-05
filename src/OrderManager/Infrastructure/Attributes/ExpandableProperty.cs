using System.ComponentModel.DataAnnotations;

namespace OrderManager.ApplicationCore.Infrastructure.Attributes;

/// <summary>
/// A property/field which can be expanded
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class ExpandableProperty : ValidationAttribute {
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {

        if (value is null or not IDictionary<string, object>)
            return new ValidationResult($"The required attrribute must be of type IDictionary<string, object>");

        if (ValidationResult.Success is null) throw new InvalidOperationException("Validation state is null");

        return ValidationResult.Success;

    }

}
