using System;
using System.Text.RegularExpressions;

namespace RussellChurchwell_CE09

{
    public class Utilities
    {
        public Utilities()
        {
        }

        public static void PrintMenu(Customer customer)
        {
            Console.Clear();

            if(customer==null)
                Console.WriteLine("Current Customer: No Customer Selected\n");
            else
                Console.WriteLine($"Current Customer: {customer.Name}\n");

            Console.WriteLine("1: Add Customer");
            Console.WriteLine("2: Select Customer");
            Console.WriteLine("3: Display Inventory");
            Console.WriteLine("4: Add Item To Cart For Current Customer");
            Console.WriteLine("5: Remove Item From Cart For Current Customer");
            Console.WriteLine("6: Display Cart");
            Console.WriteLine("7: Check Out\n");
            Console.WriteLine("8: Exit\n");
            Console.Write("Choice: ");
        }

        public int GetIntMenu()
        {
            int menu;

            while(!int.TryParse(Console.ReadLine(), out menu) || menu<1 || menu>8)
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
            Console.WriteLine("\nPress any key to return to menu.");
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
