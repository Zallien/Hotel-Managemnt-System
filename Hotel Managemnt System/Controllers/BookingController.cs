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

        [HttpPost("AddBooking")]
        public async Task<bool> AddBooking([FromBody] AddBookingModel bookingdetails)
        {
            try
            {
                // Generate sequential booking number based on today's date
                string datePart = DateTime.Now.ToString("yyyyMMdd");

                // Count how many bookings already exist today
                int countToday = await _db.Bookings
                    .CountAsync(b => b.CreatedAt.Date == DateTime.Today);

                int nextSequence = countToday + 1;
                string sequencePart = nextSequence.ToString("D3"); // pad with zeros (001, 002, …)

                string bookingNumber = $"{datePart}-{sequencePart}";

                // Create new booking
                Guid bookingId = Guid.NewGuid();
                Bookings newBooking = new Bookings
                {
                    BookingId = bookingId,
                    BookingNumber = bookingNumber,
                    GuestName = bookingdetails.GuestName,
                    GuestEmail = bookingdetails.GuestEmail,
                    GuestPhone = bookingdetails.GuestPhone,
                    RoomTypeId = bookingdetails.RoomType,
                    RoomNumber = bookingdetails.RoomNumber,
                    CheckInDate = bookingdetails.CheckIn,
                    CheckOutDate = bookingdetails.CheckOut,
                    TotalAmount = bookingdetails.TotalAmount,
                    PaymentStatus = bookingdetails.PaymentStatus,
                    BookingStatus = bookingdetails.BookingStatus,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                // Save to database
                await _db.Bookings.AddAsync(newBooking);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // log ex if needed
                return false;
            }
        }

        [HttpGet("GetAllBookings")]
        public async Task<List<DisplayBooking>> GetAllBookings()
        {
            return await (from b in _db.Bookings
                          join r in _db.RoomTypes on b.RoomTypeId equals r.RoomTypeId
                          select new DisplayBooking
                          {
                              BookingId = b.BookingId,
                              GuestName = b.GuestName,
                              CheckIn = b.CheckInDate,
                              CheckOut = b.CheckOutDate,
                              RoomTypeName = r.RoomTypeName,
                              RoomNumber = b.RoomNumber
                          }).ToListAsync();
        }



    }
}
