using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalScrumBoard.Helpers
{
    public class SHA1
    {
        public static string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }

    public class DateTimeHelpers
    {
        public static IEnumerable<DateTime> GetAllDatesBetween(DateTime startingDate, DateTime endingDate)
        {
            if (endingDate < startingDate)
            {
                throw new ArgumentException("endingDate should be after startingDate");
            }
            var ts = endingDate - startingDate;
            for (int i = 0; i < ts.TotalDays; i++)
            {
                yield return startingDate.AddDays(i);
            }
        }
    }
}