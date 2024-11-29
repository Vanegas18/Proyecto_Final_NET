using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models;

public partial class Producto
{
    // Propiedad que representa el ID del producto
    public int Idproducto { get; set; }

    // Propiedad que representa el nombre del producto
    public string Nombre { get; set; } = null!;

    // Propiedad que representa la descripción del producto (puede ser nula)
    public string? Descripcion { get; set; }

    // Propiedad que representa el precio del producto
    public decimal Precio { get; set; }

    // Propiedad que representa el stock disponible del producto
    public int Stock { get; set; }

    // Propiedad que representa el ID de la categoría del producto
    public int IdcategoriaProducto { get; set; }

    // Colección de detalles de compra asociados al producto
    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    // Colección de detalles de venta asociados al producto
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    // Navegación a la entidad CategoriasProducto asociada
    public virtual CategoriasProducto? IdcategoriaProductoNavigation { get; set; }
}
