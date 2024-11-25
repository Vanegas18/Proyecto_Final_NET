using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
