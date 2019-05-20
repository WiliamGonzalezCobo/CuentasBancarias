namespace MODELOS_ENTITIES
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
        [Display(Name = "Usuario")]
        public string usuario1 { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener mínimo 6 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contrasenia { get; set; }

        public int id_cliente { get; set; }

        public virtual cliente cliente { get; set; }
    }
}
