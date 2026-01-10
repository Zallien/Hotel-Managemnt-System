using System.ComponentModel.DataAnnotations;

namespace Hotel_Managemnt_System.Models
{
    public class RoomType
    {

        [Key]
        public int Row { get; set; }
        public Guid RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public decimal RoomTypePrice { get; set; }
    }
}
