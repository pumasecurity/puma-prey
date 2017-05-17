using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puma.Prey.Common.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NameAttribute : ValidationAttribute
    {
        private const string _ERROR_MESSAGE = "{0} contains invalid characters.";

        public NameAttribute() : base(_ERROR_MESSAGE)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                bool isValid = false;
                
                if (!isValid)
                   return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_ERROR_MESSAGE, name);
        }
    }
}
