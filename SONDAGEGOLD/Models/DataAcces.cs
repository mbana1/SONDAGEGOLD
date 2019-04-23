using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SONDAGEGOLD.Models
{
    public class DataAcces
    {
        const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
        public static int CreerUnSondage(ClasseSondage Sondage)
        {
            int FKIDsondage = InsererLapremierePartieDuSondage(Sondage);
            InsertionReponses(Sondage, FKIDsondage);
            InsertionLiens(FKIDsondage);
            

            return FKIDsondage;
        }

        public static int InsererLapremierePartieDuSondage(ClasseSondage Sondage)
        {
            using (SqlConnection connection = new SqlConnection(cheminBase))
            {

                connection.Open();

                SqlCommand TableSondage = new SqlCommand("INSERT INTO Sondage(Question,ChoixMultiple,ClefDeSupression,NombreDeVotants,EtatDuSondage) OUTPUT INserted.IDSondage VALUES (@Question,@ChoixMultiple,@ClefDeSupression,@NombreDeVotant,@EtatDuSondage)", connection);

                TableSondage.Parameters.AddWithValue("@Question", Sondage.QuestionDuSondage);
                TableSondage.Parameters.AddWithValue("@ChoixMultiple", Sondage.QuestionChoixMultiples);
                TableSondage.Parameters.AddWithValue("@NombreDeVotant", Sondage.NombreDeVotant);
                TableSondage.Parameters.AddWithValue("@EtatDuSondage", Sondage.EtatDuSondage);
                TableSondage.Parameters.AddWithValue("@ClefDeSupression", Sondage.ClefDeSupression);
                int IdDuSondageInsere=(int)TableSondage.ExecuteScalar();

                return IdDuSondageInsere;

            }
            


        }

        public static void InsertionReponses(ClasseSondage sondage,int fkid)
        {
            

            foreach (ReponseSondage reponse in sondage.ListeDeReponse)
            {
                //const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
                using (SqlConnection connection = new SqlConnection(cheminBase))
                {

                    connection.Open();
                    SqlCommand TableReponse = new SqlCommand("INSERT INTO Reponse (FKIDSondage,Reponses,NombreDeVote) VALUES (@FKIDSondage,@Reponses,@NombreDeVote)", connection);

                    TableReponse.Parameters.AddWithValue("@FKIDSondage", fkid);
                    TableReponse.Parameters.AddWithValue("@Reponses", reponse.Reponse);
                    TableReponse.Parameters.AddWithValue("@NombreDeVote", reponse.NombreDeVote);
                    TableReponse.ExecuteReader();
                    connection.Close();
                    TableReponse.Parameters.Clear();
                }
            }

            


        }







        public static String InsertionLiens(int fkid)
        {
            //const int localhost = 50700;
            string cleDeSupression=FonctionUtiles.GeneratePassword(10);


            using (SqlConnection connection = new SqlConnection(cheminBase))
            {
                connection.Open();
                SqlCommand LiensSondage = new SqlCommand("UPDATE Sondage SET ClefDeSupression = @ClefDeSupression WHERE  IDSondage =@ID", connection);
                LiensSondage.Parameters.AddWithValue("@ClefDeSupression", cleDeSupression);
                LiensSondage.Parameters.AddWithValue("@ID", fkid);
                LiensSondage.ExecuteScalar();
                connection.Close();
                LiensSondage.Parameters.Clear();
                return cleDeSupression;
            }
        }
        public static void DesactivationSondage(int id)
        {
            using (SqlConnection connection = new SqlConnection(cheminBase))
            {
                connection.Open();
                SqlCommand Desactivation = new SqlCommand("UPDATE Sondage SET EtatDuSondage=@value WHERE  IDSondage =@ID", connection);
                Desactivation.Parameters.AddWithValue("@ID", id);
                Desactivation.Parameters.AddWithValue("@value", 0);
                Desactivation.ExecuteScalar();
                connection.Close();
                Desactivation.Parameters.Clear();
            }
        }

        public static ClasseSondage RecupererSondageEnBDD(int id)
        {

            using (SqlConnection connection = new SqlConnection(cheminBase))
            {
                connection.Open();

                SqlCommand sondage = new SqlCommand("SELECT* FROM Sondage WHERE IDSondage=@ID", connection);
                sondage.Parameters.AddWithValue("@ID", id);
                SqlDataReader datareader = sondage.ExecuteReader();
                datareader.Read();
                int idSondage = datareader.GetInt32(0);
                string question = datareader.GetString(1);
                bool choix = datareader.GetBoolean(2);
                string ClefSupression = datareader.GetString(3).Trim();
                int NombreDeVotant = datareader.GetInt32(4);
                bool EtatDuSondage = datareader.GetBoolean(5);

                connection.Close();
                sondage.Parameters.Clear();

                List<ReponseSondage> Reponses = RecupereListeDereponses(id);

                ClasseSondage SondageCourant = new ClasseSondage(idSondage, question, Reponses, ClefSupression, choix, NombreDeVotant, EtatDuSondage);


                return SondageCourant;
            }
        }

        public static List<ReponseSondage> RecupereListeDereponses(int id)
        {

            using (SqlConnection connection = new SqlConnection(cheminBase))
            {



                connection.Open();

                SqlCommand ListeReponses = new SqlCommand("SELECT* FROM Reponse WHERE FKIDSondage=@ID", connection);
                ListeReponses.Parameters.AddWithValue("@ID", id);
                SqlDataReader datareaderListe = ListeReponses.ExecuteReader();
                datareaderListe.Read();
                List<ReponseSondage> ListeReponse = new List<ReponseSondage>();
                do
                {
                    int idReponse = datareaderListe.GetInt32(0);
                    string Reponse = datareaderListe.GetString(2);
                    int NombreDeVote = datareaderListe.GetInt32(3);
                    ReponseSondage reponseSondage = new ReponseSondage(idReponse, Reponse, NombreDeVote);
                    ListeReponse.Add(reponseSondage);
                }
                while
                (datareaderListe.Read());

                connection.Close();
                ListeReponses.Parameters.Clear();
                return ListeReponse;
            }
        }
        public static void InsertionResultatDuVote(int id, List<string> ListID)
        {
            InsertionNombreDeVotant(id);
            InsertionChoixDeVote(ListID);
           
        }
        public static void InsertionNombreDeVotant(int id)
        {

            using (SqlConnection connection = new SqlConnection(cheminBase))
            {
                connection.Open();
                SqlCommand LiensSondage = new SqlCommand("UPDATE Sondage SET NombreDeVotants = NombreDeVotants + 1 WHERE IDSondage =@ID ", connection);
                LiensSondage.Parameters.AddWithValue("@ID", id);
                LiensSondage.ExecuteScalar();
                connection.Close();
                LiensSondage.Parameters.Clear();
            }
        }
        public static void InsertionChoixDeVote(List<string>ListID)
        {
            using (SqlConnection connection = new SqlConnection(cheminBase))
            {
                connection.Open();
                foreach (string id in ListID)
                {


                    SqlCommand LiensSondage = new SqlCommand("UPDATE Reponse SET NombreDeVote = NombreDeVote + 1 WHERE IDReponse =@ID ", connection);
                    LiensSondage.Parameters.AddWithValue("@ID", id);
                    LiensSondage.ExecuteNonQuery();




                }
                connection.Close();
            }


        }
    }
    
}