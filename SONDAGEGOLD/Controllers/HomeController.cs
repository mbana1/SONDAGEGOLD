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
        public ActionResult Sondage()
        {
            return View();
        }

        public ActionResult CreationSondage(string Question, string Rep1, string Rep2, string Rep3, string Rep4, string Rep5, string CheckBox)
        {
            List<string> ListeDeQuestions = ClasseSondage.GetlisteDeReponses(Rep1, Rep2, Rep3, Rep4, Rep5);
            
            List<string> liens = new List<string> { "lien1", "lien2", "lien3" };

            bool choix = Choixmultiple(CheckBox);
            ClasseSondage classeSondage = new ClasseSondage(0, Question, ListeDeQuestions, liens, choix);
            DataAcces.CreerUnSondage(classeSondage);

            return RedirectToAction("Sondage");

        }
        public ActionResult Vote(int id)
        {
            


            return View(DataAcces.AfficheSondageDansLaVueVote(id));
        }
        public ActionResult Resultat()
        {
            return View();
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
