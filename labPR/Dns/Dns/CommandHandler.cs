namespace Dns
{
    public class CommandHandler
    {
        private DnsResolver _resolver = new DnsResolver();

        public async Task ResolveDomainAsync(string domain)
        {
            await _resolver.ResolveAsync(domain);
        }

        public async Task ResolveIpAsync(string ip)
        {
            await _resolver.ResolveAsync(ip);
        }

        public void ChangeDnsServer(string ip)
        {
            _resolver.ChangeDns(ip);
        }
    }
}
