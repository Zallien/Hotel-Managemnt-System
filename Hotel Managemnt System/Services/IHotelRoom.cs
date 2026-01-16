using Hotel_Managemnt_System.ServiceModels;

namespace Hotel_Managemnt_System.Services
{
    public interface IHotelRoom
    {
        public Task<List<HotelRoomsDisplay>> GetAllRooms(BookingFilterationModel filterModel);
        public Task<List<HotelRoomsDisplay>> GetAllRooms();
    }
}
