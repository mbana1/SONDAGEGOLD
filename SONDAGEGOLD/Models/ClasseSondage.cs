﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SONDAGEGOLD.Models
{
    public class ClasseSondage
    {
        public int IdSondage { get; private set; }
        public string QuestionDuSondage { get; private set; }
        public List<ReponseSondage> ListeDeReponse { get; private set; }
        public string ClefDeSupression{ get; private set; }
        public bool QuestionChoixMultiples { get; private set; }

        public ClasseSondage(int idSondage, string questionDuSondage, List<ReponseSondage> listeDereponse, string clefDeSupression, bool questionChoixMultiples)
        {
            IdSondage = idSondage;
            QuestionDuSondage = questionDuSondage;
            ListeDeReponse = listeDereponse;
            ClefDeSupression = clefDeSupression;
            QuestionChoixMultiples = questionChoixMultiples;
        }

        public static List<ReponseSondage> GetlisteDeReponses(string Rep1, string Rep2, string Rep3, string Rep4, string Rep5)
        {
            List<ReponseSondage> ListeTriee = new List<ReponseSondage>();

            List<string> ListeNonTriee = new List<string>();
            ListeNonTriee.Add(Rep1);
            ListeNonTriee.Add(Rep2);
            ListeNonTriee.Add(Rep3);
            ListeNonTriee.Add(Rep4);
            ListeNonTriee.Add(Rep5);
            foreach(string RepValue in ListeNonTriee)
            {
                if (RepValue != "")
                {
                    ReponseSondage reponse = new ReponseSondage(0, RepValue);
                    ListeTriee.Add(reponse);
                }
            }
            return ListeTriee;
        }

        //public static List<string> GetlisteDeliens(int id,int num,string CleDeSupression)
        //{
        //    string LienDeVote = "http://localhost:" + num + "/Home/Vote?id=" + id;
        //    string LienDeResultat = "http://localhost:" + num + "/Home/Resultat?id=" + id;


        //    string LienDeSupression = "http://localhost:" + num + "/Home/Supression?id=" + id+"&"+"Clef="+ CleDeSupression;
        //    List<string> ListeDeliens = new List<string>();
        //    ListeDeliens.Add(LienDeVote);
        //    ListeDeliens.Add(LienDeResultat);
        //    ListeDeliens.Add(LienDeSupression);
        //    return ListeDeliens;
        //}

        public string LiendeVote()
        {
            string LienDeVote = "~/Home/Vote?id=" + this.IdSondage;
           
            return LienDeVote;
        }

        public string LienDeResultat()
        {
            string LienDeResultat = "~/Home/Resultat?id=" + this.IdSondage;

            return LienDeResultat;
        }

        public string LienDeSupression()
        {
            string LienDeSupression ="/Home/Supression?id=" + this.IdSondage + "&" + "Clef=" + this.ClefDeSupression.Trim();

            return LienDeSupression;
        }


    }










}