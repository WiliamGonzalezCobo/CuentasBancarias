namespace SistemaEncuestas.WebSite.Attributes
{
    #region Namespaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    #endregion

    public class ValidarCombosAttribute : ValidationAttribute, IClientValidatable
    {
        #region Public Methods

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.DisplayName),
                ValidationType = "validarcombos"
            };

            rule.ValidationParameters["combo"] = "ValorCombos";

            yield return rule;
        }

        #endregion

        #region Protected Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            string itemValue = value.ToString();

            if (itemValue.Equals("0"))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        #endregion
    }
}