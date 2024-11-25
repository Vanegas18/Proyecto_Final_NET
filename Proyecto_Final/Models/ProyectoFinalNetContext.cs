using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final.Models;

public partial class ProyectoFinalNetContext : DbContext
{
    public ProyectoFinalNetContext()
    {
    }

    public ProyectoFinalNetContext(DbContextOptions<ProyectoFinalNetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriasProducto> CategoriasProductos { get; set; }

    public virtual DbSet<CategoriasProveedore> CategoriasProveedores { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=localhost;Database=Proyecto_final_NET;Integrated Security=True;TrustServerCertificate=True;");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriasProducto>(entity =>
        {
            entity.HasKey(e => e.IdcategoriaProducto).HasName("PK__Categori__A576776E63636582");

            entity.Property(e => e.IdcategoriaProducto).HasColumnName("IDCategoriaProducto");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<CategoriasProveedore>(entity =>
        {
            entity.HasKey(e => e.IdcategoriaProveedor).HasName("PK__Categori__709242122CD958A2");

            entity.Property(e => e.IdcategoriaProveedor).HasColumnName("IDCategoriaProveedor");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Idcompra).HasName("PK__Compras__08719EC1EB95BB38");

            entity.Property(e => e.Idcompra).HasColumnName("IDCompra");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Idproveedor).HasColumnName("IDProveedor");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdproveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.Idproveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compras__IDProve__3A81B327");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IddetalleCompra).HasName("PK__DetalleC__DE482504452456E7");

            entity.Property(e => e.IddetalleCompra).HasColumnName("IDDetalleCompra");
            entity.Property(e => e.Idcompra).HasColumnName("IDCompra");
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.PrecioUnitario).HasColumnType("money");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnitario])", true)
                .HasColumnType("money");

            entity.HasOne(d => d.IdcompraNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.Idcompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCo__IDCom__3D5E1FD2");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCo__IDPro__3E52440B");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IddetalleVenta).HasName("PK__DetalleV__DA9AFB791111DA58");

            entity.Property(e => e.IddetalleVenta).HasColumnName("IDDetalleVenta");
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Idventa).HasColumnName("IDVenta");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnitario])", true)
                .HasColumnType("decimal(21, 2)");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.Idproducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVe__IDPro__45F365D3");

            entity.HasOne(d => d.IdventaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.Idventa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVe__IDVen__44FF419A");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PK__Producto__ABDAF2B4A59F8E32");

            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.IdcategoriaProducto).HasColumnName("IDCategoriaProducto");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("money");

            entity.HasOne(d => d.IdcategoriaProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdcategoriaProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__IDCat__29572725");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Idproveedor).HasName("PK__Proveedo__4CD73240F07A183C");

            entity.HasIndex(e => e.Correo, "UQ__Proveedo__60695A191855FCD3").IsUnique();

            entity.Property(e => e.Idproveedor).HasColumnName("IDProveedor");
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.IdcategoriaProveedor).HasColumnName("IDCategoriaProveedor");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(100);

            entity.HasOne(d => d.IdcategoriaProveedorNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdcategoriaProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proveedor__IDCat__2F10007B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuarios__5231116969BB05BA");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A19068FEC77").IsUnique();

            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(50);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Idventa).HasName("PK__Ventas__27134B8287EFB1C0");

            entity.Property(e => e.Idventa).HasColumnName("IDVenta");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__IDUsuari__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
