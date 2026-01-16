using Hotel_Managemnt_System.ServiceModels;
using Hotel_Managemnt_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Hotel_Managemnt_System.Pages
{
    public class AddBookingPageModel : PageModel
    {
        [BindProperty]
        public AddBookingModel Booking { get; set; } = new();
        private readonly IBooking _bookingservice;
        private readonly IRoomServices _roomservice;
        public SelectList SelectRoomType { get; set; }
        public SelectList SelectBookingStatus { get; set; } = new SelectList(new[]
        {
            new { Value = "Confirmed", Text = "Confirmed" },
            new { Value = "Pending", Text = "Pending" },
            new { Value = "Cancelled", Text = "Cancelled" }
        }, "Value", "Text");
        public SelectList SelectPaymentStatus { get; set; } = new SelectList(new[]
        {
            new { Value = "Paid", Text = "Paid" },
            new { Value = "Unpaid", Text = "Unpaid" },
            new { Value = "Refunded", Text = "Refunded" }
        }, "Value", "Text");


        public AddBookingPageModel(IBooking bookingservice, IRoomServices roomservice)
        {
            _bookingservice = bookingservice;
            _roomservice = roomservice;
        }

        public async Task OnGetAsync()
        {
            await LoadRoomTypesAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _bookingservice.AddBooking(Booking);
            return RedirectToPage("Bookingpage");
        }

        private async Task LoadRoomTypesAsync()
        {
            try
            {
                var roomTypes = await _roomservice.GetAllRoom();
                SelectRoomType = new SelectList(roomTypes, "RoomTypeId", "RoomTypeName");
            }
            catch (Exception ex)
            {

            }
        }
    }
}