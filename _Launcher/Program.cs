using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Infrastructure.Persistence.DAO.PersonneDAO;
using Infrastructure.Persistence.DAO.PersonneDAO.Interfaces;
using Newtonsoft.Json;
using Transverse.Infrastructure.Persistence.DB.Server.Sql.MySql;
using Transverse.Infrastructure.Persistence.DB.Server_;
using Transverse.Infrastructure.Server_;

namespace _Launcher
{
    class Program
    {
        static void Main()
        {
            DBServerAccess oServerAccessHomeMysSql = 
                DBServerAccessFactory.getSingleton().getServerAccess(
                    "localhost", 
                    "root",
                    "NewPcCLeV!21",
                    null, 3380
                );

            MySqlServerSqlRunner oMySqlServerSqlRunner =
                MySqlServerSqlRunnerFactory.getSingleton().getMySqlServerSqlRunner(oServerAccessHomeMysSql);

            IPersonneDAO oPersonneDAO = new SqlPersonneDAO(oMySqlServerSqlRunner);
            oPersonneDAO.getNbFemmes();

            //DBServer oServer = DBServerFactory.getSingleton().getServer("titi,560/toto", 29);
            //Server oServer2 = ServerFactory.getSingleton().getServer("titi,560/toto", 28);

            //DBServerAccess oServerAccess = DBServerAccessFactory.getSingleton().getServerAccess("titi/toto,580/yyy", "ricky", "psw", "MaBase", null);
            //.getSingleton().getServerAccess("titi/toto,580/yyy", "ricky", "psw");
            //ServerAccess oServerAccess2 = ServerAccessFactory.getSingleton().getServerAccess("titi,580/toto", "ricky", "psw", 28);

            //MySqlServerSqlRunnerFactory o1 = MySqlServerSqlRunnerFactory.getSingleton();
            //MySqlServerSqlRunnerFactory o2 = MySqlServerSqlRunnerFactory.getSingleton();
            //Console.WriteLine(o1==o2); //true


            //MySqlServerSqlRunner oMySqlServerSqlRunner1 = 
            //    MySqlServerSqlRunnerFactory.getSingleton().getMySqlServerSqlRunner(oServerAccess);
            //MySqlServerSqlRunner oMySqlServerSqlRunner2 =
            //    MySqlServerSqlRunnerFactory.getSingleton().getMySqlServerSqlRunner(oServerAccess);
            //Console.WriteLine(oMySqlServerSqlRunner1 == oMySqlServerSqlRunner2); //false


            //MySqlServerSqlRunner oMySqlServerSqlRunner =
            //    MySqlServerSqlRunnerFactory.getSingleton().getMySqlServerSqlRunner(oServerAccess);

            //Console.WriteLine(JsonConvert.SerializeObject(oMySqlServerSqlRunner.getDBServerSqlSyntaxer()));
            //Console.WriteLine(JsonConvert.SerializeObject(oMySqlServerSqlRunner.getDBServerAccess()));
            //Console.WriteLine("\n\n\n");

            //Console.WriteLine(JsonConvert.SerializeObject(oServerAccess));
            //Console.WriteLine(JsonConvert.SerializeObject(oServerAccess2));

            //Console.WriteLine("\n\n\n");
            //Console.WriteLine(oServer2.Url.GetType());
            //Console.WriteLine(JsonConvert.SerializeObject(oServer.Url));
            //Console.WriteLine(JsonConvert.SerializeObject(oServer2));

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
