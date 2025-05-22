using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Udp
{
    public class Socket
    {
        private readonly System.Net.Sockets.Socket socket;
        private readonly byte[] buffer = new byte[1024];
        private bool isListening;

        public event Action<IPEndPoint, string>? MessageReceived;

        public Socket()
        {
            socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }

        public void Bind(int port)
        {
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
        }

        public void Bind(IPAddress ip, int port)
        {
            socket.Bind(new IPEndPoint(ip, port));
        }

        public void StartListening()
        {
            isListening = true;
            Thread t = new Thread(() =>
            {
                while (isListening)
                {
                    try
                    {
                        EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
                        int received = socket.ReceiveFrom(buffer, ref remote);
                        string msg = Encoding.UTF8.GetString(buffer, 0, received);

                        if (remote is IPEndPoint remoteEP)
                        {
                            MessageReceived?.Invoke(remoteEP, msg);
                        }
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine($"[Socket error] {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[General error] {ex.Message}");
                    }
                }
            })
            {
                IsBackground = true
            };
            t.Start();
        }

        public void StopListening()
        {
            isListening = false;
            socket.Close();
        }

        public void SendTo(string message, string ip, int port)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            socket.SendTo(data, endPoint);
        }

        public void SendToBroadcast(string message, int port)
        {
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            byte[] data = Encoding.UTF8.GetBytes(message);
            IPEndPoint broadcastEP = new IPEndPoint(IPAddress.Broadcast, port);
            socket.SendTo(data, broadcastEP);
        }
    }
}
