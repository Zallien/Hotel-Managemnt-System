using Hotel_Managemnt_System.Models;

namespace Hotel_Managemnt_System.Services
{
    public interface IRoomServices
    {
        public Task<List<RoomType>> GetAllRoom();
    }
}
