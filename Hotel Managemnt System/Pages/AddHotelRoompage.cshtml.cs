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
        private readonly IHotelRoom _hotelroomservice;
        public SelectList SelectRoomType { get; set; }

        public AddHotelRoompageModel(IRoomServices roomservice, IHotelRoom hotelroomservice)
        {
            _roomservice = roomservice;
            _hotelroomservice = hotelroomservice;
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadRoomTypesAsync();
                return Page();
            }
            try
            {
                // Call the service to add the new hotel room
                var isAdded = await _hotelroomservice.AddNewHotelRoom(HotelRoom);
                if (isAdded)
                {
                    return RedirectToPage("RoomManagementpage");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "A room with this number already exists.");
                    await LoadRoomTypesAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while adding the room.");
                await LoadRoomTypesAsync();
                return Page();
            }
        }
    }
}
