namespace SistemaEncuestas.WebSite.Models
{
    #region Namespaces

    using Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class PreguntaViewModel
    {
        #region Constructors

        public PreguntaViewModel()
        {
            ItemsPregunta = new List<ItemPreguntaViewModel>();
        }

        #endregion

        #region Properties

        public string Id { get; set; }

        [Display(Name = "Pregunta de la encuesta")]
        [Required(ErrorMessage = "Debe Ingresar la pregunta.")]
        public string Descripcion { get; set; }

        [Display(Name = "Es requerido")]
        public bool Requerido { get; set; }

        [Display(Name = "Seleccione un control")]
        [ValidarCombos(ErrorMessage = "Debe seleccionar un control.")]
        public string IdMetadata { get; set; }

        public List<ItemPreguntaViewModel> ItemsPregunta { get; set; }

        public bool Eliminado { get; set; }

        public int Index { get; set; }

        #endregion
    }
}