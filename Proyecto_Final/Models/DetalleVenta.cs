using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models;

public partial class DetalleVenta
{
    public int IddetalleVenta { get; set; }

    public int Idventa { get; set; }

    public int Idproducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }


    public virtual Producto IdproductoNavigation { get; set; } = null!;

    public virtual Venta IdventaNavigation { get; set; } = null!;
}
