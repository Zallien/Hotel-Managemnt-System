using Hotel_Managemnt_System.Models;
using Hotel_Managemnt_System.ServiceModels;
using Hotel_Managemnt_System.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hotel_Managemnt_System.Pages
{
    public class BookingpageModel : PageModel
    {
        private readonly IBooking _bookingService;

        public BookingpageModel(IBooking bookingService)
        {
            _bookingService = bookingService;
        }

        public List<DisplayBooking> BookingList { get; set; } = new();

        public async Task OnGetAsync()
        {
            BookingList = await _bookingService.GetAllBookingFilterized(
                new ServiceModels.BookingFilterationModel
                {
                    Searchvalue = string.Empty,
                    Counts = 10,
                    PageNumber = 1
                });
        }
    }
}