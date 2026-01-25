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

        //Constructor
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
                                     join rm in _db.HotelRooms on b.RoomNumber equals rm.RoomId
                                     where string.IsNullOrEmpty(filterModel.Searchvalue)
                                           || b.GuestName.ToLower().Contains(filterModel.Searchvalue)
                                     select new DisplayBooking
                                     {
                                         BookingId = b.BookingId,
                                         GuestName = b.GuestName,
                                         CheckIn = b.CheckInDate,
                                         CheckOut = b.CheckOutDate,
                                         RoomTypeName = r.RoomTypeName,
                                         RoomNumber = int.Parse(rm.RoomNumber)
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
                    UpdatedAt = DateTime.Now,
                    NumberOfGuests = bookingdetails.NumberOfGuests,
                    SpecialRequests = bookingdetails.SpecialRequests,
                    PaymentAmount = bookingdetails.Payment
                };

                // Save to database
                await _db.Bookings.AddAsync(newBooking);
                await _db.SaveChangesAsync();

                // Change room availability
                await ChangeRoomAvailability(bookingdetails.RoomNumber);

                return true;
            }
            catch (Exception ex)
            {
                // log ex if needed
                return false;
            }
        }

        [HttpPost("UpdateBooking")]
        public async Task<bool> UpdateBooking([FromBody] AddBookingModel bookingdetails)
        {
            try
            {
                var existingBooking = await _db.Bookings
                    .FirstOrDefaultAsync(b => b.BookingId == bookingdetails.BookingId);
                if (existingBooking == null)
                {
                    return false; // Booking not found
                }
                // Update booking details
                existingBooking.GuestName = bookingdetails.GuestName;
                existingBooking.GuestEmail = bookingdetails.GuestEmail;
                existingBooking.GuestPhone = bookingdetails.GuestPhone;
                existingBooking.RoomTypeId = bookingdetails.RoomType;
                existingBooking.RoomNumber = bookingdetails.RoomNumber;
                existingBooking.CheckInDate = bookingdetails.CheckIn;
                existingBooking.CheckOutDate = bookingdetails.CheckOut;
                existingBooking.TotalAmount = bookingdetails.TotalAmount;
                existingBooking.PaymentStatus = bookingdetails.PaymentStatus;
                existingBooking.BookingStatus = bookingdetails.BookingStatus;
                existingBooking.UpdatedAt = DateTime.Now;
                existingBooking.NumberOfGuests = bookingdetails.NumberOfGuests;
                existingBooking.SpecialRequests = bookingdetails.SpecialRequests;

                // Save changes to database
                _db.Bookings.Update(existingBooking);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // log ex if needed
                return false;
            }
        }

        [HttpPost("GetBookingById")]
        public async Task<AddBookingModel> GetBookingById([FromBody] Guid BookingId)
        {
            AddBookingModel bookingModel = new AddBookingModel();
            try
            {
                var booking = await _db.Bookings
                    .FirstOrDefaultAsync(b => b.BookingId == BookingId);
                if (booking != null)
                {
                    bookingModel = new AddBookingModel
                    {
                        BookingId = booking.BookingId,
                        GuestName = booking.GuestName,
                        GuestEmail = booking.GuestEmail,
                        GuestPhone = booking.GuestPhone,
                        RoomType = booking.RoomTypeId,
                        RoomNumber = booking.RoomNumber,
                        CheckIn = booking.CheckInDate,
                        CheckOut = booking.CheckOutDate,
                        TotalAmount = booking.TotalAmount,
                        PaymentStatus = booking.PaymentStatus,
                        BookingStatus = booking.BookingStatus,
                        NumberOfGuests = booking.NumberOfGuests,
                        SpecialRequests = booking.SpecialRequests
                    };
                }
            }
            catch (Exception ex)
            {
                // log ex if needed
            }
            return bookingModel;
        }

        [HttpGet("GetAllBookings")]
        public async Task<List<DisplayBooking>> GetAllBookings()
        {
            return await (from b in _db.Bookings
                          join r in _db.RoomTypes on b.RoomTypeId equals r.RoomTypeId
                          join rm in _db.HotelRooms on b.RoomNumber equals rm.RoomId
                          select new DisplayBooking
                          {
                              BookingId = b.BookingId,
                              GuestName = b.GuestName,
                              CheckIn = b.CheckInDate,
                              CheckOut = b.CheckOutDate,
                              RoomTypeName = r.RoomTypeName,
                              RoomNumber = int.Parse(rm.RoomNumber)
                          }).ToListAsync();
        }

        [HttpPost("DeleteBooking")]
        public async Task<bool> DeleteBooking([FromBody] Guid BookingId)
        {
            try
            {
                var booking = await _db.Bookings
                    .FirstOrDefaultAsync(b => b.BookingId == BookingId);
                if (booking == null)
                {
                    return false;
                }
                _db.Bookings.Remove(booking);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }






        // Helper method to change room availability
        private async Task<bool> ChangeRoomAvailability(Guid roomnumberid)
        {
            try
            {
                bool isdone = false;
                var room = await _db.HotelRooms
                    .FirstOrDefaultAsync(r => r.RoomId == roomnumberid);
                if (room != null)
                {
                    room.Status = "Occupied";
                    _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    isdone = true;
                    return isdone;
                }
                return isdone;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
