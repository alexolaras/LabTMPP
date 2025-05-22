using MailKit.Net.Imap;
using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient
{
    class AtachmentHandler
    {
        private readonly string _email = "alex.olaras11@gmail.com";
        private readonly string _password = "dgxt kwwh yecu rhck";
        private readonly string _path = @"D:\PR\Downloads";

        public void DownloadAttachments()
        {
            using var client = new ImapClient();
            client.Connect("imap.gmail.com", 993, true);
            client.Authenticate(_email, _password);

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            for (int i = inbox.Count - 1; i >= 0; i--)
            {
                var mesaj = inbox.GetMessage(i);

                foreach (var attachment in mesaj.Attachments)
                {
                    if (attachment is MimePart part)
                    {
                        var fileName = part.FileName ?? "file.dat";
                        var path = Path.Combine(_path, fileName);
                        using var stream = File.Create(path);
                        part.Content.DecodeTo(stream);

                        Console.WriteLine($"Saved attachment: {fileName}");
                        client.Disconnect(true);
                        return;
                    }
                }
            }
            client.Disconnect(true);
            Console.WriteLine("No email with attachemnt was found.");
        }
    }
}
