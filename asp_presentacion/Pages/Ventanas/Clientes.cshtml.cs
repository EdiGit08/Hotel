using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ClientesModel : PageModel
    {
        private IClientesPresentacion? iPresentacion = null;
        private IOpinionesPresentacion? iopiniones = null;

        public ClientesModel(IClientesPresentacion iPresentacion, IOpinionesPresentacion? iopiniones)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                this.iopiniones = iopiniones;
                Filtro = new Clientes();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }

            this.iopiniones = iopiniones;
        }

        public static int id { get; set; } = 0;
        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Clientes? Actual { get; set; }
        [BindProperty] public Clientes? Filtro { get; set; }
        [BindProperty] public List<Clientes>? Lista { get; set; }
        [BindProperty] public List<Opiniones>? Opiniones { get; set; }

        public virtual void OnGet(string? accion = "")
        {
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

                Filtro!.Cedula = Filtro!.Cedula ?? "";

                Accion = Enumerables.Ventanas.Listas;
                var task = this.iPresentacion!.PorCedula(Filtro!);
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
                var task = this.iopiniones!.Listar();
                task.Wait();
                Opiniones = task.Result;
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
                Actual = new Clientes();
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
                CargarCombox();

                if (!ValidarPermiso()) { return; }

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

                Task<Clientes>? task = null;
                if (Actual!.Id == 0)
                {
                    task = this.iPresentacion!.Guardar(Actual!)!;
                    task.Wait();
                    Actual = task.Result;
                    id = Actual!.Id;
                    return RedirectToPage("/Ventanas/Usuarios", new { accion = "nuevo" });
                }else
                    task = this.iPresentacion!.Modificar(Actual!)!;
                task.Wait();
                Actual = task.Result;
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
                LogConversor.Log(ex, "No se puede eliminar",ViewData!);

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

        public void OnPostBtCancelar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

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

