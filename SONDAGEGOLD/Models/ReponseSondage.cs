using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SONDAGEGOLD.Models
{
    public class ReponseSondage
    {
       public int IDreponse { get; private set; }
        public string Reponse { get; private set; }

        public ReponseSondage(int dreponse, string reponse)
        {
            IDreponse = dreponse;
            Reponse = reponse;
        }
    }
}