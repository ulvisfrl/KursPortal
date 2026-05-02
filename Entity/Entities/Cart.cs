using KursPortal.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Entity.Entities
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
