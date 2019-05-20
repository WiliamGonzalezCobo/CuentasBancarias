namespace SistemaEncuestas.WebSite.Models
{
    #region Namespaces

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class CompartirViewModel
    {
        #region Properties

        [Display(Name = "Url de la encuesta")]
        public string Url { get; set; }

        [Display(Name = "Escriba los correos")]
        [RegularExpression("(([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)(\\s*(;|,)\\s*|\\s*$))*", ErrorMessage = "Uno de los correos es inválido.")]
        [Required(ErrorMessage = "Debe escribir al menos un correo.")]
        public string Correos { get; set; }

        [Display(Name = "Escriba el asunto")]
        [Required(ErrorMessage = "Debe escribir un asunto.")]
        public string Asunto { get; set; }

        public string IdEmpresa { get; set; }

        #endregion
    }
}