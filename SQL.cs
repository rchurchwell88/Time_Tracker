﻿using System;
using MySql.Data.MySqlClient;
namespace ChurchwellRussell_SprintApp
{
    public class SQL
    {
        MySqlConnection conn;
        string Sconn = "server=127.0.0.1; user = root; database = sprint_02; port = 8889; password = root";
        Utilities util =new Utilities();


        public SQL()
        {
        }

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
    }
}
