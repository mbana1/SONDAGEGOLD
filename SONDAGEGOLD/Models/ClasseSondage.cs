using System;
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
        public string ClefDeSupression { get; private set; }
        public bool QuestionChoixMultiples { get; private set; }
        public int NombreDeVotant { get; private set; }
        public bool EtatDuSondage { get; private set; }
        private const int IDsondagePartDefaut = 0;
        private const int NombreDeVotantParDefaut = 0;
        private const bool EtatDuSondageParDefaut = true;
        private const string ClefDeSupretionParDefaut="clesParDefaut";

        
        
        

        public ClasseSondage(int idSondage, string questionDuSondage, List<ReponseSondage> listeDeReponse, string clefDeSupression, bool questionChoixMultiples, int nombreDeVotant, bool etatDuSondage)
        {
            IdSondage = idSondage;
            QuestionDuSondage = questionDuSondage;
            ListeDeReponse = listeDeReponse;
            ClefDeSupression = clefDeSupression;
            QuestionChoixMultiples = questionChoixMultiples;
            NombreDeVotant = nombreDeVotant;
            EtatDuSondage = etatDuSondage;
        }

        public ClasseSondage(string questionDuSondage, List<ReponseSondage> listeDeReponse, bool questionChoixMultiples):this(IDsondagePartDefaut, questionDuSondage,  listeDeReponse, ClefDeSupretionParDefaut, questionChoixMultiples, NombreDeVotantParDefaut, EtatDuSondageParDefaut)
        {
           
        }

        public static List<ReponseSondage> GetlisteDeReponses(List<string> ListeNonTriee)
        {
            List<ReponseSondage> ListeTriee = new List<ReponseSondage>();
            foreach (string RepValue in ListeNonTriee)
            {
                if (RepValue != "")
                {
                    ReponseSondage reponse = new ReponseSondage(RepValue);
                    ListeTriee.Add(reponse);
                }
            }
            return ListeTriee;
        }

        public string LiendeVote()
        {
            string LienDeVote = "http://localhost:50700/Home/Vote?id=" + this.IdSondage;

            return LienDeVote;
        }

        public string LienDeResultat()
        {
            string LienDeResultat = "http://localhost:50700/Home/Resultat?idsondageCourant=" + this.IdSondage;

            return LienDeResultat;
        }

        public string LienDeSupression()
        {
            string LienDeSupression = "http://localhost:50700/Home/Suppression?id=" + this.IdSondage + "&" + "Clef=" + this.ClefDeSupression.Trim();

            return LienDeSupression;
        }



        public int NombreTotalDeVote()
        {
            int TotalVote=0;
            foreach (ReponseSondage vote in this.ListeDeReponse)
            {
                TotalVote = TotalVote+ vote.NombreDeVote;




            }


                return TotalVote;
        }
       




    }

}