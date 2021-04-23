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
using StartScherm;

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
                Console.ForegroundColor = ConsoleColor.Green;
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
                Console.ForegroundColor = ConsoleColor.Red;
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
        public void Genre(string Genre_select)
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
        }
    }
    public class JsonData
    {

        public string Naam { get; set; }
        public string Email { get; set; }
        public string Reservatie_code { get; set; }
        public string Film { get; set; }
        public string Zaal { get; set; }
        public string Stoel_num { get; set; }




    }
    public class Program
    {
        static void Main(string[] args)
        {
            StartScherm.StartScherm.Main();
        }
    }
}