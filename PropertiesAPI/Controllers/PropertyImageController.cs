using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertiesAPI.Models;

namespace PropertiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController : ControllerBase
    {
        private readonly PropertiesContext _context;
        public PropertyImageController(PropertiesContext context)
        {
            _context = context;
        }

        //Método para crear una imagen de una propiedad
        [HttpPost]
        [Route("CreatePropertyImage")]
        public async Task<IActionResult> CrearImagenPropiedad([FromBody] PropertyImage propertyImage)
        {
            try
            {
                await _context.PropertyImage.AddAsync(propertyImage);
                var rta = await _context.SaveChangesAsync();

                if (rta == 1)
                {
                    return Ok(new { message = "Imagen de la propiedad creada exitosamente" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error desconocido al crear la imagen de la propiedad" });
                }
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error de la base de datos al crear la imagen de la propiedad", details = dbEx.InnerException.Message.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear la imagen de la propiedad", details = ex.Message });
            }
        }
    }
}
