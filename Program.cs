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
            Utilities util = new Utilities();
            SQL sql = new SQL();
            MySqlConnection conn = sql.Connect();
            MySqlCommand cmd;
            MySqlDataReader rdr=null;

            while (menuRun)
            {
                Console.Clear();
                util.PrintMenu();

                int menuChoice=util.GetIntLimit(2);

                switch(menuChoice)
                {
                    case 1:
                        Console.Clear();
                        printRaw();
                        util.GetKey();
                        break;

                    case 2:
                        break;
                }

            }

            void printRaw()
            {
                string query = "select calendar_dayId, " +
                    "Date, Day, description, " +
                    "category, time_spent_on_activity from raw " +
                    "join day_names on day_names.id = raw.day_name " +
                    "join activity_descrption b on b.id = raw.category_description " +
                    "join `activity_category` c on c.id = raw.`activity_descriptions` " +
                    "join `calender_Dates` a on a.id=raw.`calendar_date` " +
                    "where user_Id = 1; ";
                cmd = new MySqlCommand(query, conn);

                try
                {
                    rdr = cmd.ExecuteReader();
                }
                catch(MySqlException e)
                {
                    Console.WriteLine(e.ToString());
                }

                if(rdr.HasRows)
                {
                    while(rdr.Read())
                    {
                        Console.WriteLine($"Day num: {rdr.GetString("calendar_dayId")}");
                        Console.WriteLine($"Date:  {rdr.GetString("Date")}");
                        Console.WriteLine($"Day: {rdr.GetString("Day")}");
                        Console.WriteLine($"Description: {rdr.GetString("description")}");
                        Console.WriteLine($"Category: {rdr.GetString("category")}");
                        Console.WriteLine($"Hours: {rdr.GetString("time_spent_on_activity")}\n");
                    }

                    rdr.Close();
                }
            }


        }
    }
}
