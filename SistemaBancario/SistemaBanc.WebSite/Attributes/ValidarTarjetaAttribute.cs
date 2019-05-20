namespace SistemaEncuestas.WebSite.Attributes
{
    #region Namespaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using Utilidades.Recursos;

    #endregion

    public class ValidarTarjetaAttribute : ValidationAttribute, IClientValidatable
    {
        #region Attributes

        private readonly string _franquicia;
        private readonly string _idCliente;

        #endregion

        #region Constructor

        public ValidarTarjetaAttribute(string franquicia, string idCliente)
        {
            _franquicia = franquicia;
            _idCliente = idCliente;
        }

        #endregion

        #region Properties

        public string Franquicia
        {
            get
            {
                return _franquicia;
            }
        }

        public string IdCliente
        {
            get
            {
                return _idCliente;
            }
        }

        #endregion

        #region Public Methods

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string errorVI = Mensajes.nombreRecursoVI;
            string errorMC = Mensajes.nombreRecursoMC;
            string errorDI = Mensajes.nombreRecursoDI;
            string errorAE = Mensajes.nombreRecursoAE;
            string errorDC = Mensajes.nombreRecursoDC;

            string jsonErroresFranquicias = string.Format(Mensajes.JSONMensajesFranquicia.ToString(), errorVI, errorMC, errorDI, errorAE, errorDC).Replace("\'", "\"");

            var rule = new ModelClientValidationRule
            {
                ErrorMessage = jsonErroresFranquicias,
                ValidationType = "tarjeta"
            };

            rule.ValidationParameters["nombrecliente"] = _idCliente;

            yield return rule;
        }

        #endregion

        #region Protected Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var franquiciaObject = validationContext.ObjectType.GetProperty(this._franquicia);

            if (franquiciaObject == null)
            {
                return new ValidationResult(string.Format("Propiedad Desconocida {0}", this._franquicia));
            }

            var franquiciaValue = franquiciaObject.GetValue(validationContext.ObjectInstance, null);
            string franquicia = franquiciaValue as string;

            if (string.IsNullOrEmpty(franquicia))
            {
                return ValidationResult.Success;
            }

            string regex = string.Empty;
            string mensajeFranquicia = string.Empty;

            switch (franquicia)
            {
                case "1": // Visa
                    regex = @"^4\d{3}-?\d{4}-?\d{4}-?\d{4}$";
                    mensajeFranquicia = Mensajes.ValidacionErrorTarjetaVisa;
                    break;
                case "2": // MasterCard
                    regex = @"^(2|5)[1-5]\d{2}-?\d{4}-?\d{4}-?\d{4}$";
                    mensajeFranquicia = Mensajes.ValidacionErrorTarjetaMaster;
                    break;
                case "3": // Dinners
                    regex = @"^3[0,6,8]\d{12}$";
                    mensajeFranquicia = Mensajes.ValidacionErrorTarjetaDiners;
                    break;
                case "4": // AmericanExpress
                    regex = @"^3[4,7]\d{13}$";
                    mensajeFranquicia = Mensajes.ValidacionErrorTarjetaAmerican;
                    break;
                case "5": // Discover
                    regex = @"^6\d{3}-?\d{4}-?\d{4}-?\d{4}$";
                    mensajeFranquicia = Mensajes.ValidacionErrorTarjetaDiscover;
                    break;
                default:
                    break;
            }

            string valorCampo = value == null ? string.Empty : value as string;
            if (!Regex.IsMatch(valorCampo, regex))
            {
                return new ValidationResult(mensajeFranquicia);
            }
            else
            {
                var checksum = 0;
                char tarjeta = valorCampo[0];
                for (int i = 2 - (valorCampo.Length % 2); i <= valorCampo.Length; i += 2)
                {
                    checksum += int.Parse(valorCampo[i - 1].ToString());
                }

                for (var i = (valorCampo.Length % 2) + 1; i < valorCampo.Length; i += 2)
                {
                    var digit = int.Parse(valorCampo[i - 1].ToString()) * 2;
                    if (digit < 10)
                    {
                        checksum += digit;
                    }
                    else
                    {
                        checksum += digit - 9;
                    }
                }

                if ((checksum % 10) == 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
        }

        #endregion
    }
}