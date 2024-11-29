using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models
{
    public partial class Usuario
    {
        // Propiedad que representa el ID del usuario
        public int Idusuario { get; set; }

        // Propiedad que representa el nombre del usuario
        public string Nombre { get; set; } = null!;

        // Propiedad que representa el correo del usuario
        public string Correo { get; set; } = null!;

        // Propiedad que representa la contraseña del usuario
        public string Contraseña { get; set; } = null!;

        // Propiedad que representa el rol del usuario, por defecto es "Cliente"
        public string Rol { get; set; } = "Cliente";

        // Colección que representa las ventas asociadas al usuario
        public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
    }
}
