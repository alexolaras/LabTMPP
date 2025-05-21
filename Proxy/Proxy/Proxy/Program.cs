using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proxy
{
    public class Program
    {
        static void Main(string[] args)
        {
            DocumentViewerProxy viewerProxy = new DocumentViewerProxy("Confidential_Report.pdf");

            viewerProxy.View();

            Thread.Sleep(3000);

            viewerProxy.Close();
        }
    }
}
