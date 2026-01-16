namespace Hotel_Managemnt_System.ServiceModels
{
    public class AddHotelRoom
    {
        public Guid? RoomId { get; set; }

        public string RoomNumber { get; set; }
        public Guid RoomTypeId { get; set; }
        public decimal PricePerNight { get; set; }
        public string Status { get; set; } // e.g. Available, Occupied, Maintenance
    }
}
