using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ProyectoFinalNetContext _context;

        public ComprasController(ProyectoFinalNetContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var proyectoFinalNetContext = _context.Compras.Include(c => c.IdproveedorNavigation);
            return View(await proyectoFinalNetContext.ToListAsync());
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            // Pasa la lista de proveedores y productos a la vista
            ViewData["Idproveedor"] = new SelectList(_context.Proveedores, "Idproveedor", "Nombre");
            ViewData["Productos"] = _context.Productos.ToList();  // Pasa los productos disponibles para la selección
            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Compra compra, int[] productos, int[] cantidades, decimal[] preciosUnitarios)
        {
            if (ModelState.IsValid)
            {
                // 1. Guardar la compra en la base de datos para obtener el Idcompra
                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();  // Esto genera el Idcompra

                // 2. Crear los detalles de la compra y asignarlos al Idcompra
                for (int i = 0; i < productos.Length; i++)
                {
                    var detalleCompra = new DetalleCompra
                    {
                        Idcompra = compra.Idcompra,  // Asignar el Idcompra generado
                        Idproducto = productos[i],
                        Cantidad = cantidades[i],
                        PrecioUnitario = preciosUnitarios[i],
                        Subtotal = cantidades[i] * preciosUnitarios[i]  // Calcular el subtotal
                    };

                    _context.DetalleCompras.Add(detalleCompra);  // Guardar el detalle de la compra
                }

                // 3. Guardar los detalles de la compra en la base de datos
                await _context.SaveChangesAsync();

                // 4. Calcular el total de la compra (suma de los subtotales de los detalles)
                compra.Total = _context.DetalleCompras
                    .Where(d => d.Idcompra == compra.Idcompra)
                    .Sum(d => (decimal?)d.Subtotal) ?? 0m;  // Sumar los subtotales de los detalles

                // 5. Actualizar la compra con el total calculado
                _context.Compras.Update(compra);
                await _context.SaveChangesAsync();  // Guardar los cambios, incluida la actualización del total

                return RedirectToAction(nameof(Index));  // Redirigir a la lista de compras
            }

            // Si la validación falla, recargar la vista con los datos actuales
            ViewData["Idproveedor"] = new SelectList(_context.Proveedores, "Idproveedor", "Nombre", compra.Idproveedor);
            ViewData["Productos"] = _context.Productos.ToList();  // Pasa los productos disponibles para la selección
            return View(compra);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.DetalleCompras)
                .ThenInclude(d => d.IdproductoNavigation)
                .FirstOrDefaultAsync(m => m.Idcompra == id);

            if (compra == null)
            {
                return NotFound();
            }

            ViewData["Idproveedor"] = new SelectList(_context.Proveedores, "Idproveedor", "Nombre", compra.Idproveedor);
            ViewData["Productos"] = _context.Productos.ToList();  // Pasa la lista de productos a la vista
            return View(compra);
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Compra compra, int[] productos, int[] cantidades, decimal[] preciosUnitarios)
        {
            if (id != compra.Idcompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar la compra
                    _context.Update(compra);
                    await _context.SaveChangesAsync();

                    // Eliminar los detalles existentes
                    var detallesAntiguos = _context.DetalleCompras.Where(d => d.Idcompra == compra.Idcompra);
                    _context.DetalleCompras.RemoveRange(detallesAntiguos);
                    await _context.SaveChangesAsync();

                    // Agregar los nuevos detalles de compra
                    for (int i = 0; i < productos.Length; i++)
                    {
                        var detalleCompra = new DetalleCompra
                        {
                            Idcompra = compra.Idcompra,  // Asignar el Idcompra
                            Idproducto = productos[i],
                            Cantidad = cantidades[i],
                            PrecioUnitario = preciosUnitarios[i],
                            Subtotal = cantidades[i] * preciosUnitarios[i]  // Calcular el subtotal
                        };
                        _context.DetalleCompras.Add(detalleCompra);
                    }

                    // Guardar los nuevos detalles de compra
                    await _context.SaveChangesAsync();

                    // Recalcular el total de la compra
                    compra.Total = _context.DetalleCompras
                        .Where(d => d.Idcompra == compra.Idcompra)
                        .Sum(d => (decimal?)d.Subtotal) ?? 0m;

                    // Actualizar el total de la compra
                    _context.Compras.Update(compra);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.Idcompra))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["Idproveedor"] = new SelectList(_context.Proveedores, "Idproveedor", "Nombre", compra.Idproveedor);
            ViewData["Productos"] = _context.Productos.ToList();
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdproveedorNavigation)
                .FirstOrDefaultAsync(m => m.Idcompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.Idcompra == id);
        }
    }
}
