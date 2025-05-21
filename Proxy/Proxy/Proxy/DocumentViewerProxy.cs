using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public class DocumentViewerProxy : IDocumentViewer
    {
        private DocumentViewer _documentViewer;
        private Dictionary<string, string> _validUsers = new Dictionary<string, string>
        {
            { "editor1", "docpass1" },
            { "editor2", "docpass2" }
        };

        public DocumentViewerProxy(string fileName)
        {
            _documentViewer = new DocumentViewer(fileName);
        }

        private bool Authenticate(string username, string password)
        {
            return _validUsers.ContainsKey(username) && _validUsers[username] == password;
        }

        public void View()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            if (Authenticate(username, password))
            {
                _documentViewer.View();
                Console.WriteLine("Access granted. Document is now open.");
            }
            else
            {
                Console.WriteLine("Access denied. Invalid credentials.");
            }
        }

        public void Close()
        {
            _documentViewer.Close();
            Console.WriteLine("Document has been closed.");
        }
    }
}
