using Microsoft.AspNetCore.Http.Extensions;
using SendGrid;
using SendGrid.Helpers.Mail;
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
        public async Task ExecuteEmail(string subject, string plainTextContent, string fact, Uri imageUrl)
        {
            var client = new SendGridClient("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");
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
