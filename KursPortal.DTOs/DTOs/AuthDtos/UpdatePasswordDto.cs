using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.DTOs.DTOs.AuthDtos
{
    public class UpdatePasswordDto
    {
        public Guid UserId { get; set; }
        public string ResetToken { get; set; }
        public string Password { get; set; }
    }
}
