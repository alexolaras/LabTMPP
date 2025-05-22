using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;


namespace EmailClient
{
    class EmailService
    {
        private readonly string _email = "alex.olaras11@gmail.com";
        private readonly string _password = "dgxt kwwh yecu rhck";

        private void SendEmail(MimeEntity body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Alex", _email));
            message.To.Add(new MailboxAddress("Alex", _email));
            message.Subject = "Email test - lab 5 pr";
            message.ReplyTo.Add(new MailboxAddress("Reply here", _email));
            message.Body = body;

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate(_email, _password);
            client.Send(message);
            client.Disconnect(true);
            Console.WriteLine("Email sent successfully.\n");
        }
        public void SendTextEmail()
        {
            var text = new TextPart("plain")
            {
                Text = "Email sent from the console app."
            };

            SendEmail(text);
        }

        public void SendEmailWithAttachment()
        {
            var builder = new BodyBuilder
            {
                TextBody = "Email with attachement sent from the console app."
            };
            builder.Attachments.Add(@"D:\PR\DesignPatterns.txt");

            SendEmail(builder.ToMessageBody());
        }
    }
}
