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
        public ActionResult CreationSondage(string Question,List<string>Reponse, string CheckBox)
        {
            List<ReponseSondage> ListeDeQuestions = ClasseSondage.GetlisteDeReponses(Reponse);



            bool choix =FonctionUtiles.Choixmultiple(CheckBox);
            ClasseSondage classeSondage = new ClasseSondage(Question,ListeDeQuestions,choix);


            int IDSondage = DataAcces.CreerUnSondage(classeSondage);
            ClasseSondage SondageCourant = DataAcces.RecupererSondageEnBDD(IDSondage);
            //on écrit new pius le nom du parametre du controller qu'on aimerais ateindre = à la valeur qu'on aimerais intégrer
            return RedirectToAction("SondageLiensDeGestion", new { id = IDSondage, Clef = SondageCourant.ClefDeSupression });

        }
        public ActionResult SondageLiensDeGestion(int id, string Clef)
        {

            ClasseSondage classeSondage = DataAcces.RecupererSondageEnBDD(id);
            if (classeSondage.ClefDeSupression == Clef)
            {
                return View(classeSondage);

            }

            return View();
        }
        public ActionResult Vote(int id)
        {

            if (DataAcces.RecupererSondageEnBDD(id).EtatDuSondage == true)
            {
                if (FonctionUtiles.VerifieSiUtilisateurDejaVoter(Request.Cookies, id))
                {

                    return RedirectToAction("DejaVoter", new { idsondageCourant = id });


                }
                else
                {

                    return View(DataAcces.RecupererSondageEnBDD(id));

                    //Response.Cookies.Add(Operation.CreateUserCookie(IDQuestion));
                }
              
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

            Response.Cookies.Add(FonctionUtiles.CreationCookiesUtilisateur(IDsondageCourant));

            return RedirectToAction("Resultat", new { idsondageCourant = IDsondageCourant });


          

        }
        public ActionResult DejaVoter()
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


       
    }
   

}
