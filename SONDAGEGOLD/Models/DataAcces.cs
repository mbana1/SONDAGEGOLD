using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace SONDAGEGOLD.Models
{
    public class DataAcces
    {
        public static void CreerUnSondage(ClasseSondage Sondage)
        {
            const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
            SqlConnection connection = new SqlConnection(cheminBase);

            connection.Open();

            SqlCommand TableSondage = new SqlCommand("INSERT INTO Sondage(Question,ChoixMultiple) VALUES (@Question,@ChoixMultiple)", connection);

            TableSondage.Parameters.AddWithValue("@Question", Sondage.QuestionDuSondage);
            TableSondage.Parameters.AddWithValue("@ChoixMultiple", Sondage.QuestionChoixMultiples);
            //TableSondage.Parameters.AddWithValue("@LienDeVote", Sondage.ListeDeliens[0]);
            //TableSondage.Parameters.AddWithValue("@LienResultat", Sondage.ListeDeliens[1]);
            //TableSondage.Parameters.AddWithValue("@LienSupression", Sondage.ListeDeliens[2]);
            TableSondage.ExecuteReader();

           int FKIDsondage= RecupererLeDernierID();
            InsertionReponses(Sondage, FKIDsondage);
            InsertionLiens(FKIDsondage);

            connection.Close();
            TableSondage.Parameters.Clear();


        }
       
        

        public static int RecupererLeDernierID()
        {
            const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
            SqlConnection connection = new SqlConnection(cheminBase);



            connection.Open();

            SqlCommand DernieID = new SqlCommand("SELECT MAX(IDSondage) FROM Sondage", connection);

            
            SqlDataReader datareader = DernieID.ExecuteReader();

            datareader.Read();

            
            int ID = datareader.GetInt32(0);
            connection.Close();
            DernieID.Parameters.Clear();
            return ID;

        }
        public static void InsertionReponses(ClasseSondage sondage,int fkid)
        {
            

            foreach (string reponse in sondage.ListeDereponse)
            {
                const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
                SqlConnection connection = new SqlConnection(cheminBase);

                connection.Open();
                SqlCommand TableReponse = new SqlCommand("INSERT INTO Reponse (FKIDSondage,Reponses) VALUES (@FKIDSondage,@Reponses)", connection);

                TableReponse.Parameters.AddWithValue("@FKIDSondage", fkid);
                TableReponse.Parameters.AddWithValue("@Reponses", reponse);
                TableReponse.ExecuteReader();
                connection.Close();
                TableReponse.Parameters.Clear();

            }

            


        }







        public static void InsertionLiens(int fkid)
        {
            const int localhost = 50700;
            string cleDeSupression=FonctionUtiles.GeneratePassword(10);
           List<string>lien= ClasseSondage.GetlisteDeliens(fkid, localhost, cleDeSupression);
            const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
            SqlConnection connection = new SqlConnection(cheminBase);
            connection.Open();
            SqlCommand LiensSondage = new SqlCommand("UPDATE Sondage SET LienDeVote = @LienDeVote, LienResultat = @LienResultat, LienSupression = @LienSupression WHERE  IDSondage =@ID", connection);

            LiensSondage.Parameters.AddWithValue("@LienDeVote", lien[0]);
            LiensSondage.Parameters.AddWithValue("@LienResultat", lien[1]);
            LiensSondage.Parameters.AddWithValue("@LienSupression", lien[2]);
            LiensSondage.Parameters.AddWithValue("@ID", fkid);

            LiensSondage.ExecuteReader();
                connection.Close();
            LiensSondage.Parameters.Clear();
        }

        public static ClasseSondage AfficheSondageDansLaVueVote(int id)
        {
            const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
            SqlConnection connection = new SqlConnection(cheminBase);

            

            connection.Open();

            SqlCommand sondage= new SqlCommand("SELECT* FROM Sondage WHERE IDSondage=@ID", connection);
            sondage.Parameters.AddWithValue("@ID", id);
            SqlDataReader datareader = sondage.ExecuteReader();
            datareader.Read();
            int idSondage= datareader.GetInt32(0);
            string question= datareader.GetString(1);
            bool choix= datareader.GetBoolean(2);
            List<string> liens= new List<string> { datareader.GetString(3), datareader.GetString(4), datareader.GetString(5) };
            connection.Close();
            sondage.Parameters.Clear();

            List<string> Reponses = RecupereListeDereponses(id);

            ClasseSondage SondageCourant = new ClasseSondage(idSondage, question, Reponses, liens, choix);

           
            return SondageCourant;
        }

        public static List<string>RecupereListeDereponses(int id)
        {
            const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
            SqlConnection connection = new SqlConnection(cheminBase);



            connection.Open();

            SqlCommand ListeReponses = new SqlCommand("SELECT Reponses FROM Reponse WHERE FKIDSondage=@ID", connection);
            ListeReponses.Parameters.AddWithValue("@ID", id);
            SqlDataReader datareaderListe = ListeReponses.ExecuteReader();
            datareaderListe.Read();
            List<string> Reponses = new List<string>();
            do
            {
                string test = datareaderListe.GetString(0);
                Reponses.Add(test);
            }
            while
            (datareaderListe.Read());

            connection.Close();
            ListeReponses.Parameters.Clear();
            return Reponses;
        }

    }

}