using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClosedXML.Excel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font.Constants;
using iText.Kernel.Font;

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
        [BindProperty] public Usuarios? Usuario { get; set; }


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

                var task = this.iPresentacion!.PorCodigo(Filtro!);
                task.Wait();

                if (!ValidarPermiso())
                {
                    var usuariosPresentacion = new UsuariosPresentacion();
                    var usuarios = usuariosPresentacion.Listar().Result;
                    var usuario = usuarios.FirstOrDefault(u => u.Nombre!.ToLower() == variable_session!.ToLower());

                    
                    
                    Lista = task.Result.Where(x => x.Cliente == usuario!.Cliente).ToList();
                }

                else { Lista = task.Result; }

                    Accion = Enumerables.Ventanas.Listas;
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
                Actual.Codigo = Guid.NewGuid().ToString().Substring(0,8);

                var variable_session = HttpContext.Session.GetString("Usuario");
                var usuariosPresentacion = new UsuariosPresentacion();
                var usuarios = usuariosPresentacion.Listar().Result;
                Usuario = usuarios.FirstOrDefault(u => u.Nombre!.ToLower() == variable_session!.ToLower());

                Actual.Cliente = Usuario!.Cliente;
                var task = this.iclientes!.Listar();
                Actual._Cliente = task.Result.FirstOrDefault(x => x.Id == Actual.Cliente);
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

        public virtual IActionResult OnPostExportarExcel()
        {

            try {
                    var task = this.iPresentacion!.Listar();
                    task.Wait();

                    if (!ValidarPermiso())
                    {

                        var variable_session = HttpContext.Session.GetString("Usuario");
                        var usuariosPresentacion = new UsuariosPresentacion();
                        var usuarios = usuariosPresentacion.Listar().Result;
                        var usuario = usuarios.FirstOrDefault(u => u.Nombre!.ToLower() == variable_session!.ToLower());



                        Lista = task.Result.Where(x => x.Cliente == usuario!.Cliente).ToList();
                    }

                    else { Lista = task.Result; }

                    using var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("Reservas");

                    worksheet.Cell(1, 5).Value = "ID";
                    worksheet.Cell(1, 6).Value = "CODIGO";
                    worksheet.Cell(1, 7).Value = "FECHA ENTRADA";
                    worksheet.Cell(1, 8).Value = "FECHA SALIDA";
                    worksheet.Cell(1, 9).Value = "CLIENTE";
                    worksheet.Cell(1, 10).Value = "HABITACION";

                    var headerRange = worksheet.Range("E1:J1");
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                    headerRange.Style.Font.FontColor = XLColor.White;


                for (int i = 0; i < Lista.Count; i++)
                    {
                        var reserva = Lista[i];
                        worksheet.Cell(i + 2, 5).Value = reserva.Id;
                        worksheet.Cell(i + 2, 6).Value = reserva.Codigo;
                        worksheet.Cell(i + 2, 7).Value = reserva.Fecha_Entrada;
                        worksheet.Cell(i + 2, 8).Value = reserva.Fecha_Salida;
                        worksheet.Cell(i + 2, 9).Value = reserva._Cliente!.Nombre;
                        worksheet.Cell(i + 2, 10).Value = reserva._Habitacion!.Nombre;
                }

                    worksheet.Columns().AdjustToContents();
                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    stream.Position = 0;

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Reservas.xlsx");
               }

            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return Page();
            }

        }

        public virtual IActionResult OnPostExportarPDF()
        {
            try
            {
                var task = this.iPresentacion!.Listar();
                task.Wait();

                if (!ValidarPermiso())
                {

                    var variable_session = HttpContext.Session.GetString("Usuario");
                    var usuariosPresentacion = new UsuariosPresentacion();
                    var usuarios = usuariosPresentacion.Listar().Result;
                    var usuario = usuarios.FirstOrDefault(u => u.Nombre!.ToLower() == variable_session!.ToLower());



                    Lista = task.Result.Where(x => x.Cliente == usuario!.Cliente).ToList();
                }

                else { Lista = task.Result; }

                using var stream = new MemoryStream();
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                var titulo = new Paragraph("Reservas")
                            .SetFont(boldFont)
                            .SetFontSize(14);

                document.Add(titulo);

                var table = new Table(6);
                table.AddHeaderCell("ID");
                table.AddHeaderCell("CODIGO");
                table.AddHeaderCell("FECHA ENTRADA");
                table.AddHeaderCell("FECHA SALIDA");
                table.AddHeaderCell("CLIENTE");
                table.AddHeaderCell("HABITACION");


                foreach (var reserva in Lista)
                {
                    table.AddCell(reserva.Id.ToString());
                    table.AddCell(reserva.Codigo);
                    table.AddCell(reserva.Fecha_Entrada.ToString());
                    table.AddCell(reserva.Fecha_Salida.ToString());
                    table.AddCell(reserva._Cliente!.Nombre);
                    table.AddCell(reserva._Habitacion!.Nombre);

                }

                document.Add(table);
                document.Close();

                return File(stream.ToArray(), "application/pdf", "Reservas.pdf");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }

    }
}

