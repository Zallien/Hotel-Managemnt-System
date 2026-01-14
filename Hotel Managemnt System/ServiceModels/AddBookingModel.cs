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
        public int RoomNumber { get; set; }

        // Booking Details
        [Required(ErrorMessage = "Check In Date is Required")]
        public DateTime CheckIn { get; set; }
        [Required(ErrorMessage = "Check Out Date is Required")]
        public DateTime CheckOut { get; set; }

        // Payment Details
        [Required(ErrorMessage = "Total Amount is Required")]
        public decimal TotalAmount { get; set; }
        [Required(ErrorMessage = "Payment Status is Required")]
        public string PaymentStatus { get; set; }
        [Required(ErrorMessage = "Booking Status is Required")]
        public string BookingStatus { get; set; }

        //Guest
        [Required(ErrorMessage = "Number of Guest is Required")]
        public int NumberOfGuests { get; set; }
        public string SpecialRequests { get; set; }



    }
}
