namespace SistemaEncuestas.WebSite.Models
{
    #region Namespaces

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    public class EncuestaViewModel
    {
        #region Constructors

        public EncuestaViewModel()
        {
            Preguntas = new List<PreguntaViewModel>();
        }

        #endregion

        #region Properties

        public string Id { get; set; }

        [Display(Name = "Nombre Encuesta")]
        [Required(ErrorMessage = "Debe Ingresar un nombre para la encuesta.")]
        public string NombreEncuesta { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Debe Ingresar una descripción para la encuesta.")]
        public string DescripcionEncuesta { get; set; }

        [Display(Name = "Es interno")]
        public bool EsInterno { get; set; }

        public List<PreguntaViewModel> Preguntas { get; set; }

        public string IdEmpresa { get; set; }

        [Display(Name = "Encuesta activa")]
        public bool Activo { get; set; }

        public string Url { get; set; }

        #endregion
    }
}