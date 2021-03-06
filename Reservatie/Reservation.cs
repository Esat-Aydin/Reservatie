using Cinema;
using ConsoleTables;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Net;
using Film;
using Scherm;
using Gebruiker;
using Chair;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;


namespace Reservation
{
    public abstract class Reserveren
    {
        public string UppercaseFirst(string str)
        {
            return Regex.Replace(str, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
        }
        public void ReserveringBeheren()
        {
            ConsoleCommands CommandLine = new ConsoleCommands();
            // Inladen Json Module 
            string FullPathFilms = Path.GetFullPath(@"Filmsdata.json");
            string FullPathSnacks = Path.GetFullPath(@"snacksdrinks.json");
            string FullPathReservations = Path.GetFullPath(@"SampleLog.json");
            var MyFilmsData = new WebClient().DownloadString(FullPathFilms);
            string myJsonString = new WebClient().DownloadString(FullPathSnacks);
            string myUserData = new WebClient().DownloadString(FullPathReservations);

            // Omzetten
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            dynamic DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            dynamic DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);
            Scherm.Screens.CinemaBanner();
            ConsoleCommands.Textkleur("wit");
            Console.Write("\t\t\tVoer hier uw reserverings code in:\n");
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            var Reservatie_code = Console.ReadLine();
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            int Index =-1;

            ConsoleCommands.Textkleur("wit");
            for (int i = 0; i < DynamicUserData.Count; i++)
            {
                string Res_code = (string)DynamicUserData[i]["Reservatie_code"];
                if (Res_code == Reservatie_code)
                {
                    Index = i;
                    Scherm.Screens.CinemaBanner();
                    Console.WriteLine("\t\t\t\tReservering: ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Reservering_check(DynamicUserData, i);
                    break;
                }
            }

            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

            Console.Write("Om uw reservering te annuleren toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write("] om het programma opnieuw op te starten toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write("]\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart"); string Optie = Console.ReadLine();
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            if(Optie == "2")
            {
                Console.Clear();
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Environment.Exit(1);
            }
            if(Optie == "1")
            {
                ReserveringAnnuleren(Index, DynamicUserData);
                Thread.Sleep(3000);
                Console.Clear();
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Environment.Exit(1);
            }





        }
        public void ReservationCodePercentage()
        {
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("rood");
            for (int i = 0; i <= 100; i++)
            {
                ConsoleCommands.Textkleur("wit");
                Console.Write("\rProgress: ");
                if (i != 100)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"{i}%   ");
                    Thread.Sleep(25);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{i}%   ");
                    Thread.Sleep(25);
                }
            }
            Console.WriteLine("");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

        }
        public void ReserveringMaken(string UserInput)
        {
            Film.Film FilmObject = new Film.Film();
            MedewerkerClass.Medewerker admin = new MedewerkerClass.Medewerker();
            Gebruiker.Gebruiker Klant = new Gebruiker.Gebruiker();
            ConsoleCommands CommandLine = new ConsoleCommands();
            List<string> Autofill = new List<string>();
            // Inladen Json Module 
            dynamic DynamicData = JsonData.JsonSerializer("Snacks");
            dynamic DynamicUserData = JsonData.JsonSerializer("Users");
            dynamic DynamicFilmData = JsonData.JsonSerializer("Films");
            Console.Clear(); ConsoleCommands.Textkleur("rood");
            Scherm.Screens.CinemaBanner();
            if (UserInput == "1")
            {
                ConsoleCommands.Textkleur("wit");
                Console.Write("\t\t\tNaar welke film bent u opzoek: \n\n\t\t\t\t ["); ConsoleCommands.Textkleur("zwart"); Console.Write("0"); ConsoleCommands.Textkleur("wit"); Console.Write("] Terug gaan\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                string Film = Console.ReadLine();
                string Film_search = UppercaseFirst(Film);
                int counter = 0;
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                for (int i = 0; i < DynamicFilmData.Count; i++)
                {
                    string Film_zoeken = (string)DynamicFilmData[i]["FilmTitle"];
                    if (Film_search == Film_zoeken)
                    {
                        Scherm.Screens.CinemaBanner();

                        ConsoleCommands.Textkleur("wit");
                        Console.Write($"U heeft gezocht naar de volgende film: "); ConsoleCommands.Textkleur("rood"); Console.WriteLine(Film_zoeken);
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("rood");


                        Klant.ZoekOptie(Film_search, DynamicFilmData);
                    }
                    else
                    {
                        if (Film_search == "0")
                        {
                            Scherm.Screens.ReturnToPreviousScreen("ReserveringMaken");
                        }
                    }
                    try
                    {
                        if (Film_search[0..Film_search.Length] == Film_zoeken[0..Film_search.Length])
                        {
                            Autofill.Add(Film_zoeken);
                        }
                    }
                    catch
                    {
                        Autofill = Autofill;
                    }
                    counter++;

                }
                if (Autofill.Count < 1)
                {
                    Scherm.Screens.CustomError("We hebben geen films gevonden met die criteria! Probeer het nogmaals met een andere zoekterm.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    ReserveringMaken(UserInput);
                }
                bool InWhile = true;
                while (InWhile)
                {
                    Console.WriteLine("Op basis van uw input hebben we deze films gevonden:\n");
                    for (int i = 1; i < Autofill.Count + 1; i++)
                    {
                        Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write(i); ConsoleCommands.Textkleur("wit"); Console.Write($"] {Autofill[i - 1]}\n");
                    }
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    string Filmkeuze = Console.ReadLine();
                   
                    for (int i = 1; i < Autofill.Count + 1; i++)
                    {
                        if (Filmkeuze == i.ToString())
                        {
                            Scherm.Screens.CinemaBanner();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.Write($"U heeft gekozen voor de volgende film: "); ConsoleCommands.Textkleur("rood"); Console.WriteLine(Autofill[i - 1]);
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("rood");
                            Klant.ZoekOptie(Autofill[i - 1], DynamicFilmData);
                            InWhile = false;
                        }
                    }
                    Scherm.Screens.ErrorMessageInput();
                    Thread.Sleep(2000);
                    Console.Clear();
                    Scherm.Screens.CinemaBanner();

                }
                if (counter >= DynamicFilmData.Count)
                {

                    Scherm.Screens.ErrorMessageInput();
                    Thread.Sleep(1500);
                    Console.Clear();
                    ReserveringMaken(UserInput);
                }

            }
            else if (UserInput == "2")
            {
                Scherm.Screens.CinemaBanner();
                List<string> Show_films = new List<string>();
                Dictionary<string, string[]> Show_tijden = new Dictionary<string, string[]>();
                ConsoleCommands.Textkleur("wit");
                Console.Write("\t\t\t\tKies een genre uit\t\t\t\t\t \n\n");
                ConsoleCommands.Textkleur("wit");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] Action\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("] Comedy\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("3"); ConsoleCommands.Textkleur("wit"); Console.Write("] Thriller\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("4"); ConsoleCommands.Textkleur("wit"); Console.Write("] Romantiek\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("5"); ConsoleCommands.Textkleur("wit"); Console.Write("] Drama\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("6"); ConsoleCommands.Textkleur("wit"); Console.Write("] Sci-Fi\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("7"); ConsoleCommands.Textkleur("wit"); Console.Write("] Familie\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("8"); ConsoleCommands.Textkleur("wit"); Console.Write("] Horror\n");
                Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("0"); ConsoleCommands.Textkleur("wit"); Console.Write("] Terug gaan\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                var Genre_select = Console.ReadLine();
                if (Genre_select == "0")
                {
                    Scherm.Screens.ReturnToPreviousScreen("ReserveringMaken");
                }
                else if (Genre_select != "0" && Genre_select != "1" && Genre_select != "2" && Genre_select != "3" && Genre_select != "4" && Genre_select != "5" && Genre_select != "6" && Genre_select != "7" && Genre_select != "8" )
                {
                    Scherm.Screens.ErrorMessageInput();
                    Thread.Sleep(1500);
                    Console.Clear();
                    ReserveringMaken(UserInput);
                }
                Console.Clear();
                ConsoleCommands.Textkleur("wit");
                CommandLine.Genre(Genre_select);
                string SearchedGenre = CommandLine.Genre(Genre_select);
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("wit");
                Scherm.Screens.CinemaBanner();
                ConsoleCommands.Textkleur("wit");
                Console.Write("\t\t\tWe hebben deze films gevonden onder de genre "); ConsoleCommands.Textkleur("rood"); Console.Write(SearchedGenre); ConsoleCommands.Textkleur("wit"); Console.Write(":\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                for (int i = 0; i < DynamicFilmData.Count; i++)
                {
                    for (int j = 0; j < DynamicFilmData[i]["FilmGenres"].Count; j++)
                    {
                        string Genre_zoeken = (string)DynamicFilmData[i]["FilmGenres"][j];
                        if (CommandLine.Genre_search == Genre_zoeken)
                        {
                            Show_films.Add(DynamicFilmData[i]["FilmTitle"].ToString());
                        }
                    }
                }

                Klant.GenreOptie(Show_films, DynamicFilmData,SearchedGenre);


            }
            else if (UserInput == "3")
            {
                Scherm.Screens.CinemaBanner();
                ConsoleCommands.Textkleur("wit");
                Console.Write("\t\t\tHieronder vind u een lijst met alle films: \n\n\t\t\t\t ["); ConsoleCommands.Textkleur("zwart"); Console.Write("0"); ConsoleCommands.Textkleur("wit"); Console.Write("] Terug gaan\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");

                /*string Film = Console.ReadLine();
                /*if (Film == "0")
                {
                    Scherm.Screens.ReturnToPreviousScreen("ReserveringMaken");
                }*/

                var table = new ConsoleTable("Film Naam", "Film Genre 1", "Film Genre 2", "Film Genre 3", "Zaal"); //Preset Table
                dynamic genres = DynamicFilmData[0]["FilmGenres"];
                List<string> All_Films = new List<string>();
                var zaal = DynamicFilmData[0]["FilmRoom"];
                for (int i = 0; i < DynamicFilmData.Count; i++)
                {
                    All_Films.Add(DynamicFilmData[i]["FilmTitle"].ToString()); //Lijst aangemaakt met alle films
                    All_Films.Sort(); //Lijst met films gesorteerd
                }
                ConsoleCommands.Textkleur("wit");
                for (int i = 0; i < All_Films.Count; i++)
                {

                    for (int j = 0; j < DynamicFilmData.Count; j++)
                    {
                        if (All_Films[i] == DynamicFilmData[j]["FilmTitle"].ToString())
                        {
                            genres = DynamicFilmData[j]["FilmGenres"]; //Juiste genres zoeken bij de films
                            zaal = DynamicFilmData[j]["FilmRoom"]; //Juiste zaal zoeken bij de films
                            table.AddRow($"Toets [{i + 1}] voor {All_Films[i]}", genres[0], genres[1], genres[2], zaal); //Tabel invullen met juiste data
                        }
                    }
                }
                table.AddRow("Toets [0] om terug te gaan", null, null, null, null);
                table.Write(Format.Alternative); //Format veranderen ivm "Counter"
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                
                int choice;
                bool loop = true;
                while (loop)
                {
                    
                    try
                    {
                        choice = Int32.Parse(Console.ReadLine());
                        if (choice.ToString() == "0")
                        {
                            Scherm.Screens.ReturnToPreviousScreen("ReserveringMaken");
                        }
                        else if (choice >= 1 && choice <= All_Films.Count)
                        {

                            loop = false;
                            Console.Clear();
                            Scherm.Screens.CinemaBanner();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.Write("U heeft gekozen voor de volgende film:\t"); ConsoleCommands.Textkleur("rood"); Console.WriteLine(All_Films[choice - 1]);
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            
                            Klant.ZoekOptie(All_Films[choice - 1], DynamicFilmData);
                        }
                        else
                        {
                            Scherm.Screens.ErrorMessageInput();
                        }
                    }
                    catch
                    {
                        Scherm.Screens.ErrorMessageInput();
                    }
            
                    
                }

            }

            else if (UserInput == "4")
            {
                Scherm.Screens.CinemaBanner();
                List<string> DagenvdWeek = new List<string>();
                List<string> Show_Tijden = new List<string>();
                DagenvdWeek.Add("Maandag");
                DagenvdWeek.Add("Dinsdag");
                DagenvdWeek.Add("Woensdag");
                DagenvdWeek.Add("Donderdag");
                DagenvdWeek.Add("Vrijdag");
                DagenvdWeek.Add("Zaterdag");
                DagenvdWeek.Add("Zondag");
                int Count = 1;
                List<string> ListofFilms = new List<string>();
                string Times = null;
                dynamic Dagen = DynamicFilmData[0]["FilmDays"];
                Dictionary<string, List<string>> DictofListofString = new Dictionary<string, List<string>>();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("\t\tVoer de datum in voor wanneer u zou willen reserveren (DD-MM-YYYY): ");
                Console.Write("\n\t\t\t\t["); ConsoleCommands.Textkleur("zwart"); Console.Write("0"); ConsoleCommands.Textkleur("wit"); Console.Write("] Terug gaan\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                string FilmDateSearch = Console.ReadLine();
                bool InputisDate = false;
                DateTime TestDateTime = new DateTime();
                while (InputisDate == false)
                {
                    if (IsDateUserInputInteger(FilmDateSearch) == true && (FilmDateSearch[2].Equals('-') && FilmDateSearch[5].Equals('-')))
                    {
                        TestDateTime = DateTimeReturner(FilmDateSearch);
                        InputisDate = true;
                    }
                    else if (FilmDateSearch == "0")
                    {
                        Scherm.Screens.ReturnToPreviousScreen("ReserveringMaken");
                    }
                    else
                    {
                        Screens.CustomError("Dat is geen geldige input! Probeer het opnieuw met de format DD-MM-YYYY - Voorbeeld: 16-05-2021");
                        FilmDateSearch = Console.ReadLine();
                    }
                }
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                bool filmsShown = false;
                while (true)
                {
                    if (DateInFutureCheck(TestDateTime) == true)
                    {
                        string ConvertedDate = DateConverter(FilmDateSearch, TestDateTime);

                        var table = new ConsoleTable("Film", DayReturner(ConvertedDate) + ", " + FilmDateSearch);
                        for (int i = 0; i < DynamicFilmData.Count; i++)
                        {
                            DictofListofString[DynamicFilmData[i]["FilmTitle"].ToString()] = new List<string>();
                            if (DynamicFilmData[i]["FilmDays"][DayReturner(ConvertedDate)].Count > 0)
                            {


                                for (int x = 0; x < DynamicFilmData[i]["FilmDays"][DayReturner(ConvertedDate)].Count; x++)
                                {

                                    Show_Tijden.Add(DynamicFilmData[i]["FilmDays"][DayReturner(ConvertedDate)][x].ToString());
                                    DictofListofString[DynamicFilmData[i]["FilmTitle"].ToString()].Add(DynamicFilmData[i]["FilmDays"][DayReturner(ConvertedDate)][x].ToString());
                                }
                                if (DynamicFilmData[i]["FilmDays"][DayReturner(ConvertedDate)].Count == 1)
                                {
                                    Times = DictofListofString[DynamicFilmData[i]["FilmTitle"].ToString()][0];
                                }
                                else if (DynamicFilmData[i]["FilmDays"][DayReturner(ConvertedDate)].Count == 2)
                                {
                                    Times = DictofListofString[DynamicFilmData[i]["FilmTitle"].ToString()][0] + ", " + DictofListofString[DynamicFilmData[i]["FilmTitle"].ToString()][1];
                                }
                                else if (DynamicFilmData[i]["FilmDays"][DayReturner(ConvertedDate)].Count == 3)
                                {
                                    Times = DictofListofString[DynamicFilmData[i]["FilmTitle"].ToString()][0] + ", " + DictofListofString[DynamicFilmData[i]["FilmTitle"].ToString()][1] + ", " + DictofListofString[DynamicFilmData[i]["FilmTitle"].ToString()][2];
                                }
                                table.AddRow(("Toets [" + (Count) + "] voor " + DynamicFilmData[i]["FilmTitle"]), Times);
                                ListofFilms.Add(DynamicFilmData[i]["FilmTitle"].ToString());

                                Count++;
                            }

                        }
                        while (filmsShown == false)
                        {
                            Screens.CinemaBanner();
                            table.Write(Format.Alternative);
                            filmsShown = true;
                        }

                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("zwart");
                        int IntFilmKeuze = 0;

                        bool CorrectInput = false;
                        while (CorrectInput == false)
                        {
                            bool CorrectFilmInput = false;
                            while (CorrectFilmInput == false)
                            {
                                string Film_keuze = Console.ReadLine();
                                if (Int32.TryParse(Film_keuze, out IntFilmKeuze))
                                {
                                    if (IntFilmKeuze >= 1 && IntFilmKeuze < Count)
                                    {
                                        Scherm.Screens.CinemaBanner();
                                        Console.Write($"\nU heeft gekozen voor"); ConsoleCommands.Textkleur("rood");
                                        Console.Write($" { ListofFilms[IntFilmKeuze - 1]}"); ConsoleCommands.Textkleur("wit"); Console.Write($". Uw gekozen datum: "); ConsoleCommands.Textkleur("rood");
                                        Console.Write($"{ DayReturner(ConvertedDate)}, {FilmDateSearch}\n\n"); ConsoleCommands.Textkleur("wit"); Console.Write($"Voor welke van de onderstaande tijden zou u willen reserveren?\n");
                                        Console.WriteLine("_____________________________________________________________________________________________\n");
                                        if (DictofListofString[ListofFilms[IntFilmKeuze - 1]].Count == 1)
                                        {
                                            ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); ConsoleCommands.Textkleur("wit"); Console.Write("1"); Console.Write($"] voor: {DictofListofString[ListofFilms[IntFilmKeuze - 1]][0]}.\n");

                                        }
                                        else if (DictofListofString[ListofFilms[IntFilmKeuze - 1]].Count == 2)
                                        {
                                            ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor: {DictofListofString[ListofFilms[IntFilmKeuze - 1]][0]}.\n");
                                            ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor: {DictofListofString[ListofFilms[IntFilmKeuze - 1]][1]}.\n");
                                        }
                                        else if (DictofListofString[ListofFilms[IntFilmKeuze - 1]].Count == 3)
                                        {
                                            ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor: {DictofListofString[ListofFilms[IntFilmKeuze - 1]][0]}.\n");
                                            ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor: {DictofListofString[ListofFilms[IntFilmKeuze - 1]][1]}.\n");
                                            ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("3"); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor: {DictofListofString[ListofFilms[IntFilmKeuze - 1]][2]}.\n");
                                        }

                                        CorrectInput = true;
                                        int intTijdKeuze = 0;
                                        bool CorrectInput2 = false;
                                        while (CorrectInput2 == false)
                                        {
                                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                                            string Tijd_keuze = Console.ReadLine();
                                            if (Int32.TryParse(Tijd_keuze, out intTijdKeuze))
                                            {
                                                if (intTijdKeuze > 3 || intTijdKeuze <= 0)
                                                {
                                                    Scherm.Screens.ErrorMessageInput();
                                                }
                                                else
                                                {
                                                    if (intTijdKeuze == 1)
                                                    {
                                                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                                                        Console.Write("U heeft gekozen voor "); ConsoleCommands.Textkleur("rood"); Console.Write(DictofListofString[ListofFilms[IntFilmKeuze - 1]][0]); ConsoleCommands.Textkleur("wit");
                                                        Console.WriteLine($"\n\nEen ogenblik geduld alstublieft..");
                                                        Thread.Sleep(1500);
                                                        //ReserveerCodeMail(ListofFilms[IntFilmKeuze - 1], DictofListofString[ListofFilms[IntFilmKeuze - 1]][0], FilmDateSearch);
                                                        var stoelen = new StoelKeuze(ListofFilms[IntFilmKeuze - 1], FilmDateSearch, DictofListofString[ListofFilms[IntFilmKeuze - 1]][0]);
                                                        stoelen.Chair();
                                                        CorrectFilmInput = true;
                                                    }
                                                    else if (intTijdKeuze == 2)
                                                    {
                                                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                                                        Console.Write("U heeft gekozen voor "); ConsoleCommands.Textkleur("rood"); Console.Write(DictofListofString[ListofFilms[IntFilmKeuze - 1]][1]); ConsoleCommands.Textkleur("wit");
                                                        Console.WriteLine($"\n\nEen ogenblik geduld alstublieft..");
                                                        Thread.Sleep(1500);
                                                        //ReserveerCodeMail(ListofFilms[IntFilmKeuze - 1], DictofListofString[ListofFilms[IntFilmKeuze - 1]][1], FilmDateSearch);
                                                        var stoelen = new StoelKeuze(ListofFilms[IntFilmKeuze - 1], FilmDateSearch, DictofListofString[ListofFilms[IntFilmKeuze - 1]][1]);
                                                        stoelen.Chair();
                                                        CorrectFilmInput = true;
                                                    }
                                                    else if (intTijdKeuze == 3)
                                                    {
                                                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                                                        Console.Write("U heeft gekozen voor "); ConsoleCommands.Textkleur("rood"); Console.Write(DictofListofString[ListofFilms[IntFilmKeuze - 1]][2]); ConsoleCommands.Textkleur("wit");
                                                        Console.WriteLine($"\n\nEen ogenblik geduld alstublieft..");
                                                        Thread.Sleep(1500);
                                                        //ReserveerCodeMail(ListofFilms[IntFilmKeuze - 1], DictofListofString[ListofFilms[IntFilmKeuze - 1]][2], FilmDateSearch);
                                                        var stoelen = new StoelKeuze(ListofFilms[IntFilmKeuze - 1], FilmDateSearch, DictofListofString[ListofFilms[IntFilmKeuze - 1]][2]);
                                                        stoelen.Chair();
                                                        CorrectFilmInput = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Scherm.Screens.ErrorMessageInput();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Scherm.Screens.ErrorMessageInput();
                                    }
                                }
                                else
                                {
                                    Scherm.Screens.ErrorMessageInput();
                                }
                            }
                        }

                    }
                    else
                    {
                        bool ErrorFixed = false;
                        while (ErrorFixed == false)
                        {
                            Screens.CustomError("De ingevoerde datum is niet in de toekomst! Voer een toekomstige datum (vanaf vandaag) in.");
                            FilmDateSearch = Console.ReadLine();
                            if (IsDateUserInputInteger(FilmDateSearch) == true && ((FilmDateSearch[2].Equals('/') && FilmDateSearch[5].Equals('/')) || (FilmDateSearch[2].Equals('-') && FilmDateSearch[5].Equals('-'))))
                            {
                                if (DateInFutureCheck(DateTimeReturner(FilmDateSearch)) == true)
                                {
                                    TestDateTime = DateTimeReturner(FilmDateSearch);
                                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                                    ErrorFixed = true;
                                }
                                else
                                {
                                    ErrorFixed = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        bool IsDateUserInputInteger(string UserInput)
        {
            try
            {
                int x = 0;

                var SlicedDays = UserInput.Substring(0, 2);
                var SlicedMonths = UserInput.Substring(4, 2);
                var SlicedYears = UserInput.Substring(6, 2);
                Int32.TryParse(SlicedDays, out x);
                Int32.TryParse(SlicedMonths, out x);
                Int32.TryParse(SlicedYears, out x);
                return true;
            }
            catch
            {
                return false;
            }
        }
        DateTime DateTimeReturner(string FilmDateSearch)
        {
            while (true) {
                try
                {
                    int InputDays = 0;
                    string DaysofInput = FilmDateSearch.Substring(0, 2);
                    Int32.TryParse(DaysofInput, out InputDays);

                    int InputMonth = 0;
                    string MonthofInput = FilmDateSearch.Substring(3, 2);
                    Int32.TryParse(MonthofInput, out InputMonth);

                    int InputYear = 0;
                    string YearofInput = FilmDateSearch.Substring(6, 4);
                    Int32.TryParse(YearofInput, out InputYear);
                    var TestDateTime = new DateTime(InputYear, InputMonth, InputDays, 10, 2, 0, DateTimeKind.Local);
                    return TestDateTime;
                }
                catch
                {
                    Screens.CustomError("Dat is geen geldige datum! Probeer het opnieuw.");
                    FilmDateSearch = Console.ReadLine();
                }
            }
        }
        public string ReserveringsCodeGenerator() // Deze method genereert een random code die fungeert als reserveringscode - Callen: [CLASSOBJECT].ReserveringsCodeGenerator(); -- Probeer: Klant.ReserveringsCodeGenerator();
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[16];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var Reservatiecode = new String(stringChars);
            return Reservatiecode;
        }
        public void ReserveerCodeMail(string Gezochte_Film, string Show_Tijden, string FilmDatum = null, string[] Stoelen = null, string zaal = null) //[FILMDATUM NIET AF] Deze method regelt de reservering en mailt het vervolgens naar de gebruiker - Callen: Gebruiker.ReserveerCodeMail();
        {
            static void ReservationToJSon(Gebruiker.Gebruiker Klant, string GeneratedCode)
            {
                List<JsonData> _data = new List<JsonData>();
                string FullPathReservations = Path.GetFullPath(@"SampleLog.json");
                var DataUser = File.ReadAllText(FullPathReservations); //PATH VERANDEREN NAAR JOUW EIGEN BESTANDSLOCATIE ALS JE HIER EEN ERROR KRIJGT
                var JsonData = JsonConvert.DeserializeObject<List<JsonData>>(DataUser)
                          ?? new List<JsonData>();

                JsonData.Add(new JsonData()
                {
                    Reservatie_code = GeneratedCode,
                    Naam = Klant.Naam,
                    Email = Klant.Email,
                    Film = Klant.Film,
                    FilmTime = Klant.Film_Time,
                    FilmDate = Klant.Film_Day,
                    Stoel_num = Klant.Stoel_num,
                    Zaal = Klant.Zaal

                });

                DataUser = JsonConvert.SerializeObject(JsonData);

                File.WriteAllText(FullPathReservations, DataUser);

            }
            Gebruiker.Gebruiker Klant = new Gebruiker.Gebruiker();
            // informatie voor eventueel mailen reservatie code.
            Scherm.Screens.CinemaBanner();
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("Om te kunnen reserveren hebben wij uw naam en email adres nodig.");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("rood");
            Console.Write("Naam: ");
            ConsoleCommands.Textkleur("zwart");
            string Naam_klant = Console.ReadLine();
            ConsoleCommands.Textkleur("rood");
            Console.Write("Email Adres: ");
            ConsoleCommands.Textkleur("zwart");
            string Naam_email = Console.ReadLine();
            // Eventuele betaal methode?
            Klant.Naam = Naam_klant;
            Klant.Email = Naam_email;
            Klant.Film = Gezochte_Film;
            Klant.Film_Time = Show_Tijden;
            Klant.Film_Day = FilmDatum;
            Klant.Stoel_num = Stoelen;
            Klant.Zaal = zaal;
            ReserveringStatus(Klant);
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("Kloppen de bovenstaande gegevens?\n\n");
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] Ja\t["); ConsoleCommands.Textkleur("zwart");
            Console.Write(2);ConsoleCommands.Textkleur("wit"); Console.Write("] Nee\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
            string UserInput = Console.ReadLine();
            bool CorrectInput = false;
            while (CorrectInput != true)
            {
                if (UserInput == "1")
                {
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Klant.SnacksOption(Klant);
                    CorrectInput = true;
                }
                else if (UserInput == "2")
                {
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Console.WriteLine("We vragen u opnieuw voor uw gegevens.");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("rood");
                    Console.Write("Naam: ");
                    ConsoleCommands.Textkleur("zwart");
                    Naam_klant = Console.ReadLine();
                    ConsoleCommands.Textkleur("rood");
                    Console.Write("Email Adres: ");
                    ConsoleCommands.Textkleur("zwart");
                    Naam_email = Console.ReadLine();
                    Klant.Naam = Naam_klant;
                    Klant.Email = Naam_email;
                    ReserveringStatus(Klant);
                    Klant.SnacksOption(Klant);
                    CorrectInput = true;
                }
                else
                {
                    Scherm.Screens.ErrorMessageInput();
                    UserInput = Console.ReadLine();
                }
            }

            // Einde reserveren.
            Scherm.Screens.CinemaBanner();
            ReserveringStatus(Klant);
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            Console.WriteLine("Bedankt voor het reserveren!\n");
            Console.WriteLine("Een ogenblik geduld alstublieft uw reserveringscode wordt geladen.");
            Thread.Sleep(1000);
            ReservationCodePercentage();
            string GeneratedCode = this.ReserveringsCodeGenerator();
            // Random generator voor het maken van de reservatie code.


            ConsoleCommands.Textkleur("wit");
            Console.Write("Reserverings Code: ");
            ConsoleCommands.Textkleur("rood");
            Console.WriteLine(GeneratedCode);
            ConsoleCommands.Textkleur("wit");
            Console.Write("\nEr is een bevestigingsmail verzonden naar: "); ConsoleCommands.Textkleur("rood"); Console.Write($"{Klant.Email}\n");
            
            Mail_Sender(Klant, GeneratedCode);
            ReservationToJSon(Klant, GeneratedCode);

            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("Bedankt voor het online reserveren en we zien u graag binnenkort in onze bioscoop."); ConsoleCommands.Textkleur("rood"); Console.Write("\nSla uw reserverings code op!\n"); ConsoleCommands.Textkleur("wit");
            Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands CommandLine = new ConsoleCommands();
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("]  Om af te sluiten\n");
            // Email bevestiging.
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            string Mail_Bevestiging = Console.ReadLine();
            while (true)
            {
                if (Mail_Bevestiging == "1")
                {
                    CommandLine.RestartOption();
                    // Data Reservering toevoegen.

                }
                else
                {
                    Screens.ErrorMessageInput();
                    Mail_Bevestiging = Console.ReadLine();
                }
            }

        }
        public void Mail_Sender(Gebruiker.Gebruiker Klant, string GeneratedCode)
        {
            try
            {
                var message = new MimeMessage();
                // Email verzender
                message.From.Add(new MailboxAddress("ProjectB", "ProjectB1J@gmail.com"));
                // Email geadresseerde
                message.To.Add(new MailboxAddress(Klant.Naam, Klant.Email));
                // Email onderwerp
                message.Subject = $"Bevestiging Bioscoop Reservering {Klant.Film}";
                // Email text
                string StoelenKlant = "";
                for (int i = 0; i < Klant.Stoel_num.Length; i++)
                {
                    StoelenKlant += Klant.Stoel_num[i] + " ";
                    
                }
                message.Body = new TextPart("plain")
                {
                    Text = @$"Hallo {Klant.Naam},

Bedankt voor het reserveren via onze bioscoop.

Hieronder vindt u de reserverings code.

Reserverings code: {GeneratedCode}
Film: {Klant.Film}
Datum: {Klant.Film_Day}
Tijd: {Klant.Film_Time}
Zaal: {Klant.Zaal}
Stoel(en): {StoelenKlant}

" +

@"
We hopen u snel te zien in de bioscoop!
" +
    "\nMet vriendelijke groet,\n\n" +
    "CinemaReservation"

                };


                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);

                    // authenticate smtp server
                    client.Authenticate("ProjectB1J@gmail.com", "Hogeschoolrotterdam");
                    // verzenden email
                    client.Send(message);
                    client.Disconnect(true);
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                };
            }
            catch
            {
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.Write("Het versturen van de bevestiging is");ConsoleCommands.Textkleur("rood"); Console.Write(" niet "); ConsoleCommands.Textkleur("wit"); Console.Write("gelukt!\n");
                Console.WriteLine("_____________________________________________________________________________________________\n");
            }
            
        }
        public static void Reservering_check(dynamic dynamicUserData, int i)
        {
            
            Console.Write("Naam: "); ConsoleCommands.Textkleur("rood"); Console.Write(dynamicUserData[i]["Naam"] + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Email Adres: "); ConsoleCommands.Textkleur("rood"); Console.Write(dynamicUserData[i]["Email"] + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Film: "); ConsoleCommands.Textkleur("rood"); Console.Write(dynamicUserData[i]["Film"] + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Datum: "); ConsoleCommands.Textkleur("rood"); Console.Write(dynamicUserData[i]["FilmDate"] + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Tijd: "); ConsoleCommands.Textkleur("rood"); Console.Write(dynamicUserData[i]["FilmTime"] + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Zaal: "); ConsoleCommands.Textkleur("rood"); Console.Write(dynamicUserData[i]["Zaal"] + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Stoel: "); ConsoleCommands.Textkleur("rood"); Console.Write(dynamicUserData[i]["Stoel_num"] + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Reservering Code: "); ConsoleCommands.Textkleur("rood"); Console.Write(dynamicUserData[i]["Reservatie_code"] + "\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");


        }
        public void ReserveringAnnuleren(int Index, dynamic DynamicUserData)
        {
            Console.WriteLine("Weet u zeker dat u uw reservering wilt annuleren?");
            Console.Write("Om te annuleren toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write("] om het programma opnieuw op te starten toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write("]\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart"); string Optie = Console.ReadLine();
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            if(Optie == "1")
            {
                string Username = (string)DynamicUserData[Index]["Naam"];
                string Email = (string)DynamicUserData[Index]["Email"];
                string Film = (string)DynamicUserData[Index]["Film"];
                string Rescode = (string)DynamicUserData[Index]["Reservatie_code"];
                string datum = (string)DynamicUserData[Index]["FilmDate"];
                string FilmTime = (string)DynamicUserData[Index]["FilmTime"];
                try
                {
                    var message = new MimeMessage();
                    // Email verzender
                    message.From.Add(new MailboxAddress("ProjectB", "ProjectB1J@gmail.com"));
                    // Email geadresseerde
                    message.To.Add(new MailboxAddress(Username, Email));
                    // Email onderwerp
                    message.Subject = $"Annulering Bioscoop Reservering {Film}";
                    // Email text
                    message.Body = new TextPart("plain")
                    {
                        Text = @$"Hallo {Username},

U heeft uw reservering voor de film {Film} geannuleerd.

Hieronder vindt u de reservering die u heeft geannuleerd:

-   Reserverings code: {Rescode}
-   Film: {Film}
-   Datum: {datum}
-   Tijd: {FilmTime}

" +

    @"
We hopen u voldoende te hebben ge??nformeerd.
" +
        "\nMet vriendelijke groet,\n\n" +
        "CinemaReservation"

                    };


                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);

                        // authenticate smtp server
                        client.Authenticate("ProjectB1J@gmail.com", "Hogeschoolrotterdam");
                        // verzenden email
                        client.Send(message);
                        client.Disconnect(true);
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    };
                }
                catch
                {
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Het versturen van de bevestiging is niet gelukt.");
                }
                string FullPathReservations = Path.GetFullPath(@"SampleLog.json");
                DynamicUserData.Remove(DynamicUserData[Index]);
                dynamic UserData = JsonConvert.SerializeObject(DynamicUserData);
                File.WriteAllText(FullPathReservations, UserData);
                Console.WriteLine("Uw reservering is geannuleerd, we hopen u snel weer te zien in onze bioscoop");
            }
            if (Optie == "2")
            {
                Console.Clear();
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Environment.Exit(1);
            }
        }
        public void ReserveringStatus(Gebruiker.Gebruiker Klant, params string[] args)
        {
            Scherm.Screens.CinemaBanner();
            Console.WriteLine($"\t\t\t\tRESERVERING\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            Console.Write("Naam: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Naam + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Email Adres: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Email + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Film: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Film + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Datum: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Film_Day + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Tijd: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Film_Time + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Zaal: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Zaal + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write($"Stoel(en): ");
            for (int i = 0; i < Klant.Stoel_num.Length; i++)
            {
                ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Stoel_num[i]);
                if (i == Klant.Stoel_num.Length - 1)
                {
                    Console.Write("\n");
                }
                else
                {
                    Console.Write(", ");
                }
            }
            
            ConsoleCommands.Textkleur("wit");
        }
        public bool DateInFutureCheck(DateTime UserInput)
        {
            DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            var UnixNow = (UserInput - Epoch).TotalSeconds;
            if (unixTimestamp > UnixNow)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string DateConverter(string InputDate, DateTime TestDateTime)
        {
            


                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                var UnixDateTime = (TestDateTime.ToUniversalTime() - epoch).TotalSeconds; // hier wordt het geconvert naar Unix!
                var timeSpan = TimeSpan.FromSeconds(UnixDateTime);
                var localDateTime = epoch.Add(timeSpan).ToLocalTime(); // Weer terug geconvert naar DateTime

                string UserChosenDay = localDateTime.ToString("ddd"); // dit convert het naar de dag van de week (dus: Mon/Tues/Wed/Thu/Fri/Sat/Sun)
                return UserChosenDay;

        }
        public string DayReturner(string InputDay)
        {
            if (InputDay == "zo")
            {
                return "Zondag";
            }
            else if (InputDay == "ma")
            {
                return "Maandag";
            }
            else if (InputDay == "di")
            {
                return "Dinsdag";
            }
            else if (InputDay == "wo")
            {
                return "Woensdag";
            }
            else if (InputDay == "do")
            {
                return "Donderdag";
            }
            else if (InputDay == "vr")
            {
                return "Vrijdag";
            }
            else
            {
                return "Zaterdag";
            }
        }
        public void BetalingStatusFilm(Gebruiker.Gebruiker Klant, params string[] args)
        {
            Scherm.Screens.CinemaBanner();
            Console.WriteLine($"\t\t\t\tAfrekenen\n\n");
            Console.Write("\t\tU heeft de volgende film geselecteerd: \n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            Console.Write("Naam: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Naam + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Email Adres: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Email + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Film: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Film + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Datum: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Film_Day + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Tijd: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Film_Time + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Zaal: "); ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Zaal + "\n");
            ConsoleCommands.Textkleur("wit"); Console.Write($"Stoel(en): ");
            for (int i = 0; i < Klant.Stoel_num.Length; i++)
            {
                ConsoleCommands.Textkleur("rood"); Console.Write(Klant.Stoel_num[i]);
                if (i == Klant.Stoel_num.Length - 1)
                {
                    Console.Write("\n");
                }
                else
                {
                    Console.Write(", ");
                }
            }

            ConsoleCommands.Textkleur("wit");
        }
        public void Betaling(Gebruiker.Gebruiker Klant = null, decimal totaal = 0, List<string> Mandje = null)
        {
            
            dynamic DynamicFilmData = JsonData.JsonSerializer("Films");
            string Filmprice = DynamicFilmData[0]["FilmPrice"];
            ConsoleCommands.Textkleur("wit");
            decimal korting = 0;
            string stringTotaal = "";
            int Count = 1;
            if (Klant != null && Mandje == null)
            {
                this.BetalingStatusFilm(Klant);
            }
            if (Mandje != null)
            {
                this.BetalingStatusFilm(Klant);
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.WriteLine("U heeft de volgende snacks geselecteerd: ");
                ConsoleCommands.Textkleur("rood");
                for (int i = 0; i < Mandje.Count; i += 2)
                {
                    ConsoleCommands.Textkleur("wit"); Console.Write("\n["); ConsoleCommands.Textkleur("rood"); Console.Write(Count); ConsoleCommands.Textkleur("wit"); Console.Write($"] {Mandje[i]}\n");
                    Count++;
                }
                ConsoleCommands.Textkleur("wit");
            }
            if (Mandje == null)
            {
                totaal += Convert.ToDecimal(Filmprice, new CultureInfo("en-US"));
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                Console.OutputEncoding = System.Text.Encoding.UTF8; ConsoleCommands.Textkleur("wit");
                Console.Write($"De totaal prijs is: "); ConsoleCommands.Textkleur("rood"); System.Console.Out.Write("????? " + totaal + "\n");ConsoleCommands.Textkleur("wit");
                Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.Write("Hoe zou u willen betalen?\n\nToets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write("] voor IDEAL\nToets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write("] voor Paypal\n");
                Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                Console.ReadLine(); 
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("_____________________________________________________________________________________________\n");
                Thread.Sleep(3000);
            }
            else
            {
                totaal += Convert.ToDecimal(Filmprice, new CultureInfo("en-US"));
                korting = (totaal * Convert.ToDecimal(0.15, new CultureInfo("en-US")));
                totaal -= korting;
                stringTotaal = String.Format("{0:0.00}", totaal);
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.OutputEncoding = System.Text.Encoding.UTF8; ConsoleCommands.Textkleur("wit");
                Console.Write($"De totaal prijs is: "); ConsoleCommands.Textkleur("rood"); System.Console.Out.Write("????? " + stringTotaal + "\n");ConsoleCommands.Textkleur("wit");

                Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.Write("Hoe zou u willen betalen?\n\nToets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write("] voor IDEAL\nToets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write("] voor Paypal\n");
                Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("_____________________________________________________________________________________________\n");
                Thread.Sleep(3000);
            }
            


        }

    }

}

