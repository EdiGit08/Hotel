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
    public class AuditoriasModel : PageModel
    {
        private IAuditoriasPresentacion? iPresentacion = null;

        public AuditoriasModel(IAuditoriasPresentacion iPresentacion)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                Filtro = new Auditorias();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Auditorias? Actual { get; set; }
        [BindProperty] public Auditorias? Filtro { get; set; }
        [BindProperty] public List<Auditorias>? Lista { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

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

                // Estas lineas se encargan de revisar si el usuario tiene acceso a la informacion
                var usuariosPresentacion = new UsuariosPresentacion();
                var usuarios = usuariosPresentacion.Listar().Result;
                var usuario = usuarios.FirstOrDefault(u => u.Nombre!.ToLower() == variable_session!.ToLower() && (u.Rol == 1 || u.Rol == 3));

                if (usuario == null)
                {
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

        public virtual IActionResult OnPostExportarExcel()
        {

            try
            {
                var task = this.iPresentacion!.Listar();
                task.Wait();

                Lista = task.Result;

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Auditorias");

                worksheet.Cell(1, 5).Value = "ID";
                worksheet.Cell(1, 6).Value = "CODIGO";
                worksheet.Cell(1, 7).Value = "ACCION";
                worksheet.Cell(1, 8).Value = "FECHA";

                var headerRange = worksheet.Range("E1:H1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Font.FontColor = XLColor.White;


                for (int i = 0; i < Lista.Count; i++)
                {
                    var auditoria = Lista[i];
                    worksheet.Cell(i + 2, 5).Value = auditoria.Id;
                    worksheet.Cell(i + 2, 6).Value = auditoria.Codigo;
                    worksheet.Cell(i + 2, 7).Value = auditoria.Accion;
                    worksheet.Cell(i + 2, 8).Value = auditoria.Fecha;
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

                Lista = task.Result;

                using var stream = new MemoryStream();
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                var titulo = new Paragraph("Auditorias")
                            .SetFont(boldFont)
                            .SetFontSize(14);

                document.Add(titulo);

                var table = new Table(4);
                table.AddHeaderCell("ID");
                table.AddHeaderCell("CODIGO");
                table.AddHeaderCell("ACCION");
                table.AddHeaderCell("FECHA");


                foreach (var reserva in Lista)
                {
                    table.AddCell(reserva.Id.ToString());
                    table.AddCell(reserva.Codigo);
                    table.AddCell(reserva.Accion);
                    table.AddCell(reserva.Fecha.ToString());

                }

                document.Add(table);
                document.Close();

                return File(stream.ToArray(), "application/pdf", "Auditorias.pdf");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }
    }
}
