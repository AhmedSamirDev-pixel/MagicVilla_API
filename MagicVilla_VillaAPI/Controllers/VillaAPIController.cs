using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MagicVilla_VillaAPI.Controllers
{
    //[Route("api/VillaAPI")]
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public VillaAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        // Document the response types for this action
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            return Ok(await _db.Villas.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        // Document the response types for this action
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
                return BadRequest();
            var villa = await _db.Villas.FirstOrDefaultAsync(villa => villa.Id == id);
            if (villa == null)
                return NotFound();
            return Ok(villa);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO villaDTO)
        {
            if (villaDTO == null)
                return BadRequest();

            if (await _db.Villas
                .FirstOrDefaultAsync(villa => villa.Name.ToLower() == villaDTO.Name
                .ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }

            Villa villa = new()
            {
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Occupancy = villaDTO.Occupancy,
                ImageUrl = villaDTO.ImageUrl,
                Amenity = villaDTO.Amenity
            };

           await _db.Villas.AddAsync(villa);
            _db.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();
            var villa = await _db.Villas.FirstOrDefaultAsync(villa => villa.Id == id);
            if (villa == null)
                return NotFound();
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
                return BadRequest();
            Villa villa = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Occupancy = villaDTO.Occupancy,
                ImageUrl = villaDTO.ImageUrl,
                Amenity = villaDTO.Amenity
            };
            _db.Villas.Update(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialVilla(int id,[FromBody]JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
                return BadRequest();
            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(villa => villa.Id == id);
            VillaUpdateDTO villaDTO = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Details = villa.Details,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                Occupancy = villa.Occupancy,
                ImageUrl = villa.ImageUrl,
                Amenity = villa.Amenity
            };

            if (villa == null)
                return NotFound();
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa villaModel = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Occupancy = villaDTO.Occupancy,
                ImageUrl = villaDTO.ImageUrl,
                Amenity = villaDTO.Amenity
            };
            
            _db.Villas.Update(villaModel);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }




    }

}
