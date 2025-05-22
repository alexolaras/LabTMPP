namespace Dns
{
    class Program
{
    static async Task Main(string[] args)
    {
        var handler = new CommandHandler();

        while (true)
        {
            Console.WriteLine("1. Resolve domain to IP");
            Console.WriteLine("2. Resolve IP to domain");
            Console.WriteLine("3. Change DNS server");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            var option = Console.ReadLine();
            Console.WriteLine();

            switch (option)
            {
                case "1":
                    Console.Write("Enter domain: ");
                    var domain = Console.ReadLine();
                    await handler.ResolveDomainAsync(domain);
                    break;

                case "2":
                    Console.Write("Enter IP address: ");
                    var ip = Console.ReadLine();
                    await handler.ResolveIpAsync(ip);
                    break;

                case "3":
                    Console.Write("Enter new DNS IP: ");
                    var dns = Console.ReadLine();
                    handler.ChangeDnsServer(dns);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
}
