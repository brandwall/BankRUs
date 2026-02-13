using System.ComponentModel.DataAnnotations;

namespace BankRUs.Api.Attributes
{
    public class IsPositiveDecimal : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // om det är null så skippar den bara den här valideringen
            // för att kräva ett värde använd [Required] attributet
            if (value is null)
                return ValidationResult.Success;

            if (value is not decimal amount)
                return new ValidationResult("Value must be of type decimal");

            if (amount <= 0)
                return new ValidationResult("Amount must be more than 0");

            return ValidationResult.Success;
        }
    }
}
