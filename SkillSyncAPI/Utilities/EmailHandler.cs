using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace SkillSyncAPI.Utilities
{
    public record EmailDto(string To, string Subject, string Body);

    public interface IEmailHandler
    {
        Task SendEmailAsync(EmailDto emailDto);
    }

    public class EmailHandler : IEmailHandler
    {
        private readonly string? _smtpServer;
        private readonly int _smtpPort;
        private readonly string? _mailFrom;
        private readonly string? _mailUsername;
        private readonly string? _mailPassword;

        public EmailHandler(
            string smtpServer,
            int smtpPort,
            string mailUsername,
            string mailPassword,
            string mailFrom
        )
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _mailFrom = mailFrom;
            _mailUsername = mailUsername;
            _mailPassword = mailPassword;
        }

        public async Task SendEmailAsync(EmailDto emailDto)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_mailFrom!));
                email.To.Add(MailboxAddress.Parse(emailDto.To));
                email.Subject = emailDto.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = emailDto.Body };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.None);
                // Local SMTP doesn't need auth
                // await smtp.AuthenticateAsync(_mailUsername, _mailPassword!);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Log error but don't throw - email failure shouldn't crash the app
                Console.WriteLine($"Email send failed: {ex.Message}");
            }
        }
    }
}
