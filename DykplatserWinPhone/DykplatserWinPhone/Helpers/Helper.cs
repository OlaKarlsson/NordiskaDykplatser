using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DykplatserWinPhone.Helpers
{
    public class Helper
    {
        public static string[] ExtractURLsFromString(string str)
        {
            string RegexPattern = @"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>";
            // Find matches.

            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(str, RegexPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            string[] MatchList = new string[matches.Count];

            // Report on each match.
            int c = 0;

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                MatchList[c] = match.Groups["url"].Value;
                c++;
            }

            return MatchList;

        }
    }

}
