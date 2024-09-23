using System.ComponentModel.DataAnnotations;

public class Producto
{
    [Key]
    public string Codigo { get; set; }
    [Required]
    public string Nombre { get; set; }
    public string Caracteristicas { get; set; }
    public decimal Precio { get; set; }
    public string Moneda { get; set; }

    [Required]
    public string EmpresaNIT { get; set; }
}
