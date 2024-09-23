using System.ComponentModel.DataAnnotations;

public class Usuario
{
    [Key]
    public string Correo { get; set; }
    [Required]
    public string Contrasena { get; set; }
}
