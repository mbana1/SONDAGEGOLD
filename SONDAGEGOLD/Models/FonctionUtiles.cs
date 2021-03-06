﻿using System;
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
        public static List<string> GetlisteDeReponses(List<string> ListeNonTriee)
        {
            List<string> ListeTriee = new List<string>();

            foreach (string RepValue in ListeNonTriee)
            {
               if (RepValue !=null)
               {
                 
                  ListeTriee.Add(RepValue);
                }
            }
            return ListeTriee;
        }
        public static HttpCookie CreationCookiesUtilisateur(int id)
        {

            HttpCookie httpCookie = new HttpCookie("UserCookies" + id);
            httpCookie.Value = "";
            httpCookie.Expires = DateTime.MaxValue;
            return httpCookie;
        }
        public static bool VerifieSiUtilisateurDejaVoter(HttpCookieCollection cookies, int idQuestion)
        {
            return cookies["UserCookies" + idQuestion] != null;
        }

    
    public static bool Choixmultiple(string choix)
        {
            bool Choix = false;
            if (choix == "on")
            {
                Choix = true;

            }

            return Choix;
        }
    }
}
