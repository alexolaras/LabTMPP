using System;
using System.Net;
using DnsClient;


namespace Dns
{
    public class DnsResolver
    {
        private LookupClient _client = new LookupClient();

        public async Task ResolveAsync(string input)
        {
            if (IPAddress.TryParse(input, out var ip))
            {
                try
                {
                    var reverseResult = await _client.QueryReverseAsync(ip);
                    if (reverseResult.Answers.Count == 0)
                    {
                        Console.WriteLine("No domain found for the given IP.");
                        return;
                    }

                    foreach (var ptr in reverseResult.Answers)
                        Console.WriteLine($"Domain: {ptr}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error resolving domain from IP: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    var result = await _client.QueryAsync(input, QueryType.A);
                    if (result.Answers.Count == 0)
                    {
                        Console.WriteLine("No IP address found for the domain.");
                        return;
                    }

                    foreach (var record in result.Answers)
                        Console.WriteLine($"IP: {record}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error resolving IP from domain: {ex.Message}");
                }
            }
        }

        public void ChangeDns(string dnsIp)
        {
            if (IPAddress.TryParse(dnsIp, out var dns))
            {
                _client = new LookupClient(dns);
                Console.WriteLine($"DNS server changed to: {dns}");
            }
            else
            {
                Console.WriteLine("Invalid DNS IP address.");
            }
        }
    }
}

