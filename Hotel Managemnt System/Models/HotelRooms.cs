using System.ComponentModel.DataAnnotations;

namespace Hotel_Managemnt_System.Models
{
    public class HotelRooms
    {
        [Key]
        public int Row { get; set; }
        public Guid RoomId { get; set; }
        public string RoomNumber { get; set; }
        public Guid RoomTypeId { get; set; }
        public decimal PricePerNight { get; set; }
        public string Status { get; set; } // e.g. Available, Occupied, Maintenance
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
