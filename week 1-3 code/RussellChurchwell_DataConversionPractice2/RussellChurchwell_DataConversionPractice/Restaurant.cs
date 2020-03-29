// Class data object for restaurant

using System;
namespace RussellChurchwell_DataConversionPractice
{

    public class Restaurant
    {
        // Local vars
        private string name;
        private decimal rating;

        public Restaurant(string _name, decimal _rating)
        {
            // Constructor assignments
            name = _name;
            rating = _rating;
        }

        // Get var name
        public string Name
        {
            get
            {
                return name;
            }
        }

        // Get var rating
        public decimal Rating
        {
            get
            {
                return rating;
            }
        }

        public ConsoleColor Color(char num)
        {
            if (num == '0')
                return ConsoleColor.Red;
            else if(num == '1')
                return ConsoleColor.Red;
            else if(num == '2')
                return ConsoleColor.Red;

            else if (num == '3')
                return ConsoleColor.Yellow;
            else
                return ConsoleColor.Green;
        }

        public ConsoleColor Color(string num)
        {
            if (num == "0")
                return ConsoleColor.Red;
            else if (num == "1")
                return ConsoleColor.Red;
            else if (num == "2")
                return ConsoleColor.Red;

            else if (num == "3")
                return ConsoleColor.Yellow;
            else
                return ConsoleColor.Green;
        }

    }
}
