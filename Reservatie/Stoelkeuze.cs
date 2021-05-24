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
        public List<string> chairsTaken;

        public StoelKeuze(string Naam, string datum = null, string tijd = null)
        {
            this.FilmNaam = Naam;
            this.Datum = datum;
            this.Tijd = tijd;
            string MyFilmsData = new WebClient().DownloadString(@"C:\Users\Dylan\Source\Repos\Reservatie\Reservatie\Filmsdata.json");
            this.DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);
            string myUserData = new WebClient().DownloadString(@"C:\Users\Dylan\Source\Repos\Reservatie\Reservatie\SampleLog.json");
            this.DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            string myRoomData = new WebClient().DownloadString(@"C:\Users\Dylan\Source\Repos\Reservatie\Reservatie\seats (2).json");
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
                Console.WriteLine("");
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
                        return Tuple.Create(newChairs, 0, newData);
                    }
                    newData = changeAllData(newData, newChairs[i], "-");
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
                        return Tuple.Create(newChairs, 0, newData);
                    }
                    newData = changeAllData(newData, newChairs[i], "-");
                }
            }
            /*
            for (int i = 0; i < howMany; i++)
            {
                newData = changeAllData(newData, newChairs[i],"-");
            }*/

            console(room, newData);
            string newinput;
            Console.WriteLine("Toets [1] om de aangeraden stoelen te kiezen.");
            Console.WriteLine("Toets [2] om zelf stoelen te kiezen");
            Console.WriteLine("Toets [3] om opnieuw uw stoel uit te kiezen");
            while (true)
            {
                newinput = Console.ReadLine();
                if (newinput == "1")
                {
                    return Tuple.Create(newChairs, howMany, newData);
                }
                else if (newinput == "2")
                {
                    string [][] aData = NewAllData(this.AllData);
                    aData = changeAllData(aData, newChairs[0],"-");
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
                    Console.WriteLine("Verkeerde input, probeer het opnieuw");
                }
            }
            
        }
        public string[] WhatChairs(int howMany, List<string> AlreadyTaken, dynamic room)
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
                input = Console.ReadLine();
                input = input.ToUpper();
                if (AlreadyTaken.Contains(input))
                {
                    Console.WriteLine("Deze stoelen zijn al gereservereerd");
                    i--;
                }
                else if (newChairs.Contains(input))
                {
                    Console.WriteLine($"U heeft stoel {newChairs[i]} al gekozen.");
                    i--;
                }
                else if (input.Length == 1)
                {
                    Console.WriteLine("Verkeerde input, probeer '0X'");
                    i--;
                }
                else if (input.Length == 2)
                {
                    if((Letters.Contains("" + input[0]))&& (Letters.Contains("" + input[1])))
                    {
                        Console.WriteLine("Verkeerde input, probeer '0X'");
                        i--;
                    }
                    else if (Letters.Contains("" + input[0]) && Numbers.Contains(Convert.ToInt32("" + input[1])))
                    {
                        differentinput = "" + input[1] + input[0];
                        newChairs[i] = differentinput;
                        Console.WriteLine(newChairs[i]);
                        var newData = changeAllData(this.AllData, differentinput,"-");
                        console(room, newData);
                        Console.WriteLine("Type nu de volgende stoel in.");
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
                            else
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                ding = NewAllData(this.AllData);
                            }
                            console(room, ding);
                            Console.WriteLine("aaaa");
                            Console.WriteLine(ding[0][4]);
                        }
                        //var newData = changeAllData(AllData, input);
                        //console(room, newData);
                        Console.WriteLine("Type nu de volgende stoel in.");
                        
                    }
                    else
                    {
                        Console.WriteLine("Verkeerde input, probeer '0X'");
                        i--;
                    }
                }
                else if (input.Length == 3)
                {
                    if (Letters.Contains("" + input[0]) && Numbers.Contains(Convert.ToInt32("" + input[1] + input[2]))){
                        differentinput = "" + input[1] + input[2] + input[0];
                        newChairs[i] = differentinput;
                        var newData = changeAllData(AllData, differentinput, "-");
                        console(room, newData);
                        Console.WriteLine("Type nu de volgende stoel in.");
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
                            else
                            {
                                i = newTuple.Item2;
                                newChairs = newTuple.Item1;
                                ding = NewAllData(this.AllData);
                            }
                            console(room, ding);
                        }
                        //var newData = changeAllData(AllData, input, "-");
                        Console.WriteLine("Type nu de volgende stoel in.");
                    }
                    else
                    {
                        Console.WriteLine("Verkeerde input, probeer '0X'");
                        i--;
                    }
                }
                else
                {
                    Console.WriteLine("Verkeerde input, probeer '0X'");
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
            //var MyFilmsData = new WebClient().DownloadString(@"C:\Users\Dylan\Source\Repos\Reservatie\Reservatie\Filmsdata.json");
            //string myUserData = new WebClient().DownloadString(@"C:\Users\Dylan\Source\Repos\Reservatie\Reservatie\SampleLog.json");
            //string myRoomData = new WebClient().DownloadString(@"C:\Users\Dylan\Source\Repos\Reservatie\Reservatie\seats (2).json");
            //dynamic DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            //dynamic DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);
            //dynamic DynamicRoomData = JsonConvert.DeserializeObject(myRoomData);
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
                    this.chairsTaken = chairs;
                    //Console.WriteLine(chairs[1][0]);
                }
            }
            // To here can also be seperate method
            string[][] AllData = new string[this.DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count][];
            for (int d = 0; d < AllData.Length; d++)
            {
                AllData[d] = new string[this.DynamicRoomData[Int32.Parse((room - 1).ToString())]["seat_number"].Count];
            }
            for(int i = 0; i< chairs.Count; i++)
            {
                AllData = changeAllData(AllData, chairs[i], "X");
            }
            this.AllData = AllData;
            /*
            int checkallchairs = 0;
            for (int r = 0; r < AllData.Length; r++)
            {
                for (int s = 0; s < AllData[r].Length; s++)
                {
                    if (!(chairs.Count == 0))
                    {
                        if (chairs[checkallchairs].Length == 2)
                        {
                            if (chairs[checkallchairs][0].ToString() == $"{s + 1}" && chairs[checkallchairs][1].ToString() == Alphabet[r])
                            {
                                AllData[r][s] = "X";
                                if (!(checkallchairs >= chairs.Count - 1))
                                {
                                    checkallchairs++;
                                }
                            }
                        }
                        else if (chairs[checkallchairs].Length == 3)
                        {
                            if (!(checkallchairs >= chairs.Count - 1))
                            {
                                checkallchairs++;
                            }
                        }
                    }
                    else
                    {
                        AllData[r][s] = null;
                    }
                }
            }
            */
            Scherm.Screens.CinemaBanner();
            Console.WriteLine("Hoeveel stoelen zou u willen reserveren?");
            ConsoleCommands.Textkleur("zwart");
            int stoelen = Int32.Parse(Console.ReadLine());
            // Table needs to be somewhat dynamic
            ConsoleCommands.Textkleur("wit");
            console(room, AllData);
            Console.WriteLine("Type nu elke stoel, gevolgd door ENTER.");
            ConsoleCommands.Textkleur("zwart");
            List<string> newChairs = new List<string>();
            Console.WriteLine("Test");
            /* reserveren van stoelen
             * kijken of een gegeven stoel niet al gereserveerd is.
             *  - loopen door alle gereserveerde stoelen
             *  - deze informatie vergelijken met de input
             * kijken of een gegeven stoel het juiste formaat heeft 1A of A1
             *  - controlleren of er 1 numeric
             *  - controlleren of de andere dan in het alphabet zit/charater to int!!!!!!!!!
             */
            var Chosen = WhatChairs(stoelen, chairs, room);
            room = ("" + room);
            ReserveerCodeMail(this.FilmNaam, this.Tijd, Chosen, room, this.Datum);


            /*for (int i = 0; i < DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count - 1; i++)
            {
                // So does this
                table.AddRow(Alphabet[i], AllData[i][0], AllData[i][1], AllData[i][2], AllData[i][3], AllData[i][4], AllData[i][5], AllData[i][6], AllData[i][7], AllData[i][8], AllData[i][9], AllData[i][10], AllData[i][11], AllData[i][12], AllData[i][13], AllData[i][14], AllData[i][15], AllData[i][16], AllData[i][17], AllData[i][18], AllData[i][19]);
            }*/
            //table.Write(Format.Alternative);
        }
    }
}
