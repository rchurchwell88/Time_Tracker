using System;
using System.Text.RegularExpressions;

namespace RussellChurchwell_DataConversionPractice
{
    public class Utilities
    {
        public Utilities(){}

        public static void PrintMenu()
        {
            Console.Clear();

            Console.WriteLine("1: Convert SQL to JSON");
            Console.WriteLine("2: Showcase Our 5 Star Rating System( Coming Soon! )");
            Console.WriteLine("3: Showcase Our Animated Bar Graph Review System( Coming Soon! )");
            Console.WriteLine("4: Play A Card Game( Coming Soon! )");


            Console.WriteLine("\n5: Exit\n");
            Console.Write("Choice: ");
        }

        public int GetIntMenu()
        {
            int menu;

            while(!int.TryParse(Console.ReadLine(), out menu) || menu<1 || menu>5)
            {
                Console.Write("Invalid entry, try again: ");
            }

            return menu;
        }

        public int GetIntExp(int limit)
        {
            int exp;

            while (!int.TryParse(Console.ReadLine(), out exp) || exp > limit || exp < 1 )
            {
                Console.Write("Invalid entry, try again: ");
            }

            return exp;
        }

        public int GetIntChips()
        {
            int exp;

            while (!int.TryParse(Console.ReadLine(), out exp) || exp < 1)
            {
                Console.Write("Invalid entry, try again: ");
            }

            return exp;
        }

        public int GetIntCount()
        {
            int count;

            while (!int.TryParse(Console.ReadLine(), out count) || count < 1 )
            {
                Console.Write("Invalid entry, try again: ");
            }

            return count;
        }

        public void GetKey()
        {
            Console.WriteLine("\nPress enter to return to menu.");
            Console.ReadKey();
        }

        public string GetName()
        {
            string name=Console.ReadLine();
            string pattern = @"^[a-zA-z ]+$";
            name = name.Trim();

            while (!Regex.IsMatch(name, pattern))
            {
                Console.Write("Invalid entry, try again: ");
                name = Console.ReadLine();
                name = name.Trim();
            }

            return name;
        }

        public float GetFloatCount()
        {
            float count;

            while (!float.TryParse(Console.ReadLine(), out count) || count < 1)
            {
                Console.Write("Invalid entry, try again: ");
            }

            return count;
        }

        public decimal GetDecimalCount()
        {
            decimal count;

            while (!decimal.TryParse(Console.ReadLine(), out count) || count < 0)
            {
                Console.Write("Invalid entry, try again: ");
            }

            return count;
        }

        public int GetIntEmployee()
        {
            int menu;

            while (!int.TryParse(Console.ReadLine(), out menu) || menu < 1)
            {
                Console.Write("Invalid entry, try again: ");
            }

            return menu;
        }
    }
}
