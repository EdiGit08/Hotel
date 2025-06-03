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
    public class PagosModel : PageModel
    {
        private IPagosPresentacion? iPresentacion = null;
        private IReservasPresentacion? ireservas = null;
        private IPromocionesPresentacion? ipromociones = null;

        public PagosModel(IPagosPresentacion iPresentacion,
            IReservasPresentacion? ireservas,
             IPromocionesPresentacion? ipromociones)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                this.ipromociones= ipromociones;
                this.ireservas = ireservas;
                Filtro = new Pagos();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Pagos? Actual { get; set; }
        [BindProperty] public Pagos? Filtro { get; set; }
        [BindProperty] public List<Pagos>? Lista { get; set; }
        [BindProperty] public List<Reservas>? Reservas { get; set; }
        [BindProperty] public List<Promociones>? Promociones { get; set; }
        [BindProperty] public int IdR { get; set;  }



        public virtual void OnGet(int Id) {

            IdR = Id;

            if (Id != 0)
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
                
                

                if (!ValidarPermiso()) {
                    var lista = task.Result;
                    var usuarios_presentacion = new UsuariosPresentacion();
                    var usuarios = usuarios_presentacion.Listar().Result;
                    var usuario = usuarios.FirstOrDefault(x => x.Nombre == variable_session);

                    var reservas = ireservas!.Listar().Result.Where(x => x.Cliente == usuario!.Cliente).ToList();
                    Lista = new List<Pagos>();

                    foreach(var reserva in reservas)
                    {
                        var pago = lista.FirstOrDefault(x => x.Reserva == reserva.Id);
                        if(pago != null)
                        {
                            Lista.Add(pago);
                        }
                    }
                }

                else { Lista = task.Result; }

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
                var task = this.ireservas!.Listar();
                var task2 = this.ipromociones!.Listar();
                task.Wait();
                task2.Wait();
                Reservas = task.Result;
                Promociones = task2.Result;
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
                Actual = new Pagos();
                Actual.Codigo = Guid.NewGuid().ToString().Substring(0, 16);
                CargarCombox();

                Actual.Reserva = IdR;
                var task = this.ireservas!.Listar();
                task.Wait();
                Actual._Reserva = task.Result.FirstOrDefault(x => x.Id == IdR);
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

        public virtual void OnPostBtGuardar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;
                Actual!._Reserva = null;
                var ac = Actual;

                

                Task<Pagos>? task = null;
                if (Actual!.Id == 0)
                    task = this.iPresentacion!.Guardar(Actual!)!;
                else
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

        public virtual IActionResult OnPostExportarExcel()
        {

            try
            {
                var task = this.iPresentacion!.Listar();
                task.Wait();

                
                if (!ValidarPermiso())
                {
                    var lista = task.Result;
                    var variable_session = HttpContext.Session.GetString("Usuario");
                    var usuarios_presentacion = new UsuariosPresentacion();
                    var usuarios = usuarios_presentacion.Listar().Result;
                    var usuario = usuarios.FirstOrDefault(x => x.Nombre == variable_session);

                    var reservas = ireservas!.Listar().Result.Where(x => x.Cliente == usuario!.Cliente).ToList();
                    Lista = new List<Pagos>();

                    foreach (var reserva in reservas)
                    {
                        var pago = lista.FirstOrDefault(x => x.Reserva == reserva.Id);
                        if (pago != null)
                        {
                            Lista.Add(pago);
                        }
                    }
                }

                else { Lista = task.Result; }

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Pagos");

                worksheet.Cell(1, 5).Value = "ID";
                worksheet.Cell(1, 6).Value = "CODIGO";
                worksheet.Cell(1, 7).Value = "MEDIO PAGO";
                worksheet.Cell(1, 8).Value = "TOTAL";
                worksheet.Cell(1, 9).Value = "RESERVA";

                var headerRange = worksheet.Range("E1:I1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Font.FontColor = XLColor.White;


                for (int i = 0; i < Lista.Count; i++)
                {
                    var pago = Lista[i];
                    worksheet.Cell(i + 2, 5).Value = pago.Id;
                    worksheet.Cell(i + 2, 6).Value = pago.Codigo;
                    worksheet.Cell(i + 2, 7).Value = pago.Medio;
                    worksheet.Cell(i + 2, 8).Value = pago._Reserva!.Codigo;
                }

                worksheet.Columns().AdjustToContents();

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Pagos.xlsx");
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
                    var lista = task.Result;
                    var usuarios_presentacion = new UsuariosPresentacion();
                    var usuarios = usuarios_presentacion.Listar().Result;
                    var usuario = usuarios.FirstOrDefault(x => x.Nombre == variable_session);

                    var reservas = ireservas!.Listar().Result.Where(x => x.Cliente == usuario!.Cliente).ToList();
                    Lista = new List<Pagos>();

                    foreach (var reserva in reservas)
                    {
                        var pago = lista.FirstOrDefault(x => x.Reserva == reserva.Id);
                        if (pago != null)
                        {
                            Lista.Add(pago);
                        }
                    }
                }

                else { Lista = task.Result; }

                using var stream = new MemoryStream();
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                var titulo = new Paragraph("Pagos")
                            .SetFont(boldFont)
                            .SetFontSize(14);

                document.Add(titulo);

                var table = new Table(6);
                table.AddHeaderCell("ID");
                table.AddHeaderCell("CODIGO");
                table.AddHeaderCell("MEDIO");
                table.AddHeaderCell("TOTAL");
                table.AddHeaderCell("DESCUENTO");
                table.AddHeaderCell("RESERVA");


                foreach (var reserva in Lista)
                {
                    table.AddCell(reserva.Id.ToString());
                    table.AddCell(reserva.Codigo);
                    table.AddCell(reserva.Medio);
                    table.AddCell(reserva.Total.ToString());
                    table.AddCell(reserva._Promocion!.Descuento.ToString());
                    table.AddCell(reserva._Reserva!.Codigo);

                }

                document.Add(table);
                document.Close();

                return File(stream.ToArray(), "application/pdf", "Pagos.pdf");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }
    }
}

