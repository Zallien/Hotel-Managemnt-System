using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Hotel_Managemnt_System.ServiceModels
{
    public class AddBookingModel
    {

        public Guid? BookingId { get; set; }

        // Guest Details
        [Required(ErrorMessage = "Guest Name is Required")]
        public string GuestName { get; set; }
        [Required(ErrorMessage = "Guest Email Address is Required"), EmailAddress]
        public string GuestEmail { get; set; }
        [Required(ErrorMessage = "Guest Phone Number is Required")]
        public string GuestPhone { get; set; }

        // Room Details
        [Required(ErrorMessage = "Room Type is Required")]
        public Guid RoomType { get; set; }
        [Required(ErrorMessage = "Room Number is Required")]
        public Guid RoomNumber { get; set; }

        // Booking Details
        [Required(ErrorMessage = "Check In Date is Required"), Range(typeof(DateTime), "2024-01-01", "2099-12-31")]
        public DateTime CheckIn { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Check Out Date is Required")]
        public DateTime CheckOut { get; set; } = DateTime.Now.AddDays(1);

        // Payment Details
        [Required(ErrorMessage = "Total Amount is Required")]
        public decimal TotalAmount { get; set; }
        [Required(ErrorMessage = "Payment Status is Required")]
        public string PaymentStatus { get; set; }
        [Required(ErrorMessage = "Booking Status is Required")]
        public string BookingStatus { get; set; }
        [Required(ErrorMessage = "Payment is Required")]
        public decimal Payment { get; set; }

        //Guest
        [Required(ErrorMessage = "Number of Guest is Required")]
        public int NumberOfGuests { get; set; } = 1;
        public string SpecialRequests { get; set; }



    }
}
