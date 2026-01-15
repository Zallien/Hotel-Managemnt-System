using Hotel_Managemnt_System.Models;
using Hotel_Managemnt_System.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Managemnt_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HotelRoomController : Controller
    {

        private readonly Contextdb _db;
        //Constructor
        public HotelRoomController(Contextdb mdb)
        {
            _db = mdb;
        }

        [HttpGet("GetAllRooms")]
        public async Task<List<HotelRoomsDisplay>> GetAllRooms()
        {
            List<HotelRoomsDisplay> allrooms = new List<HotelRoomsDisplay>();
            try
            {
                allrooms = await (from a in _db.HotelRooms
                                  join b in _db.RoomTypes
                                  on a.RoomTypeId equals b.RoomTypeId
                                  select new HotelRoomsDisplay
                                  {
                                      RoomId = a.RoomId,
                                      RoomNumber = a.RoomNumber,
                                      RoomType = b.RoomTypeName,
                                      PricePerNight = a.PricePerNight,
                                      Status = a.Status
                                  }).ToListAsync();
            }
            catch (Exception ex)
            {
                // log ex if needed
            }
            return allrooms;


        }

        [HttpPost("AddNewRoom")]
        public async Task<bool> AddNewRoom([FromBody] AddHotelRoom newroom)
        {
            bool isAdded = false;
            try
            {
                bool roomExists = await _db.HotelRooms.AnyAsync(r => r.RoomNumber == newroom.RoomNumber);
                if (roomExists)
                {
                    isAdded = false;
                    return isAdded;
                }
                HotelRooms room = new HotelRooms
                {
                    RoomId = Guid.NewGuid(),
                    RoomNumber = newroom.RoomNumber,
                    RoomTypeId = newroom.RoomTypeId,
                    PricePerNight = newroom.PricePerNight,
                    Status = newroom.Status,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _db.HotelRooms.Add(room);
                await _db.SaveChangesAsync();
                isAdded = true;
            }
            catch (Exception ex)
            {
                // log ex if needed
            }
            return isAdded;
        }

        [HttpPost("GetRoomById")]
        public async Task<HotelRoomsDisplay> GetRoomById([FromBody] Guid roomId)
        {
            HotelRoomsDisplay roomDetails = null;
            try
            {
                roomDetails = await (from a in _db.HotelRooms
                                     join b in _db.RoomTypes
                                     on a.RoomTypeId equals b.RoomTypeId
                                     where a.RoomId == roomId
                                     select new HotelRoomsDisplay
                                     {
                                         RoomId = a.RoomId,
                                         RoomNumber = a.RoomNumber,
                                         RoomType = b.RoomTypeName,
                                         PricePerNight = a.PricePerNight,
                                         Status = a.Status
                                     }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log ex if needed
            }
            return roomDetails;

        }

        [HttpPost("UpdateHotelRoom")]
        public async Task<bool> UpdateHotelRoom([FromBody] AddHotelRoom updatedRoom)
        {
            bool isUpdated = false;
            try
            {
                if (updatedRoom.RoomId == null || updatedRoom.RoomId == Guid.Empty)
                {
                    isUpdated = false;
                    return isUpdated;
                }

                var existingRoom = await _db.HotelRooms.FirstOrDefaultAsync(r => r.RoomId == updatedRoom.RoomId);
                if (existingRoom != null)
                {
                    existingRoom.RoomNumber = updatedRoom.RoomNumber;
                    existingRoom.RoomTypeId = updatedRoom.RoomTypeId;
                    existingRoom.PricePerNight = updatedRoom.PricePerNight;
                    existingRoom.Status = updatedRoom.Status;
                    existingRoom.UpdatedAt = DateTime.Now;
                    await _db.SaveChangesAsync();
                    isUpdated = true;
                }
            }
            catch (Exception ex)
            {
                // log ex if needed
            }
            return isUpdated;
        }
    }
}
