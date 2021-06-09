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
        public int hoeveelstoelen;

        public StoelKeuze(string Naam, string datum = null, string tijd = null)
        {
            this.FilmNaam = Naam;
            this.Datum = datum;
            this.Tijd = tijd;
            string FullPathFilms = Path.GetFullPath(@"Filmsdata.json");
            string FullPathsReservations = Path.GetFullPath(@"SampleLog.json");
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
                Console.WriteLine("\t\t\t\t\tVoor");
                table.Write(Format.Alternative);

                Console.WriteLine("\t\t\t\t\tAchter");
                Console.WriteLine("  'X' = al gereserveerde stoelen.  '-' = uw gekozen stoelen.  '+' = de aanbevolen stoelen");

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
                Console.WriteLine("\t\t\t\t\tVoor");
                table.Write(Format.Alternative);

                Console.WriteLine("\t\t\t\t\tAchter");
                Console.WriteLine("  'X' = al gereserveerde stoelen.  '-' = uw gekozen stoelen.  '+' = de aanbevolen stoelen");

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
                Console.WriteLine("\t\t\t\t\tVoor");
                table.Write(Format.Alternative);

                Console.WriteLine("\t\t\t\t\tAchter");
                Console.WriteLine("  'X' = al gereserveerde stoelen.  '-' = uw gekozen stoelen.  '+' = de aanbevolen stoelen");

                Console.WriteLine("_____________________________________________________________________________________________\n");
            }

        }
        public string[][] NewAllData(string[][] old)
        {
            string[][] newdata = new string[old.Length][];
            for (int p = 0; p < newdata.Length; p++)
            {
                newdata[p] = new string[old[p].Length];
            }
            for (int j = 0; j < newdata.Length; j++)
            {
                for (int p = 0; p < newdata[j].Length; p++)
                {
                    newdata[j][p] = old[j][p];
                }
            }
            return newdata;
        }

        public Tuple<string[], int, string[][]> RecommendationChairs(string input, int howMany, string[] newChairs, dynamic room)
        {
            string[][] newData = NewAllData(this.AllData);

            if (input.Length == 2)
            {
                newChairs[0] = input;
                newData = changeAllData(newData, newChairs[0], "-");
                for (int i = 1; i < howMany; i++)
                {
                    newChairs[i] = "" + (Convert.ToInt32("" + input[0]) + i) + input[1];
                    if (this.chairsTaken.Contains(newChairs[i]))
                    {
                        newChairs[i] = "";
                        i--;
                        return Tuple.Create(newChairs, i, newData);
                    }

                    newData = changeAllData(newData, newChairs[i], "+");

                }
            }
            else
            {
                newChairs[0] = input;
                newData = changeAllData(newData, newChairs[0], "-");
                for (int i = 1; i < howMany; i++)
                {
                    newChairs[i] = "" + (Convert.ToInt32("" + input[0] + input[1]) + i) + input[2];
                    if (this.chairsTaken.Contains(newChairs[i]))
                    {
                        newChairs[i] = "";
                        i--;
                        return Tuple.Create(newChairs, i, newData);
                    }

                    newData = changeAllData(newData, newChairs[i], "+");

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
            Screens.KeuzeLine(3, "Opnieuw een stoel uitkiezen");
            Screens.KeuzeLine(0, "Terug gaan");

            Console.WriteLine("_____________________________________________________________________________________________\n");
            while (true)
            {
                ConsoleCommands.Textkleur("zwart");
                newinput = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                if (newinput == "0")
                {
                    this.Chair();
                }
                else if (newinput == "1")
                {
                    return Tuple.Create(newChairs, howMany, newData);
                }
                else if (newinput == "2")
                {

                    string[][] aData = NewAllData(this.AllData);
                    aData = changeAllData(aData, newChairs[0], "-");

                    return Tuple.Create(newChairs, 0, aData);
                }
                else if (newinput == "3")
                {
                    for (int q = 0; q < newChairs.Length; q++)
                    {
                        newChairs[q] = "";
                    }
                    return Tuple.Create(newChairs, -1, this.AllData);
                }
                else
                {
                    Screens.CustomError("U kunt alleen een getal kiezen tussen de 0 en 3! Probeer het opnieuw:");
                }
            }

        }
        public string[] WhatChairs(int howMany, dynamic room)
        {
            string[][] newAllData = AllData;
            var DynNumbers = this.DynamicRoomData[(Convert.ToInt32("" + room)) - 1]["seat_number"];
            var DynLetters = this.DynamicRoomData[(Convert.ToInt32("" + room)) - 1]["row_number"];
            int[] Numbers = new int[DynNumbers.Count];
            string[] Letters = new string[DynLetters.Count];
            for (int n = 0; n < Numbers.Length; n++)
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
            for (int i = 0; i < howMany; i++)
            {
                ConsoleCommands.Textkleur("zwart");
                input = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                input = input.ToUpper();
                if (input.Length == 2)
                {
                    differentinput = "" + input[1] + input[0];
                }
                else if (input.Length == 3)
                {
                    differentinput = "" + input[1] + input[2] + input[0];
                }
                if (input == "0")
                {
                    if (i == 0)
                    {
                        this.Chair();
                    }
                    else
                    {
                        newChairs[i - 1] = "";
                        i = i - 2;
                        newAllData = NewAllData(this.AllData);
                        for (int q = 0; q <= i; q++)
                        {
                            newAllData = changeAllData(newAllData, newChairs[q], "-");
                        }
                        console(room, newAllData);
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Type nu de volgende stoel in of type '0' om terug te gaan.");
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                }
                else if (this.chairsTaken.Contains(input) || this.chairsTaken.Contains(differentinput))
                {
                    Screens.CustomError("Deze stoel is al gereserveerd! Probeer het opnieuw:");
                    i--;
                }
                else if (newChairs.Contains(differentinput) || newChairs.Contains(input))
                {
                    Screens.CustomError("U heeft deze stoel al gekozen! Probeer het opnieuw:");
                    i--;
                }
                else if (input.Length == 1)
                {
                    Screens.CustomError("U kunt een stoel opgeven met formaat: '0X'! Probeer het opnieuw:");
                    i--;
                }
                else if (input.Length == 2)
                {
                    if ((Letters.Contains("" + input[0])) && (Letters.Contains("" + input[1])))
                    {
                        Screens.CustomError("U kunt een stoel opgeven met formaat: '0X'! Probeer het opnieuw:");
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
                                newAllData = newTuple.Item3;
                            }
                            else if (newTuple.Item2 == -1)
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                newAllData = NewAllData(this.AllData);
                            }
                            else
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                foreach (var A in newChairs)
                                {
                                    if (A != "")
                                    {

                                        newAllData = changeAllData(newAllData, A, "-");

                                    }
                                }
                            }
                            console(room, newAllData);
                        }
                        else
                        {

                            newAllData = changeAllData(newAllData, newChairs[i], "-");
                            console(room, newAllData);

                        }
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Type nu de volgende stoel in of type '0' om terug te gaan.");
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else if (Letters.Contains("" + input[1]) && Numbers.Contains(Convert.ToInt32("" + input[0])))
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
                                newAllData = newTuple.Item3;
                            }
                            else if (newTuple.Item2 == -1)
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                newAllData = NewAllData(this.AllData);
                            }
                            else
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                foreach (var A in newChairs)
                                {
                                    if (A != "")
                                    {

                                        newAllData = changeAllData(newAllData, A, "-");

                                    }
                                }
                            }
                            console(room, newAllData);
                        }
                        else
                        {

                            newAllData = changeAllData(newAllData, newChairs[i], "-");
                            console(room, newAllData);

                        }
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Type nu de volgende stoel in of type '0' om terug te gaan.");
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        //var newData = changeAllData(AllData, input);
                        //console(room, newData);
                    }
                    else
                    {
                        Screens.CustomError("U kunt een stoel opgeven met formaat: '0X'! Probeer het opnieuw:");
                        i--;
                    }
                }
                else if (input.Length == 3)
                {
                    if (Letters.Contains("" + input[0]) && Numbers.Contains(Convert.ToInt32("" + input[1] + input[2])))
                    {
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
                                newAllData = newTuple.Item3;
                            }
                            else if (newTuple.Item2 == -1)
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                newAllData = NewAllData(this.AllData);
                            }
                            else
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                foreach (var A in newChairs)
                                {
                                    if (A != "")
                                    {

                                        newAllData = changeAllData(newAllData, A, "-");

                                    }
                                }
                            }
                            console(room, newAllData);
                        }
                        else
                        {
                            newAllData = changeAllData(newAllData, newChairs[i], "-");
                            console(room, newAllData);
                        }
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Type nu de volgende stoel in of type '0' om terug te gaan.");
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else if (Letters.Contains("" + input[2]) && Numbers.Contains(Convert.ToInt32("" + input[0] + input[1])))
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
                                newAllData = newTuple.Item3;
                            }
                            else if (newTuple.Item2 == -1)
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                newAllData = NewAllData(this.AllData);
                            }
                            else
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                foreach (var A in newChairs)
                                {
                                    if (A != "")
                                    {

                                        newAllData = changeAllData(newAllData, A, "-");

                                    }
                                }
                            }
                            console(room, newAllData);
                        }
                        else
                        {

                            newAllData = changeAllData(newAllData, newChairs[i], "-");
                            console(room, newAllData);

                        }
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Type nu de volgende stoel in of type '0' om terug te gaan.");
                        Console.WriteLine("_____________________________________________________________________________________________\n");
                        //var newData = changeAllData(AllData, input, "-");
                    }
                    else
                    {
                        Screens.CustomError("U kunt een stoel opgeven met formaat: '0X'! Probeer het opnieuw:");
                        i--;
                    }
                }
                else
                {
                    Screens.CustomError("U kunt een stoel opgeven met formaat: '0X'! Probeer het opnieuw:");
                    i--;
                }

            }
            Console.WriteLine(newChairs.Length);
            return newChairs;
        }
        public string[][] changeAllData(string[][] NewData, string input, string whatSymbol)
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
            NewData[letterPos][numberPos] = whatSymbol;
            return NewData;
        }


        public void Chair()
        {


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
            Console.Write($"U heeft gekozen voor de film: "); ConsoleCommands.Textkleur("rood"); Console.Write(this.FilmNaam); ConsoleCommands.Textkleur("wit"); Console.Write(", op "); ConsoleCommands.Textkleur("rood"); Console.Write(this.Datum); ConsoleCommands.Textkleur("wit"); Console.Write(" om "); ConsoleCommands.Textkleur("rood"); Console.Write(this.Tijd); ConsoleCommands.Textkleur("wit"); Console.Write(" uur.");
            Console.WriteLine("");
            Console.WriteLine("Hoeveel stoelen zou u willen reserveren?");
            Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            bool stoelenloop = true;
            while (stoelenloop)
            {
                try
                {
                    ConsoleCommands.Textkleur("zwart");
                    this.hoeveelstoelen = Int32.Parse(Console.ReadLine());
                    if (this.hoeveelstoelen > 8 || this.hoeveelstoelen < 1)
                    {
                        Screens.CustomError("U kunt alleen een getal kiezen tussen 1 en 8! Probeer het opnieuw:");
                    }
                    else 
                    {
                        stoelenloop = false;
                    }
                }
                catch
                {
                    ConsoleCommands.Textkleur("wit");
                    Screens.CustomError("U kunt alleen een getal kiezen tussen 1 en 8! Probeer het opnieuw:");
                }
            }
            // Table needs to be somewhat dynamic
            ConsoleCommands.Textkleur("wit");
            console(room, AllData);
            Console.WriteLine("Type nu elke stoel of type '0' om terug te gaan, gevolgd door ENTER.");
            Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            List<string> newChairs = new List<string>();
            var chosenChairs = WhatChairs(this.hoeveelstoelen, room);
            room = ("" + room);
            Thread.Sleep(2000);
            ReserveerCodeMail(this.FilmNaam, this.Tijd, this.Datum, chosenChairs, room);



        }
    }
}
