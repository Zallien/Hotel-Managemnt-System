using Hotel_Managemnt_System.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hotel_Managemnt_System.Pages
{
    public class AddBookingPageModel : PageModel
    {
        [BindProperty]
        public AddBookingModel Booking { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Save Booking to DB
            return RedirectToPage("Bookingpage");
        }
    }
}