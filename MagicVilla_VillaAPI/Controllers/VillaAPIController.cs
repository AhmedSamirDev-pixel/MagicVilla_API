using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]   
        public IEnumerable<VillaDTO> GetVillas()
        {
            return VillaStore.VillaList;
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        public VillaDTO GetVilla(int id)
        {
            return VillaStore.VillaList.FirstOrDefault(villa => villa.Id == id);
        }
    }
}
