namespace SistemaEncuestas.WebSite.Attributes
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;

    #endregion

    public class FechaVencimientoAttribute : ValidationAttribute, IClientValidatable
    {
        #region Attributes

        private readonly string _propiedad;
        private readonly string _idCliente;

        #endregion

        #region Constructor

        public FechaVencimientoAttribute(string propiedad, string idCliente)
        {
            _propiedad = propiedad;
            _idCliente = idCliente;
        }

        #endregion

        #region Public Methods

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageString,
                ValidationType = "fechavencimiento"
            };

            rule.ValidationParameters["campomes"] = _idCliente;

            yield return rule;
        }

        #endregion

        #region Protected Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propiedadMesInfo = validationContext.ObjectType.GetProperty(this._propiedad);
            if (propiedadMesInfo == null)
            {
                return new ValidationResult(string.Format("Propiedad Desconocida {0}", this._propiedad));
            }

            var propiedadMesValue = propiedadMesInfo.GetValue(validationContext.ObjectInstance, null);

            if (string.IsNullOrEmpty(propiedadMesValue as string) || string.IsNullOrEmpty(value as string))
            {
                return ValidationResult.Success;
            }

            DateTime seleccionada = new DateTime(int.Parse(value.ToString(), CultureInfo.CurrentCulture), int.Parse(propiedadMesValue.ToString(), CultureInfo.CurrentCulture), 1);

            if (seleccionada > DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        #endregion
    }
}