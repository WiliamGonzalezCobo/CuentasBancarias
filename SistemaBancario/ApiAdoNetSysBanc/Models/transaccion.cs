//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApiAdoNetSysBanc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class transaccion
    {
        public int id_transaccion { get; set; }
        public int valor { get; set; }
        public System.DateTime fecha { get; set; }
        public int id_tipo_transaccion { get; set; }
        public int id_Cuenta { get; set; }
    
        public virtual cuenta cuenta { get; set; }
        public virtual tipo_transaccion tipo_transaccion { get; set; }
    }
}
