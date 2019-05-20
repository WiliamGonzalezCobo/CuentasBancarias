namespace ModelAdoNet.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sysBanc.cliente_cuenta")]
    public partial class cliente_cuenta
    {
        [Key]
        public int id_cliente_cuenta { get; set; }

        public int id_cliente { get; set; }

        public int id_Cuenta { get; set; }
    }
}
