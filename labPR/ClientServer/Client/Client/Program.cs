using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Client
{
    static void Main()
    {
        const string serverIp = "127.0.0.1";
        const int serverPort = 9000;

        try
        {
            Socket clientSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Connecting to the server...");
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse(serverIp), serverPort));
            Console.WriteLine("Connected to the server.\n");

            Thread receiveThread = new(() => ReceiveMessages(clientSocket));
            receiveThread.Start();

            while (true)
            {
                Console.Write("Enter message (or type 'exit'): ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input)) continue;

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Disconnecting...");
                    break;
                }

                byte[] msgBytes = Encoding.UTF8.GetBytes(input);
                clientSocket.Send(msgBytes);
            }

            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            Console.WriteLine("Disconnected from server.");
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Socket error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Client error: {ex.Message}");
        }
    }

    private static void ReceiveMessages(Socket socket)
    {
        try
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                int received = socket.Receive(buffer);
                if (received == 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, received);
                Console.WriteLine($"\n[Server]: {message}\n");
            }
        }
        catch (SocketException)
        {
            Console.WriteLine("\nConnection closed by server.");
        }
        finally
        {
            if (socket.Connected)
                socket.Close();
        }
    }
}
