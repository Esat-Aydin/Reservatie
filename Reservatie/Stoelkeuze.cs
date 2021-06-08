﻿using System;
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

namespace Chair
{
    public class StoelKeuze : Reserveren
    {
        public string FilmNaam;
        public string Datum;
        public string Tijd;
        public dynamic DynamicRoomData;
        public dynamic DynamicFilmData;
        public dynamic DynamicUserData;
        public string[] Alphabet;
        public string[][] AllData;
        public string[] chairsTaken;


        public StoelKeuze(string Naam, string datum = null, string tijd = null)
        {
            this.FilmNaam = Naam;
            this.Datum = datum;
            this.Tijd = tijd;
            string FullPathFilms = Path.GetFullPath(@"Filmsdata.json");
            string FullPathsReservations = Path.GetFullPath(@"samplelog.json");
            string FullPathSeats = Path.GetFullPath(@"Stoelkeuze.json");
            var MyFilmsData = new WebClient().DownloadString(FullPathFilms);
            string myUserData = new WebClient().DownloadString(FullPathsReservations);
            this.DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);
            this.DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            string myRoomData = new WebClient().DownloadString(FullPathSeats);


            this.DynamicRoomData = JsonConvert.DeserializeObject(myRoomData);
            this.Alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        }
        public void console(dynamic room, string[][] AllData)
        {
            if (room == 1)
            {
                var table = new ConsoleTable("", "1 ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10", "11", "12", "13", "14", "15");
                for (int i = 0; i < this.DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count; i++)
                {
                    table.AddRow(this.Alphabet[i], AllData[i][0], AllData[i][1], AllData[i][2], AllData[i][3], AllData[i][4], AllData[i][5], AllData[i][6], AllData[i][7], AllData[i][8], AllData[i][9], AllData[i][10], AllData[i][11], AllData[i][12], AllData[i][13], AllData[i][14]);
                }
                Scherm.Screens.CinemaBanner();
                table.Write(Format.Alternative);
                Console.WriteLine("    'X' = al gereserveerde stoelen.                'V' = uw gekozen stoelen.");
                Console.WriteLine("_____________________________________________________________________________________________\n");
            }
            else if (room == 2)
            {
                var table = new ConsoleTable("", "1 ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20");
                for (int i = 0; i < this.DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count; i++)
                {
                    table.AddRow(this.Alphabet[i], AllData[i][0], AllData[i][1], AllData[i][2], AllData[i][3], AllData[i][4], AllData[i][5], AllData[i][6], AllData[i][7], AllData[i][8], AllData[i][9], AllData[i][10], AllData[i][11], AllData[i][12], AllData[i][13], AllData[i][14], AllData[i][15], AllData[i][16], AllData[i][17], AllData[i][18], AllData[i][19]);
                }
                Scherm.Screens.CinemaBanner();
                table.Write(Format.Alternative);
                Console.WriteLine("    'X' = al gereserveerde stoelen.                'V' = uw gekozen stoelen.");
                Console.WriteLine("_____________________________________________________________________________________________\n");
            }
            else
            {
                var table = new ConsoleTable("", "1 ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25");
                for (int i = 0; i < this.DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count; i++)
                {
                    table.AddRow(this.Alphabet[i], AllData[i][0], AllData[i][1], AllData[i][2], AllData[i][3], AllData[i][4], AllData[i][5], AllData[i][6], AllData[i][7], AllData[i][8], AllData[i][9], AllData[i][10], AllData[i][11], AllData[i][12], AllData[i][13], AllData[i][14], AllData[i][15], AllData[i][16], AllData[i][17], AllData[i][18], AllData[i][19], AllData[i][20], AllData[i][21], AllData[i][22], AllData[i][23], AllData[i][24]);
                }
                Scherm.Screens.CinemaBanner();
                table.Write(Format.Alternative);
                Console.WriteLine("    'X' = al gereserveerde stoelen.                'V' = uw gekozen stoelen.");
                Console.WriteLine("_____________________________________________________________________________________________\n");
            }

        }
        public string[][]  NewAllData(string[][] old)
        {
            string[][] newdata = new string[old.Length][];
            for(int p = 0; p < newdata.Length; p++)
            {
                newdata[p] = new string[old[p].Length];

            }
            for(int j = 0; j < newdata.Length; j++)
            {
                for(int p = 0; p < newdata[j].Length; p++)
                {
                    newdata[j][p] = old[j][p];
                }
            }
            return newdata;
        }

