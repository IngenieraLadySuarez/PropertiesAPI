namespace PropertiesAPI.Models
{
    public class PropertyImage
    {
        public int IdProperty { get; set; }
        public string file { get; set; } = string.Empty;    
        public int Enable { get; set; }   = 0; 
    }
}
