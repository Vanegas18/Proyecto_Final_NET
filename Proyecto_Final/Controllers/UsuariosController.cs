using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Proyecto_Final.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ProyectoFinalNetContext _context;

        public UsuariosController(ProyectoFinalNetContext context)
        {
            _context = context;
        }

        // GET: Usuarios/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Usuarios/Login
        [HttpPost] // Este atributo indica que el método es un punto de entrada de una solicitud POST
        [ValidateAntiForgeryToken] // Este atributo previene ataques CSRF
        public async Task<IActionResult> Login(String correo, String contraseña)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña))
            {
                ModelState.AddModelError(string.Empty, "Correo y contraseña son requeridos.");
                return View();
            }

            var hashedPassword = HashPassword(contraseña); // Hashear la contraseña
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Contraseña == hashedPassword);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                return View();
            }

            // Crear las claims para la autenticación
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            // Crear el objeto ClaimsIdentity con las claims y el esquema de autenticación
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Crear la cookie de autenticación con el esquema de autenticación y el objeto ClaimsPrincipal
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        // GET: Usuarios/Registro
        public IActionResult Registro()
        {
            return View();
        }

        // POST: Usuarios/Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("Nombre,Correo,Contraseña")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Rol = "Cliente"; // Asignar el rol por defecto
                usuario.Contraseña = HashPassword(usuario.Contraseña); // Hashear la contraseña
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(usuario);
        }

        // GET: Usuarios/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Cerrar sesión
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuarios/AccesoDenegado
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Idusuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idusuario,Nombre,Correo,Contraseña,Rol")] Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Rol))
            {
                usuario.Rol = "Cliente"; // Asignar el rol por defecto si no se proporciona
            }
            if (ModelState.IsValid)
            {
                usuario.Contraseña = HashPassword(usuario.Contraseña); // Hashear la contraseña
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idusuario,Nombre,Correo,Contraseña,Rol")] Usuario usuario)
        {
            if (id != usuario.Idusuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.Contraseña = HashPassword(usuario.Contraseña); // Hashear la contraseña
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Idusuario))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Idusuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Busca un usuario por su id y en caso de no encontrarlo, muestra un mensaje de error
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                TempData["Error"] = "No se puede eliminar el usuario, no se encontró";
                return RedirectToAction(nameof(Index));
            }

            //// Verifica si el usuario está siendo utilizado en alguna otra entidad y en caso de estarlo, muestra un mensaje de error
            //// Puedes modificar la condición según tus necesidades
            //var entidadesAsociadas = await _context.Ventas.AnyAsync(e => e.UsuarioId == id);
            //if (entidadesAsociadas)
            //{
            //    TempData["Error"] = "No se puede eliminar el usuario, está siendo utilizado en otra entidad";
            //    return RedirectToAction(nameof(Index));
            //}

            // Hace el control de errores al eliminar un usuario
            try
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar el usuario.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Idusuario == id);
        }

        // Método para hashear la contraseña usando SHA256
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
