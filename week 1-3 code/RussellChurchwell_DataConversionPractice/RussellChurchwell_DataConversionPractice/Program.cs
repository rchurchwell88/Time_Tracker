/*Database Location
string cs = @"server= 127.0.0.1;userid=root;password=root;database=SampleRestaurantDatabase;port=8889";
Output Location
string _directory = @"../../output/";*/

// Im running Mac.

using System;
using MySql.Data.MySqlClient;
using System.IO;

namespace RussellChurchwell_DataConversionPractice
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            MySqlConnection conn;
            MySqlCommand cmd;
            MySqlDataReader rdr;
            Utilities util = new Utilities();
            bool menuRun = true;

            Connect();

            string fileText=PullFormatData();
            String dir =@"../../Out/";
            string fN = "ChurchwellRussell_ConvertedData.JSON";

            while(menuRun)
            {
                Utilities.PrintMenu();

                int choice = util.GetIntMenu();

                switch(choice)
                {
                    case 1:
                        Write(fileText);
                        Console.Clear();
                        Console.WriteLine("Done.");
                        util.GetKey();
                        break;

                    case 5:
                        menuRun = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Oh child, she isn't finished yet.. Soon.");
                        util.GetKey();
                        break;
                }
            }

            void Connect()
            {
                String connString = "server=127.0.0.1; user = root; database = ConversionPractice; port = 8889; password = root";
                conn = new MySqlConnection(connString);

                try
                {
                    conn.Open();
                    Console.Clear();

                    Console.WriteLine("Connected To Database!");
                    util.GetKey();
                }
                catch (MySqlException e)
                {
                    Console.Clear();

                    Console.WriteLine(e.ToString());
                    util.GetKey();
                }
            }

            string PullFormatData()
            {
                string outS = "";

                for (int x = 1; x < 101; x++)
                {
                    string cmdStr = $"select * from `RestaurantProfiles` WHERE id={x}";
                    cmd = new MySqlCommand(cmdStr, conn);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                      
                        if(x==1)
                            outS += "[{";
                        else
                            outS += "{";

                        if (rdr["RestaurantName"] != DBNull.Value)
                            outS += $"\n\"Restaurant Name\": \"{rdr.GetString("RestaurantName")}\",\n";
                        else
                            outS += $"\n\"Restaurant Name\": \"{"No Data On File"}\",\n";

                        if (rdr["Address"] != DBNull.Value)
                            outS += $"\"Address\": \"{rdr.GetString("Address")}\",\n";
                        else
                            outS += $"\"Address\": \"{"No Data On File"}\",\n";

                        if (rdr["HoursOfOperation"] != DBNull.Value)
                            outS += $"\"Hours Of Operation\": \"{rdr.GetString("HoursOfOperation")}\",\n";
                        else
                            outS += $"\"Hours Of Operation\": \"{"No Data On File"}\",\n";

                        if (rdr["Price"] != DBNull.Value)
                            outS += $"\"Price\": \"{rdr.GetString("Price")}\",\n";
                        else
                            outS += $"\"Price\": \"{"No Data On File"}\",\n";

                        if (rdr["UsaCityLocation"] != DBNull.Value)
                            outS += $"\"Location\": \"{rdr.GetString("UsaCityLocation")}\",\n";
                        else
                            outS += $"\"Location\": \"{"No Data On File"}\",\n";

                        if (rdr["Cuisine"] != DBNull.Value)
                            outS += $"\"Cuisine\": \"{rdr.GetString("Cuisine")}\",\n";
                        else
                            outS += $"\"Cuisine\": \"{"No Data On File"}\",\n";

                        if (rdr["FoodRating"] != DBNull.Value)
                            outS += $"\"Food Rating\": \"{rdr.GetString("FoodRating")}\",\n";
                        else
                            outS += $"\"Food Rating{x}\": \"{"No Data On File"}\",\n";

                        if (rdr["ServiceRating"] != DBNull.Value)
                            outS += $"\"Service Rating\": \"{rdr.GetString("ServiceRating")}\",\n";
                        else
                            outS += $"\"Service Rating\": \"{"No Data On File"}\",\n";

                        if (rdr["AmbienceRating"] != DBNull.Value)
                            outS += $"\"Ambience Rating\": \"{rdr.GetString("AmbienceRating")}\",\n";
                        else
                            outS += $"\"Ambience Rating\": \"{"No Data On File"}\",\n";

                        if (rdr["ValueRating"] != DBNull.Value)
                            outS += $"\"Value Rating\": \"{rdr.GetString("ValueRating")}\",\n";
                        else
                            outS += $"\"Food Rating\": \"{"No Data On File"}\",\n";

                        if (rdr["OverallPossibleRating"] != DBNull.Value)
                            outS += $"\"Overall Rating\": \"{rdr.GetString("OverallPossibleRating")}\",\n";
                        else
                            outS += $"\"Overall Rating\": \"{"No Data On File"}\",\n";

                        if (rdr["Phone"] != DBNull.Value)
                            outS += $"\"Phone\": \"{rdr.GetString("Phone")}\"\n";
                        else
                            outS += $"\"Phone\": \"{"No Data On File"}\"\n";
                    }

                    if(x<100)
                        outS += "},\n";
                    else
                        outS += "}\n";

                    rdr.Close();
                }

                outS += "]";
                Console.WriteLine(outS);

                return outS;
            }

            void Write(string _outS)
            {
                Directory.CreateDirectory(dir);
                StreamWriter writer = new StreamWriter(dir + fN);

                foreach (char c in _outS)
                {
                    writer.Write(c);
                }

                writer.Close();
            }

        }

    }
}
