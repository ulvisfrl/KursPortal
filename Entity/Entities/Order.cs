using KursPortal.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Entity.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsPaid { get; set; } = false;
        public string Status { get; set; } = "Pending";
        public string? StripeSessionId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
