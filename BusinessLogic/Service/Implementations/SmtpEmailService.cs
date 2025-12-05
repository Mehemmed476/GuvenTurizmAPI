using System.Net;
using System.Net.Mail;
using BusinessLogic.ExternalService.Abstractions;
using BusinessLogic.Settings;
using Microsoft.Extensions.Options;

namespace BusinessLogic.ExternalService.Implementations;

public class SmtpEmailService : IEmailService
{
    private readonly EmailOptions _options;

    public SmtpEmailService(IOptions<EmailOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            var smtpClient = new SmtpClient(_options.Host)
            {
                Port = _options.Port,
                Credentials = new NetworkCredential(_options.FromEmail, _options.Password),
                EnableSsl = _options.EnableSsl,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_options.FromEmail, "Guven Turizm"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email göndərilmədi: {ex.Message}");
        }
    }
}