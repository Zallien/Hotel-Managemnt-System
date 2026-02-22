using Hotel_Managemnt_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Managemnt_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class InventoryController : Controller
    {
        private readonly Contextdb _db;

        public InventoryController(Contextdb db)
        {
            _db = db;
        }

        //Get all Inventory item
        [HttpGet("GetAllItems")]
        public async Task<List<InventoryItems>> GetaLLItems()
        {
            List<InventoryItems> items = new List<InventoryItems>();
            try
            {
                
                items = await _db.InventoryItems.ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return items;
        }
    }
}
