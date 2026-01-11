using Azure;
using Hotel_Managemnt_System.Models;
using Hotel_Managemnt_System.ServiceModels;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace Hotel_Managemnt_System.Services
{
    public class BookingServices : IBooking
    {
        string BaseUrl = "http://localhost:5007";

        public async Task<List<DisplayBooking>> GetAllBookingFilterized(BookingFilterationModel filterModel)
        {
            List<DisplayBooking> allbookings = new List<DisplayBooking>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string url = $"{BaseUrl}/api/Booking/GetAllBookingFilterized"; //the url http://localhost:5007/api/Booking/GetAllBookingFilterized
                    var response = await httpClient.PostAsJsonAsync(url, filterModel);
                    if (response.IsSuccessStatusCode)
                    {
                        var rawresponse = response.Content.ReadAsStringAsync().Result;
                        allbookings = JsonConvert.DeserializeObject<List<DisplayBooking>>(rawresponse);
                    }
                }
            }
            catch (Exception ex)
            {
                // log ex if needed
            }
            return allbookings;
        }

    }
}
