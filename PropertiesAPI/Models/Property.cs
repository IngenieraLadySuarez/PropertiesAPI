using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PropertiesAPI.Models
{
    public class Property
    {
        [Key]
        [JsonIgnore] // Ignora este campo en JSON
        public int IdProperty { get; set; } // Clave primaria

        [MaxLength(50, ErrorMessage = "El campo{0} debe tener máximo {1} caracteres")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "El campo{0} debe tener máximo {1} caracteres")]
        public string Address { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "(0:C2)")]
        public decimal Price { get; set; } = 0;
        public int CudeInternal { get; set; } = 0;
        public int Year { get; set; } = 0;
        public int IdOwner { get; set; }

    }
}
