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
            string query = null;

            while (menuRun)
            {
                Console.Clear();
                util.PrintMenu();

                int menuChoice=util.GetIntLimit(3);

                switch(menuChoice)
                {
                    case 1:
                        Console.Clear();
                        printRaw();
                        util.GetKey();
                        break;

                    case 2:
                        Console.Clear();
                        insert();
                        util.GetKey();
                        break;

                    case 3:
                        Console.Clear();
                        avg();
                        util.GetKey();
                        break;
                }

            }

            void printRaw()
            {
                query = "select calendar_dayId, " +
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

            void insert()
            {
                int dateI, dayI, descriptionI, categoryI, hours;

                query = "select * from calender_Dates";
                cmd = new MySqlCommand(query, conn);

                rdr = cmd.ExecuteReader();

                if(rdr.HasRows)
                {
                    while(rdr.Read())
                    {
                        Console.WriteLine(rdr.GetInt32("id")+$": {rdr.GetString("Date")}");
                    }

                    rdr.Close();
                    Console.Write("\nPlease choose a date: ");
                }

                dateI = util.GetIntLimit(28);
                Console.Clear();

                query = "select * from day_names;";
                cmd = new MySqlCommand(query, conn);

                rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Console.WriteLine(rdr.GetInt32("id") + $": {rdr.GetString("Day")}");
                    }

                    rdr.Close();
                    Console.Write("\nPlease choose a day: ");
                }

                dayI = util.GetIntLimit(7);

                Console.Clear();

                query = "select * from activity_category";
                cmd = new MySqlCommand(query, conn);

                rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Console.WriteLine(rdr.GetInt32("id") + $": {rdr.GetString("category")}");
                    }

                    rdr.Close();
                    Console.Write("\nPlease choose a category: ");
                }

                descriptionI = util.GetIntLimit(13);

                Console.Clear();

                query = "select * from activity_descrption";
                cmd = new MySqlCommand(query, conn);

                rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Console.WriteLine(rdr.GetInt32("id") + $": {rdr.GetString("description")}");
                    }

                    rdr.Close();
                    Console.Write("\nPlease choose a description: ");
                }

                categoryI = util.GetIntLimit(19);


                Console.Clear();

                Console.Write("How many hours did you spend: ");
                hours=util.GetIntLimit(24);

                query = "insert into raw(user_Id, calendar_dayId, calendar_date, " +
                    "day_name, category_description, activity_descriptions, " +
                    $"time_spent_on_activity) values(@user_Id, @day, @dateI, @dayI, @desc, " +
                    $"@categoryI, @hours)";

                try
                {
                    cmd = new MySqlCommand(query, conn);

                    //cmd.Parameters.Add("@day", MySqlDbType.Int32).Value = 27;
                    //cmd.Parameters.Add("@dateI", MySqlDbType.Int32).Value = dateI;
                    //cmd.Parameters.Add("@dayI", MySqlDbType.Int32).Value = dayI;
                    //cmd.Parameters.Add("@desc", MySqlDbType.Int32).Value = descriptionI;
                    //cmd.Parameters.Add("@categoryI", MySqlDbType.Int32).Value = categoryI;
                    //cmd.Parameters.Add("@hours", MySqlDbType.Int32).Value = hours;

                    cmd.Parameters.AddWithValue("@user_Id", 1);
                    cmd.Parameters.AddWithValue("@day", 28);
                    cmd.Parameters.AddWithValue("@dateI", dateI);
                    cmd.Parameters.AddWithValue("@dayI", dayI);
                    cmd.Parameters.AddWithValue("@desc", categoryI);
                    cmd.Parameters.AddWithValue("@categoryI", descriptionI);
                    cmd.Parameters.AddWithValue("@hours", hours);

                    cmd.ExecuteNonQuery();
                }
                catch(MySqlException e)
                {
                    Console.WriteLine(e.ToString());
                    util.GetKey();
                }


            }

            void avg()
            {
                query = "select calendar_dayId, time_spent_on_activity from raw where user_id=1";
                cmd = new MySqlCommand(query, conn);

                    rdr = cmd.ExecuteReader();
                int avg1=0;

                if(rdr.HasRows)
                {
                    while(rdr.Read())
                    {
                        avg1 += rdr.GetInt32("time_spent_on_activity");

                        if(rdr.GetInt32("calendar_dayId") ==8|| rdr.GetInt32("calendar_dayId") == 15
                            || rdr.GetInt32("calendar_dayId") == 22)
                        {
                            Console.WriteLine($"Weekly hour average: {avg1/4}");
                            avg1 = 0;
                        }


                    }
                }
            }
        }
    }
}
