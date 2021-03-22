using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

using VisitorRegistration_ASP.Resources;

namespace VisitorRegistration_ASP.Helpers
{
    public class CustomValidationAttributeAdapterProvider
        : ValidationAttributeAdapterProvider, IValidationAttributeAdapterProvider
    {
        public CustomValidationAttributeAdapterProvider() { }

        IAttributeAdapter IValidationAttributeAdapterProvider.GetAttributeAdapter(
            ValidationAttribute attribute,
            IStringLocalizer stringLocalizer)
        {
            if (attribute.ErrorMessageResourceName == null || attribute.ErrorMessageResourceType == null)
            {
                if (attribute is RequiredAttribute)
                {
                    attribute.ErrorMessageResourceName = nameof(ValidationMessages.RequiredAttribute);
                    attribute.ErrorMessageResourceType = typeof(ValidationMessages);
                }
                //else if (attribute is EmailAddressAttribute)
                //{
                //    attribute.ErrorMessage = "Invalid Email Address.";
                //}
                //else if (attribute is CompareAttribute)
                //{
                //    attribute.ErrorMessageResourceName = "InvalidCompare";
                //    attribute.ErrorMessageResourceType = typeof(Resources.ValidationMessages);
                //}
            }

            return base.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
