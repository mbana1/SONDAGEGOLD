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

                SqlCommand TableSondage = new SqlCommand("INSERT INTO Sondage(Question,ChoixMultiple) OUTPUT INserted.IDSondage VALUES (@Question,@ChoixMultiple)", connection);

                TableSondage.Parameters.AddWithValue("@Question", Sondage.QuestionDuSondage);
                TableSondage.Parameters.AddWithValue("@ChoixMultiple", Sondage.QuestionChoixMultiples);
                int IdDuSondageInsere=(int)TableSondage.ExecuteScalar();

                return IdDuSondageInsere;

            }
            
           

            




        }


        //    public static int RecupererLeDernierID()
        //{
        //    const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
        //    SqlConnection connection = new SqlConnection(cheminBase);



        //    connection.Open();

        //    SqlCommand DernieID = new SqlCommand("SELECT MAX(IDSondage) FROM Sondage", connection);

            
        //    SqlDataReader datareader = DernieID.ExecuteReader();

        //    datareader.Read();

            
        //    int ID = datareader.GetInt32(0);
        //    connection.Close();
        //    DernieID.Parameters.Clear();
        //    return ID;

        //}
        public static void InsertionReponses(ClasseSondage sondage,int fkid)
        {
            

            foreach (ReponseSondage reponse in sondage.ListeDeReponse)
            {
                const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
                SqlConnection connection = new SqlConnection(cheminBase);

                connection.Open();
                SqlCommand TableReponse = new SqlCommand("INSERT INTO Reponse (FKIDSondage,Reponses) VALUES (@FKIDSondage,@Reponses)", connection);

                TableReponse.Parameters.AddWithValue("@FKIDSondage", fkid);
                TableReponse.Parameters.AddWithValue("@Reponses", reponse.Reponse);
                TableReponse.ExecuteReader();
                connection.Close();
                TableReponse.Parameters.Clear();

            }

            


        }







        public static String InsertionLiens(int fkid)
        {
            //const int localhost = 50700;
            string cleDeSupression=FonctionUtiles.GeneratePassword(10);
           
           
            SqlConnection connection = new SqlConnection(cheminBase);
            connection.Open();
            SqlCommand LiensSondage = new SqlCommand("UPDATE Sondage SET ClefDeSupression = @ClefDeSupression WHERE  IDSondage =@ID", connection);
            LiensSondage.Parameters.AddWithValue("@ClefDeSupression", cleDeSupression);
            LiensSondage.Parameters.AddWithValue("@ID", fkid);
            LiensSondage.ExecuteScalar();
                connection.Close();
            LiensSondage.Parameters.Clear();
            return cleDeSupression;
        }

        public static ClasseSondage RecupererSondageEnBDD(int id)
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
            string ClefSupression= datareader.GetString(3);
            //List<string> liens= new List<string> { datareader.GetString(3), datareader.GetString(4), datareader.GetString(5) };
            connection.Close();
            sondage.Parameters.Clear();

            List<ReponseSondage> Reponses = RecupereListeDereponses(id);

            ClasseSondage SondageCourant = new ClasseSondage(idSondage, question, Reponses, ClefSupression, choix);

           
            return SondageCourant;
        }

        public static List<ReponseSondage> RecupereListeDereponses(int id)
        {
            const string cheminBase = @"Server=.\sqlexpress;Initial Catalog = SONDAGEGOLD; Integrated Security = True";
            SqlConnection connection = new SqlConnection(cheminBase);



            connection.Open();
            
            SqlCommand ListeReponses = new SqlCommand("SELECT* FROM Reponse WHERE FKIDSondage=@ID", connection);
            ListeReponses.Parameters.AddWithValue("@ID", id);
            SqlDataReader datareaderListe = ListeReponses.ExecuteReader();
            datareaderListe.Read();
            List<ReponseSondage> ListeReponse = new List<ReponseSondage>();
            do
            {
                int idReponse= datareaderListe.GetInt32(0);
                string Reponse= datareaderListe.GetString(2);
                ReponseSondage reponseSondage = new ReponseSondage(idReponse, Reponse);
                ListeReponse.Add(reponseSondage);
            }
            while
            (datareaderListe.Read());

            connection.Close();
            ListeReponses.Parameters.Clear();
            return ListeReponse;
        }

    }

}