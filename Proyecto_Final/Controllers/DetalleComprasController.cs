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
    public class DetalleComprasController : Controller
    {
        private readonly ProyectoFinalNetContext _context;

        public DetalleComprasController(ProyectoFinalNetContext context)
        {
            _context = context;
        }

        // GET: DetalleCompras
        public async Task<IActionResult> Index()
        {
            var proyectoFinalNetContext = _context.DetalleCompras.Include(d => d.IdcompraNavigation).Include(d => d.IdproductoNavigation);
            return View(await proyectoFinalNetContext.ToListAsync());
        }

        // GET: DetalleCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras
                .Include(d => d.IdcompraNavigation)
                .Include(d => d.IdproductoNavigation)
                .FirstOrDefaultAsync(m => m.IddetalleCompra == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

        // GET: DetalleCompras/Create
        public IActionResult Create()
        {
            ViewData["Idcompra"] = new SelectList(_context.Compras, "Idcompra", "Idcompra");
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Nombre");  // Usar el nombre del producto en lugar del ID
            return View();
        }

        // POST: DetalleCompras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IddetalleCompra,Idcompra,Idproducto,Cantidad,PrecioUnitario")] DetalleCompra detalleCompra)
        {
            if (ModelState.IsValid)
            {
                // Calcular el subtotal
                detalleCompra.Subtotal = detalleCompra.Cantidad * detalleCompra.PrecioUnitario;

                _context.Add(detalleCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Idcompra"] = new SelectList(_context.Compras, "Idcompra", "Idcompra", detalleCompra.Idcompra);
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Nombre", detalleCompra.Idproducto);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            ViewData["Idcompra"] = new SelectList(_context.Compras, "Idcompra", "Idcompra", detalleCompra.Idcompra);
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Nombre", detalleCompra.Idproducto);
            return View(detalleCompra);
        }

        // POST: DetalleCompras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IddetalleCompra,Idcompra,Idproducto,Cantidad,PrecioUnitario,Subtotal")] DetalleCompra detalleCompra)
        {
            if (id != detalleCompra.IddetalleCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Calcular el subtotal antes de guardar
                    detalleCompra.Subtotal = detalleCompra.Cantidad * detalleCompra.PrecioUnitario;

                    _context.Update(detalleCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleCompraExists(detalleCompra.IddetalleCompra))
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

            ViewData["Idcompra"] = new SelectList(_context.Compras, "Idcompra", "Idcompra", detalleCompra.Idcompra);
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Nombre", detalleCompra.Idproducto);
            return View(detalleCompra);
        }

        // GET: DetalleCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleCompra = await _context.DetalleCompras
                .Include(d => d.IdcompraNavigation)
                .Include(d => d.IdproductoNavigation)
                .FirstOrDefaultAsync(m => m.IddetalleCompra == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }

            return View(detalleCompra);
        }

        // POST: DetalleCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleCompra = await _context.DetalleCompras.FindAsync(id);
            if (detalleCompra != null)
            {
                _context.DetalleCompras.Remove(detalleCompra);
                await _context.SaveChangesAsync();
            }

            
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleCompraExists(int id)
        {
            return _context.DetalleCompras.Any(e => e.IddetalleCompra == id);
        }
    }
}
