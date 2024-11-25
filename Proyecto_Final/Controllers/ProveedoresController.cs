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
    public class ProveedoresController : Controller
    {
        private readonly ProyectoFinalNetContext _context;

        public ProveedoresController(ProyectoFinalNetContext context)
        {
            _context = context;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index()
        {
            var proyectoFinalNetContext = _context.Proveedores.Include(p => p.IdcategoriaProveedorNavigation);
            return View(await proyectoFinalNetContext.ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
                .Include(p => p.IdcategoriaProveedorNavigation)
                .FirstOrDefaultAsync(m => m.Idproveedor == id);
            if (proveedore == null)
            {
                return NotFound();
            }

            return View(proveedore);
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            ViewData["IdcategoriaProveedor"] = new SelectList(_context.CategoriasProveedores, "IdcategoriaProveedor", "IdcategoriaProveedor");
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idproveedor,Nombre,Direccion,Telefono,Correo,IdcategoriaProveedor")] Proveedore proveedore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdcategoriaProveedor"] = new SelectList(_context.CategoriasProveedores, "IdcategoriaProveedor", "IdcategoriaProveedor", proveedore.IdcategoriaProveedor);
            return View(proveedore);
        }

        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore == null)
            {
                return NotFound();
            }
            ViewData["IdcategoriaProveedor"] = new SelectList(_context.CategoriasProveedores, "IdcategoriaProveedor", "IdcategoriaProveedor", proveedore.IdcategoriaProveedor);
            return View(proveedore);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idproveedor,Nombre,Direccion,Telefono,Correo,IdcategoriaProveedor")] Proveedore proveedore)
        {
            if (id != proveedore.Idproveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoreExists(proveedore.Idproveedor))
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
            ViewData["IdcategoriaProveedor"] = new SelectList(_context.CategoriasProveedores, "IdcategoriaProveedor", "IdcategoriaProveedor", proveedore.IdcategoriaProveedor);
            return View(proveedore);
        }

        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
                .Include(p => p.IdcategoriaProveedorNavigation)
                .FirstOrDefaultAsync(m => m.Idproveedor == id);
            if (proveedore == null)
            {
                return NotFound();
            }

            return View(proveedore);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore != null)
            {
                _context.Proveedores.Remove(proveedore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedoreExists(int id)
        {
            return _context.Proveedores.Any(e => e.Idproveedor == id);
        }
    }
}
