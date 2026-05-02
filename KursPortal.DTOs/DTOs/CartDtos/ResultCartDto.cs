using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DTOs.DTOs.CartDtos
{
    public class ResultCartDto
    {
        public Guid UserId { get; set; }
        public ICollection<CartItemDto> CartItems { get; set; }
    }
}
