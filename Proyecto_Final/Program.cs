using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProyectoFinalNetContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

// Autenticación y autorización
// Autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuarios/Login";
        options.AccessDeniedPath = "/Usuarios/AccessDenied";
    });
// Básicamente se configura la autenticación por cookies, se establece la ruta de login y la ruta de acceso denegado.

// Autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Gerente"));
    options.AddPolicy("RequireAdminOrManagerRole", policy => policy.RequireRole("Administrador", "Gerente"));
});
// Se configuran las políticas de autorización, en este caso se establecen tres políticas, una que requiere el rol de Administrador, otra que requiere el rol de Gerente y una tercera que requiere uno de los dos roles anteriores.

builder.Services.AddSession(); // Se habilita el uso de sesiones

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Se habilita la autenticación
app.UseAuthorization(); // Se habilita la autorización

app.UseSession(); // Se habilita el uso de sesiones

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();