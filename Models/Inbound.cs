using System;
using System.ComponentModel.DataAnnotations;

namespace OfficeSuppliesManagement.Models
{
    public class Inbound
    {
        [Key]
        public int InboundId { get; set; }
        public string ItemCode { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PurchaseQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
