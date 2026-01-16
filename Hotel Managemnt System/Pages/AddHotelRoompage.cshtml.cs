using Hotel_Managemnt_System.ServiceModels;
using Hotel_Managemnt_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel_Managemnt_System.Pages
{
    public class AddHotelRoompageModel : PageModel
    {

        [BindProperty]
        public AddHotelRoom HotelRoom { get; set; }
        private readonly IRoomServices _roomservice;
        public SelectList SelectRoomType { get; set; }

        public AddHotelRoompageModel(IRoomServices roomservice)
        {
            _roomservice = roomservice;
        }

        public async Task OnGetAsync()
        {
            await LoadRoomTypesAsync();
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