        public Tuple<string[], int, string[][]> RecommendationChairs(string input, int howMany, string[] newChairs, dynamic room)
        {
            string[][] newData = NewAllData(this.AllData);

            if(input.Length == 2)
            {
                for (int i = 0; i < howMany; i++)
                {
                    newChairs[i] = "" + (Convert.ToInt32("" + input[0]) + i) + input[1];
                    if (this.chairsTaken.Contains(newChairs[i]))
                    {
                        newChairs[i] = "";
                        i--;
                        return Tuple.Create(newChairs, i, newData);
                    }
                    newData = changeAllData(newData, newChairs[i], "V");
                }
            }
            else
            {
                for (int i = 0; i < howMany; i++)
                {
                    newChairs[i] = "" + (Convert.ToInt32("" + input[0] + input[1]) + i) + input[2];
                    if (this.chairsTaken.Contains(newChairs[i]))
                    {
                        newChairs[i] = "";
                        i--;
                        return Tuple.Create(newChairs, i, newData);
                    }
                    newData = changeAllData(newData, newChairs[i], "V");
                }
            }
            /*
            for (int i = 0; i < howMany; i++)
            {
                newData = changeAllData(newData, newChairs[i],"-");
            }*/

            console(room, newData);
            string newinput;
            Console.WriteLine("_____________________________________________________________________________________________\n");
            Screens.KeuzeLine(1, "Automatisch aangeraden stoelen kiezen");
            Screens.KeuzeLine(2, "Zelf handmatig stoelen kiezen");
            Screens.KeuzeLine(3, "Opnieuw een stoel uitkiezen\n");
            Console.WriteLine("_____________________________________________________________________________________________\n");
            while (true)
            {
                ConsoleCommands.Textkleur("zwart");
                newinput = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                if (newinput == "1")
                {
                    return Tuple.Create(newChairs, howMany, newData);
                }
                else if (newinput == "2")
                {
                    string [][] aData = NewAllData(this.AllData);
                    aData = changeAllData(aData, newChairs[0],"V");
                    return Tuple.Create(newChairs, 0, aData);
                }
                else if (newinput == "3")
                {
                    for(int q = 0; q < newChairs.Length; q++)
                    {
                        newChairs[q] = "";
                    }
                    return Tuple.Create(newChairs,-1,this.AllData);
                }
                else
                {
                    Screens.ErrorMessageInput();
                }
            }
            
        }
        public string[] WhatChairs(int howMany, dynamic room)
        {
            string[][] ding = AllData;
            var DynNumbers = this.DynamicRoomData[(Convert.ToInt32("" + room)) - 1]["seat_number"];
            var DynLetters = this.DynamicRoomData[(Convert.ToInt32("" + room)) - 1]["row_number"];
            int[] Numbers = new int[DynNumbers.Count];
            string[] Letters = new string[DynLetters.Count];
            for(int n = 0; n < Numbers.Length; n++)
            {
                Numbers[n] = DynNumbers[n];
            }
            for (int l = 0; l < Letters.Length; l++)
            {
                Letters[l] = DynLetters[l];
            }
            string[] newChairs = new string[howMany];
            string input = "";
            string differentinput = "";
            for(int i = 0; i < howMany; i++)
            {
                ConsoleCommands.Textkleur("zwart");
                input = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                input = input.ToUpper();
                if(input.Length == 2)

                {
                    differentinput = "" + input[1] + input[0];
                }
                else if(input.Length == 3)
                {
                    differentinput = "" + input[1] + input[2] + input[0];
                }
                if (this.chairsTaken.Contains(input)|| this.chairsTaken.Contains(differentinput))

                {
                    Screens.CustomError("Deze stoelen zijn al gereservereerd");
                    i--;
                }

                else if (newChairs.Contains(differentinput)|| newChairs.Contains(input))
                {
                    Screens.CustomError("U heeft die stoel al gekozen!");
                    i--;
                }
                else if (input.Length == 1)
                {
                    Screens.CustomError("Verkeerde input. Kies een rij (bijv. 1) en een stoel (bijv. A): 1A");
                    i--;
                }
                else if (input.Length == 2)
                {
                    if((Letters.Contains("" + input[0]))&& (Letters.Contains("" + input[1])))
                    {
                        Screens.CustomError("Verkeerde input. Kies een rij (bijv. 1) en een stoel (bijv. A): 1A");
                        i--;
                    }
                    else if (Letters.Contains("" + input[0]) && Numbers.Contains(Convert.ToInt32("" + input[1])))
                    {
                        differentinput = "" + input[1] + input[0];
                        newChairs[i] = differentinput;
                        if (i == 0)
                        {
                            var newTuple = RecommendationChairs(differentinput, howMany, newChairs, room);
                            if (newTuple.Item2 == howMany)
                            {
                                return newTuple.Item1;
                            }
                            else if (newTuple.Item2 == 0)
                            {
                                newChairs = newTuple.Item1;
                                for (int t = 1; t < newChairs.Length; t++)
                                {
                                    newChairs[t] = "";
                                }
                                i = newTuple.Item2;
                                ding = newTuple.Item3;
                            }
                            else if(newTuple.Item2 == -1)
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                ding = NewAllData(this.AllData);
                            }
                            else
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                foreach(var A in newChairs)
                                {
                                    if(A != "")
                                    {
                                        ding = changeAllData(ding, A, "V");
                                    }
                                }
                            }
                            console(room, ding);
                        }
                        else
                        {
                            ding = changeAllData(ding,newChairs[i], "V");
                            console(room, ding);
                        }
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Type nu de volgende stoel in");
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else if (Letters.Contains("" + input[1]) && Numbers.Contains(Convert.ToInt32("" + input[0]))){
                        newChairs[i] = input;
                        Console.WriteLine(newChairs[i]);
                        if (i == 0)
                        {
                            var newTuple = RecommendationChairs(input, howMany, newChairs, room);
                            if(newTuple.Item2 == howMany)
                            {
                                return newTuple.Item1;
                            }
                            else if(newTuple.Item2 == 0)
                            {
                                newChairs = newTuple.Item1;
                                for(int t = 1; t < newChairs.Length; t++)
                                {
                                    newChairs[t] = "";
                                }
                                i = newTuple.Item2;
                                ding = newTuple.Item3;
                            }
                            else if(newTuple.Item2 == -1)
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                ding = NewAllData(this.AllData);
                            }
                            else
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                foreach (var A in newChairs)
                                {
                                    if (A != "")
                                    {
                                        ding = changeAllData(ding, A, "V");
                                    }
                                }
                            }
                            console(room, ding);
                        }
                        else
                        {
                            ding = changeAllData(ding, newChairs[i], "V");
                            console(room, ding);
                        }
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Type nu de volgende stoel in");
                        Console.WriteLine("_____________________________________________________________________________________________\n");

              
                    }
                    else
                    {
                        Screens.CustomError("Verkeerde input. Kies een rij (bijv. 1) en een stoel (bijv. A): 1A");
                        i--;
                    }
                }
                else if (input.Length == 3)
                {
                    if (Letters.Contains("" + input[0]) && Numbers.Contains(Convert.ToInt32("" + input[1] + input[2]))){
                        differentinput = "" + input[1] + input[2] + input[0];
                        newChairs[i] = differentinput;
                        if (i == 0)
                        {
                            var newTuple = RecommendationChairs(differentinput, howMany, newChairs, room);
                            if (newTuple.Item2 == howMany)
                            {
                                return newTuple.Item1;
                            }
                            else if (newTuple.Item2 == 0)
                            {
                                newChairs = newTuple.Item1;
                                for (int t = 1; t < newChairs.Length; t++)
                                {
                                    newChairs[t] = "";
                                }
                                i = newTuple.Item2;
                                ding = newTuple.Item3;
                            }
                            else if (newTuple.Item2 == -1)
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                ding = NewAllData(this.AllData);
                            }
                            else
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                foreach (var A in newChairs)
                                {
                                    if (A != "")
                                    {
                                        ding = changeAllData(ding, A, "V");
                                    }
                                }
                            }
                            console(room, ding);
                        }
                        else
                        {
                            ding = changeAllData(ding, newChairs[i], "-");
                            console(room, ding);
                        }
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Type nu de volgende stoel in");
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else if(Letters.Contains("" + input[2]) && Numbers.Contains(Convert.ToInt32("" + input[0] + input[1])))
                    {
                        newChairs[i] = input;
                        if (i == 0)
                        {
                            var newTuple = RecommendationChairs(input, howMany, newChairs, room);
                            if (newTuple.Item2 == howMany)
                            {
                                return newTuple.Item1;
                            }
                            else if (newTuple.Item2 == 0)
                            {
                                newChairs = newTuple.Item1;
                                for (int t = 1; t < newChairs.Length; t++)
                                {
                                    newChairs[t] = "";
                                }
                                i = newTuple.Item2;
                                ding = newTuple.Item3;
                            }
                            else if (newTuple.Item2 == -1)
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                ding = NewAllData(this.AllData);
                            }
                            else
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                foreach (var A in newChairs)
                                {
                                    if (A != "")
                                    {
                                        ding = changeAllData(ding, A, "V");
                                    }
                                }
                            }
                            console(room, ding);
                        }
                        else
                        {
                            ding = changeAllData(ding, newChairs[i], "V");
                            console(room, ding);
                        }
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Type nu de volgende stoel in");
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        //var newData = changeAllData(AllData, input, "-");
                    }
                    else
                    {
                        Screens.CustomError("Verkeerde input. Kies een rij (bijv. 1) en een stoel (bijv. A): 1A");
                        i--;
                    }
                }
                else
                {
                    Screens.CustomError("Verkeerde input. Kies een rij (bijv. 1) en een stoel (bijv. A): 1A");
                    i--;
                }

            }
            Console.WriteLine(newChairs.Length);
            return newChairs;
        }
        public string[][] changeAllData(string[][] NewData, string input,string what)
        {
            int numberPos;
            int letterPos;
            if (input.Length == 2)
            {
                numberPos = Convert.ToInt32("" + input[0]) - 1;
                letterPos = Array.IndexOf(this.Alphabet, "" + input[1]);
            }
            else
            {
                numberPos = Convert.ToInt32("" + input[0] + input[1]) - 1;
                letterPos = Array.IndexOf(this.Alphabet, "" + input[2]);
            }
            NewData[letterPos][numberPos] = what;
            return NewData;
        }


        public void Chair()
        {


            string FullPathSeats = Path.GetFullPath(@"Stoelkeuze.json");
            string FullPathFilms = Path.GetFullPath(@"Filmsdata.json");
            string FullPathSnacksDrinks = Path.GetFullPath(@"snacksdrinks.json");
            string FullPathsReservations = Path.GetFullPath(@"samplelog.json");
            var MyFilmsData = new WebClient().DownloadString(FullPathFilms);
            string myJsonString = new WebClient().DownloadString(FullPathSnacksDrinks);
            string myUserData = new WebClient().DownloadString(FullPathsReservations);
            string myRoomData = new WebClient().DownloadString(FullPathSeats);

            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            dynamic DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            dynamic DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);
            dynamic DynamicRoomData = JsonConvert.DeserializeObject(myRoomData);

            List<string> chairs = new List<string>();
            // From here
            var room = this.DynamicFilmData[0]["FilmRoom"];
            for (int i = 0; i < this.DynamicFilmData.Count; i++)
            {
                if (this.DynamicFilmData[i]["FilmTitle"] == this.FilmNaam)
                {
                    room = this.DynamicFilmData[i]["FilmRoom"];
                }
            }

            for (int i = 0; i < this.DynamicUserData.Count; i++)
            {
                if (this.DynamicUserData[i]["Film"] == this.FilmNaam && this.DynamicUserData[i]["FilmDate"] == this.Datum)
                {
                    for (int j = 0; j < this.DynamicUserData[i]["Stoel_num"].Count; j++)
                    {
                        chairs.Add(this.DynamicUserData[i]["Stoel_num"][j].ToString());
                    }
                    chairs.Sort();

                    Console.WriteLine(chairs.Count());
                    string[] ChairsTaken = new string[chairs.Count];
                    for (int p = 0; p < chairs.Count; p++)
                    {
                        ChairsTaken[p] = chairs[p];
                    }
                    this.chairsTaken = ChairsTaken;

                }
            }
            if (this.chairsTaken == null)
            {
                this.chairsTaken = new string[] { "" };
            }
            // To here can also be seperate method
            string[][] AllData = new string[this.DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count][];
            for (int d = 0; d < AllData.Length; d++)
            {
                AllData[d] = new string[this.DynamicRoomData[Int32.Parse((room - 1).ToString())]["seat_number"].Count];
            }

            for (int i = 0; i < chairs.Count; i++)
            {
                AllData = changeAllData(AllData, chairs[i], "X");
            }
            this.AllData = AllData;

            Scherm.Screens.CinemaBanner();
            Console.Write("U heeft gekozen voor de film: "); ConsoleCommands.Textkleur("rood"); Console.Write(this.FilmNaam); ConsoleCommands.Textkleur("wit"); Console.Write(", op "); ConsoleCommands.Textkleur("rood"); Console.Write(this.Datum); ConsoleCommands.Textkleur("wit"); Console.Write(" om "); ConsoleCommands.Textkleur("rood"); Console.Write(this.Tijd); ConsoleCommands.Textkleur("wit"); Console.Write(" uur.");

            Console.WriteLine("");
            Console.WriteLine("Hoeveel stoelen zou u willen reserveren?");
            Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            int stoelen = Int32.Parse(Console.ReadLine());
            // Table needs to be somewhat dynamic
            ConsoleCommands.Textkleur("wit");
            console(room, AllData);
            Console.WriteLine("Type nu elke stoel, gevolgd door ENTER.");
            Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            List<string> newChairs = new List<string>();
            /* reserveren van stoelen
             * kijken of een gegeven stoel niet al gereserveerd is.
             *  - loopen door alle gereserveerde stoelen
             *  - deze informatie vergelijken met de input
             * kijken of een gegeven stoel het juiste formaat heeft 1A of A1
             *  - controlleren of er 1 numeric
             *  - controlleren of de andere dan in het alphabet zit/charater to int!!!!!!!!!
             */
            var Chosen = WhatChairs(stoelen, room);
            room = ("" + room);

            ReserveerCodeMail(this.FilmNaam, this.Tijd, this.Datum, Chosen, room);





            /* Fixes/Changes/ToDoList
             * 1. Fix recommendationchair overriding already taken chairs.
             * 2. Fix input and output colours and lines.
             * 3. Print out the console table everytime it takes a new input.
            */
        }
    }
}
