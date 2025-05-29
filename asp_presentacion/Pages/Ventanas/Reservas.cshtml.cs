using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace asp_presentacion.Pages.Ventanas
{
    public class ReservasModel : PageModel
    {
        private IReservasPresentacion? iPresentacion = null;
        private IClientesPresentacion? iclientes = null;
        private IRecepcionistasPresentacion? irecepcionistas = null;
        private IHabitacionesPresentacion? ihabitaciones = null;

        public ReservasModel(IReservasPresentacion iPresentacion,
            IClientesPresentacion? iclientes,
            IRecepcionistasPresentacion? irecepcionistas,
            IHabitacionesPresentacion? ihabitaciones)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                this.iclientes = iclientes;
                this.ihabitaciones=ihabitaciones;
                this.irecepcionistas=irecepcionistas;
                Filtro = new Reservas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Reservas? Actual { get; set; }
        [BindProperty] public Reservas? Filtro { get; set; }
        [BindProperty] public List<Reservas>? Lista { get; set; }
        [BindProperty] public List<Clientes>? Clientes { get; set; }
        [BindProperty] public List<Habitaciones>? Habitaciones { get; set; }
        [BindProperty] public List<Recepcionistas>? Recepcionistas { get; set; }

        public virtual void OnGet(string? accion = "") {
            if (accion == "nuevo")
                OnPostBtNuevo();
            else
                OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                var variable_session = HttpContext.Session.GetString("Usuario");
                if (String.IsNullOrEmpty(variable_session))
                {
                    HttpContext.Response.Redirect("/");
                    return;
                }

                Filtro!.Codigo = Filtro!.Codigo ?? "";

                Accion = Enumerables.Ventanas.Listas;
                var task = this.iPresentacion!.PorCodigo(Filtro!);
                task.Wait();
                Lista = task.Result;
                Actual = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        private void CargarCombox()
        {
            try
            {
                var task = this.ihabitaciones!.Listar();
                var task2 = this.iclientes!.Listar();
                var task3 = this.irecepcionistas!.Listar();
                task.Wait();
                task2.Wait();
                task3.Wait();
                Habitaciones = task.Result;
                Clientes = task2.Result;
                Recepcionistas = task3.Result;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtNuevo()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;
                Actual = new Reservas();
                CargarCombox();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();

                if (!ValidarPermiso()) { return; }

                CargarCombox();
                Accion = Enumerables.Ventanas.Editar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual IActionResult OnPostBtGuardar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;
                int Id= Actual!.Id;
                Task<Reservas>? task = null;
                if (Actual!.Id == 0)
                    task = this.iPresentacion!.Guardar(Actual!)!;
                else
                    task = this.iPresentacion!.Modificar(Actual!)!;
                task.Wait();
                Actual = task.Result;

                if (Id == 0)
                    return RedirectToPage("/Ventanas/Servicios_Reservas", new { Id = Actual.Id });

                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, "Los datos no fueron agregados correctamente", ViewData!);
            }
            return Page();
        }

        public virtual void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();

                if (!ValidarPermiso()) { return; }

                Accion = Enumerables.Ventanas.Borrar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, "No se puede eliminar", ViewData!);
            }
        }

        public virtual void OnPostBtBorrar()
        {
            try
            {
                var task = this.iPresentacion!.Borrar(Actual!);
                Actual = task.Result;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, "No se puede eliminar", ViewData!);
            }
        }

        public IActionResult OnPostBtCancelar() { return RedirectToPage("/Index"); }

        public void OnPostBtCerrar()
        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
                    OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public bool ValidarPermiso()
        {
            var variable_session = HttpContext.Session.GetString("Usuario");

            // Estas lineas se encargan de revisar si el usuario tiene acceso a la informacion o no
            var usuariosPresentacion = new UsuariosPresentacion();
            var usuarios = usuariosPresentacion.Listar().Result;
            var usuario = usuarios.FirstOrDefault(u => u.Nombre!.ToLower() == variable_session!.ToLower() && (u.Rol == 1 || u.Rol == 3));

            if (usuario == null)
                return false;
            return true;
        }
    }
}

