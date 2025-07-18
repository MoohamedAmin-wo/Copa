﻿using System.Net;
using System.Net.Mail;

namespace Cupa.Application.Services.MaillingService;
public class EmailSender(IOptions<Mail> mailSettings) : IEmailSender
{
    private readonly Mail _mailSettings = mailSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        MailMessage message = new()
        {
            From = new MailAddress(_mailSettings.Email, _mailSettings.DisplayName),
            Body = htmlMessage,
            Subject = subject,
            IsBodyHtml = true
        };
        message.To.Add(email);
        SmtpClient smtpClient = new(_mailSettings.Host)
        {
            Port = _mailSettings.Port,
            Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(message);
        smtpClient.Dispose();
    }
}
