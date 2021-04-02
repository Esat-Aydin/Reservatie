using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Reservatie
{
    class Json_Genres
    {
        public string film { get; set; }
        public string room { get; set; }
        public string premiere_date { get; set; }
        public string end_date { get; set; }

    }
    static class Genre { public static int Genre_search;
                         public static int Film_choice;
    }

    class Program
    {
        public static void Main(string[] args)
        {

            try
            {
                String Json_Data = File.ReadAllText("C:\\Genre.json");
                dynamic Dynamic_Data = JsonConvert.DeserializeObject(Json_Data);

                //Console.WriteLine(Json);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                //Console.WriteLine(e.Message);
            }
            //Console.Read();
            Console.WriteLine("Welkom op de startpagina van de bioscoop.");
            Console.WriteLine("Selecteer '1' om te zoeken op genre.");
            Console.WriteLine("Selecteer '2' om te zoeken op een specifieke film.");
            Console.WriteLine("Selecteer '3' om uw reservering te bekijken.");
            var Start_options = Console.ReadLine();

            if (Start_options == "1")
            {
                Console.Write("Op welke genre wilt u zoeken: ");
                Genre.Genre_search = Convert.ToInt32(Console.ReadLine());
                if (Genre.Genre_search == 0)
                {
                    Titel(Dynamic_Data);
                    Console.WriteLine("Kies welke film u wilt bekijken");
                    Genre.Film_choice = Convert.ToInt32(Console.ReadLine());
                    if (Genre.Film_choice == 0)
                    {
                        Titel(Dynamic_Data);
                    }
                }
            }
        }
        public static void Titel(dynamic Dynamic_Data)
        {
            if (Genre.Genre_search == 0)
            {
                {
                    Console.WriteLine(Dynamic_Data.Action.Titel[0].Key);
                }
                if (Genre.Film_choice == 0)
                {
                    Console.WriteLine(Dynamic_Data.Action.Titel[0].Value);
                }
            }

        }
    }
}
