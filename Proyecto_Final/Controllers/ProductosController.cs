using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models;

namespace Proyecto_Final.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProyectoFinalNetContext _context;

        public ProductosController(ProyectoFinalNetContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var proyectoFinalNetContext = _context.Productos.Include(p => p.IdcategoriaProductoNavigation);
            return View(await proyectoFinalNetContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdcategoriaProductoNavigation)
                .FirstOrDefaultAsync(m => m.Idproducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            // Obtener el nombre de la categoría
            ViewData["NombreCategoria"] = producto.IdcategoriaProductoNavigation.Nombre;

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["IdcategoriaProducto"] = new SelectList(_context.CategoriasProductos, "IdcategoriaProducto", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idproducto,Nombre,Descripcion,Precio,Stock,IdcategoriaProducto")] Producto producto)
        {
            if (!ModelState.IsValid)
            {
                // Log para depuración
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(modelError.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdcategoriaProducto"] = new SelectList(_context.CategoriasProductos, "IdcategoriaProducto", "Nombre", producto.IdcategoriaProducto);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdcategoriaProducto"] = new SelectList(_context.CategoriasProductos, "IdcategoriaProducto", "Nombre", producto.IdcategoriaProducto);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idproducto,Nombre,Descripcion,Precio,Stock,IdcategoriaProducto")] Producto producto)
        {
            if (id != producto.Idproducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Idproducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdcategoriaProducto"] = new SelectList(_context.CategoriasProductos, "IdcategoriaProducto", "IdcategoriaProducto", producto.IdcategoriaProducto);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdcategoriaProductoNavigation)
                .FirstOrDefaultAsync(m => m.Idproducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Busca un producto por su id y en caso de no encontrarlo, muestra un mensaje de error
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                TempData["Error"] = "No se puede eliminar el producto, no se encontro";
                return RedirectToAction(nameof(Index));
            }

            // Verifica si el producto está siendo utilizado en alguna venta y en caso de estarlo, muestra un mensaje de error
            var ventasAsociadas = await _context.DetalleVentas.AnyAsync(dv => dv.Idproducto == id);
            var comprasAsociadas = await _context.DetalleCompras.AnyAsync(dc => dc.Idproducto == id);
            if (ventasAsociadas || comprasAsociadas)
            {
                TempData["Error"] = "No se puede eliminar el producto, esta siendo utilizado en una venta o compra";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Success"] = "Producto eliminado correctamente";
            }

            // Hace el control de errores al eliminar un producto
            try
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar el producto.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Idproducto == id);
        }
    }
}
