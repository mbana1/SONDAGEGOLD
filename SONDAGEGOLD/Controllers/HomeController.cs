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

        public ActionResult CreationSondage(string Question,List<string>Reponse, string CheckBox)
        {
            List<ReponseSondage> ListeDeQuestions = ClasseSondage.GetlisteDeReponses(Reponse);

          

            bool choix = Choixmultiple(CheckBox);
            ClasseSondage classeSondage = new ClasseSondage(Question, ListeDeQuestions,choix);

            
            int IDSondage= DataAcces.CreerUnSondage(classeSondage);
            ClasseSondage SondageCourant = DataAcces.RecupererSondageEnBDD(IDSondage);
            //on écrit new pius le nom du parametre du controller qu'on aimerais ateindre = à la valeur qu'on aimerais intégrer
            return RedirectToAction("SondageLiensDeGestion", new { id = IDSondage, Clef= SondageCourant.ClefDeSupression });

        }
        public ActionResult Vote(int id)
        {
         
            if (DataAcces.RecupererSondageEnBDD(id).EtatDuSondage == true)
            {

                return View(DataAcces.RecupererSondageEnBDD(id));



            }
            else
            { 
                    return RedirectToAction("VoteDesactiver", new { idsondageCourant = id });
            }
           

        }
        public ActionResult VoteDesactiver(int idsondageCourant)
        {

            return View();
        }
            public ActionResult EnregistrerVote(List<string>Idliste,int IDsondageCourant)
        {


            //DataAcces.InsertionResultaDuVote();
           List<string> liste=FonctionUtiles.GetlisteDeReponses(Idliste);

            DataAcces.InsertionResultatDuVote(IDsondageCourant, liste);



            return RedirectToAction("Resultat", new { idsondageCourant = IDsondageCourant });


          

        }
        public ActionResult teste()
        {
          


            return View();
        }
        public ActionResult Resultat(int idsondageCourant)
        {
           ClasseSondage sondage= DataAcces.RecupererSondageEnBDD(idsondageCourant);
           

            return View(sondage);
        }
        public ActionResult Suppression(int id,string Clef)
        {
            ClasseSondage classeSondage = DataAcces.RecupererSondageEnBDD(id);
            if (classeSondage.ClefDeSupression == Clef)
            {


                DataAcces.DesactivationSondage(id);
                return RedirectToAction("VoteDesactiver", new { idsondageCourant = id });

            }

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
