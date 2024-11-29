using System;
using System.Collections.Generic;

namespace Proyecto_Final.Models
{
    // Definición parcial de la clase CategoriasProducto
    public partial class CategoriasProducto
    {
        // Propiedad que almacena el identificador único de la categoría de producto
        public int IdcategoriaProducto { get; set; }

        // Propiedad que almacena el nombre de la categoría de producto
        public string Nombre { get; set; } = null!;

        // Propiedad opcional que almacena la descripción de la categoría de producto
        public string? Descripcion { get; set; }

        // Colección que almacena los productos asociados a esta categoría
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
