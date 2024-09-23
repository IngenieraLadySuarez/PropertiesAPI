using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertiesAPI.Models
{
    public class Owner
    {
        
        // [Key]
        //public int IdOwner { get; set; } // Primary key

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string Address { get; set; } = string.Empty;

        public DateTime? Birthday { get; set; }
        public byte[]? Photo { get; set; }
    }
}
