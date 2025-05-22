using MailKit.Net.Imap;
using MailKit;
using MailKit.Net.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient
{
    class EmailReader
    {
        private readonly string _email = "alex.olaras11@gmail.com";
        private readonly string _password = "dgxt kwwh yecu rhck";

        public void ListEmailsPOP3()
        {
            using var client = new Pop3Client();
            client.Connect("pop.gmail.com", 995, true);
            client.Authenticate(_email, _password);

            Console.WriteLine($"Numar total de emailuri: {client.Count}");
            int numarMesaje = Math.Min(5, client.Count);

            for (int i = client.Count - numarMesaje; i < client.Count; i++)
            {
                var mesaj = client.GetMessage(i);
                Console.WriteLine($"\n--- Email {i + 1} ---");
                Console.WriteLine($"From: {mesaj.From}");
                Console.WriteLine($"To: {mesaj.Subject}");
            }

            client.Disconnect(true);
        }

        public void ListEmailsIMAP()
        {
            using var client = new ImapClient();
            client.Connect("imap.gmail.com", 993, true);
            client.Authenticate(_email, _password);

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            Console.WriteLine($"Number of emails in the inbox: {inbox.Count}");
            int numarMesaje = Math.Min(5, inbox.Count);

            for (int i = inbox.Count - numarMesaje; i < inbox.Count; i++)
            {
                var mesaj = inbox.GetMessage(i);
                Console.WriteLine($"\n--- Email {i + 1} ---");
                Console.WriteLine($"From: {mesaj.From}");
                Console.WriteLine($"Subject: {mesaj.Subject}");
                Console.WriteLine($"Date: {mesaj.Date.LocalDateTime}");
            }

            client.Disconnect(true);
        }
    }
}
