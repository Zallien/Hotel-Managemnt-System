using Hotel_Managemnt_System.Models;
using Hotel_Managemnt_System.ServiceModels;
using Hotel_Managemnt_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel_Managemnt_System.Pages
{
    public class EditHotelRoompageModel : PageModel
    {
        [BindProperty]
        public AddHotelRoom HotelRoom { get; set; } = new AddHotelRoom();
        private readonly IRoomServices _roomservice;
        private readonly IHotelRoom _hotelroomservice;
        public SelectList SelectRoomType { get; set; }

        public EditHotelRoompageModel(IRoomServices roomservice, IHotelRoom hotelroomservice)
        {
            _roomservice = roomservice;
            _hotelroomservice = hotelroomservice;
        }

        public async Task OnGetAsync(Guid HotelRoomId)
        {
            await LoadRoomTypesAsync();
            await LoadHotelRoomDetailsAsync(HotelRoomId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadRoomTypesAsync();
                return Page();
            }

            if (HotelRoom.RoomId == Guid.Empty)
            {
                await LoadRoomTypesAsync();
                return Page();
            }

            bool isupdated = await UpdateHotelRoom();
            if (isupdated == false)
            {
                return Page();
            }
            return Redirect("RoomManagementpage");

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
        private async Task LoadHotelRoomDetailsAsync(Guid roomId)
        {
            try
            {
                HotelRoom = await _hotelroomservice.GetHotelRoomById(roomId);
            }
            catch (Exception ex)
            {
            }
        }
        private async Task<bool> UpdateHotelRoom()
        {
            bool isupdated = false;
            try
            {
                isupdated = await _hotelroomservice.UpdateHotelRoom(HotelRoom);
            }
            catch (Exception ex)
            {
                isupdated = false;
            }
            return isupdated;
        }

    }
}
