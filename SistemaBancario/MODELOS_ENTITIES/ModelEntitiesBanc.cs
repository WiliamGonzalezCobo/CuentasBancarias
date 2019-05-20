namespace MODELOS_ENTITIES
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelEntitiesBanc : DbContext
    {
        public ModelEntitiesBanc()
            : base("name=ModelEntitiesBanc")
        {
        }

        public virtual DbSet<cliente> clientes { get; set; }
        public virtual DbSet<cuenta> cuentas { get; set; }
        public virtual DbSet<tipo_identificacion> tipo_identificacion { get; set; }
        public virtual DbSet<tipo_transaccion> tipo_transaccion { get; set; }
        public virtual DbSet<transaccion> transaccions { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cliente>()
                .Property(e => e.nombres)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.identificacion)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.celular)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.usuarios)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.cuentas)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cuenta>()
                .HasMany(e => e.transaccions)
                .WithRequired(e => e.cuenta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipo_identificacion>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tipo_identificacion>()
                .HasMany(e => e.clientes)
                .WithRequired(e => e.tipo_identificacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipo_transaccion>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tipo_transaccion>()
                .HasMany(e => e.transaccions)
                .WithRequired(e => e.tipo_transaccion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.usuario1)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.contrasenia)
                .IsUnicode(false);
        }
    }
}
