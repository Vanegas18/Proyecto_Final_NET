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
    public class DetalleVentasController : Controller
    {
        private readonly ProyectoFinalNetContext _context;

        public DetalleVentasController(ProyectoFinalNetContext context)
        {
            _context = context;
        }

        // GET: DetalleVentas
        public async Task<IActionResult> Index()
        {
            var proyectoFinalNetContext = _context.DetalleVentas.Include(d => d.IdproductoNavigation).Include(d => d.IdventaNavigation);
            return View(await proyectoFinalNetContext.ToListAsync());
        }

        // GET: DetalleVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVentas
                .Include(d => d.IdproductoNavigation)
                .Include(d => d.IdventaNavigation)
                .FirstOrDefaultAsync(m => m.IddetalleVenta == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        // GET: DetalleVentas/Create
        public IActionResult Create()
        {
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Idproducto");
            ViewData["Idventa"] = new SelectList(_context.Ventas, "Idventa", "Idventa");
            return View();
        }

        // POST: DetalleVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IddetalleVenta,Idventa,Idproducto,Cantidad,PrecioUnitario,Subtotal")] DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Idproducto", detalleVenta.Idproducto);
            ViewData["Idventa"] = new SelectList(_context.Ventas, "Idventa", "Idventa", detalleVenta.Idventa);
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVentas.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Idproducto", detalleVenta.Idproducto);
            ViewData["Idventa"] = new SelectList(_context.Ventas, "Idventa", "Idventa", detalleVenta.Idventa);
            return View(detalleVenta);
        }

        // POST: DetalleVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IddetalleVenta,Idventa,Idproducto,Cantidad,PrecioUnitario,Subtotal")] DetalleVenta detalleVenta)
        {
            if (id != detalleVenta.IddetalleVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleVentaExists(detalleVenta.IddetalleVenta))
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
            ViewData["Idproducto"] = new SelectList(_context.Productos, "Idproducto", "Idproducto", detalleVenta.Idproducto);
            ViewData["Idventa"] = new SelectList(_context.Ventas, "Idventa", "Idventa", detalleVenta.Idventa);
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVenta = await _context.DetalleVentas
                .Include(d => d.IdproductoNavigation)
                .Include(d => d.IdventaNavigation)
                .FirstOrDefaultAsync(m => m.IddetalleVenta == id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            return View(detalleVenta);
        }

        // POST: DetalleVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleVenta = await _context.DetalleVentas.FindAsync(id);
            if (detalleVenta != null)
            {
                _context.DetalleVentas.Remove(detalleVenta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleVentaExists(int id)
        {
            return _context.DetalleVentas.Any(e => e.IddetalleVenta == id);
        }
    }
}
