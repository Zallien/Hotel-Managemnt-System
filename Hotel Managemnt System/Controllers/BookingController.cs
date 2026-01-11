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
        public async Task<List<DisplayBooking>> GetAllBookingFilterized([FromBody] BookingFilterationModel filterModel)
        {
            List<DisplayBooking> allbookings = new List<DisplayBooking>();

            try
            {
                //allbookings = await _db.Bookings
                //    .Where(x => string.IsNullOrEmpty(filterModel.Searchvalue)
                //                || x.GuestName.ToLower().Contains(filterModel.Searchvalue))
                //    .Skip((filterModel.PageNumber - 1) * filterModel.Counts)
                //    .Take(filterModel.Counts)
                //    .ToListAsync();

                allbookings = await (from b in _db.Bookings
                                     join r in _db.RoomTypes on b.RoomTypeId equals r.RoomTypeId
                                     where string.IsNullOrEmpty(filterModel.Searchvalue)
                                           || b.GuestName.ToLower().Contains(filterModel.Searchvalue)
                                     select new DisplayBooking
                                     {
                                         BookingId = b.BookingId,
                                         GuestName = b.GuestName,
                                         CheckIn = b.CheckInDate,
                                         CheckOut = b.CheckOutDate,
                                         RoomTypeName = r.RoomTypeName,
                                         RoomNumber = b.RoomNumber
                                     })
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
