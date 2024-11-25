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
    public class CategoriasProveedoresController : Controller
    {
        private readonly ProyectoFinalNetContext _context;

        public CategoriasProveedoresController(ProyectoFinalNetContext context)
        {
            _context = context;
        }

        // GET: CategoriasProveedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriasProveedores.ToListAsync());
        }

        // GET: CategoriasProveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriasProveedore = await _context.CategoriasProveedores
                .FirstOrDefaultAsync(m => m.IdcategoriaProveedor == id);
            if (categoriasProveedore == null)
            {
                return NotFound();
            }

            return View(categoriasProveedore);
        }

        // GET: CategoriasProveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriasProveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdcategoriaProveedor,Nombre,Descripcion")] CategoriasProveedore categoriasProveedore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriasProveedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriasProveedore);
        }

        // GET: CategoriasProveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriasProveedore = await _context.CategoriasProveedores.FindAsync(id);
            if (categoriasProveedore == null)
            {
                return NotFound();
            }
            return View(categoriasProveedore);
        }

        // POST: CategoriasProveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdcategoriaProveedor,Nombre,Descripcion")] CategoriasProveedore categoriasProveedore)
        {
            if (id != categoriasProveedore.IdcategoriaProveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriasProveedore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasProveedoreExists(categoriasProveedore.IdcategoriaProveedor))
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
            return View(categoriasProveedore);
        }

        // GET: CategoriasProveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriasProveedore = await _context.CategoriasProveedores
                .FirstOrDefaultAsync(m => m.IdcategoriaProveedor == id);
            if (categoriasProveedore == null)
            {
                return NotFound();
            }

            return View(categoriasProveedore);
        }

        // POST: CategoriasProveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriasProveedore = await _context.CategoriasProveedores.FindAsync(id);
            if (categoriasProveedore != null)
            {
                _context.CategoriasProveedores.Remove(categoriasProveedore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriasProveedoreExists(int id)
        {
            return _context.CategoriasProveedores.Any(e => e.IdcategoriaProveedor == id);
        }
    }
}
