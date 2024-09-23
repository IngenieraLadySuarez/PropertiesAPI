using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertiesAPI.Models
{
    public class PropertyTrace
    {
        public DateTime DateSale { get; set; }
        [MaxLength(50, ErrorMessage = "El campo{0} debe tener máximo {1} caracteres")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "(0:C2)")]
        public decimal value { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "(0:C2)")]
        public decimal Tax { get; set; } = 0;
        public int IdProperty { get; set; }

    }
}
