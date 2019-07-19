using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NJsonSchema.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coyote.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddIdentityErrorsToModelState(this ModelStateDictionary modelState, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError("", error.Description);
            }
        }

        public static void AddErrorListToModelState(this ModelStateDictionary modelState, List<string> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError("", error);
            }
        }

        public static void AddValidationErrorsToModelState(this ModelStateDictionary modelState, List<ValidationError> errors)
        {
            foreach (var error in errors)
            {
                //modelState.AddModelError(error.PropertyName, error.Error);
            }
        }
    }
}
