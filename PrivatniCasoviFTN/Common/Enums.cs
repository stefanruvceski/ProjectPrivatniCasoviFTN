using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public enum CLASSES { SUBJECT = 0, CLASS, FIRM, USER, COMMENT, PRICELIST, STUDENTCLASS, TEACHERCLASS, TEACHERSUBJECT };
    public enum CLASS_STATUS { REQUESTED = 0, ACCEPTED, DECLINED};

    public class Dictionaries
    {
        public static Dictionary<string, string> Programming = new Dictionary<string, string>()
        {
            {"OET","PROGRAMMING" },
            {"PJISP","PROGRAMMING" },
            {"SAE","PROGRAMMING" },
            {"LPRS","PROGRAMMING" },
            {"OS","PROGRAMMING" },
            {"OP","PROGRAMMING" },
            {"HCI","PROGRAMMING" },
            {"ADS","PROGRAMMING" },
            {"WEB1","PROGRAMMING" },
            {"CLOUD","PROGRAMMING" },
            {"RVA","PROGRAMMING" },
            {"SCADA","PROGRAMMING" },
            {"OET","PROGRAMMING" },
            {"BP1","PROGRAMMING" },
        };

        public static Dictionary<string, string> Mathematics = new Dictionary<string, string>()
        {
            {"ANALIZA","MATHEMATICS" },
             {"ALGEBRA","MATHEMATICS" },
        };

        public static Dictionary<string, string> Electrotehnics = new Dictionary<string, string>()
        {
            {"OET","ELECTROTEHNICS" },
             {"EES","ELECTROTEHNICS" },
        };
    }
}