using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace ChurchwellRussell_WorkingApp
{
    public class SQL
    {
        MySqlConnection conn=null;
        string Sconn = "server=127.0.0.1; user = root; database = Researched App01; port = 8889; password = root";
        Utilities util = new Utilities();
        MySqlCommand cmd = null;
        MySqlDataReader rdr = null;
        string user = null;
        bool registered = false;


        public SQL(){}

        public MySqlConnection Connect()
        {
            // Connection object
            conn = new MySqlConnection(Sconn);

            try
            {
                // Try to open connection
                conn.Open();

                // Clear and write if connected
                Console.Clear();
                Console.WriteLine("Connection opened.");

                // Get key press
                util.GetKey();
            }
            catch (MySqlException e)
            {
                // Print error
                Console.WriteLine(e.ToString());
            }

            return conn;
        }

        // get gender from gender table
        public string pullTableGender(string query, MySqlConnection _conn)
        {
            // SQL vars
            cmd = new MySqlCommand(query, _conn);
            string outP = null;

            // execute and pull vars
            try
            {
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    outP += $"id={rdr.GetString("id")}";
                    outP += $" : sex = {rdr.GetString("sex")}\n";
                }
            }
            catch(MySqlException e)
            {
                // print thrown exceptions
                Console.WriteLine(e.ToString());
            }

            // close reader and return output
            rdr.Close();
            return outP;
        }

        // unused method for login testing
        public string pullTableLoginUsers(string query, MySqlConnection _conn)
        {
            cmd = new MySqlCommand(query, _conn);
            string outP = null;

            try
            {
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    outP += $"id: {rdr.GetString("id")}\n";
                    outP += $"userName: {rdr.GetString("userName")}\n";
                    outP += $"password: {rdr.GetString("password")}";
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            rdr.Close();
            rdr = null;

            return outP;
        }

        // validate user credentials
        public bool validateLogin(string _userName, string _userPassword, MySqlConnection _conn)
        {
            // local sql and user vars
            string query = "select userName, password from loginUsers";
            string userName = null;
            string password = null;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr =cmd.ExecuteReader();

            try
            { 
                // while reader is open read in user info from database
                while (rdr.Read())
                {

                    userName = rdr.GetString("username");
                    string tmp = userName;
                    password = rdr.GetString("password");

                    // validate and log in if info matches
                    if (userName == _userName && password == _userPassword)
                    {
                        rdr.Close();
                        user = tmp;

                        registered = true;

                        return true;
                    }
                }

                // close reader
                rdr.Close();
            }
            catch(MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            // fail login if info doesnt match
            return false;
        }

        // method for profile sorting
        public void printSortMaster(int gender, MySqlConnection _conn)
        { 
            // if user isnt logged in or regestured return
            if(!registered)
            {
                Console.WriteLine("Please register/login first.");
                util.GetKey();
                return;
            }

            // gender prompt
            Console.Write("Please chose your preferred gender" +
                    "\n\n1: Female " +
                    "\n2: Male " +
                    "\n3: Both\n\nChoice: ");
            gender = util.GetIntLimit(3);

            // querys for bot male and female
            string query = "select A.id, name, gender, color from mUser " +
                "A join gender B on A.genderId = B.id join EyeColor C " +
                $"on A.eyeColorId = C.id where A.genderId = {gender} ";

            if (gender == 3)
                query = "select A.id, name, gender, color from mUser " +
                "A join gender B on A.genderId = B.id join EyeColor C " +
                $"on A.eyeColorId = C.id";

            // close reader
            if(rdr!=null)
                rdr.Close();

            // sql command and reader vars
            cmd = new MySqlCommand(query,_conn);
            rdr = cmd.ExecuteReader();
            bool sortRun = true;

            // pull and print profile data
            for (int i = 1; i < 2000;  i++)
            {

                int x = 1;

                while (rdr.Read() && sortRun)
                {
                    Console.Clear();

                    //// Print said data
                    int id = rdr.GetInt32("id");

                    string name = rdr.GetString("name");
                    Console.WriteLine($"Name: {name}");

                    string genderL = rdr.GetString("gender");
                    Console.WriteLine($"Gender: {genderL}");

                    string color = rdr.GetString("color");
                    Console.WriteLine($"Eye Color: {color}\n");

                    Console.WriteLine("\nLeft Arrow: Discard Profile");
                    Console.WriteLine("Right Arrow: Match Profile\n");
                    Console.WriteLine("Down Arrow: Return To Menu");


                    switch(Console.ReadKey().Key)
                    {

                        // dicard with left key
                        case ConsoleKey.LeftArrow:
                            {
                                rdr.Close();

                                x++;

                                // print either male or female data
                                if (gender == 2)
                                    query = $"select A.id, name, gender, color from mUser " +
                                    "A join gender B on A.genderId = B.id join EyeColor C " +
                                    $"on A.eyeColorId = C.id and A.id=(1000+{x})";
                                else
                                    query = $"select A.id, name, gender, color from mUser " +
                                    "A join gender B on A.genderId = B.id join EyeColor C " +
                                    $"on A.eyeColorId = C.id and A.id={x}";

                                // close and restart reader with updated query
                                cmd = new MySqlCommand(query, _conn);
                                rdr.Close();
                                rdr = cmd.ExecuteReader();

                                
                            }
                            break;

                        // keep profile with right arrow
                        case ConsoleKey.RightArrow:
                        {
                                try
                                {
                                    // insert into database
                                    string insert = "insert into fUser(id) values(@id)";
                                    MySqlCommand push=new MySqlCommand(insert, _conn);

                                    rdr.Close();

                                    // gender check
                                    if(gender==2)
                                        push.Parameters.AddWithValue("@id", 1000+x);
                                    else
                                        push.Parameters.AddWithValue("@id", x);
                                    push.ExecuteNonQuery();

                                    x++;

                                    // gender check
                                    if(gender==2)
                                        query = $"select A.id, name, gender, color from mUser " +
                                        "A join gender B on A.genderId = B.id join EyeColor C " +
                                        $"on A.eyeColorId = C.id and A.id=(1000+{x})";
                                    else
                                        query = $"select A.id, name, gender, color from mUser " +
                                        "A join gender B on A.genderId = B.id join EyeColor C " +
                                        $"on A.eyeColorId = C.id and A.id={x}";

                                    // refresh query
                                    cmd = new MySqlCommand(query,_conn);
                                    rdr.Close();
                                    rdr = cmd.ExecuteReader();
                                }
                                catch(MySqlException e)
                                {
                                    Console.WriteLine(e.ToString());
                                    util.GetKey();
                                }
                        }
                        break;

                        // close sort
                        case ConsoleKey.DownArrow:
                            {
                                sortRun = false;
                            }
                            break;
                    }
                }
            }

            // close reader
            rdr.Close();
            util.GetKey();
        }

        // method to show selected profiles
        public void chat(MySqlConnection _conn)
        {
            // registration check
            if(!registered)
            {
                Console.WriteLine("Please register/login first.");
                util.GetKey();

                return;
            }

            // sql vars for selected user
            string query = "select A.id, name from mUser A join " +
                "fUser B on A.id=B.id;";
            cmd = new MySqlCommand(query, _conn);
            rdr = cmd.ExecuteReader();
            int id, x = 1;
            string name = null; ;
            User tmpU;
            List<User> finalUsers = new List<User>();

            // print profiles and prompt
            if(rdr.HasRows)
            {
                Console.WriteLine("Please chose a matched profile to chat with\n");

                while(rdr.Read())
                {
                    id = rdr.GetInt32("id");
                    name = rdr.GetString("name");
                    Console.WriteLine($"{x}: {name}");
                    tmpU = new User(id, name);
                    finalUsers.Add(tmpU);
                    x++;
                }

                Console.Write("\n\nChoice: ");
            }
            else
            {
                Console.WriteLine("Please sort profiles first.");

                if (rdr != null)
                    rdr.Close();

                util.GetKey();

                return;
            }

            // input chat choice
            int choice = util.GetIntLimit(finalUsers.Count);

            // mock chat example
            Console.Clear();
            tmpU = finalUsers[choice-1];
            Console.WriteLine($"{tmpU.UserName}: Hey there!! Tell me about yourself.");
            util.GetKey();
        }

        // Method to purge fUser rows
        public void Purge(MySqlConnection _conn)
        {
            if (rdr != null)
                rdr.Close();

            string query = "delete from fUser";
            cmd = new MySqlCommand(query, _conn);

            try
            {
                cmd.Parameters.AddWithValue("","");
                cmd.ExecuteNonQuery();
            }
            catch(MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // method to register a user
        public void Register(MySqlConnection _conn)
        {
            Console.Write("Please enter username: ");
            string userN = util.GetString(Console.ReadLine());
            Console.Write("Please enter password: ");
            string password = util.GetString(Console.ReadLine());

            string query = "insert into loginUsers(userName, password) values(@userName, @password)";
            cmd = new MySqlCommand(query, _conn);

            cmd.Parameters.AddWithValue("@userName", userN);
            cmd.Parameters.AddWithValue("@password", password);

            cmd.ExecuteNonQuery();

        }
    }
}
