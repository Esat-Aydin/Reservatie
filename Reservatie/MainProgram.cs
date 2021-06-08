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
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
        }
        public void RestartOption()
        {
                Console.Clear();
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Environment.Exit(1);
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
                Genre_search = "Romantic";
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
                Genre_search = "Family";
            }
            else if(Genre_select == "8")
            {
                Genre_search = "Horror";
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
        public string FilmPrice { get; set; }
        public string FilmDate { get; set; }
        public string Film_Day { get; set; }
        public string FilmTime { get; set; }
        public string Zaal { get; set; }
        public string[] Stoel_num { get; set; }


        public static dynamic JsonSerializer(string Object)
        {
            string FullPathSeats = Path.GetFullPath(@"Stoelkeuze.json");
            string FullPathFilms = Path.GetFullPath(@"Filmsdata.json");
            string FullPathSnacksDrinks = Path.GetFullPath(@"snacksdrinks.json");
            string FullPathsReservations = Path.GetFullPath(@"SampleLog.json");


            var MyFilmsData = new WebClient().DownloadString(FullPathFilms);
            var myJsonString = new WebClient().DownloadString(FullPathSnacksDrinks);
            var myUserData = new WebClient().DownloadString(FullPathsReservations);
            var myRoomData = new WebClient().DownloadString(FullPathSeats);



            // Omzetten
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            dynamic DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            dynamic DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);
            dynamic DynamicSeatsData = JsonConvert.DeserializeObject(myRoomData);
            
            if(Object == "Films")
            {
                return DynamicFilmData;
            }
            else if(Object == "Snacks")
            {
                return DynamicData;
            }
            else if(Object == "Seats")
            {
                return DynamicSeatsData;
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
            //Console.SetWindowSize(120,40);
            Scherm.Screens.AdminOrUserScreen();
        }
    }
}