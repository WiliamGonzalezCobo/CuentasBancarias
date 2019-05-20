using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBanc.WebCliente.Models
{
    public class TransaccionModelo
    {
        public int id_transaccion { get; set; }
        public int id_Cuenta { get; set; }

        [Display(Name = "Monto")]
        public int monto { get; set; }

        [Display(Name = "Tipo Transaccion")]
        public int tipoTransaccion { get; set; }


        [Display(Name = "Cuenta Destino")]
        public int id_CuentaDestino { get; set; }

        public int id_Cliente { get; set; }

        public DateTime fecha { get; set; }
    }
}