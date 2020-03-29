using System;
using MySql.Data.MySqlClient;

namespace ChurchwellRussell_WorkingApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // SQL and menu vars
            SQL sql = new SQL();
            Utilities util = new Utilities();
            MySqlConnection conn= sql.Connect();
            sql.Purge(conn);
            bool menuRun = true;
            int menuChoice = -1;

            Console.Clear();

            // menu print and prompt
            while(menuRun)
            {
                Console.Clear();

                // get input
                util.printMenu();
                menuChoice = util.GetIntMenu();

                // menu switch
                switch(menuChoice)
                {
                    // login
                    case 1:
                        login();
                        break;

                    // register
                    case 2:
                        Console.Clear();
                        sql.Register(conn);
                        break;

                    // sort
                    case 3:
                        sql.Purge(conn);
                        Console.Clear();
                        sort();
                        break;

                    // chat
                    case 4:
                        Console.Clear();
                        sql.chat(conn);
                        sql.Purge(conn);
                        break;

                    // exit
                    case 5:
                        menuRun = false;
                        break;
                }
            }

            // base login method
            void login()
            {
                // user vars
                string username, password;
                Console.Clear();

                // prompt user/pass
                Console.Write("Please enter your user name: ");
                username = Console.ReadLine();

                Console.Clear();
                Console.Write("Please enter your password: ");
                password = Console.ReadLine();

                Console.Clear();

                // talk to login child
                if (sql.validateLogin($"{username}", $"{password}", conn))
                {
                    Console.WriteLine("login success!");
                    util.UserL = username;
                }
                else
                    Console.WriteLine("Login failure.");

                util.GetKey();
            }

            // sort base
            void sort()
            {
                int gender=1;

                sql.printSortMaster(gender, conn);
            }
        }
    }
}
