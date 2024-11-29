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
    public class CategoriasProductosController : Controller
    {
        private readonly ProyectoFinalNetContext _context;

        public CategoriasProductosController(ProyectoFinalNetContext context)
        {
            _context = context;
        }

        // GET: CategoriasProductos
        public async Task<IActionResult> Index()
        {
            // Devuelve una lista de todas las categorías de productos
            return View(await _context.CategoriasProductos.ToListAsync());
        }

        // GET: CategoriasProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Busca la categoría de producto por ID
            var categoriasProducto = await _context.CategoriasProductos
                .FirstOrDefaultAsync(m => m.IdcategoriaProducto == id);
            if (categoriasProducto == null)
            {
                return NotFound();
            }

            return View(categoriasProducto);
        }

        // GET: CategoriasProductos/Create
        public IActionResult Create()
        {
            // Devuelve la vista para crear una nueva categoría de producto
            return View();
        }

        // POST: CategoriasProductos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdcategoriaProducto,Nombre,Descripcion")] CategoriasProducto categoriasProducto)
        {
            if (ModelState.IsValid)
            {
                // Añade la nueva categoría de producto al contexto y guarda los cambios
                _context.Add(categoriasProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriasProducto);
        }

        // GET: CategoriasProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Busca la categoría de producto por ID para editarla
            var categoriasProducto = await _context.CategoriasProductos.FindAsync(id);
            if (categoriasProducto == null)
            {
                return NotFound();
            }
            return View(categoriasProducto);
        }

        // POST: CategoriasProductos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdcategoriaProducto,Nombre,Descripcion")] CategoriasProducto categoriasProducto)
        {
            if (id != categoriasProducto.IdcategoriaProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualiza la categoría de producto en el contexto y guarda los cambios
                    _context.Update(categoriasProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasProductoExists(categoriasProducto.IdcategoriaProducto))
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
            return View(categoriasProducto);
        }

        // GET: CategoriasProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Busca la categoría de producto por ID para eliminarla
            var categoriasProducto = await _context.CategoriasProductos
                .FirstOrDefaultAsync(m => m.IdcategoriaProducto == id);
            if (categoriasProducto == null)
            {
                return NotFound();
            }

            return View(categoriasProducto);
        }

        // POST: CategoriasProductos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriasProducto = await _context.CategoriasProductos.FindAsync(id);
            if (categoriasProducto == null)
            {
                TempData["mensaje"] = "No se encontró la categoria";
                return RedirectToAction(nameof(Index));
            }

            // Verifica si hay productos asociados a la categoría antes de eliminarla
            var productosAsociados = await _context.Productos.AnyAsync(p => p.IdcategoriaProducto == id);
            if (productosAsociados)
            {
                TempData["mensaje"] = "No se puede eliminar la categoria, tiene productos asociados";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // Elimina la categoría de producto del contexto y guarda los cambios
                _context.CategoriasProductos.Remove(categoriasProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                TempData["mensaje"] = "No se puede eliminar la categoria";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoriasProductoExists(int id)
        {
            // Verifica si la categoría de producto existe en el contexto
            return _context.CategoriasProductos.Any(e => e.IdcategoriaProducto == id);
        }
    }
}
