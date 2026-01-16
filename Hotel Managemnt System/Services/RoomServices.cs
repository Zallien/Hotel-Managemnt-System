using Hotel_Managemnt_System.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace Hotel_Managemnt_System.Services
{
    public class RoomServices : IRoomServices
    {
        private readonly HttpClient _httpClient;

        public RoomServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RoomType>> GetAllRoom()
        {
            List<RoomType> allrooms = new List<RoomType>();
            try
            {
                var response = await _httpClient.GetAsync("api/Rooms/GetRoomTpes");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    allrooms = JsonConvert.DeserializeObject<List<RoomType>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching rooms: {ex.Message}");
            }

            return allrooms;
        }
    }
}
