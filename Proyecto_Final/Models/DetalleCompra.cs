using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models;

public partial class DetalleCompra
{
    public int IddetalleCompra { get; set; }

    public int Idcompra { get; set; }

    public int Idproducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Compra IdcompraNavigation { get; set; } = null!;

    public virtual Producto IdproductoNavigation { get; set; } = null!;
}
