namespace Udp
{
    public class Handler
    {
        private readonly Socket udpSocket;

        public Handler(Socket socket)
        {
            udpSocket = socket;
        }

        public void SendUnicast(string message, string ip, int port)
        {
            try
            {
                udpSocket.SendTo(message, ip, port);
                Console.WriteLine($"[Unicast sent] to {ip}:{port} -> {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unicast send error: {ex.Message}");
            }
        }

        public void SendBroadcast(string message, int port)
        {
            try
            {
                udpSocket.SendToBroadcast(message, port);
                Console.WriteLine($"[Broadcast sent] -> {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Broadcast send error: {ex.Message}");
            }
        }
    }
}
