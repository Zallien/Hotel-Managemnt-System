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
        [BindProperty]
        public string SearchValue { get; set; } = "";


        // Constructor injection for services
        public BookingpageModel(IBooking bookingService)
        {
            _bookingService = bookingService;
        }

        // Get method to fetch initial data
        public async Task OnGetAsync()
        {
            await LoadBookings();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            await LoadBookings();
            return Page();
        }

        public async Task LoadBookings()
        {
            BookingList = await _bookingService.GetAllBookingFilterized(
                new BookingFilterationModel
                {
                    Searchvalue = SearchValue ?? string.Empty,
                    Counts = 10,
                    PageNumber = 1
                }
            );
        }
    }
}