using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChromySharp.Plugin
{
    public static class StringHelper
    {
        public static string GetExtension(this string str)
        {
            var regex = new Regex("(\\.\\w{3,4})($|\\?)");
            var matches = regex.Matches(str).Cast<Match>();
            var enumerable = matches as Match[] ?? matches.ToArray();
            if (!enumerable.Any())
            {
                throw new InvalidConstraintException("No file extension in string");
            }

            var extension = enumerable.Last().Groups[1].Value;
            return extension;
        }
    }
}
