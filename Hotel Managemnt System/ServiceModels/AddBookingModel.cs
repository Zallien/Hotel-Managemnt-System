namespace Hotel_Managemnt_System.ServiceModels
{
    public class AddBookingModel
    {
        // Guest Details
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public string GuestPhone { get; set; }

        // Room Details
        public Guid RoomType { get; set; }
        public int RoomNumber { get; set; }

        // Booking Details
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        // Payment Details
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string BookingStatus { get; set; }



    }
}
