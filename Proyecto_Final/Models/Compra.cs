using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models;

public partial class Compra
{
    public int Idcompra { get; set; }

    public DateTime Fecha { get; set; }
    public int Idproveedor { get; set; }

    public decimal Total { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual Proveedore? IdproveedorNavigation { get; set; }
}
