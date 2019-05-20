using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaBanc.WebCliente.Models
{
    public class CuentaModel
    {
        [Display(Name ="id Cuenta")]
        public int id_Cuenta { get; set; }

        public int saldo { get; set; }

        public DateTime fecha_creacion { get; set; }

        [Display(Name = "id Cliente")]
        public int id_Cliente { get; set; }
    }
}