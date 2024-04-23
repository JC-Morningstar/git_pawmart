using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace pawmart_jc.Models
{
    public partial class Pawmart_BDContext : DbContext
    {
        public Pawmart_BDContext()
        {
        }

        public Pawmart_BDContext(DbContextOptions<Pawmart_BDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<DetallesDelPedido> DetallesDelPedidos { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=JC;Initial Catalog=Pawmart_BD;Integrated Security=True", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellido).HasMaxLength(100);

                entity.Property(e => e.Contraseña).HasMaxLength(255);

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(255)
                    .HasColumnName("Correo_electronico");

                entity.Property(e => e.DireccionEnvio)
                    .HasMaxLength(255)
                    .HasColumnName("Direccion_envio");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.OtrosDatosContacto).HasColumnName("Otros_datos_contacto");
            });

            modelBuilder.Entity<DetallesDelPedido>(entity =>
            {
                entity.ToTable("Detalles_del_Pedido");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdPedido).HasColumnName("ID_pedido");

                entity.Property(e => e.IdProducto).HasColumnName("ID_producto");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Precio_unitario");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.DetallesDelPedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK__Detalles___ID_pe__3F466844");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetallesDelPedidos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__Detalles___ID_pr__403A8C7D");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FechaHoraPedido)
                    .HasColumnName("Fecha_hora_pedido")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdCliente).HasColumnName("ID_cliente");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Pedidos__ID_clie__3C69FB99");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("Fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre).HasMaxLength(255);

                entity.Property(e => e.OtrasCaracteristicas).HasColumnName("Otras_caracteristicas");

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TipoMascota)
                    .HasMaxLength(50)
                    .HasColumnName("Tipo_mascota");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
