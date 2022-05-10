using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Attributes
{
    public class StorageTypeAttribute : ValidationAttribute
    {
        private string[] allowedValues;

        public StorageTypeAttribute(string[] allowedValues)
        {
            this.allowedValues = allowedValues;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueString = value as string;

            if (allowedValues.Contains(valueString))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"{valueString} is not a valid storage type");
        }
    }
}
