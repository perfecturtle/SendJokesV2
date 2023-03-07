using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendJokesV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendJokesV2.Services
{
    public interface IEmailService
    {
        Task ExecuteEmail(string subject, string plainTextContent, string fact, Uri imageUrl);
    }
    public class EmailService : IEmailService
    {
        private readonly IOptions<AppSettings> _appSettings;

        public EmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }
        public async Task ExecuteEmail(string subject, string plainTextContent, string fact, Uri imageUrl)
        {
            var client = new SendGridClient(_appSettings.Value.SendGridAPIKey);
            var from = new EmailAddress("perfecturtle@hotmail.com", "haystack");

            List<EmailAddress> recipients = new List<EmailAddress>{
                new EmailAddress("haoyisar@gmail.com", "wooheh") ,
                new EmailAddress("aimeee_e@hotmail.com", "Dumpling Princess") };
            var JokesContent = $"<strong>{plainTextContent}</strong>";

            var htmlContent = $"<html>" +
                $"<body>" +
                $"<h1>Hello! Here is the joke section!</h1>" +
                $"<p>{JokesContent}</p>" +
                $"<h1>Here is the random facts section!</h1>" +
                $"<p>{fact}</p>" +
                $"<h1>This is the comic section!</h1>" +
                $"<img src='{imageUrl}' alt='Example image'></body></html>";

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, recipients, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
