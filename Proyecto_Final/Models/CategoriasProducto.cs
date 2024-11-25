using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models;

public partial class CategoriasProducto
{
    public int IdcategoriaProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
