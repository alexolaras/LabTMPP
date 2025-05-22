using System.Net;
using System.Net.Sockets;
using System.Text;


class Server
{
    private static readonly List<Socket> clients = new();
    private static readonly object locker = new();

    static void Main()
    {
        const int port = 9000;
        IPAddress ip = IPAddress.Parse("127.0.0.1");

        Socket serverSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(new IPEndPoint(ip, port));
        serverSocket.Listen(10);

        Console.WriteLine($"Server started on {ip}:{port}. Waiting for clients...");

        try
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();

                lock (locker)
                {
                    clients.Add(clientSocket);
                }

                Console.WriteLine($"Client connected: {clientSocket.RemoteEndPoint}");
                Thread clientThread = new(() => HandleClient(clientSocket));
                clientThread.Start();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}");
        }
        finally
        {
            serverSocket.Close();
        }
    }

    private static void HandleClient(Socket clientSocket)
    {
        string clientId = clientSocket.RemoteEndPoint.ToString();
        byte[] buffer = new byte[1024];

        try
        {
            while (true)
            {
                int received = clientSocket.Receive(buffer);
                if (received == 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, received);
                Console.WriteLine($"Received from {clientId}: {message}");

                string response = $"[Server] Echo from {clientId}: {message}";
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                clientSocket.Send(responseBytes);

            }
        }
        catch (SocketException)
        {
            Console.WriteLine($"Connection lost with client: {clientId}");
        }
        finally
        {
            lock (locker)
            {
                clients.Remove(clientSocket);
            }

            if (clientSocket.Connected)
                clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();

            Console.WriteLine($"Client disconnected: {clientId}");
        }
    }
}
