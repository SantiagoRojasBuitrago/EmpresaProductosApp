using System.ComponentModel.DataAnnotations;

public class Empresa
{
    [Key]
    public string NIT { get; set; }
    [Required]
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
}
