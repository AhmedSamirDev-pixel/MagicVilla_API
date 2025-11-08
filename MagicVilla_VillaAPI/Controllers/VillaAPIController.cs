using AutoMapper;
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
        private readonly IMapper _mapper;
        public VillaAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        // Document the response types for this action
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
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
            return Ok(_mapper.Map<VillaDTO>(villa));

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            if (createDTO == null)
                return BadRequest();

            if (await _db.Villas
                .FirstOrDefaultAsync(villa => villa.Name.ToLower() == createDTO.Name
                .ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }
            // Using AutoMapper
            Villa villa = _mapper.Map<Villa>(createDTO);

            #region Mnual Mapping
            //Villa villa = new()
            //{
            //    Name = createDTO.Name,
            //    Details = createDTO.Details,
            //    Rate = createDTO.Rate,
            //    Sqft = createDTO.Sqft,
            //    Occupancy = createDTO.Occupancy,
            //    ImageUrl = createDTO.ImageUrl,
            //    Amenity = createDTO.Amenity
            //}; 
            #endregion

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
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDTO updateDTO)
        {
            if (updateDTO == null || id != updateDTO.Id)
                return BadRequest();

            // Using AutoMapper
            Villa villa = _mapper.Map<Villa>(updateDTO);

            #region Manual Mapping
            //Villa villa = new()
            //{
            //    Id = updateDTO.Id,
            //    Name = updateDTO.Name,
            //    Details = updateDTO.Details,
            //    Rate = updateDTO.Rate,
            //    Sqft = updateDTO.Sqft,
            //    Occupancy = updateDTO.Occupancy,
            //    ImageUrl = updateDTO.ImageUrl,
            //    Amenity = updateDTO.Amenity
            //}; 
            #endregion

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

            // Using AutoMapper
            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

            #region Manual Mapping
            //VillaUpdateDTO villaDTO = new()
            //{
            //    Id = villa.Id,
            //    Name = villa.Name,
            //    Details = villa.Details,
            //    Rate = villa.Rate,
            //    Sqft = villa.Sqft,
            //    Occupancy = villa.Occupancy,
            //    ImageUrl = villa.ImageUrl,
            //    Amenity = villa.Amenity
            //}; 
            #endregion

            if (villa == null)
                return NotFound();
            patchDTO.ApplyTo(villaDTO, ModelState);

            // Using AutoMapper
            Villa villaModel = _mapper.Map<Villa>(villaDTO);

            #region Manual Mapping
            //Villa villaModel = new()
            //{
            //    Id = villaDTO.Id,
            //    Name = villaDTO.Name,
            //    Details = villaDTO.Details,
            //    Rate = villaDTO.Rate,
            //    Sqft = villaDTO.Sqft,
            //    Occupancy = villaDTO.Occupancy,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Amenity = villaDTO.Amenity
            //}; 
            #endregion

            _db.Villas.Update(villaModel);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();
        }




    }

}
