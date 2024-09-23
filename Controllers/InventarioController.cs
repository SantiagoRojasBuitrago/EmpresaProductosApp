using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Authorization;
[Authorize]
public class InventarioController : Controller
{
    private readonly ApplicationDbContext _context;

    public InventarioController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult GenerarPDF()
    {
        var productos = _context.Productos.ToList();

        using (var stream = new MemoryStream())
        {
            // Crear un documento PDF
            using (var pdfWriter = new PdfWriter(stream))
            {
                using (var pdfDocument = new PdfDocument(pdfWriter))
                {
                    var document = new Document(pdfDocument);
                    document.Add(new Paragraph("Inventario de Productos").SetFontSize(20));

                    // Crear tabla
                    var table = new Table(2); // Dos columnas
                    table.AddHeaderCell("Nombre");
                    table.AddHeaderCell("Precio");

                    foreach (var producto in productos)
                    {
                        table.AddCell(producto.Nombre);
                        table.AddCell($"{producto.Precio} {producto.Moneda}");
                    }

                    document.Add(table);
                }
            }

            // Devolver el PDF como un archivo
            return File(stream.ToArray(), "application/pdf", "Inventario.pdf");
        }
    }
}
