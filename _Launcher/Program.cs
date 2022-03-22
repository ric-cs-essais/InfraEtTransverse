using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Transverse.Infrastructure.Persistence.DB.Server_;
using Transverse.Infrastructure.Server_;

namespace _Launcher
{
    class Program
    {
        static void Main()
        {
            //DBServer oServer = DBServerFactory.getSingleton().getServer("titi,560/toto", 29);
            //Server oServer2 = ServerFactory.getSingleton().getServer("titi,560/toto", 28);

            DBServerAccess oServerAccess = DBServerAccessFactory.getSingleton().getServerAccess("titi/toto,580/yyy", "ricky", "psw");
            ServerAccess oServerAccess2 = ServerAccessFactory.getSingleton().getServerAccess("titi,580/toto", "ricky", "psw", 28);

            //Console.WriteLine(oServer2.Url.GetType());
            //Console.WriteLine(JsonConvert.SerializeObject(oServer.Url));
            //Console.WriteLine(JsonConvert.SerializeObject(oServer2));

            Console.WriteLine(JsonConvert.SerializeObject(oServerAccess));
            Console.WriteLine(JsonConvert.SerializeObject(oServerAccess2));

            Console.ReadKey();

        }

        private static void test1() { 
            List<string> oPortPrefixesList = new List<string>();
            oPortPrefixesList.Add(":");
            oPortPrefixesList.Add(",");
            string sPortPrefixes = string.Join("|", oPortPrefixesList);
            string sRegExp = $"[A-Z]+({sPortPrefixes})"+"([0-9]{2,})";
            Regex oRegExp = new Regex(sRegExp, RegexOptions.IgnoreCase);
            string sURL;
            sURL = "https:\\www.toto.fr/uuu/rrr:878/xxx.php";
            sURL = "localhost,459/jjj";
            sURL = "https://localhost:480";
            Match oMatch = oRegExp.Match(@$"{sURL}");

            string sPort = "??";
            if(oMatch.Success)
            {

                sPort = oMatch.Groups[2].Value;

            }
            Console.WriteLine(sPort);

            Console.ReadKey();
        }
    }
}
