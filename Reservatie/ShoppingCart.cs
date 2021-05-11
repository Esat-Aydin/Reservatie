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
using Gebruiker;

namespace ShoppingCart
{
    class Cart
    {

        public static void WinkelMandje(dynamic DynamicData, List<Object> Mandje)
        {

            ConsoleCommands CommandLine = new ConsoleCommands();
            //Json file met alle snacks.
            Scherm.Screens.CinemaBanner();
            ConsoleCommands.Textkleur("wit");
            Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] om een lijst van snacks te zien"); Console.Write("\nToets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("] om een lijst van dranken te zien\n");
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
                    Mandje.Add(DynamicData.snacks[0].Name);
                    Mandje.Add(DynamicData.snacks[0].Price);
                }
                if (SnacksKeuze == "2")
                {
                    Mandje.Add(DynamicData.snacks[1].Name);
                    Mandje.Add(DynamicData.snacks[1].Price);
                }
                if (SnacksKeuze == "3")
                {
                    Mandje.Add(DynamicData.snacks[2].Name);
                    Mandje.Add(DynamicData.snacks[2].Price);
                }
                if (SnacksKeuze == "4")
                {
                    Mandje.Add(DynamicData.snacks[3].Name);
                    Mandje.Add(DynamicData.snacks[3].Price);
                }
                if (SnacksKeuze == "5")
                {
                    Mandje.Add(DynamicData.snacks[4].Name);
                    Mandje.Add(DynamicData.snacks[4].Price);
                }
                if (SnacksKeuze == "6")
                {
                    Mandje.Add(DynamicData.snacks[5].Name);
                    Mandje.Add(DynamicData.snacks[5].Price);
                }
                if (SnacksKeuze == "7")
                {
                    Mandje.Add(DynamicData.snacks[6].Name);
                    Mandje.Add(DynamicData.snacks[6].Price);
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
                    Mandje.Add(DynamicData.dranken[0].Name);
                    Mandje.Add(DynamicData.dranken[0].Price);
                }
                if (DrankenKeuze == "2")
                {
                    Mandje.Add(DynamicData.dranken[1].Name);
                    Mandje.Add(DynamicData.dranken[1].Price);
                }
                if (DrankenKeuze == "3")
                {
                    Mandje.Add(DynamicData.dranken[2].Name);
                    Mandje.Add(DynamicData.dranken[2].Price);
                }
                if (DrankenKeuze == "4")
                {
                    Mandje.Add(DynamicData.dranken[3].Name);
                    Mandje.Add(DynamicData.dranken[3].Price);
                }
                if (DrankenKeuze == "5")
                {
                    Mandje.Add(DynamicData.dranken[4].Name);
                    Mandje.Add(DynamicData.dranken[4].Price);
                }
                if (DrankenKeuze == "6")
                {
                    Mandje.Add(DynamicData.dranken[5].Name);
                    Mandje.Add(DynamicData.dranken[5].Price);
                }
                if (DrankenKeuze == "7")
                {
                    Mandje.Add(DynamicData.dranken[6].Name);
                    Mandje.Add(DynamicData.dranken[6].Price);
                }
                if (DrankenKeuze == "8")
                {
                    Mandje.Add(DynamicData.dranken[7].Name);
                    Mandje.Add(DynamicData.dranken[7].Price);
                }
                if (DrankenKeuze == "9")
                {
                    Mandje.Add(DynamicData.dranken[8].Name);
                    Mandje.Add(DynamicData.dranken[8].Price);
                }
                if (DrankenKeuze == "10")
                {
                    Mandje.Add(DynamicData.dranken[9].Name);
                    Mandje.Add(DynamicData.dranken[9].Price);
                }
                if (DrankenKeuze == "11")
                {
                    Mandje.Add(DynamicData.dranken[10].Name);
                    Mandje.Add(DynamicData.dranken[10].Price);
                }

                if (DrankenKeuze == "12")
                {
                    Mandje.Add(DynamicData.dranken[11].Name);
                    Mandje.Add(DynamicData.dranken[11].Price);
                }

            }
            Console.WriteLine("Zou u nog iets anders willen bestellen?\nToets [1] voor ja\nToets [2] voor nee.");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            string Meerbestellen = Console.ReadLine();

            if (Meerbestellen == "1")
            {

                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                WinkelMandje(DynamicData, Mandje);
            }

            if (Meerbestellen == "2")
            {

                Console.Clear();
                //Eventuele check out methode
            }

        }
    }
}
