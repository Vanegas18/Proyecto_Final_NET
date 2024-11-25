using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models;

public partial class Proveedore
{
    public int Idproveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string Correo { get; set; } = null!;

    public int IdcategoriaProveedor { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual CategoriasProveedore? IdcategoriaProveedorNavigation { get; set; }
}
