using Hotel_Managemnt_System.ServiceModels;
using Newtonsoft.Json;

namespace Hotel_Managemnt_System.Services
{
    public class HotelRoomServices : IHotelRoom
    {
        private readonly HttpClient _httpClient;

        public HotelRoomServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Get All Rooms with filteration
        public async Task<List<HotelRoomsDisplay>> GetAllRooms(BookingFilterationModel filterModel)
        {
            var allrooms = new List<HotelRoomsDisplay>();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/HotelRoom/GetAllRooms", filterModel);
                if (response.IsSuccessStatusCode)
                {
                    var rawResponse = await response.Content.ReadAsStringAsync();
                    allrooms = JsonConvert.DeserializeObject<List<HotelRoomsDisplay>>(rawResponse) ?? new List<HotelRoomsDisplay>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API error: {response.StatusCode}, content: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetAllRooms: {ex.Message}");
            }
            return allrooms;
        }

        //Get All Rooms without filteration
        public async Task<List<HotelRoomsDisplay>> GetAllRooms()
        {
            var allrooms = new List<HotelRoomsDisplay>();
            try
            {
                var response = await _httpClient.GetAsync("api/HotelRoom/GetAllRooms");
                if (response.IsSuccessStatusCode)
                {
                    var rawResponse = await response.Content.ReadAsStringAsync();
                    allrooms = JsonConvert.DeserializeObject<List<HotelRoomsDisplay>>(rawResponse) ?? new List<HotelRoomsDisplay>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API error: {response.StatusCode}, content: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetAllRooms: {ex.Message}");
            }
            return allrooms;
        }


    }
}
