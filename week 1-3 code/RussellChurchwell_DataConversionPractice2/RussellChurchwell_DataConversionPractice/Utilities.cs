using System;
using System.Text.RegularExpressions;

namespace RussellChurchwell_DataConversionPractice
{
    public class Utilities
    {
        public Utilities(){}

        // Print main menu
        public static void PrintMenu()
        {
            Console.Clear();

            Console.WriteLine("1: Convert SQL to JSON");
            Console.WriteLine("2: Showcase Our 5 Star Rating System");
            Console.WriteLine("3: Showcase Our Animated Bar Graph Review System( Coming Soon! )");
            Console.WriteLine("4: Play A Card Game( Coming Soon! )");


            Console.WriteLine("\n5: Exit\n");
            Console.Write("Choice: ");
        }

        // Print sort menu
        public static void PrintSortMenu()
        {
            Console.Clear();
            Console.WriteLine("How would you like to sort?\n");

            Console.WriteLine("1: A-Z Sort");
            Console.WriteLine("2: Z-A Sort");
            Console.WriteLine("3: Best To Worst Sort");
            Console.WriteLine("4: Worst To Best Sort");
            Console.WriteLine("5: X Sort\n");

            Console.Write("Choice: ");
        }

        // Print sort sub menu
        public static void PrintSortSubMenu()
        {
            Console.Clear();
            Console.WriteLine("How would you like to sort?\n");

            Console.WriteLine("1: 5 Stars");
            Console.WriteLine("2: 4 Stars");
            Console.WriteLine("3: 3 Stars");
            Console.WriteLine("4: 2 Stars");
            Console.WriteLine("5: 1 Star");
            Console.WriteLine("6: Unrated\n");

            Console.Write("Choice: ");
        }

        // Pull menu choice int
        public int GetIntMenu()
        {
            int menu;

            // Choice has to be between 1-5
            while(!int.TryParse(Console.ReadLine(), out menu) || menu<1 || menu>5)
            {
                Console.Write("Invalid entry, try again: ");
            }

            return menu;
        }

        // Pull sub menu choice
        public int GetIntSubMenu()
        {
            int count;

            // Choice has to be between 1-6
            while (!int.TryParse(Console.ReadLine(), out count) || count < 1 || count > 6 )
            {
                Console.Write("Invalid entry, try again: ");
            }

            return count;
        }

        // Prompt to get key press
        public void GetKey()
        {
            Console.WriteLine("\nPress enter to return to menu.");
            Console.ReadKey();
        }
    }
}
