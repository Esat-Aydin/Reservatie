using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Reservatie
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                String Json = File.ReadAllText("C:\\Genre.json");
                Console.WriteLine(Json);
            }
            catch ( Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Console.Read();
        }
    }

}
