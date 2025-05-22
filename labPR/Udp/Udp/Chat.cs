namespace Udp
{
    public class Chat
    {
        private readonly Socket udpSocket;
        private readonly Handler messageHandler;
        private const int ListenPort = 5000;
        private const int BroadcastPort = 5000;

        public Chat()
        {
            udpSocket = new Socket();
            messageHandler = new Handler(udpSocket);
        }

        public void Start()
        {
            udpSocket.Bind(ListenPort);
            udpSocket.MessageReceived += (remote, msg) =>
            {
                Console.WriteLine($"[Received from {remote}] {msg}");
            };
            udpSocket.StartListening();

            Console.WriteLine("Chat client started. Type 'exit' to quit.");
            Console.WriteLine("To broadcast, type: /b YourMessageHere");
            Console.WriteLine("To send direct: /u IP PORT Message");

            while (true)
            {
                string? input = Console.ReadLine();
                if (input == null) continue;

                if (input.Trim().ToLower() == "exit")
                    break;

                if (input.StartsWith("/b "))
                {
                    string msg = input[3..];
                    messageHandler.SendBroadcast(msg, BroadcastPort);
                }
                else if (input.StartsWith("/u "))
                {
                    var parts = input.Split(' ', 4);
                    if (parts.Length == 4 &&
                        int.TryParse(parts[2], out int port))
                    {
                        messageHandler.SendUnicast(parts[3], parts[1], port);
                    }
                    else
                    {
                        Console.WriteLine("Invalid unicast command. Format: /u IP PORT MESSAGE");
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command. Use /b or /u.");
                }
            }

            udpSocket.StopListening();
        }
    }
}
