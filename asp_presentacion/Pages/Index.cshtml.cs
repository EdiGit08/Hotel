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
        private IHabitacionesPresentacion? iPresentacion = null;

        public IndexModel(IHabitacionesPresentacion iPresentacion)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                Filtro = new Habitaciones();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public bool EstaLogueado = false;

        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Contrasena { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Habitaciones? Filtro { get; set; }
        [BindProperty] public List<Habitaciones>? Lista { get; set; }

        public void OnGet()
        {
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

                GuardarHabitaciones();

                if (usuario == null)
                {
                    OnPostBtClean();
                    return;
                }

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
                return RedirectToPage("/Ventanas/Usuarios", new {accion = "nuevo"});
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
            //Lineas para traer las habitaciones al index
            Filtro!.Nombre = Filtro!.Nombre ?? "";

            Accion = Enumerables.Ventanas.Listas;
            var task = this.iPresentacion!.PorNombre(Filtro!);
            task.Wait();
            Lista = task.Result;
        }
    }
}
