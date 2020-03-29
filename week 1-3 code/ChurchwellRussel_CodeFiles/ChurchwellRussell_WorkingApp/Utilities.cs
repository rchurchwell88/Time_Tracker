using System;
using System.Text.RegularExpressions;

namespace ChurchwellRussell_WorkingApp

{
    public class Utilities
    {
        private string user = "(not logged in)\n";
        public Utilities(){}

        public void printMenu()
        {
            Console.WriteLine($"User: {user}");
            Console.Write("1: Login\n" +
                "2: Register\n" +
                "3: Chose profiles\n" +
                "4: Chat with your matches\n\n5: EXIT");

            Console.Write("\n\nChoice: ");
        }

        // Validate string
        public string GetString(string _inp)
        {
            while(string.IsNullOrWhiteSpace(_inp))
            {
                Console.Write("Ivalid entry, try again: ");
                _inp = Console.ReadLine();
            }

            return _inp;
        }

        // Validate main menu input
        public int GetIntMenu()
        {
            int menu;

            while(!int.TryParse(Console.ReadLine(), out menu) || menu<1  || menu>5)
            {
                Console.Write("Invalid entry, try again: ");
            }

            return menu;
        }

        // Validate dynamic submenus bases on limit
        public int GetIntLimit(int limit)
        {
            int count;

            while (!int.TryParse(Console.ReadLine(), out count) || count < 1 || count > limit )
            {
                Console.Write("Invalid entry, try again: ");
            }

            return count;
        }

        // "Hit enter to coninue" - get user response
        public void GetKey()
        {
            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
        }

        public string UserL
        {
            set { user = value; }
            get { return user; }
        }
    }
}
