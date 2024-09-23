using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertiesAPI.Models;

namespace PropertiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly PropertiesContext _context;
        public PropertyController(PropertiesContext context)
        {
            _context = context;
        }

        //método para crear propiedades
        [HttpPost]
        [Route("CreateProperty")]
        public async Task<IActionResult> CrearPropiedad([FromBody] Property property)
        {
            try
            {
                await _context.Property.AddAsync(property);
                var rta = await _context.SaveChangesAsync();
                if (rta == 1)
                {
                    return Ok(new { message = "Propiedad creada exitosamente" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error desconocido al crear la propiedad" });
                }
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error de la base de datos al crear la propiedad", details = dbEx.InnerException.Message.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al crear la propiedad", details = ex.Message });
            }
        }

        //Método para listar las propiedades creadas
        [HttpGet]
        [Route("PropertyList")]
        public async Task<ActionResult<IEnumerable<Property>>> ListaPropiedades(
                    [FromQuery] string? name,
                    [FromQuery] string? address,
                    [FromQuery] decimal? priceMin,
                    [FromQuery] decimal? priceMax,
                    [FromQuery] int? yearMin,
                    [FromQuery] int? yearMax)
        {
            try
            {
                var query = _context.Property.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.Name.Contains(name));
                }

                if (!string.IsNullOrEmpty(address))
                {
                    query = query.Where(p => p.Address.Contains(address));
                }

                if (priceMin.HasValue)
                {
                    query = query.Where(p => p.Price >= priceMin.Value);
                }

                if (priceMax.HasValue)
                {
                    query = query.Where(p => p.Price <= priceMax.Value);
                }

                if (yearMin.HasValue)
                {
                    query = query.Where(p => p.Year >= yearMin.Value);
                }

                if (yearMax.HasValue)
                {
                    query = query.Where(p => p.Year <= yearMax.Value);
                }

                var propiedad = await query.ToListAsync();

                return Ok(propiedad);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving properties", details = ex.Message });
            }
        }

        //Método para actulizar el precio de una propiedad filtrada por el IdProperty de la misma
        [HttpPut]
        [Route("UpdatePrice/{id}")]
        public async Task<IActionResult> ActualizarPrecio(int id, [FromBody] decimal newPrice)
        {
            try
            {
                var property = await _context.Property.FindAsync(id);
                if (property == null)
                {
                    return NotFound(new { message = "Propiedad no encontrada" });
                }

                property.Price = newPrice;
                _context.Property.Update(property);
                var rta = await _context.SaveChangesAsync();

                if (rta == 1)
                {
                    return Ok(new { message = "Precio actualizado exitosamente" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error desconocido al actualizar el precio" });
                }
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error de la base de datos al actualizar el precio", details = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al actualizar el precio", details = ex.Message });
            }
        }

        // Método para actualizar una propiedad completa o parcialmente
        [HttpPut]
        [Route("UpdateProperty/{id}")]
        public async Task<IActionResult> ActualizarPropiedad(int id, [FromBody] Property updateModel)
        {
            try
            {
                var property = await _context.Property.FindAsync(id);
                if (property == null)
                {
                    return NotFound(new { message = "Propiedad no encontrada" });
                }

                // Actualiza solo las propiedades que no son nulas en el modelo de actualización
                if (!string.IsNullOrEmpty(updateModel.Name)) property.Name = updateModel.Name;
                if (!string.IsNullOrEmpty(updateModel.Address)) property.Address = updateModel.Address;
                if (updateModel.Price != 0)
                {
                    property.Price = updateModel.Price;
                }

                if (updateModel.CudeInternal != 0)
                {
                    property.CudeInternal = updateModel.CudeInternal;
                }

                if (updateModel.Year != 0)
                {
                    property.Year = updateModel.Year;
                }

                if (updateModel.IdOwner != 0)
                {
                    property.IdOwner = updateModel.IdOwner;
                }


                _context.Property.Update(property);
                var rta = await _context.SaveChangesAsync();

                if (rta == 1)
                {
                    return Ok(new { message = "Propiedad actualizada exitosamente" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error desconocido al actualizar la propiedad" });
                }
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error de la base de datos al actualizar la propiedad", details = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error al actualizar la propiedad", details = ex.Message });
            }
        }
    }
}
