using System.Text.RegularExpressions;

namespace Ntp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the time zone (e.g. GMT+2 or GMT-5). Type 'exit' to quit.");

            while (true)
            {
                Console.Write("Time zone: ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Empty input. Please try again.");
                    continue;
                }

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (!TryParseTimeZoneOffset(input, out int offset))
                {
                    Console.WriteLine("Invalid format. Example valid input: GMT+3 or GMT-10");
                    continue;
                }

                DateTime utcTime;
                try
                {
                    utcTime = NtpClient.GetNetworkTime();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting time from server: {ex.Message}");
                    continue;
                }

                DateTime localTime = utcTime.AddHours(offset);
                Console.WriteLine($"The exact time in {input} is: {localTime:HH:mm:ss - dd.MM.yyyy}\n");
            }
        }

        private static bool TryParseTimeZoneOffset(string input, out int offset)
        {
            offset = 0;
            string pattern = @"^GMT([+-])(\d{1,2})$";
            var match = Regex.Match(input, pattern);

            if (!match.Success)
                return false;

            string sign = match.Groups[1].Value;
            if (!int.TryParse(match.Groups[2].Value, out offset))
                return false;

            if (offset < 0 || offset > 11)
                return false;

            if (sign == "-")
                offset = -offset;

            return true;
        }
    }
}
