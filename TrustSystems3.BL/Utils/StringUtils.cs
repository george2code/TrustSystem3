using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TrustSystems3.BL.Utils
{
    public static class StringUtils
    {
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string Join<T>(this IEnumerable<T> target, Func<T, object> valueSelector)
        {
            StringBuilder result = new StringBuilder();
            foreach (T item in target)
            {
                result.Append(valueSelector(item).ToString());
                result.Append(",");
            }
            result.Length--;
            //remove the trailing comma    
            return result.ToString();
        }

        public static List<String> ExtractEmails(string text)
        {
            List<String> list = new List<string>();

            //instantiate with this pattern 
            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
                RegexOptions.IgnoreCase);
            //find items that matches with our pattern
            MatchCollection emailMatches = emailRegex.Matches(text);

            StringBuilder sb = new StringBuilder();

            foreach (Match emailMatch in emailMatches)
            {
                list.Add(emailMatch.Value);
            }

            return list;
        }

        public static string TruncateWithPreservation(string s, int len)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            string[] parts = s.Split(' ');
            StringBuilder sb = new StringBuilder();

            foreach (string part in parts)
            {
                if (sb.Length + part.Length > len)
                    break;

                sb.Append(' ');
                sb.Append(part);
            }

            sb.Append("...");

            return sb.ToString();
        }
    }
}