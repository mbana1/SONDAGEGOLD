using SONDAGEGOLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SONDAGEGOLD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SondageLiensDeGestion(int id,string Clef)
        {

           ClasseSondage classeSondage= DataAcces.RecupererSondageEnBDD(id);
           if(classeSondage.ClefDeSupression==Clef)
            {
                return View(classeSondage);

            }

            return View();
        }
        public ActionResult Sondage()
        {


            return View();
        }

        public ActionResult CreationSondage(string Question, string Rep1, string Rep2, string Rep3, string Rep4, string Rep5, string CheckBox)
        {
            List<ReponseSondage> ListeDeQuestions = ClasseSondage.GetlisteDeReponses(Rep1, Rep2, Rep3, Rep4, Rep5);

            string liens = "lien1";

            bool choix = Choixmultiple(CheckBox);
            ClasseSondage classeSondage = new ClasseSondage(0, Question, ListeDeQuestions, liens, choix,0);

            int IDSondage= DataAcces.CreerUnSondage(classeSondage);
            ClasseSondage SondageCourant = DataAcces.RecupererSondageEnBDD(IDSondage);
            //on écrit new pius le nom du parametre du controller qu'on aimerais ateindre = à la valeur qu'on aimerais intégrer
            return RedirectToAction("SondageLiensDeGestion", new { id = IDSondage, Clef= SondageCourant.ClefDeSupression });

        }
        public ActionResult Vote(int id)
        {
            return View(DataAcces.RecupererSondageEnBDD(id));
        }
         public ActionResult EnregistrerVote(string id0, string id1, string id2, string id3, string id4,int IDsondageCourant)
        {


            //DataAcces.InsertionResultaDuVote();
           List<string> liste= FonctionUtiles.GetlisteDeReponses(id0, id1, id2, id3, id4);

            DataAcces.InsertionResultatDuVote(IDsondageCourant, liste);



            return RedirectToAction("Resultat", new { idsondageCourant = IDsondageCourant });


          

        }
        //public ActionResult EnregistrementVote(int id,List<string> votes)
        //{

        //    return View();
        //}
        public ActionResult Resultat(int idsondageCourant)
        {
           ClasseSondage sondage= DataAcces.RecupererSondageEnBDD(idsondageCourant);
           

            return View(sondage);
        }
        public ActionResult Suppression(int id,string Clef)
        {
            return View();
        }


        public bool Choixmultiple(string choix)
        {
            bool Choix=false;
            if (choix == "on")
            {
                Choix = true;

            }
            
            return Choix;
        }
    }
   

}
