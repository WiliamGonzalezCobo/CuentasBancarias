namespace SistemaEncuestas.WebSite.Models
{
    #region Namespaces

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class RegistroEmpresaViewModel
    {
        #region Properties

        [DataType(DataType.Text)]
        [Display(Name = "NIT de la empresa")]
        [RegularExpression("[0-9]{8}-[0-9]{1}", ErrorMessage = "El NIT es inválido.")]
        [Required(ErrorMessage = "Escriba el NIT de la empresa.")]
        public string Nit { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Escriba el nombre de la empresa.")]
        public string Nombre { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Escriba la dirección de la empresa.")]
        public string Direccion { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Página o sitio web")]
        [RegularExpression("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$", ErrorMessage = "Debe escribir una página web.")]
        public string SitioWeb { get; set; }

        #endregion
    }
}