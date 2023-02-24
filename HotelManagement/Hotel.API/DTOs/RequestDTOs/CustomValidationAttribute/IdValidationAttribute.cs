namespace Hotel.API.DTOs.RequestDTOs.CustomValidationAttribute
{
    using System.ComponentModel.DataAnnotations;

    public class IdValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((int)value <= 0)
            {
                return new ValidationResult("Must be greater than 0!");
            }

            return ValidationResult.Success;
        }
    }

}
