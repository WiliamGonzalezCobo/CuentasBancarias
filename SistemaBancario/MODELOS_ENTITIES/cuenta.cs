namespace MODELOS_ENTITIES
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sysBanc.cuenta")]
    public partial class cuenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cuenta()
        {
            transaccions = new HashSet<transaccion>();
        }

        [Key]
        public int id_Cuenta { get; set; }

        public int saldo { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha Creacion")]
        public DateTime fecha_creacion { get; set; }

        public int id_Cliente { get; set; }

        public virtual cliente cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transaccion> transaccions { get; set; }
    }
}
