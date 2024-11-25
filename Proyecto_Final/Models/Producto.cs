using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models;

public partial class Producto
{
    public int Idproducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public int IdcategoriaProducto { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual CategoriasProducto IdcategoriaProductoNavigation { get; set; } = null!;
}
