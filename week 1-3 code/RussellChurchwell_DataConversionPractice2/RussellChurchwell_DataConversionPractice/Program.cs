/*Database Location
string cs = @"server= 127.0.0.1;userid=root;password=root;database=SampleRestaurantDatabase;port=8889";
Output Location
string _directory = @"../../output/";*/

// Im running Mac.

using System;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections.Generic;

namespace RussellChurchwell_DataConversionPractice
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // MySQL Vars
            MySqlConnection conn;
            MySqlCommand cmd;
            MySqlDataReader rdr;

            // Utility handler 
            Utilities util = new Utilities();
            bool menuRun = true;

            // Connect to database
            Connect();

            // File IO vars
            string fileText = PullFormatData();
            String dir = @"../../Out/";
            string fN = "ChurchwellRussell_ConvertedData.JSON";

            // Master restaurant list
            List<Restaurant> start;

            // Main menu system
            while (menuRun)
            {
                // Print menu
                Utilities.PrintMenu();

                // Menu choice var
                int choice = util.GetIntMenu();

                // Menu Logic
                switch (choice)
                {
                    // JSon IO
                    case 1:
                        Write(fileText);
                        Console.Clear();
                        Console.WriteLine("Done.");
                        util.GetKey();
                        break;

                    // Star rating System
                    case 2:
                        start=GenMaster();
                        Sort();
                        util.GetKey();
                        break;

                    // Kill system
                    case 5:
                        menuRun = false;
                        break;

                    // Coming Soon
                    default:
                        Console.Clear();
                        Console.WriteLine("Oh child, she isn't finished yet.. Soon.");
                        util.GetKey();
                        break;
                }
            }

            void Connect()
            {
                // Connection elements
                String connString = "server=127.0.0.1; user = root; database = ConversionPractice; port = 8889; password = root";
                conn = new MySqlConnection(connString);

                // Open connection
                try
                {
                    conn.Open();
                    Console.Clear();

                    Console.WriteLine("Connected To Database!");
                    util.GetKey();
                }

                // Failure/Exception called( Danger Will Robinson! )
                catch (MySqlException e)
                {
                    Console.Clear();

                    Console.WriteLine(e.ToString());
                    util.GetKey();
                }
            }


            string PullFormatData()
            {
                // String for JSON bracket formation
                string outS = "";

                // Loop to output each restaurant object
                for (int x = 1; x < 101; x++)
                {
                    // SQL query vars
                    string cmdStr = $"select * from `RestaurantProfiles` WHERE id={x}";
                    cmd = new MySqlCommand(cmdStr, conn);
                    rdr = cmd.ExecuteReader();

                    // Read all input SQL vals
                    while (rdr.Read())
                    {
                        // JSON Conditionals

                        if (x == 1)
                            outS += "[{";
                        else
                            outS += "{";

                        if (rdr["RestaurantName"] != DBNull.Value)
                            outS = $"\n\"Restaurant Name\": \"{rdr.GetString("RestaurantName")}\",\n";
                        
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

                    if (x < 100)
                        outS += "},\n";
                    else
                        outS += "}\n";

                    rdr.Close();
                }

                // Out and return conditionals
                outS += "]";
                Console.WriteLine(outS);

                return outS;
            }

            // Write to file
            void Write(string _outS)
            {
                // Create writer/directory
                Directory.CreateDirectory(dir);
                StreamWriter writer = new StreamWriter(dir + fN);

                // Write per char
                foreach (char c in _outS)
                {
                    writer.Write(c);
                }

                // Close writer
                writer.Close();
            }

            // Generate master list
            List<Restaurant> GenMaster()
            {
                List<Restaurant> master=new List<Restaurant>();
                List<Restaurant> final = new List<Restaurant>();
                Restaurant tmp;

                for (int x = 1; x < 101; x++)
                {
                    // Vars to generate 
                    string cmdStr = $"select RestaurantName, OverallRating from `RestaurantProfiles` WHERE id={x}";
                    cmd = new MySqlCommand(cmdStr, conn);
                    rdr = cmd.ExecuteReader();
                    string name;
                    decimal rating;

                    // While reader has vals
                    while (rdr.Read())
                    {
                        // Add to name and pad to format
                        if (rdr["RestaurantName"] != DBNull.Value)
                        {
                            name = rdr.GetString("RestaurantName");
                            name = name.PadRight(39);
                        }

                        // no val, hopefully this isnt tripped
                        else
                            name = "empty";

                        // Same as name above
                        if (rdr["OverallRating"] != DBNull.Value)
                            rating = rdr.GetDecimal("OverallRating");

                        // It has no rating
                        else
                            rating = 0;

                        // Add to collection
                        tmp = new Restaurant(name, rating);
                        master.Add(tmp);
                    }

                    // Close reader
                    rdr.Close();

                    
                }

                // Return val
                return master;


            }

            void Sort()
            {
                // Sort vals
                int choice;
                Utilities.PrintSortMenu();

                // Format menu choice
                choice = util.GetIntMenu();

                switch(choice)
                {
                    case 1:
                        {
                            Console.Clear();

                            if(start!=null)
                            foreach (var item in start)
                            {
                                    // Init item to compare
                                    string init = item.Rating.ToString();
                                    string stars = ": Rating: ";
                                    char num = init[0];

                                    if(num == '0')
                                        stars += "Unrated";

                                    if (num == '1')
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        stars += "*";
                                    }

                                    if (num == '2')
                                    {

                                        stars += "**";
                                    }

                                    if(num == '3')
                                        stars += "***";

                                    if(num == '4')
                                        stars += "****";

                                    if(num == '5')
                                        stars += "*****";
                                    
                                    // Write write result
                                    Console.Write($"{item.Name}");
                                    Console.ForegroundColor = item.Color(num);
                                    Console.WriteLine($"{stars}");
                                    Console.ForegroundColor = ConsoleColor.Green;
                            }
                        }
                        break;

                    case 2:
                        
                        // Same as case 1

                        Console.Clear();

                        if (start != null)
                        for (int x = start.Count-1; x > -1; x--)
                        {
                                string init = start[x].Rating.ToString();
                                string stars = "Rating: ";
                                char num = init[0];

                                if (num == '0')
                                    stars += "Unrated";

                                if (num == '1')
                                    stars += "*";

                                if (num == '2')
                                    stars += "**";

                                if (num == '3')
                                    stars += "***";

                                if (num == '4')
                                    stars += "****";

                                if (num == '5')
                                    stars += "*****";


                                Console.Write($"{start[x].Name} : ");

                                if (num == '0')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else if (num == '1')
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else if (num == '2')
                                    Console.ForegroundColor = ConsoleColor.Red;

                                else if (num == '3')
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                else
                                    Console.ForegroundColor = ConsoleColor.Green;


                                Console.WriteLine($"{stars}\n", Console.ForegroundColor);

                                Console.ForegroundColor = ConsoleColor.Green;
                            }

                        break;

                    case 3:
                        {

                            // Same as case 2

                            Console.Clear();
                            int sort = 5;

                            while(sort>-1)
                            {
                                foreach (var item in start)
                                {
                                    string init = item.Rating.ToString();
                                    string stars = "Rating: ";
                                    string num = init.Substring(0,1);
                                    int tmp = Int32.Parse(num);

                                    if (tmp==sort)
                                    {
                                        if(num == "0")
                                            stars += "Unrated";

                                        if (num == "1")
                                            stars += "*";

                                        if (num == "2")
                                            stars += "**";

                                        if (num == "3")
                                            stars += "***";

                                        if (num == "4")
                                            stars += "****";

                                        if (num == "5")
                                            stars += "*****";


                                        Console.Write($"{item.Name} : ");

                                        Console.ForegroundColor = item.Color(num);

                                        Console.WriteLine($"{stars}\n");

                                        Console.ForegroundColor = ConsoleColor.Green;
                                    }

                                }
                                sort--;
                            }
                        }
                        break;

                    case 4:
                        {

                            // Same as case 3

                            Console.Clear();

                            int sort = 0;

                            while (sort < 6)
                            {
                                foreach (var item in start)
                                {
                                    string init = item.Rating.ToString();
                                    string stars = "Rating: ";
                                    string num = init.Substring(0, 1);
                                    int tmp = Int32.Parse(num);

                                    if (tmp == sort)
                                    {
                                        if (num == "0")
                                            stars += "Unrated";

                                        if (num == "1")
                                            stars += "*";

                                        if (num == "2")
                                            stars += "**";

                                        if (num == "3")
                                            stars += "***";

                                        if (num == "4")
                                            stars += "****";

                                        if (num == "5")
                                            stars += "*****";


                                        Console.Write($"{item.Name} : ");

                                        Console.ForegroundColor = item.Color(num);

                                        Console.WriteLine($"{stars}\n");

                                        Console.ForegroundColor = ConsoleColor.Green;
                                    }

                                }
                                sort++;
                            }
                            break;


                        }

                    case 5:
                        {
                            // Print sub menu get menu selection
                            Utilities.PrintSortSubMenu();
                            int menu = util.GetIntSubMenu();

                            // Sort based on stars
                            switch(menu)
                            {
                                case 1:
                                {
                                    Console.Clear();
                                    subSort(5);
                                }
                                break;

                                case 2:
                                {
                                    Console.Clear();
                                    subSort(4);
                                }
                                break;

                                case 3:
                                {
                                    Console.Clear();
                                    subSort(3);
                                }
                                break;

                                case 4:
                                {
                                    Console.Clear();
                                    subSort(2);
                                }
                                break;

                                case 5:
                                {
                                    Console.Clear();
                                    subSort(1);
                                }
                                break;

                                case 6:
                                {
                                    Console.Clear();
                                    subSort(0);
                                }
                                break;
                            }
                        }
                        break;
                }
            }

            void subSort(int limit)
            {
                // Start loop to output rating sort
                foreach (var item in start)
                {
                    // Conversion vars to out
                    string tmp = item.Rating.ToString();
                    tmp = tmp.Substring(0, 1);
                    int rating = int.Parse(tmp);

                    // Check rating vs rating limit and write result
                    if (rating == limit)
                    {
                        if(limit==5)
                            Console.WriteLine($"{item.Name} : Rating *****");

                        if(limit==4)
                            Console.WriteLine($"{item.Name} : Rating ****");

                        if (limit == 3)
                        {
                            Console.Write($"{item.Name} : ");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            
                            Console.WriteLine("Rating ***",Console.ForegroundColor);

                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        if (limit == 2)
                        {
                            Console.Write($"{item.Name} : ");
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine("Rating **", Console.ForegroundColor);

                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        if(limit==1)
                        {
                            Console.Write($"{item.Name} : ");
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine("Rating *", Console.ForegroundColor);

                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        if (limit==0)
                        {
                            Console.Write($"{item.Name} : ");
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine("Unrated", Console.ForegroundColor);

                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                    }

                }
            }
        }
    }
}
