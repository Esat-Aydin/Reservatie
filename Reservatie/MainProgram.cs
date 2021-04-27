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
using Scherm;

namespace Cinema
{

    public class ConsoleCommands
    {
        public string UserInput;
        public string Genre_search;
        public ConsoleCommands(string UserInput = null)
        {
            this.UserInput = UserInput;
        }
        public static void Textkleur(string kleur)
        {
            if (kleur == "groen")
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (kleur == "zwart")
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (kleur == "wit")
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (kleur == "rood")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
        }
        public void RestartOption()
        {

            Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            Textkleur("wit");
            Console.WriteLine("Toets 'R' om het progamma opnieuw op te starten.");
            Textkleur("zwart");
            string restart = Console.ReadLine();
            if (restart.ToUpper() == "R")
            {
                Console.Clear();
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Environment.Exit(1);
            }
        }
        public string Genre(string Genre_select)
        {


            if (Genre_select == "1")
            {
                Genre_search = "Action";
            }
            else if (Genre_select == "2")
            {
                Genre_search = "Comedy";
            }
            else if (Genre_select == "3")
            {
                Genre_search = "Thriller";
            }
            else if (Genre_select == "4")
            {
                Genre_search = "Romantiek";
            }
            else if (Genre_select == "5")
            {
                Genre_search = "Drama";
            }
            else if (Genre_select == "6")
            {
                Genre_search = "Sci-Fi";
            }
            else if (Genre_select == "7")
            {
                Genre_search = "Familie";
            }
            return Genre_search;
        }
    }
    public class JsonData
    {

        public string Naam { get; set; }
        public string Email { get; set; }
        public string Reservatie_code { get; set; }
        public string Film { get; set; }
        public string Film_Day { get; set; }
        public string FilmTime { get; set; }
        public string Zaal { get; set; }
        public string Stoel_num { get; set; }


        public static dynamic JsonSerializer(string Object)
        {

            var MyFilmsData = new WebClient().DownloadString(@"C:\Users\woute\Source\Repos\Esat-Aydin\Reservatie\Reservatie\Filmsdata.json");
            string myJsonString = new WebClient().DownloadString(@"C:\Users\woute\source\repos\Esat-Aydin\Reservatie\Reservatie\snacksdrinks.json");
            string myUserData = new WebClient().DownloadString(@"C:\Users\woute\source\repos\Esat-Aydin\Reservatie\Reservatie\SampleLog.json");

            // Omzetten
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            dynamic DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            dynamic DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);
            
            if(Object == "Films")
            {
                return DynamicFilmData;
            }
            else if(Object == "Snacks")
            {
                return DynamicData;
            }
            else
            {
                return DynamicUserData;
            }
        }

    }


    public class Program
    {
        static void Main(string[] args)
        {
            Scherm.Screens.AdminOrUserScreen();
        }
    }
}