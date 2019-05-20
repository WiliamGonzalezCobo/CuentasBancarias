namespace ModelAdoNet.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sysBanc.cliente")]
    public partial class cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cliente()
        {
            usuario = new HashSet<usuario>();
        }

        [Key]
        public int id_cliente { get; set; }

        [Required]
        [StringLength(50)]
        public string nombres { get; set; }

        [Required]
        [StringLength(50)]
        public string apellidos { get; set; }

        [Required]
        [StringLength(50)]
        public string identificacion { get; set; }

        [Required]
        [StringLength(50)]
        public string celular { get; set; }

        [Required]
        [StringLength(50)]
        public string direccion { get; set; }

        public int id_tipo_identificacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuario> usuario { get; set; }

        public virtual tipo_identificacion tipo_identificacion { get; set; }
    }
}
