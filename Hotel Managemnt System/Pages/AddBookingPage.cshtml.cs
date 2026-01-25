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
        private readonly IBooking _bookingservice;
        private readonly IRoomServices _roomservice;
        private readonly IHotelRoom _hotelroomservice;


        [BindProperty]
        public AddBookingModel Booking { get; set; } = new();

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


        public AddBookingPageModel(IBooking bookingservice, IRoomServices roomservice, IHotelRoom hotelroomservice)
        {
            _bookingservice = bookingservice;
            _roomservice = roomservice;
            _hotelroomservice = hotelroomservice;
        }

        public async Task OnGetAsync()
        {
            await LoadRoomTypesAsync();
        }


        // Handle form submission
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await LoadRoomTypesAsync();
                return Page();
            }

            await _bookingservice.AddBooking(Booking);
            return RedirectToPage("Bookingpage");
        }

        // Load Room Types for dropdown
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

        // Load Available Rooms based on selected Room Type
        public async Task<JsonResult> OnGetAvailableRoomsAsync(Guid roomTypeId)
        {
            try
            {
                var availableRooms = await _hotelroomservice.GetAvailableRoomsByRoomType(roomTypeId);
                var roomNumbers = availableRooms.Select(r => r.RoomNumber).ToList();
                return new JsonResult(roomNumbers);
            }
            catch (Exception ex)
            {
                return new JsonResult(new List<int>());
            }
        }
    }
}