using System.ComponentModel.DataAnnotations;

namespace Hotel_Managemnt_System.Models
{
    public class Bookings
    {

        [Key]
        public int Row { get; set; }
        // Core booking info
        public Guid BookingId { get; set; }
        public string BookingNumber { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public string GuestPhone { get; set; }

        // Room details
        public Guid RoomTypeId { get; set; }
        public Guid RoomNumber { get; set; }

        // Dates
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        // Guest details
        public int NumberOfGuests { get; set; }
        public string SpecialRequests { get; set; }

        // Payment & status
        public decimal TotalAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentStatus { get; set; } // e.g. Pending, Paid
        public string BookingStatus { get; set; } // e.g. Confirmed, Cancelled

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
