using System;
using System.Text.RegularExpressions;

namespace ChurchwellRussell_SprintApp

{
    public class Utilities
    {
        public Utilities(){}


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

        // Print main menu
        public void PrintMenu()
        {
            Console.WriteLine("1: Show data organized");
            Console.WriteLine("2: Input new tasks/times");
            Console.WriteLine("3: Run calculations\n");
            Console.WriteLine("4: Exit");

            Console.Write("\nChoice: ");
        }
    }
}
