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
        public static List<string> GetlisteDeReponses(string Rep1, string Rep2, string Rep3, string Rep4, string Rep5)
        {
            List<string> ListeTriee = new List<string>();

            List<string> ListeNonTriee = new List<string>();
            ListeNonTriee.Add(Rep1);
            ListeNonTriee.Add(Rep2);
            ListeNonTriee.Add(Rep3);
            ListeNonTriee.Add(Rep4);
            ListeNonTriee.Add(Rep5);
            foreach (string RepValue in ListeNonTriee)
            {
               if (RepValue !=null)
               {
                 
                  ListeTriee.Add(RepValue);
                }
            }
            return ListeTriee;
        }

    }
}
