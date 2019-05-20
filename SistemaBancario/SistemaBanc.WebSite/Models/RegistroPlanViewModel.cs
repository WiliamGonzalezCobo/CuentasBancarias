namespace SistemaEncuestas.WebSite.Models
{
    #region Namespaces

    using Attributes;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class RegistroPlanViewModel
    {
        #region Properties

        [Display(Name = "Plan")]
        [ValidarCombos(ErrorMessage = "Seleccione el plan a adquirir.")]
        public string Id { get; set; }

        public string Nombre { get; set; }

        public string Precio { get; set; }

        #endregion
    }
}