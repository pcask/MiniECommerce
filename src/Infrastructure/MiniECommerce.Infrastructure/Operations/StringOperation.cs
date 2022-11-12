using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Operations
{
    public class StringOperation
    {
        public static string CharacterRegulatory(string name)
            => name.ToLower().Trim().Replace(@"\", "")
                .Replace("!", "")
                .Replace("'", "")
                .Replace("^", "")
                .Replace("+", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("/", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("=", "")
                .Replace("?", "")
                .Replace("_", "")
                .Replace(" ", "-")
                .Replace("@", "")
                .Replace("€", "")
                .Replace("¨", "")
                .Replace("~", "")
                .Replace(",", "")
                .Replace(";", "")
                .Replace(":", "")
                .Replace(".", "-")
                .Replace("æ", "")
                .Replace("ß", "")
                .Replace("ö", "o")
                .Replace("ü", "u")
                .Replace("ı", "i")
                .Replace("ğ", "g")
                .Replace("â", "a")
                .Replace("î", "i")
                .Replace("ş", "s")
                .Replace("ç", "c")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");
    }
}
