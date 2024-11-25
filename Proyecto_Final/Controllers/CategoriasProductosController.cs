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
            return View(await _context.CategoriasProductos.ToListAsync());
        }

        // GET: CategoriasProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
            return View();
        }

        // POST: CategoriasProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdcategoriaProducto,Nombre,Descripcion")] CategoriasProducto categoriasProducto)
        {
            if (ModelState.IsValid)
            {
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

            var categoriasProducto = await _context.CategoriasProductos.FindAsync(id);
            if (categoriasProducto == null)
            {
                return NotFound();
            }
            return View(categoriasProducto);
        }

        // POST: CategoriasProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            var categoriasProducto = await _context.CategoriasProductos
                .FirstOrDefaultAsync(m => m.IdcategoriaProducto == id);
            if (categoriasProducto == null)
            {
                return NotFound();
            }

            return View(categoriasProducto);
        }

        // POST: CategoriasProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriasProducto = await _context.CategoriasProductos.FindAsync(id);
            if (categoriasProducto != null)
            {
                _context.CategoriasProductos.Remove(categoriasProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriasProductoExists(int id)
        {
            return _context.CategoriasProductos.Any(e => e.IdcategoriaProducto == id);
        }
    }
}
