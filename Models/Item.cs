using System.ComponentModel.DataAnnotations;

namespace OfficeSuppliesManagement.Models
{
    public class Item
    {
        [Key]
        public string ItemCode { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Origin { get; set; }

        [Required]
        public string Specification { get; set; }

        [Required]
        public string Model { get; set; }

        public string ImagePath { get; set; } // 移除[Required]

        public int Quantity { get; set; }
    }
}
