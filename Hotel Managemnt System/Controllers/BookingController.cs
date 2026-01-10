using Hotel_Managemnt_System.Models;
using Hotel_Managemnt_System.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Managemnt_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookingController : Controller
    {
        private readonly Contextdb _db;

        public BookingController(Contextdb mdb)
        {
            _db = mdb;
        }

        [HttpPost("GetAllBookingFilterized")]
        public async Task<List<Bookings>> GetAllBookingFilterized([FromBody] BookingFilterationModel filterModel)
        {
            List<Bookings> allbookings = new List<Bookings>();

            try
            {
                allbookings = await _db.Bookings
                    .Where(x => string.IsNullOrEmpty(filterModel.Searchvalue)
                                || x.GuestName.ToLower().Contains(filterModel.Searchvalue))
                    .Skip((filterModel.PageNumber - 1) * filterModel.Counts)
                    .Take(filterModel.Counts)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // log ex if needed
            }

            return allbookings;
        }

    }
}
