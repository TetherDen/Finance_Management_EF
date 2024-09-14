using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Management
{
    internal static class TextColor
    {
        public static string SuccessfulText(string text)  // Green color for successful messages
        {
            return $"\x1b[32m{text}\x1b[0m";
        }
        public static string ErrorText(string text)  // Red color for errors
        {
            return $"\x1b[91m{text}\x1b[0m";
        }
        public static string RequestText(string text) // Yellow color for input requests
        {
            return $"\x1b[33m{text}\x1b[0m";
        }

        public static string MenuText(string text) // menu Text
        {
            //return $"\x1b[36m{text}\x1b[0m";  //  Cyan color
            return $"\x1b[1m{text}\x1b[0m";    // Bold ?

        }
        public static string InfoText(string text) //  TODO: choose color
        {
            return $"\x1b[36m{text}\x1b[0m";
        }
    }
}
