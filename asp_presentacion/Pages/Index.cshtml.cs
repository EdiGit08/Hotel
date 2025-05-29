using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        public bool EstaLogueado = false;
        public static int RolActual { get; set; } = 0;
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Contrasena { get; set; }
        [BindProperty] public List<Habitaciones>? Lista { get; set; }

        public void OnGet(string? accion = "")
        {
            if (accion == "cerrar") OnPostBtClose();

            GuardarHabitaciones();

            var variable_session = HttpContext.Session.GetString("Usuario");
            if (!String.IsNullOrEmpty(variable_session))
                EstaLogueado = true;
        }

        public void OnPostBtClean()
        {
            try
            {
                Email = string.Empty;
                Contrasena = string.Empty;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtEnter()
        {
            try
            {
                if (string.IsNullOrEmpty(Email) &&
                    string.IsNullOrEmpty(Contrasena))
                {
                    OnPostBtClean();
                    return;
                }

                // Consulta los usuarios en la base de datos para compararlos con las variables del loggin
                var usuariosPresentacion = new UsuariosPresentacion();
                var usuarios = usuariosPresentacion.Listar().Result;
                var usuario = usuarios.FirstOrDefault(u => u.Nombre!.ToLower() == Email!.ToLower() && u.Contrasena == Contrasena);
                
                if (usuario == null)
                {
                    OnPostBtClean();
                    return;
                }

                RolActual = usuario!.Rol;

                GuardarHabitaciones();

                RolActual = usuario!.Rol;

                GuardarHabitaciones();

                ViewData["Logged"] = true;
                HttpContext.Session.SetString("Usuario", Email!);
                EstaLogueado = true;
                OnPostBtClean();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtClose()
        {
            try
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Redirect("/");
                EstaLogueado = false;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IActionResult OnPostBtCrear()
        {
            try
            {
                return RedirectToPage("/Ventanas/Clientes", new {accion = "nuevo"});
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            return Page();
        }

        public IActionResult OnPostBtReservar()
        {
            try
            {
                return RedirectToPage("/Ventanas/Reservas", new { accion = "nuevo" });
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            return Page();
        }

        public void GuardarHabitaciones()
        {
            var HabitacionesPresentacion = new HabitacionesPresentacion();
            Lista = HabitacionesPresentacion.Listar().Result;
        }
    }
}
