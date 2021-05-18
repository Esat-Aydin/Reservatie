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
using Cinema;
using Film;
using Scherm;
using Reservation;
using System.Globalization;
using Gebruiker;

namespace ShoppingCart
{
    class Cart
    {

        public static void WinkelMandje(dynamic DynamicData, List<string> Mandje, Gebruiker.Gebruiker Klant)
        {
            Gebruiker.Gebruiker Object = new Gebruiker.Gebruiker();
            ConsoleCommands CommandLine = new ConsoleCommands();
            //Json file met alle snacks.
            Scherm.Screens.CinemaBanner();
            ConsoleCommands.Textkleur("wit");
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] Snacks"); Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("]Dranken\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            string SnackorDrinks = Console.ReadLine();
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

            var tableDranken = new ConsoleTable("Dranken", "Prijs");
            var table = new ConsoleTable("Snacks", "Prijs");
            if (SnackorDrinks == "1")
            {
                for (int i = 0; i < DynamicData.snacks.Count; i++)
                {
                    table.AddRow("Toets [" + (i + 1) + "] " + DynamicData.snacks[i].Name, DynamicData.snacks[i].Price);
                }
                table.Write(Format.Alternative);
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                var SnacksKeuze = Console.ReadLine();
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                if (SnacksKeuze == "1")
                {
                    Mandje.Add(DynamicData.snacks[0].Name.ToString());
                    Mandje.Add(DynamicData.snacks[0].Price.ToString());
                }
                if (SnacksKeuze == "2")
                {
                    Mandje.Add(DynamicData.snacks[1].Name.ToString());
                    Mandje.Add(DynamicData.snacks[1].Price.ToString());
                }
                if (SnacksKeuze == "3")
                {
                    Mandje.Add(DynamicData.snacks[2].Name.ToString());
                    Mandje.Add(DynamicData.snacks[2].Price.ToString());
                }
                if (SnacksKeuze == "4")
                {
                    Mandje.Add(DynamicData.snacks[3].Name.ToString());
                    Mandje.Add(DynamicData.snacks[3].Price.ToString());
                }
                if (SnacksKeuze == "5")
                {
                    Mandje.Add(DynamicData.snacks[4].Name.ToString());
                    Mandje.Add(DynamicData.snacks[4].Price.ToString());
                }
                if (SnacksKeuze == "6")
                {
                    Mandje.Add(DynamicData.snacks[5].Name.ToString());
                    Mandje.Add(DynamicData.snacks[5].Price.ToString());
                }
                if (SnacksKeuze == "7")
                {
                    Mandje.Add(DynamicData.snacks[6].Name.ToString());
                    Mandje.Add(DynamicData.snacks[6].Price.ToString());
                }
            }

            if (SnackorDrinks == "2")
            {
                for (int i = 0; i < DynamicData.dranken.Count; i++)
                {
                    tableDranken.AddRow("Toets [" + (i + 1) + "] " + DynamicData.dranken[i].Name, DynamicData.dranken[i].Price);

                }
                tableDranken.Write(Format.Alternative);
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                var DrankenKeuze = Console.ReadLine();
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                if (DrankenKeuze == "1")
                {
                    Mandje.Add(DynamicData.dranken[0].Name.ToString());
                    Mandje.Add(DynamicData.dranken[0].Price.ToString());
                }
                if (DrankenKeuze == "2")
                {
                    Mandje.Add(DynamicData.dranken[1].Name.ToString());
                    Mandje.Add(DynamicData.dranken[1].Price.ToString());
                }
                if (DrankenKeuze == "3")
                {
                    Mandje.Add(DynamicData.dranken[2].Name.ToString());
                    Mandje.Add(DynamicData.dranken[2].Price.ToString());
                }
                if (DrankenKeuze == "4")
                {
                    Mandje.Add(DynamicData.dranken[3].Name.ToString());
                    Mandje.Add(DynamicData.dranken[3].Price.ToString());
                }
                if (DrankenKeuze == "5")
                {
                    Mandje.Add(DynamicData.dranken[4].Name.ToString());
                    Mandje.Add(DynamicData.dranken[4].Price.ToString());
                }
                if (DrankenKeuze == "6")
                {
                    Mandje.Add(DynamicData.dranken[5].Name.ToString());
                    Mandje.Add(DynamicData.dranken[5].Price.ToString());
                }
                if (DrankenKeuze == "7")
                {
                    Mandje.Add(DynamicData.dranken[6].Name.ToString());
                    Mandje.Add(DynamicData.dranken[6].Price.ToString());
                }
                if (DrankenKeuze == "8")
                {
                    Mandje.Add(DynamicData.dranken[7].Name.ToString());
                    Mandje.Add(DynamicData.dranken[7].Price.ToString());
                }
                if (DrankenKeuze == "9")
                {
                    Mandje.Add(DynamicData.dranken[8].Name.ToString());
                    Mandje.Add(DynamicData.dranken[8].Price.ToString());
                }
                if (DrankenKeuze == "10")
                {
                    Mandje.Add(DynamicData.dranken[9].Name.ToString());
                    Mandje.Add(DynamicData.dranken[9].Price.ToString());
                }
                if (DrankenKeuze == "11")
                {
                    Mandje.Add(DynamicData.dranken[10].Name.ToString());
                    Mandje.Add(DynamicData.dranken[10].Price.ToString());
                }

                if (DrankenKeuze == "12")
                {
                    Mandje.Add(DynamicData.dranken[11].Name.ToString());
                    Mandje.Add(DynamicData.dranken[11].Price.ToString());
                }

            }
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] Meer bestellen"); Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("] Verder gaan\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            string Meerbestellen = Console.ReadLine();
            decimal totaal = 0;
            for (int i = 1; i < Mandje.Count; i += 2)
            {
                totaal += Convert.ToDecimal(Mandje[i], new CultureInfo("en-US"));
            }
            
            if (Meerbestellen == "1")
            {

                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                WinkelMandje(DynamicData, Mandje, Klant);
            }

            if (Meerbestellen == "2")
            {
                Console.Clear();
                Gebruiker.Gebruiker.Betaling(Klant,totaal, Mandje);
            }

        }
    }
}
