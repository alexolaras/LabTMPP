using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public class DocumentViewer : IDocumentViewer
    {
        public string FileName { get; set; }
        public string ViewStatus { get; set; }

        public DocumentViewer(string fileName)
        {
            FileName = fileName;
            ViewStatus = "Closed";
        }

        public void View()
        {
            Console.WriteLine($"Opening document: {FileName}...");
            ViewStatus = "Viewing";
        }

        public void Close()
        {
            Console.WriteLine($"Closing document: {FileName}...");
            ViewStatus = "Closed";
        }
    }
}