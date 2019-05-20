namespace SistemaEncuestas.WebSite.Models
{
    #region Namespaces

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class RegistroUsuarioViewModel
    {
        #region Properties

        [DataType(DataType.Text)]
        [Display(Name = "Usuario")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Escriba la contraseña.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "La contraseña es diferente a la escrita.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "Escriba la confirmación de la contraseña.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Escriba el nombre del usuario.")]
        public string Nombre { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Primer apellido")]
        [Required(ErrorMessage = "Escriba el primer apellido del usuario.")]
        public string PrimerApellido { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Segundo apellido")]
        [Required(ErrorMessage = "Escriba el segundo apellido del usuario.")]
        public string SegundoApellido { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo electrónico")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$", ErrorMessage = "El correo electrónico no es válido.")]
        [Required(ErrorMessage = "Escriba el correo electrónico.")]
        public string Correo { get; set; }

        #endregion
    }
}