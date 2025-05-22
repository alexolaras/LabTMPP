using System;

namespace EmailClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("1 - Email with a text");
                Console.WriteLine("2 - Email with an attachent");
                Console.WriteLine("3 - Display email (POP3)");
                Console.WriteLine("4 - Display email (IMAP)");
                Console.WriteLine("5 - Download attachemnt from the email");
                Console.WriteLine("0 - Exit");
                string optiune = Console.ReadLine();

                EmailService emailService = new EmailService();
                EmailReader emailReader = new EmailReader();
                AtachmentHandler atachmentHandler = new AtachmentHandler();

                switch (optiune)
                {
                    case "1":
                        emailService.SendTextEmail();
                        break;

                    case "2":
                        emailService.SendEmailWithAttachment();
                        break;

                    case "3":
                        emailReader.ListEmailsPOP3();
                        break;

                    case "4":
                        emailReader.ListEmailsIMAP();
                        break;

                    case "5":
                        atachmentHandler.DownloadAttachments();
                        break;

                    case "0":
                        Console.WriteLine("Exit.");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
