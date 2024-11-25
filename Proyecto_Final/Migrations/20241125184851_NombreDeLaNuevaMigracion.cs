using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Final.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeLaNuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "CategoriasProductos",
            //    columns: table => new
            //    {
            //        IDCategoriaProducto = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Categori__A576776E63636582", x => x.IDCategoriaProducto);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CategoriasProveedores",
            //    columns: table => new
            //    {
            //        IDCategoriaProveedor = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Categori__709242122CD958A2", x => x.IDCategoriaProveedor);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Usuarios",
            //    columns: table => new
            //    {
            //        IDUsuario = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Contraseña = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        Rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Usuarios__5231116969BB05BA", x => x.IDUsuario);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Productos",
            //    columns: table => new
            //    {
            //        IDProducto = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        Precio = table.Column<decimal>(type: "money", nullable: false),
            //        Stock = table.Column<int>(type: "int", nullable: false),
            //        IDCategoriaProducto = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Producto__ABDAF2B4A59F8E32", x => x.IDProducto);
            //        table.ForeignKey(
            //            name: "FK__Productos__IDCat__29572725",
            //            column: x => x.IDCategoriaProducto,
            //            principalTable: "CategoriasProductos",
            //            principalColumn: "IDCategoriaProducto");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Proveedores",
            //    columns: table => new
            //    {
            //        IDProveedor = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        Telefono = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        IDCategoriaProveedor = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Proveedo__4CD73240F07A183C", x => x.IDProveedor);
            //        table.ForeignKey(
            //            name: "FK__Proveedor__IDCat__2F10007B",
            //            column: x => x.IDCategoriaProveedor,
            //            principalTable: "CategoriasProveedores",
            //            principalColumn: "IDCategoriaProveedor");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Ventas",
            //    columns: table => new
            //    {
            //        IDVenta = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Fecha = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
            //        IDUsuario = table.Column<int>(type: "int", nullable: false),
            //        Total = table.Column<decimal>(type: "money", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Ventas__27134B8287EFB1C0", x => x.IDVenta);
            //        table.ForeignKey(
            //            name: "FK__Ventas__IDUsuari__4222D4EF",
            //            column: x => x.IDUsuario,
            //            principalTable: "Usuarios",
            //            principalColumn: "IDUsuario");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Compras",
            //    columns: table => new
            //    {
            //        IDCompra = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Fecha = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
            //        IDProveedor = table.Column<int>(type: "int", nullable: false),
            //        Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Compras__08719EC1EB95BB38", x => x.IDCompra);
            //        table.ForeignKey(
            //            name: "FK__Compras__IDProve__3A81B327",
            //            column: x => x.IDProveedor,
            //            principalTable: "Proveedores",
            //            principalColumn: "IDProveedor");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DetalleVentas",
            //    columns: table => new
            //    {
            //        IDDetalleVenta = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        IDVenta = table.Column<int>(type: "int", nullable: false),
            //        IDProducto = table.Column<int>(type: "int", nullable: false),
            //        Cantidad = table.Column<int>(type: "int", nullable: false),
            //        PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
            //        Subtotal = table.Column<decimal>(type: "decimal(21,2)", nullable: true, computedColumnSql: "([Cantidad]*[PrecioUnitario])", stored: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__DetalleV__DA9AFB791111DA58", x => x.IDDetalleVenta);
            //        table.ForeignKey(
            //            name: "FK__DetalleVe__IDPro__45F365D3",
            //            column: x => x.IDProducto,
            //            principalTable: "Productos",
            //            principalColumn: "IDProducto");
            //        table.ForeignKey(
            //            name: "FK__DetalleVe__IDVen__44FF419A",
            //            column: x => x.IDVenta,
            //            principalTable: "Ventas",
            //            principalColumn: "IDVenta");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DetalleCompras",
            //    columns: table => new
            //    {
            //        IDDetalleCompra = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        IDCompra = table.Column<int>(type: "int", nullable: false),
            //        IDProducto = table.Column<int>(type: "int", nullable: false),
            //        Cantidad = table.Column<int>(type: "int", nullable: false),
            //        PrecioUnitario = table.Column<decimal>(type: "money", nullable: false),
            //        Subtotal = table.Column<decimal>(type: "money", nullable: true, computedColumnSql: "([Cantidad]*[PrecioUnitario])", stored: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__DetalleC__DE482504452456E7", x => x.IDDetalleCompra);
            //        table.ForeignKey(
            //            name: "FK__DetalleCo__IDCom__3D5E1FD2",
            //            column: x => x.IDCompra,
            //            principalTable: "Compras",
            //            principalColumn: "IDCompra");
            //        table.ForeignKey(
            //            name: "FK__DetalleCo__IDPro__3E52440B",
            //            column: x => x.IDProducto,
            //            principalTable: "Productos",
            //            principalColumn: "IDProducto");
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IDProveedor",
                table: "Compras",
                column: "IDProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_IDCompra",
                table: "DetalleCompras",
                column: "IDCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_IDProducto",
                table: "DetalleCompras",
                column: "IDProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_IDProducto",
                table: "DetalleVentas",
                column: "IDProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_IDVenta",
                table: "DetalleVentas",
                column: "IDVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IDCategoriaProducto",
                table: "Productos",
                column: "IDCategoriaProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_IDCategoriaProveedor",
                table: "Proveedores",
                column: "IDCategoriaProveedor");

            migrationBuilder.CreateIndex(
                name: "UQ__Proveedo__60695A191855FCD3",
                table: "Proveedores",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__60695A19068FEC77",
                table: "Usuarios",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IDUsuario",
                table: "Ventas",
                column: "IDUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleCompras");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "CategoriasProductos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "CategoriasProveedores");
        }
    }
}
