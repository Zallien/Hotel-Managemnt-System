namespace Hotel_Managemnt_System.ServiceModels
{
    public class DisplayBooking
    {
        public Guid BookingId { get; set; }
        public string RoomTypeName { get; set; }
        public string GuestName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int RoomNumber { get; set; }
    }
}
