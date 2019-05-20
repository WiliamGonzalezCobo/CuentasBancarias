namespace SistemaEncuestas.WebSite.Models
{
    #region Namespaces

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class ItemPreguntaViewModel
    {
        #region Properties

        public string Id { get; set; }

        [Display(Name = "Opción a ingresar")]
        [Required(ErrorMessage = "Debe Ingresar una opción.")]
        public string Valor { get; set; }

        public bool Eliminado { get; set; }

        #endregion
    }
}