namespace Hotel_Managemnt_System.Services
{
    public class InventoryManagementServices : iInventory
    {
        private readonly HttpClient _httpClient;

        public InventoryManagementServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Get All Item


    }
}
