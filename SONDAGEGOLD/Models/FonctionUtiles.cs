using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SONDAGEGOLD.Models
{
    public class FonctionUtiles
    {
        public static string GeneratePassword(int Length)
        {
            return GeneratePassword(Length, "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
        }
        static string GeneratePassword(int Length, bool CaseSensitive)
        {
            if (CaseSensitive)
                return GeneratePassword(Length);
            else
                return GeneratePassword(Length, "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray());
        }
        static string GeneratePassword(int Length, char[] Chars)
        {
            string Password = string.Empty;
            System.Random rnd = new System.Random();
            for (int i = 0; i < Length; i++)
                Password += Chars[rnd.Next(Chars.Length)];
            return Password;
        }

    }
}
