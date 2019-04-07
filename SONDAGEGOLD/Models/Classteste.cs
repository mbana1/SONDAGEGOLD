using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SONDAGEGOLD.Models
{
    public class Classteste
    {

 
        public string QuestionDuSondage { get; private set; }
        public string Rep1 { get; private set; }
        public string Rep2 { get; private set; }
        public string Rep3 { get; private set; }
        public string Rep4 { get; private set; }

        public string Rep5{ get; private set; }
        public string CheckBox { get; private set; }

        public Classteste(string questionDuSondage, string rep1, string rep2, string rep3, string rep4, string rep5, string checkBox)
        {
            QuestionDuSondage = questionDuSondage;
            Rep1 = rep1;
            Rep2 = rep2;
            Rep3 = rep3;
            Rep4 = rep4;
            Rep5 = rep5;
            CheckBox = checkBox;
        }

        //public Classteste(string questionDuSondage, string rep1, string rep2, string rep3, string rep4, string rep5, string checkBox)
        //{
        //    QuestionDuSondage = questionDuSondage ?? throw new ArgumentNullException(nameof(questionDuSondage));
        //    Rep1 = rep1 ?? throw new ArgumentNullException(nameof(rep1));
        //    Rep2 = rep2 ?? throw new ArgumentNullException(nameof(rep2));
        //    Rep3 = rep3 ?? throw new ArgumentNullException(nameof(rep3));
        //    Rep4 = rep4 ?? throw new ArgumentNullException(nameof(rep4));
        //    Rep5 = rep5 ?? throw new ArgumentNullException(nameof(rep5));
        //    CheckBox = checkBox ?? throw new ArgumentNullException(nameof(checkBox));
        //}
    }
}