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
    public class StoelKeuze
    {
        public string FilmNaam;
        public string Datum;
        public string Tijd;

        public StoelKeuze(string Naam, string datum = null, string tijd = null)
        {
            this.FilmNaam = Naam;
            this.Datum = datum;
            this.Tijd = tijd;
        }
        public void console(dynamic room, string[] Alphabet, dynamic DynamicRoomData, string[][] AllData)
        {
            if (room == 1)
            {
                var table = new ConsoleTable("", "1 ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10", "11", "12", "13", "14", "15");
                for (int i = 0; i < DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count; i++)
                {
                    table.AddRow(Alphabet[i], AllData[i][0], AllData[i][1], AllData[i][2], AllData[i][3], AllData[i][4], AllData[i][5], AllData[i][6], AllData[i][7], AllData[i][8], AllData[i][9], AllData[i][10], AllData[i][11], AllData[i][12], AllData[i][13], AllData[i][14]);
                }
                Scherm.Screens.CinemaBanner();
                table.Write(Format.Alternative);
            }
            else if (room == 2)
            {
                var table = new ConsoleTable("", "1 ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20");
                for (int i = 0; i < DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count; i++)
                {
                    table.AddRow(Alphabet[i], AllData[i][0], AllData[i][1], AllData[i][2], AllData[i][3], AllData[i][4], AllData[i][5], AllData[i][6], AllData[i][7], AllData[i][8], AllData[i][9], AllData[i][10], AllData[i][11], AllData[i][12], AllData[i][13], AllData[i][14], AllData[i][15], AllData[i][16], AllData[i][17], AllData[i][18], AllData[i][19]);
                }
                Scherm.Screens.CinemaBanner();
                table.Write(Format.Alternative);
            }
            else
            {
                var table = new ConsoleTable("", "1 ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25");
                for (int i = 0; i < DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count; i++)
                {
                    table.AddRow(Alphabet[i], AllData[i][0], AllData[i][1], AllData[i][2], AllData[i][3], AllData[i][4], AllData[i][5], AllData[i][6], AllData[i][7], AllData[i][8], AllData[i][9], AllData[i][10], AllData[i][11], AllData[i][12], AllData[i][13], AllData[i][14], AllData[i][15], AllData[i][16], AllData[i][17], AllData[i][18], AllData[i][19], AllData[i][20], AllData[i][21], AllData[i][22], AllData[i][23], AllData[i][24]);
                }
                Scherm.Screens.CinemaBanner();
                table.Write(Format.Alternative);
            }

        }



        public void Chair()
        {
            var MyFilmsData = new WebClient().DownloadString(@"C:\Users\djvan\source\repos\Reservatie1\Reservatie\Filmsdata.json");
            string myJsonString = new WebClient().DownloadString(@"C:\Users\djvan\source\repos\Reservatie1\Reservatie\snacksdrinks.json");
            string myUserData = new WebClient().DownloadString(@"C:\Users\djvan\source\repos\Reservatie1\Reservatie\SampleLog.json");
            string myRoomData = new WebClient().DownloadString(@"C:\Users\djvan\source\repos\Reservatie1\Reservatie\seats (2).json");
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            dynamic DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            dynamic DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);
            dynamic DynamicRoomData = JsonConvert.DeserializeObject(myRoomData);
            List<string> chairs = new List<string>();
            // From here
            var room = DynamicFilmData[0]["FilmRoom"];
            for (int i = 0; i < DynamicFilmData.Count; i++)
            {
                if (DynamicFilmData[i]["FilmTitle"] == this.FilmNaam)
                {
                    room = DynamicFilmData[i]["FilmRoom"];
                }
            }
            for (int i = 0; i < DynamicUserData.Count; i++)
            {
                if (DynamicUserData[i]["Film"] == this.FilmNaam && DynamicUserData[i]["Datum"] == this.Datum)
                {
                    for (int j = 0; j < DynamicUserData[i]["Stoel_num"].Count; j++)
                    {
                        chairs.Add(DynamicUserData[i]["Stoel_num"][j].ToString());
                    }
                    chairs.Sort();
                    //Console.WriteLine(chairs[1][0]);
                }
            }
            // To here can also be seperate method
            string[][] AllData = new string[DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count][];
            for (int d = 0; d < AllData.Length; d++)
            {
                AllData[d] = new string[DynamicRoomData[Int32.Parse((room - 1).ToString())]["seat_number"].Count];
            }
            string[] Alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            int checkallchairs = 0;
            for (int r = 0; r < AllData.Length; r++)
            {
                for (int s = 0; s < AllData[r].Length; s++)
                {
                    if (!(chairs.Count == 0))
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
                    else
                    {
                        AllData[r][s] = null;
                    }
                }
            }
            
            Console.WriteLine("Hoeveel stoelen zou u willen reserveren?");
            ConsoleCommands.Textkleur("zwart");
            int stoelen = Int32.Parse(Console.ReadLine());
            // Table needs to be somewhat dynamic
            ConsoleCommands.Textkleur("wit");
            console(room, Alphabet, DynamicRoomData, AllData);
            Console.WriteLine("Type nu elke stoel, gevolgd door ENTER.");
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
            for (int i = 0; i < stoelen; i++)
            {
                newChairs.Add(Console.ReadLine());
                for(int j = 0; j < chairs.Count; j++)
                {
                    if (chairs[j] == newChairs[i])
                    {
                        Console.WriteLine("Deze stoelen zijn al gereservereerd");
                        newChairs[i] = null;
                        i--;
                    }
                }
            }
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("U heeft de volgende stoelen gekozen. Type 'JA' als dit klopt.");
            for (int i = 0; i < stoelen; i++)
            {
                Console.WriteLine($"[{i + 1}] {newChairs[i]}");
            }

            /*for (int i = 0; i < DynamicRoomData[Int32.Parse((room - 1).ToString())]["row_number"].Count - 1; i++)
            {
                // So does this
                table.AddRow(Alphabet[i], AllData[i][0], AllData[i][1], AllData[i][2], AllData[i][3], AllData[i][4], AllData[i][5], AllData[i][6], AllData[i][7], AllData[i][8], AllData[i][9], AllData[i][10], AllData[i][11], AllData[i][12], AllData[i][13], AllData[i][14], AllData[i][15], AllData[i][16], AllData[i][17], AllData[i][18], AllData[i][19]);
            }*/
            //table.Write(Format.Alternative);
        }
    }
}
