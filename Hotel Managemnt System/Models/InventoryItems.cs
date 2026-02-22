using System.ComponentModel.DataAnnotations;

namespace Hotel_Managemnt_System.Models
{
    public class InventoryItems
    {
        [Key]
        public int Row { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemNumber { get; set; }
        public int CurrentStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
