using Microsoft.AspNetCore.Mvc;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using System.Linq;

public class InventarioController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IConverter _converter;

    public InventarioController(ApplicationDbContext context, IConverter converter)
    {
        _context = context;
        _converter = converter;
    }

    public IActionResult GenerarPDF()
    {
        var productos = _context.Productos.ToList();

        var html = "<h1>Inventario de Productos</h1><table><tr><th>Nombre</th><th>Precio</th></tr>";
        foreach (var producto in productos)
        {
            html += $"<tr><td>{producto.Nombre}</td><td>{producto.Precio} {producto.Moneda}</td></tr>";
        }
        html += "</table>";

        var pdfDoc = new HtmlToPdfDocument()
        {
            GlobalSettings = { PaperSize = PaperKind.A4 },
            Objects = { new ObjectSettings { HtmlContent = html } }
        };

        byte[] pdf = _converter.Convert(pdfDoc);

        return File(pdf, "application/pdf", "Inventario.pdf");
    }
}
