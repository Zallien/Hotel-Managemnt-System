using Hotel_Managemnt_System.ServiceModels;

namespace Hotel_Managemnt_System.Services
{
    public interface IHotelRoom
    {
        public Task<List<HotelRoomsDisplay>> GetAllRooms(BookingFilterationModel filterModel);
        public Task<List<HotelRoomsDisplay>> GetAllRooms();
        public Task<AddHotelRoom> GetHotelRoomById(Guid RoomId);
        public Task<bool> UpdateHotelRoom(AddHotelRoom hotelroom);
        public Task<bool> AddNewHotelRoom(AddHotelRoom newroom);

    }
}
