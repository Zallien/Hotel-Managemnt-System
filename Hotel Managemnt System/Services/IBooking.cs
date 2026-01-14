using Hotel_Managemnt_System.Models;
using Hotel_Managemnt_System.ServiceModels;

namespace Hotel_Managemnt_System.Services
{
    public interface IBooking
    {

        public Task<List<DisplayBooking>> GetAllBookingFilterized(BookingFilterationModel filterModel);
        public Task<bool> AddBooking(AddBookingModel bookingModel);
    }
}
