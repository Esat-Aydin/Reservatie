using System;
using System.IO;
using System.Linq;
using System.Threading;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using ConsoleTables;
using Gebruiker;
using Film;
using MedewerkerClass;
using Cinema;

namespace StartScherm

{
    public class StartScherm
    {
        public static void Main(){

            // Voor de creation van de objects
            string[] FilmGenresArray = new string[3];
            FilmGenresArray[0] = "Action";
            FilmGenresArray[1] = "Comedy";
            FilmGenresArray[2] = "Thriller";
            string[] FilmTimesArray = new string[1];
            FilmTimesArray[0] = "12:00";
            int RoomofFilm = 3;
            string TitleofFilm = "John Wick";
            string DefaultAdmin_Password = "admin";

            // Objects
            Film.Film FilmObject = new Film.Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray);
            Medewerker admin = new Medewerker(null, DefaultAdmin_Password);
            Gebruiker.Gebruiker Klant = new Gebruiker.Gebruiker();
            ConsoleCommands CommandLine = new ConsoleCommands();
            //Startpagina
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();

            Console.Write(@"
   _____ _                            _____                                _   _             
  / ____(_)                          |  __ \                              | | (_)            
 | |     _ _ __   ___ _ __ ___   __ _| |__) |___  ___  ___ _ ____   ____ _| |_ _  ___  _ __  
 | |    | | '_ \ / _ \ '_ ` _ \ / _` |  _  // _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \ 
 | |____| | | | |  __/ | | | | | (_| | | \ \  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | |
  \_____|_|_| |_|\___|_| |_| |_|\__,_|_|  \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_|");
            Console.ForegroundColor = ConsoleColor.Gray;
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("\n---------------------------------------------------------------------------------------------");
            Console.WriteLine("\t\t\t\tWelkom bij CinemaReservation!\t\t\t\t    |");
            Console.WriteLine("---------------------------------------------------------------------------------------------\n");

            Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] Zoeken op genre\n\n");
            Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("] Zoek een film\n\n");
            Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("3"); ConsoleCommands.Textkleur("wit"); Console.Write("] Reservering bekijken\n\n");
            Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("4"); ConsoleCommands.Textkleur("wit"); Console.Write("] Inloggen als bioscoop medewerker\n\n");
            Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("5"); ConsoleCommands.Textkleur("wit"); Console.Write("] Account registreren\n\n");
            Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("6"); ConsoleCommands.Textkleur("wit"); Console.Write("] Lijst met alle films bekijken.\n");

            ConsoleCommands.Textkleur("wit"); Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Black;

            var Start_options = Console.ReadLine();
            bool isErrorPrinted = false;

            while (Start_options != "1" && Start_options != "2" && Start_options != "3" && Start_options != "4" && Start_options != "5" && Start_options != "6")
            {
                if (isErrorPrinted == false)
                {
                    ConsoleCommands.Textkleur("rood");
                    Console.Write("ERROR: "); ConsoleCommands.Textkleur("wit");
                    Console.Write("Verkeerde input! Probeer het nogmaals met een van de zwartgekleurde nummers als input.\n");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("---------------------------------------------------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Black;
                    isErrorPrinted = true;
                }

                Console.ForegroundColor = ConsoleColor.Black;
                Start_options = Console.ReadLine();
                Klant.UserInputMethod(Start_options);
            }

            Klant.UserInputMethod(Start_options);
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("---------------------------------------------------------------------------------------------");






        }
    }
}
