using System;
using System.Collections.Generic;
using System.Text;

namespace Reservatie
{
    class Info
    {
        
        public static void Reservering(string[] args)
        {
            Console.WriteLine("Kies een genre uit de volgende lijst:");
            Console.WriteLine("- Action");
            Console.WriteLine("- Family");
            Console.WriteLine("- Comedy");
            Console.WriteLine("- Sci-Fi");
            Console.WriteLine("- Romantic");
            Console.WriteLine("- Thriller");
            Console.WriteLine("Type hier de uitgekozen genre in en toets dan op ENTER: ");
            string genre = Console.ReadLine();
            if(genre == "Action"){
                Console.WriteLine("De volgende Action films worden actueel getoond:");
            }
        }
    }
}


