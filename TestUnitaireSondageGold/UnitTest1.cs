using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SONDAGEGOLD.Models;

namespace TestUnitaireSondageGold
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TesteChoixmultipleTransformeOnEnTrue()
        {
            string teste="on";

            Assert.IsTrue(true == FonctionUtiles.Choixmultiple(teste),"la fonction Choixmultiple Ne transforme pas le ON en True");

        }

        [TestMethod]
        public void TesteChoixmultipleTransformeNullEnFalse()
        {
            string teste =null;

            Assert.IsTrue(false == FonctionUtiles.Choixmultiple(teste), "la fonction Choixmultiple Ne transforme pas le Null en false");

        }

        [TestMethod]
        public void TesteLesValeursParDefautDeLaClaseReponseSondage()
        {

            ReponseSondage Reponse = new ReponseSondage("Football");
            Assert.IsTrue(Reponse.IDreponse == 0 && Reponse.NombreDeVote ==0 && Reponse.Reponse=="Football");
        }
        //[TestMethod]
        //public void TestePourcentageClasseReponseSondage()
        //{

        //    ReponseSondage Reponse = new ReponseSondage("Football");
        //    Assert.IsTrue(Reponse.PourcentageDeVote(1) == "0,00%");
        //}
        [TestMethod]
        public void TesteLesValeursParDefautDeLaClaseSondage()
        {

            ReponseSondage Reponse = new ReponseSondage("Football");
            ReponseSondage Reponse1 = new ReponseSondage("Handball");
            ReponseSondage Reponse2 = new ReponseSondage("Tenis");
            List<ReponseSondage> ListeDeReponse = new List<ReponseSondage>();
            ListeDeReponse.Add(Reponse);
            ListeDeReponse.Add(Reponse1);
            ListeDeReponse.Add(Reponse2);
            ClasseSondage sondage = new ClasseSondage("Quel est le plus beau sport", ListeDeReponse, false);
            Assert.IsTrue(sondage.IdSondage == 0 && sondage.NombreDeVotant==0 && sondage.ClefDeSupression == "ClesParDefaut");
        }
        [TestMethod]
        public void TesteLesLiensDeVote()
        {

            ReponseSondage Reponse = new ReponseSondage("Football");
            ReponseSondage Reponse1 = new ReponseSondage("Handball");
            ReponseSondage Reponse2 = new ReponseSondage("Tenis");
            List<ReponseSondage> ListeDeReponse = new List<ReponseSondage>();
            ListeDeReponse.Add(Reponse);
            ListeDeReponse.Add(Reponse1);
            ListeDeReponse.Add(Reponse2);
            ClasseSondage sondage = new ClasseSondage("Quel est le plus beau sport", ListeDeReponse, false);
            Assert.IsTrue(sondage.LiendeVote() == "http://localhost:50700/Home/Vote?id=0");
        }
        [TestMethod]
        public void TesteLesLiensResulta()
        {

            ReponseSondage Reponse = new ReponseSondage("Football");
            ReponseSondage Reponse1 = new ReponseSondage("Handball");
            ReponseSondage Reponse2 = new ReponseSondage("Tenis");
            List<ReponseSondage> ListeDeReponse = new List<ReponseSondage>();
            ListeDeReponse.Add(Reponse);
            ListeDeReponse.Add(Reponse1);
            ListeDeReponse.Add(Reponse2);
            ClasseSondage sondage = new ClasseSondage("Quel est le plus beau sport", ListeDeReponse, false);
            Assert.IsTrue(sondage.LienDeResultat() == "http://localhost:50700/Home/Resultat?idsondageCourant=0");
        }
        [TestMethod]
        public void TesteLesLiensSupression()
        {

            ReponseSondage Reponse = new ReponseSondage("Football");
            ReponseSondage Reponse1 = new ReponseSondage("Handball");
            ReponseSondage Reponse2 = new ReponseSondage("Tenis");
            List<ReponseSondage> ListeDeReponse = new List<ReponseSondage>();
            ListeDeReponse.Add(Reponse);
            ListeDeReponse.Add(Reponse1);
            ListeDeReponse.Add(Reponse2);
            ClasseSondage sondage = new ClasseSondage("Quel est le plus beau sport", ListeDeReponse, false);
            Assert.IsTrue(sondage.LienDeSupression() == "http://localhost:50700/Home/Suppression?id=0&Clef="+sondage.ClefDeSupression);
        }
        [TestMethod]
       public void TesteUnitaireCréationSondage()
       {
            ReponseSondage Reponse = new ReponseSondage("UnitTest1");
            ReponseSondage Reponse1 = new ReponseSondage("UnitTest2");
            ReponseSondage Reponse2 = new ReponseSondage("UnitTest3");
            List<ReponseSondage> ListeDeReponse = new List<ReponseSondage>();
            ListeDeReponse.Add(Reponse);
            ListeDeReponse.Add(Reponse1);
            ListeDeReponse.Add(Reponse2);
            ClasseSondage sondage = new ClasseSondage("Creation d'un sondage UniTest", ListeDeReponse, false);
        int idUnitest=DataAcces.CreerUnSondage(sondage);
            ClasseSondage SondageUniTest = DataAcces.RecupererSondageEnBDD(idUnitest);
            Assert.IsTrue(SondageUniTest.QuestionChoixMultiples == false, "le choixmultiple n'as pas été pris en compte");
       }
        [TestMethod]
        public void TesteUnitaireQuestionDuSondage()
        {
            ReponseSondage Reponse = new ReponseSondage("UnitTest1");
            ReponseSondage Reponse1 = new ReponseSondage("UnitTest2");
            ReponseSondage Reponse2 = new ReponseSondage("UnitTest3");
            List<ReponseSondage> ListeDeReponse = new List<ReponseSondage>();
            ListeDeReponse.Add(Reponse);
            ListeDeReponse.Add(Reponse1);
            ListeDeReponse.Add(Reponse2);
            ClasseSondage sondage = new ClasseSondage("Creation d'un sondage UniTest", ListeDeReponse, false);
            int idUnitest = DataAcces.CreerUnSondage(sondage);
            ClasseSondage SondageUniTest = DataAcces.RecupererSondageEnBDD(idUnitest);
            Assert.IsTrue(SondageUniTest.QuestionDuSondage =="Creation d'un sondage UniTest", "la Question du Sondage n'as pas été correctement Enregistrer");
        }

        [TestMethod]
        public void TesteUnitaireLesReponsesDuSondage()
        {
            ReponseSondage Reponse = new ReponseSondage("UnitTest1");
            ReponseSondage Reponse1 = new ReponseSondage("UnitTest2");
            ReponseSondage Reponse2 = new ReponseSondage("UnitTest3");
            List<ReponseSondage> ListeDeReponse = new List<ReponseSondage>();
            ListeDeReponse.Add(Reponse);
            ListeDeReponse.Add(Reponse1);
            ListeDeReponse.Add(Reponse2);
            ClasseSondage sondage = new ClasseSondage("Creation d'un sondage UniTest", ListeDeReponse, false);
            int idUnitest = DataAcces.CreerUnSondage(sondage);
            ClasseSondage SondageUniTest = DataAcces.RecupererSondageEnBDD(idUnitest);
            Assert.IsTrue(SondageUniTest.ListeDeReponse[0].Reponse =="UnitTest1"&& SondageUniTest.ListeDeReponse[1].Reponse=="UnitTest2" && SondageUniTest.ListeDeReponse[2].Reponse =="UnitTest3", "la Question du Sondage n'as pas été correctement Enregistrer");
        }
        [TestMethod]
        public void TesteDesactivationSondage()
        {
            ReponseSondage Reponse = new ReponseSondage("UnitTest1");
            ReponseSondage Reponse1 = new ReponseSondage("UnitTest2");
            ReponseSondage Reponse2 = new ReponseSondage("UnitTest3");
            List<ReponseSondage> ListeDeReponse = new List<ReponseSondage>();
            ListeDeReponse.Add(Reponse);
            ListeDeReponse.Add(Reponse1);
            ListeDeReponse.Add(Reponse2);
            ClasseSondage sondage = new ClasseSondage("Creation d'un sondage UniTest", ListeDeReponse, false);
            int idUnitest = DataAcces.CreerUnSondage(sondage);
            DataAcces.DesactivationSondage(idUnitest);
            ClasseSondage SondageUniTest = DataAcces.RecupererSondageEnBDD(idUnitest);
            Assert.IsTrue(SondageUniTest.EtatDuSondage==false,"le sondage n'as pas été désactivé" );
        }

    }
}
