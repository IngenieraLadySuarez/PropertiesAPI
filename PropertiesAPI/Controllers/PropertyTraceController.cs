using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertiesAPI.Models;

namespace PropertiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTraceController : ControllerBase
    {
        private readonly PropertiesContext _context;
        public PropertyTraceController(PropertiesContext context)
        {
            _context = context;
        }

        //Método para crear una venta de una propiedad
        [HttpPost]
        [Route("PropertyTrace")]
        public async Task<IActionResult> CrearVentaPropiedad([FromBody] PropertyTrace propertyTrace)
        {
            try
            {
                await _context.PropertyTrace.AddAsync(propertyTrace);
                var rta = await _context.SaveChangesAsync();

                if (rta == 1)
                {
                    return Ok(new { message = "Venta de la propiedad creada exitosamente" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error desconocido al crear la venta de la propiedad" });
                }
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error de la base de datos al crear la venta de la propiedad", details = dbEx.InnerException.Message.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear la venta de la propiedad", details = ex.Message });
            }
        }
    }
}
