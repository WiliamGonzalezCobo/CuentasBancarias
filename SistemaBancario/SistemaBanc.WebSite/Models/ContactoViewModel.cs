namespace SistemaEncuestas.WebSite.Models
{
    #region Namespaces

    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class ContactoViewModel
    {
        #region Properties

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Ingrese su nombre completo.")]
        public string Nombre { get; set; }

        [DisplayName("Correo electrónico")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$", ErrorMessage = "Ingrese un correo válido.")]
        [Required(ErrorMessage = "Ingrese su correo electrónico.")]
        public string Email { get; set; }

        [DisplayName("Asunto")]
        [Required(ErrorMessage = "Ingrese el asunto del correo.")]
        public string Asunto { get; set; }

        [DisplayName("Mensaje")]
        [Required(ErrorMessage = "Ingrese el mensaje del correo.")]
        public string Mensaje { get; set; }

        #endregion
    }
}