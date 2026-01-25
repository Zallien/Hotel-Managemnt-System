using Hotel_Managemnt_System.Models;
using Hotel_Managemnt_System.ServiceModels;
using Hotel_Managemnt_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Hotel_Managemnt_System.Pages
{
    public class UpdateBookingModel : PageModel
    {
        [BindProperty]
        public AddBookingModel Booking { get; set; } = new AddBookingModel();
        private readonly IBooking _bookingService;
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


        public UpdateBookingModel(IBooking bookingService, IRoomServices roomservice)
        {
            _bookingService = bookingService;
            _roomservice = roomservice;
        }


        public async Task<IActionResult> OnGetAsync(Guid bookingId)
        {
            if (bookingId == Guid.Empty)
            {
                return RedirectToPage("Bookingpage");
            }

            //await LoadRoomTypesAsync();
            await LoadBookingDetailsAsync(bookingId);

            if (Booking == null)
            {
                return RedirectToPage("Bookingpage");
            }

            Booking.BookingId = bookingId;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //await LoadRoomTypesAsync();
                return Page();
            }

            if (Booking.BookingId == Guid.Empty)
            {
                //await LoadRoomTypesAsync();
                return Page();
            }

            await _bookingService.UpdateBooking(Booking);
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

        private async Task LoadBookingDetailsAsync(Guid bookingId)
        {
            try
            {
                
                Booking = await _bookingService.GetBookingById(bookingId);
            }
            catch (Exception ex)
            {
                // Handle exception
            }


        }
    }
}
