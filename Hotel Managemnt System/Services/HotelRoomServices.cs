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

        //Get Hotel Room by ID
        public async Task<AddHotelRoom> GetHotelRoomById(Guid RoomId)
        {
            AddHotelRoom hotelRoom = new AddHotelRoom();
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/HotelRoom/GetRoomById", RoomId);
                if (response.IsSuccessStatusCode)
                {
                    var rawResponse = await response.Content.ReadAsStringAsync();
                    hotelRoom = JsonConvert.DeserializeObject<AddHotelRoom>(rawResponse) ?? new AddHotelRoom();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API error: {response.StatusCode}, content: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetHotelRoomById: {ex.Message}");
            }
            return hotelRoom;
        }

        //Update Hotel Room
        public async Task<bool> UpdateHotelRoom(AddHotelRoom hotelroom)
        {
            bool isupdated = false;
            try
            {
                if (hotelroom.RoomId == null || hotelroom.RoomId == Guid.Empty)
                {
                    return isupdated;
                }
                var response = await _httpClient.PostAsJsonAsync($"api/HotelRoom/UpdateHotelRoom", hotelroom);
                if (response.IsSuccessStatusCode)
                {
                    isupdated = true;
                    return isupdated;
                }
            }
            catch (Exception)
            {
                isupdated = false;
            }
            return isupdated;
        }

        //Add New Hotel Room
        public async Task<bool> AddNewHotelRoom(AddHotelRoom newroom)
        {
            bool isAdded = false;
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/HotelRoom/AddNewRoom", newroom);
                if (response.IsSuccessStatusCode)
                {
                    isAdded = true;
                    return isAdded;
                }
            }
            catch (Exception)
            {
                isAdded = false;
            }
            return isAdded;
        }

        //Filter Available Rooms based on Room Type
        public async Task<List<HotelRoomsDisplay>> GetAvailableRoomsByRoomType(Guid RoomTypeId)
        {
            var availableRooms = new List<HotelRoomsDisplay>();
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/HotelRoom/FilterRoomAvailableByRoomType", RoomTypeId);
                if (response.IsSuccessStatusCode)
                {
                    var rawResponse = await response.Content.ReadAsStringAsync();
                    availableRooms = JsonConvert.DeserializeObject<List<HotelRoomsDisplay>>(rawResponse) ?? new List<HotelRoomsDisplay>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API error: {response.StatusCode}, content: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetAvailableRoomsByRoomType: {ex.Message}");
            }
            return availableRooms;
        }


    }
}
