namespace Hotel_Managemnt_System.ServiceModels
{
    public class HotelRoomsDisplay
    {
        public Guid RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public string Status { get; set; }
    }
}
