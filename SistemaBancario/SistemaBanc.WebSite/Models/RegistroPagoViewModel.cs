namespace SistemaEncuestas.WebSite.Models
{
    #region Namespaces

    using Attributes;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class RegistroPagoViewModel
    {
        #region Properties

        [Display(Name = "Tipo de tarjeta.")]
        [ValidarCombos(ErrorMessage = "Debe seleccionar un tipo de tarjeta.")]
        public string TipoTarjeta { get; set; }

        [Display(Name = "Número de la tarjeta")]
        [ValidarTarjeta(franquicia: "Franquicia", idCliente: "Pago.Franquicia")]
        public string NumeroTarjeta { get; set; }

        [Display(Name = "CVV de la tarjeta")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "Ingresa los dígitos de verificación.")]
        public string CodigoSeguridad { get; set; }

        [Display(Name = "Mes")]
        [ValidarCombos(ErrorMessage = "Seleccione el mes de vencimiento de la tarjeta.")]
        public string MesTarjeta { get; set; }

        [Display(Name = "Año")]
        [ValidarCombos(ErrorMessage = "Seleccione el mes de vencimiento de la tarjeta.")]
        [FechaVencimiento(propiedad: "VencimientoMes", idCliente: "Pago.MesTarjeta", ErrorMessage = "La fecha de vencimiento debe ser mayor que el mes actual.")]
        public string AnioTarjeta { get; set; }

        [Display(Name = "País de origen")]
        [ValidarCombos(ErrorMessage = "Seleccione el año de vencimiento de la tarjeta.")]
        public string PaisTarjeta { get; set; }

        [Display(Name = "Número de cuotas")]
        [ValidarCombos(ErrorMessage = "Seleccione el número de cuotas.")]
        public int NumeroCuotas { get; set; }

        [Display(Name = "Franquicia.")]
        [ValidarCombos(ErrorMessage = "Seleccione el mes de vencimiento de la tarjeta.")]
        public string Franquicia { get; set; }

        #endregion
    }
}