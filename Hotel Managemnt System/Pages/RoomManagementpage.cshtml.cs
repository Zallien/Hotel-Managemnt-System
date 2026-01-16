using Hotel_Managemnt_System.Models;
using Hotel_Managemnt_System.ServiceModels;
using Hotel_Managemnt_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hotel_Managemnt_System.Pages
{
    public class RoomManagementpageModel : PageModel
    {
        [BindProperty]
        public List<HotelRoomsDisplay> Rooms { get; set; }
        readonly IHotelRoom _hotelRoomService;
        
        public RoomManagementpageModel(IHotelRoom hotelRoomService)
        {
            _hotelRoomService = hotelRoomService;
            Rooms = new List<HotelRoomsDisplay>();
            
        }

        public async Task OnGetAsync()
        {
            await LoadRoomsAsync();
        }

        private async Task LoadRoomsAsync()
        {
            Rooms = await _hotelRoomService.GetAllRooms();
        }
    }
}
