using System;
using System.ComponentModel.DataAnnotations;

namespace OfficeSuppliesManagement.Models
{
    public class Outbound
    {
        [Key]
        public int OutboundId { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public DateTime OutboundDate { get; set; }
        public string Status { get; set; }
    }
}
