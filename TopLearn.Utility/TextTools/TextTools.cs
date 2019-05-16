using System;
using System.Collections.Generic;
using System.Text;

namespace TopLearn.Utility.TextTools
{
    public static class TextTools
    {
        public static string FixEmail(this string email)
        {
            return email.ToLower().Trim();
        }

        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
