using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class Servicios_ReservasModel : PageModel
    {
        private IServicios_ReservasPresentacion? iPresentacion = null;
        private IServiciosPresentacion? iServicios = null;
        private IReservasPresentacion? iReservas = null;


        public Servicios_ReservasModel(IServicios_ReservasPresentacion iPresentacion,
            IServiciosPresentacion? iServicios,
            IReservasPresentacion? iReservas)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                this.iServicios = iServicios;
                this.iReservas = iReservas;
                Filtro = new Servicios_Reservas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Servicios_Reservas? Actual { get; set; }
        [BindProperty] public Servicios_Reservas? Filtro { get; set; }
        [BindProperty] public List<Servicios_Reservas>? Lista { get; set; }
        [BindProperty] public List<Servicios>? Servicios { get; set; }
        [BindProperty] public List<Reservas>? Reservas { get; set; }
        [BindProperty] public int IdR { get; set; }

        public virtual void OnGet(int Id) { IdR = Id; OnPostBtNuevo(); }

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
                var task = this.iServicios!.Listar();
                var task2 = this.iReservas!.Listar();
                task.Wait();
                task2.Wait();
                Servicios = task.Result;
                Reservas = task2.Result;
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
                Actual = new Servicios_Reservas();
                Actual.Reserva = IdR;
                Actual.Codigo = Guid.NewGuid().ToString().Substring(0, 8);
                CargarCombox();

                var task = this.iReservas!.Listar();
                task.Wait();
                var reservas_lista = task.Result;
                Actual._Reserva = reservas_lista.FirstOrDefault(x => x.Id == IdR);
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

                Actual!.Reserva = IdR;
                Actual._Reserva = null;


                Task<Servicios_Reservas>? task = null;
                if (Actual!.Id == 0)
                    task = this.iPresentacion!.Guardar(Actual!)!;
                else
                    task = this.iPresentacion!.Modificar(Actual!)!;
                task.Wait();
                Actual = task.Result;
                if (Actual.Id != 0)
                    return RedirectToPage("/Ventanas/Pagos", new {Id = IdR});
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, "Los datos no fueron agregados correctamente (asegurese de que las referencias existan)", ViewData!);
            }
            return Page();
        }

        public virtual void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();
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
    }
}
