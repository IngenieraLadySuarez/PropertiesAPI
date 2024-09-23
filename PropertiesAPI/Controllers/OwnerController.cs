using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertiesAPI.Models;

namespace PropertiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly PropertiesContext _context;

        public OwnerController(PropertiesContext context)
        {
            _context = context;
        }

        //Método para crear un dueño de propiedad
        [HttpPost]
        [Route("CreateOwner")]
        public async Task<IActionResult> CrearDueño([FromForm] CreateOwnerRequest request)
        {
            try
            {
                var owner = new Owner
                {
                    Name = request.Name,
                    Address = request.Address,
                    Birthday = request.Birthday
                };

                if (request.PhotoFile != null && request.PhotoFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await request.PhotoFile.CopyToAsync(memoryStream);
                        owner.Photo = memoryStream.ToArray();
                    }
                }

                await _context.Owner.AddAsync(owner);
                var rta = await _context.SaveChangesAsync();

                if (rta == 1)
                {
                    return Ok(new { message = "Owner created successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unknown error occurred while creating the owner" });
                }
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Database error occurred while creating the owner", details = dbEx.InnerException.Message.ToString() });
            }
            catch (Exception ex)
            {             
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the owner", details = ex.Message });
            }
        }

    }
}
