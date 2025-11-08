using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/VillaAPI")]
    //[ApiController]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogger<VillaAPIController> _logger;
        //public VillaAPIController(ILogger<VillaAPIController> logger)
        //{
            //_logger = logger;
        //}

        private readonly ILogging _logger;
        public VillaAPIController(ILogging logger)
        {
            _logger = logger;
        }




        [HttpGet]
        // Document the response types for this action
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting all villas", "");
            return Ok(VillaStore.VillaList);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        // Document the response types for this action
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.Log("Get Villa Error with Id " + id, "error");
                return BadRequest();
            }
            VillaDTO villa = VillaStore.VillaList.FirstOrDefault(villa => villa.Id == id);
            if (villa == null)
                return NotFound();
            return Ok(villa);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null)
                return BadRequest();

            if (VillaStore.VillaList
                .FirstOrDefault(villa => villa.Name.ToLower() == villaDTO.Name
                .ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }

            if (villaDTO.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError, "Id should not be provided or must be zero.");

            villaDTO.Id = VillaStore.VillaList.OrderByDescending(villa => villa.Id).FirstOrDefault().Id + 1;
            VillaStore.VillaList.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);



        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();
            VillaDTO villa = VillaStore.VillaList.FirstOrDefault(villa => villa.Id == id);
            if (villa == null)
                return NotFound();
            VillaStore.VillaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
                return BadRequest();
            VillaDTO villa = VillaStore.VillaList.FirstOrDefault(villa => villa.Id == id);
            if (villa == null)
                return NotFound();
            villa.Name = villaDTO.Name;
            villa.Occupancy = villaDTO.Occupancy;
            villa.Sqft = villaDTO.Sqft;
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id,[FromBody]JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
                return BadRequest();
            VillaDTO villa = VillaStore.VillaList.FirstOrDefault(villa => villa.Id == id);
            if (villa == null)
                return NotFound();
            patchDTO.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return NoContent();
        }




    }

}
