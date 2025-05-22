using System.Net.Sockets;
using System.Net;


namespace Ntp
{
    public static class NtpClient
    {
        public static DateTime GetNetworkTime()
        {
            const string ntpServer = "pool.ntp.org";
            var ntpData = new byte[48];
            ntpData[0] = 0x1B;

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;
            var ipEndPoint = new IPEndPoint(addresses[0], 123);

            using (var socket = new UdpClient())
            {
                socket.Connect(ipEndPoint);
                socket.Send(ntpData, ntpData.Length);

                var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                var asyncResult = socket.BeginReceive(null, null);

                if (!asyncResult.AsyncWaitHandle.WaitOne(3000))
                {
                    throw new Exception("Timeout waiting for NTP response");
                }

                var response = socket.EndReceive(asyncResult, ref remoteIpEndPoint);

                const byte serverReplyTime = 40;
                ulong intPart = BitConverter.ToUInt32(response, serverReplyTime);
                ulong fractPart = BitConverter.ToUInt32(response, serverReplyTime + 4);

                intPart = SwapEndianness(intPart);
                fractPart = SwapEndianness(fractPart);

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

                return networkDateTime.ToUniversalTime();
            }
        }

        private static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                          ((x & 0x0000ff00) << 8) +
                          ((x & 0x00ff0000) >> 8) +
                          ((x & 0xff000000) >> 24));
        }
    }
}
