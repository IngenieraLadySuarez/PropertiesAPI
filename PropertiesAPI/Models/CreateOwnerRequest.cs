using System.ComponentModel.DataAnnotations;

public class CreateOwnerRequest
{
    [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
    public string Address { get; set; } = string.Empty;

    public DateTime? Birthday { get; set; }
    public IFormFile PhotoFile { get; set; }  // Solo el archivo
}