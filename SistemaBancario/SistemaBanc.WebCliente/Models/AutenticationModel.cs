using System.ComponentModel.DataAnnotations;

namespace SistemaBanc.WebCliente.Models
{
    public class AutenticationModel
    {
        [MinLength(4, ErrorMessage = "El usuario debe tener mínimo 4 caracteres.")]
        [Required(ErrorMessage = "Ingrese su Usuario.")]
        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener mínimo 6 caracteres.")]
        [Required(ErrorMessage = "Ingrese su Contraseña.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

    }
}