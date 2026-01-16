using Hotel_Managemnt_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Managemnt_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly Contextdb _db;

        public RoomsController(Contextdb mdb)
        {
            _db = mdb;
        }

        [HttpGet("GetRoomTpes")]
        public async Task<List<RoomType>> GetRoomTpes()
        {
            var listofrooms = await _db.RoomTypes.ToListAsync();
            return listofrooms;
        }

    }
}
