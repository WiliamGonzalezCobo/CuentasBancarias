namespace ModelAdoNet.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sysBanc.usuario")]
    public partial class usuario
    {
        [Key]
        public int id_usuario { get; set; }

        [Column("usuario")]
        [Required]
        [StringLength(50)]
        public string usuario1 { get; set; }

        [Required]
        [StringLength(50)]
        public string contrasenia { get; set; }

        public int id_cliente { get; set; }

        public virtual cliente cliente { get; set; }
    }
}
