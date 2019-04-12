﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SONDAGEGOLD.Models
{
    public class ReponseSondage
    {
        public int IDreponse { get; private set; }
        public string Reponse { get; private set; }
        public int NombreDeVote { get; private set; }
       

        public ReponseSondage(int idreponse, string reponse, int nombreDeVote)
        {
            IDreponse = idreponse;
            Reponse = reponse;
            NombreDeVote = nombreDeVote;
        }

        public string PourcentageDeVote(int NombreDeVotant)
        {
            double result = (Convert.ToDouble(this.NombreDeVote) / Convert.ToDouble(NombreDeVotant));
           
            string Pourcentage = $"{result:P}";
            return Pourcentage;
        }
       
    }
}