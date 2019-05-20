using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.Models
{
    public class ClienteCuentaModel
    {
        public int id_cliente { get; set; }


        public string nombres { get; set; }


        public string apellidos { get; set; }


        public string identificacion { get; set; }


        public string celular { get; set; }


        public string direccion { get; set; }
        public int saldo { get; set; }
    }

}