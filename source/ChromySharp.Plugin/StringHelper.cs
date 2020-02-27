using System.Data;
using System.Linq;

namespace ChromySharp.Plugin
{
    public static class StringHelper
    {
        public static string GetExtension(this string str)
        {
            if (!str.Contains("."))
            {
                throw new InvalidConstraintException("No '.' char");
            }

            var extension = str.Split('.').Last();
            return extension;
        }
    }
}
