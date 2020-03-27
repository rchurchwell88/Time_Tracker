using System;
using MySql.Data.MySqlClient;
using System.Linq;

namespace ChurchwellRussell_SprintApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            bool menuRun = true;
            Utilities util = new Utilities;

            util.PrintMenu();
            util.GetKey();
        }
    }
}
