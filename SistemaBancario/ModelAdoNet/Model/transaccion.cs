namespace ModelAdoNet.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sysBanc.transaccion")]
    public partial class transaccion
    {
        [Key]
        public int id_transaccion { get; set; }

        public int valor { get; set; }

        public DateTime fecha { get; set; }

        public int id_tipo_transaccion { get; set; }

        public int id_Cuenta { get; set; }

        public virtual cuenta cuenta { get; set; }

        public virtual tipo_transaccion tipo_transaccion { get; set; }
    }
}
