using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models;

public partial class Venta
{
    public int Idventa { get; set; }

    public DateTime Fecha { get; set; }

    public int Idusuario { get; set; }

    public decimal Total { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
