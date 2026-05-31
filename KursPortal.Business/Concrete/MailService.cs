using KursPortal.Business.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KursPortal.Business.Concrete
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();

            mail.IsBodyHtml = isBodyHtml;

            foreach (var to in tos)
                mail.To.Add(to);

            mail.Subject = subject;
            mail.Body = body;

            mail.From = new MailAddress(_configuration["Mail:Username"]);

            SmtpClient smtp = new();

            smtp.Host = _configuration["Mail:Host"];
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential(
                _configuration["Mail:Username"],
                _configuration["Mail:Password"]
            );

            await smtp.SendMailAsync(mail);
        }
        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();

            mail.AppendLine("Salam,<br>Hesabınızın şifrəsini sıfırlamaq üçün bir müraciət aldıq. Şifrənizi yeniləmək üçün aşağıdakı keçidə daxil olun:<br><strong><a target=\"_blank\" href=\"");

            mail.AppendLine(_configuration["ClientUrl"]);

            mail.AppendLine("/Account/UpdatePassword?userId=");

            mail.AppendLine(userId);

            mail.AppendLine("&token=");

            mail.AppendLine(Uri.EscapeDataString(resetToken));

            mail.AppendLine("\">Şifrənizi yeniləmək üçün keçid edin </a></strong><br>Əgər bu müraciəti siz etməmisinizsə, bu maili sadəcə görməzdən gələ bilərsiniz. Hesabınız tamamilə təhlükəsizdir.<br><br><br>Hörmətlə,<br>KursPortal Komandası");

            await SendMailAsync(to, "Şifrə sıfırlama istəyi", mail.ToString());
        }
    }
}
