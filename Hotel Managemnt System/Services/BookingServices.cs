using Hotel_Managemnt_System.Models;
using Hotel_Managemnt_System.ServiceModels;
using Newtonsoft.Json;

namespace Hotel_Managemnt_System.Services
{
    public class BookingServices : IBooking
    {
        private readonly HttpClient _httpClient;

        public BookingServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Get Booking with filteration
        public async Task<List<DisplayBooking>> GetAllBookingFilterized(BookingFilterationModel filterModel)
        {
            var allbookings = new List<DisplayBooking>();

            try
            {
                
                var response = await _httpClient.PostAsJsonAsync("api/Booking/GetAllBookingFilterized", filterModel);

                if (response.IsSuccessStatusCode)
                {
                    var rawResponse = await response.Content.ReadAsStringAsync();
                    allbookings = JsonConvert.DeserializeObject<List<DisplayBooking>>(rawResponse) ?? new List<DisplayBooking>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API error: {response.StatusCode}, content: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetAllBookingFilterized: {ex.Message}");
            }

            return allbookings;
        }

        //Add Booking
        public async Task<bool> AddBooking(AddBookingModel bookingModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Booking/AddBooking", bookingModel);
                if (response.IsSuccessStatusCode)
                {
                    var Res = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(Res);
                    return result;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API error: {response.StatusCode}, content: {error}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddBooking: {ex.Message}");
                return false;
            }
        }
    }
}