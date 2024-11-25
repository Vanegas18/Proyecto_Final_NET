using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models;

public partial class CategoriasProveedore
{
    public int IdcategoriaProveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Proveedore> Proveedores { get; set; } = new List<Proveedore>();
}
