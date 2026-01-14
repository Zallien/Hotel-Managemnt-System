using Hotel_Managemnt_System.Models;
using Hotel_Managemnt_System.ServiceModels;
using Hotel_Managemnt_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel_Managemnt_System.Pages
{
    public class BookingpageModel : PageModel
    {
        private readonly IBooking _bookingService;

        // Properties to hold data for the page
        public List<DisplayBooking> BookingList { get; set; } = new();

        [BindProperty]
        public AddBookingModel AddBooking { get; set; }




        // Constructor injection for services
        public BookingpageModel(IBooking bookingService)
        {
            _bookingService = bookingService;
        }

        // Get method to fetch initial data
        public async Task OnGetAsync()
        {
            BookingList = await _bookingService.GetAllBookingFilterized(
                new BookingFilterationModel
                {
                    Searchvalue = string.Empty,
                    Counts = 10,
                    PageNumber = 1
                }
            );
        }
    }
}