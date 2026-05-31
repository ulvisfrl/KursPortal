using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Abstract
{
    public interface IAuthService
    {
        Task PasswordResetAsync(string email);
        Task VerifyResetTokenAsync(string resetToken, Guid userId);
    }
}
