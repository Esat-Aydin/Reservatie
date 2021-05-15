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
using ShoppingCart;

namespace Gebruiker
{
    public class Gebruiker : Reserveren
    {
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Film { get; set; }
        public string Film_Time { get; set; }
        public string Film_Day { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }


        public Gebruiker(string Naam = null, string Email = null, string Password = null, bool isAdmin = false)
        {
            this.Naam = Naam;
            this.Email = Email;
            // Password word gebruikt als de user of (A): een nieuw account maakt of (B): een bestaand account heeft en in wilt loggen
            this.Password = Password;
            // isAdmin word gebruikt als de user admin rechten heeft 
            this.isAdmin = isAdmin;

        }


        public void UserInputMethod(string UserInput)
        {
            Film.Film FilmObject = new Film.Film();
            MedewerkerClass.Medewerker admin = new MedewerkerClass.Medewerker();
            Gebruiker Klant = new Gebruiker();
            ConsoleCommands CommandLine = new ConsoleCommands();
            // Inladen Json Module 
            dynamic DynamicData = JsonData.JsonSerializer("Snacks");
            dynamic DynamicUserData = JsonData.JsonSerializer("Users");
            dynamic DynamicFilmData = JsonData.JsonSerializer("Films");
            string TitleofFilm = null;
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("wit");
            if (this.isAdmin == true && (UserInput == "!password"))
            {
                Scherm.Screens.CinemaBanner();
                Console.WriteLine("\t\t\t ADMIN CONSOLE");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.WriteLine("Type nu het huidige admin wachtwoord in: ");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                UserInput = Console.ReadLine();

                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                if (UserInput == Password)
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Type nu het nieuwe wachtwoord in: ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    UserInput = Console.ReadLine();
                    Password = UserInput;
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Het wachtwoord is succesvol veranderd naar: " + Password);
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                }
            }
            if (this.isAdmin == true && (UserInput == "!help"))
            {
                Scherm.Screens.CinemaBanner();
                Console.WriteLine("\t\t\t ADMIN CONSOLE");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.WriteLine("Om het admin-wachtwoord opnieuw in te stellen, type: !password");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Om een film toe te voegen aan de database, type: !newfilm");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Om alle reserveringen te bekijken, type: !reserveringen");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Om reserveringen per zaal te zien, type: !zaalreserveringen");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            }
            if (this.isAdmin == true && (UserInput == "!newfilm"))
            {
                Scherm.Screens.CinemaBanner();
                Console.WriteLine("\t\t\t FILM TOEVOEGEN");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.WriteLine("Hoeveel genre's heeft de nieuwe film? Er is een maximum van drie genre's!");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                var StringArrayGenreLength_Input = Console.ReadLine();
                try
                {
                    int StringArrayGenreLength = Int32.Parse(StringArrayGenreLength_Input);
                    string[] FilmGenresArray = new string[StringArrayGenreLength];
                    Scherm.Screens.CinemaBanner();
                    Console.WriteLine("\t\t\t FILM TOEVOEGEN");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    if (StringArrayGenreLength == 1)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U kunt kiezen uit de volgende genres: "); ConsoleCommands.Textkleur("rood"); Console.Write("\n1. Action \n2. Comedy \n3. Thriller \n4. Romantic \n5. Horror \n6. Drama\n\n"); ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("Voer nu de genre van de film in: "); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[0] = Console.ReadLine();

                    }
                    else if (StringArrayGenreLength == 2)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Voer nu de eerste genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[0] = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Voer nu de tweede genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[1] = Console.ReadLine();
                    }
                    else if (StringArrayGenreLength == 3)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Voer nu de eerste genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[0] = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Voer nu de tweede genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[1] = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Voer nu de derde genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[2] = Console.ReadLine();
                    }
                    //-Dictionary met alle genres-//
                    Dictionary<string, string> DictOfGenres = new Dictionary<string, string>()
                        {
                            {"1", "Action"},
                            {"2", "Comedy"},
                            {"3", "Thriller"},
                            {"4", "Romantic"},
                            {"5", "Horror"},
                            {"6", "Drama" }
                        };
                    //---------------------------// Moet nog worden ingecodeerd in de code hieronder //

                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Console.WriteLine("Voer nu de titel van de nieuwe film in:");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                    TitleofFilm = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Console.WriteLine("Voer nu de zaal in van de film " + TitleofFilm + ": ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");

                    var FilmZaalInput = Console.ReadLine(); int RoomofFilm = Int32.Parse(FilmZaalInput); // Userinput (string) word hier verandert naar een int variabel

                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Console.WriteLine("Voer nu de gewenste dag in"); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    var dag_UserInput = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Console.WriteLine("Hoeveel tijdssloten wilt u beschikbaar stellen per dag? (maximaal 3): ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                    try
                    {
                        var tijdsSlotenInput = Console.ReadLine();
                        int TimeSlots = Int32.Parse(tijdsSlotenInput); // Userinput (string) word hier verandert naar een int variabel
                        string[] FilmTimesArray = new string[TimeSlots];
                        var listoftimes1 = new List<string>();

                        var listoftimes2 = new List<string>();

                        var listoftimes3 = new List<string>();

                        var listoftimes4 = new List<string>();

                        var listoftimes5 = new List<string>();

                        var listoftimes6 = new List<string>();

                        var listoftimes7 = new List<string>();



                        if (TimeSlots == 1)
                        {
                            Dictionary<string, List<string>> DictOf = new Dictionary<string, List<string>>()
                        {
                            {"Maandag", listoftimes1},
                            {"Dinsdag", listoftimes2 },
                            {"Woensdag", listoftimes3},
                            {"Donderdag", listoftimes4},
                            {"Vrijdag", listoftimes5},
                            {"Zaterdag", listoftimes6},
                            {"Zondag", listoftimes7}
                        };
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine("Voer nu de tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart");

                            FilmTimesArray[0] = Console.ReadLine();
                            if (dag_UserInput == "Maandag")
                                DictOf["Maandag"].Add(FilmTimesArray[0]);
                            else if (dag_UserInput == "Dinsdag")
                                DictOf["Dinsdag"].Add(FilmTimesArray[0]);
                            else if (dag_UserInput == "Woensdag")
                                DictOf["Woensdag"].Add(FilmTimesArray[0]);
                            else if (dag_UserInput == "Donderdag")
                                DictOf["Donderdag"].Add(FilmTimesArray[0]);
                            else if (dag_UserInput == "Vrijdag")
                                DictOf["Vrijdag"].Add(FilmTimesArray[0]);
                            else if (dag_UserInput == "Zaterdag")
                                DictOf["Zaterdag"].Add(FilmTimesArray[0]);
                            else
                            {
                                DictOf["Zondag"].Add(FilmTimesArray[0]);
                            }
                            // Hier worden de FilmObject attributes verandert naar de values die net zijn doorgevoerd in de console door de admin-user //
                            // this.FilmGenres = FilmGenresArray; this.FilmTitle = TitleofFilm; this.FilmTimes = FilmTimesArray; this.FilmRoom = RoomofFilm;
                            FilmObject = new Film.Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray, DictOf);
                            FilmObject.AddFilmtoDataBase(FilmObject); // Dit voegt het object toe aan de Json file
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                            //-------------------------------------------------------------------------------------------------------------------------//
                            // Nu wordt de volgende console input gecheckt door de UserInputMethod() function te callen // 
                            UserInput = Console.ReadLine();
                            UserInputMethod(UserInput);
                        }
                        else if (TimeSlots == 2)
                        {
                            Dictionary<string, List<string>> DictOf = new Dictionary<string, List<string>>()
                        {
                            {"Maandag", listoftimes1},
                            {"Dinsdag", listoftimes2 },
                            {"Woensdag", listoftimes3},
                            {"Donderdag", listoftimes4},
                            {"Vrijdag", listoftimes5},
                            {"Zaterdag", listoftimes6},
                            {"Zondag", listoftimes7}
                        };
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart");
                            FilmTimesArray[0] = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine("Voer nu de tweede tijd van de film in: ");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart");
                            FilmTimesArray[1] = Console.ReadLine();
                            if (dag_UserInput == "Maandag")
                            {
                                DictOf["Maandag"].Add(FilmTimesArray[0]); DictOf["Maandag"].Add(FilmTimesArray[1]);
                            }
                            else if (dag_UserInput == "Dinsdag")
                            {
                                DictOf["Dinsdag"].Add(FilmTimesArray[0]); DictOf["Dinsdag"].Add(FilmTimesArray[1]);
                            }
                            else if (dag_UserInput == "Woensdag")
                            {
                                DictOf["Woensdag"].Add(FilmTimesArray[0]); DictOf["Woensdag"].Add(FilmTimesArray[1]);
                            }
                            else if (dag_UserInput == "Donderdag")
                            {
                                DictOf["Donderdag"].Add(FilmTimesArray[0]); DictOf["Donderdag"].Add(FilmTimesArray[1]);
                            }
                            else if (dag_UserInput == "Vrijdag")
                            {
                                DictOf["Vrijdag"].Add(FilmTimesArray[0]); DictOf["Vrijdag"].Add(FilmTimesArray[1]);
                            }
                            else if (dag_UserInput == "Zaterdag")
                            {
                                DictOf["Zaterdag"].Add(FilmTimesArray[0]); DictOf["Zaterdag"].Add(FilmTimesArray[1]);
                            }
                            else
                            {
                                DictOf["Zondag"].Add(FilmTimesArray[0]); DictOf["Zondag"].Add(FilmTimesArray[1]);
                            }
                            // Hier worden de FilmObject attributes verandert naar de values die net zijn doorgevoerd in de console door de admin-user //
                            // this.FilmGenres = FilmGenresArray; this.FilmTitle = TitleofFilm; this.FilmTimes = FilmTimesArray; this.FilmRoom = RoomofFilm;
                            FilmObject = new Film.Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray, DictOf);
                            FilmObject.AddFilmtoDataBase(FilmObject); // Dit voegt het object toe aan de Json file
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                            //-------------------------------------------------------------------------------------------------------------------------//
                            // Nu wordt de volgende console input gecheckt door de UserInputMethod() function te callen // 
                            UserInput = Console.ReadLine();
                            UserInputMethod(UserInput);
                        }
                        else if (TimeSlots == 3)
                        {
                            Dictionary<string, List<string>> DictOf = new Dictionary<string, List<string>>()
                        {
                            {"Maandag", listoftimes1},
                            {"Dinsdag", listoftimes2 },
                            {"Woensdag", listoftimes3},
                            {"Donderdag", listoftimes4},
                            {"Vrijdag", listoftimes5},
                            {"Zaterdag", listoftimes6},
                            {"Zondag", listoftimes7}
                        };
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart");
                            FilmTimesArray[0] = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine("Voer nu de tweede tijd van de film in: ");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart");
                            FilmTimesArray[1] = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine("Voer nu de derde tijd van de film in: ");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart");
                            FilmTimesArray[2] = Console.ReadLine();
                            if (dag_UserInput == "Maandag")
                            {
                                DictOf["Maandag"].Add(FilmTimesArray[0]); DictOf["Maandag"].Add(FilmTimesArray[1]); DictOf["Maandag"].Add(FilmTimesArray[2]);
                            }
                            else if (dag_UserInput == "Dinsdag")
                            {
                                DictOf["Dinsdag"].Add(FilmTimesArray[0]); DictOf["Dinsdag"].Add(FilmTimesArray[1]); DictOf["Dinsdag"].Add(FilmTimesArray[2]);
                            }
                            else if (dag_UserInput == "Woensdag")
                            {
                                DictOf["Woensdag"].Add(FilmTimesArray[0]); DictOf["Woensdag"].Add(FilmTimesArray[1]); DictOf["Woensdag"].Add(FilmTimesArray[2]);
                            }
                            else if (dag_UserInput == "Donderdag")
                            {
                                DictOf["Donderdag"].Add(FilmTimesArray[0]); DictOf["Donderdag"].Add(FilmTimesArray[1]); DictOf["Donderdag"].Add(FilmTimesArray[2]);
                            }
                            else if (dag_UserInput == "Vrijdag")
                            {
                                DictOf["Vrijdag"].Add(FilmTimesArray[0]); DictOf["Vrijdag"].Add(FilmTimesArray[1]); DictOf["Vrijdag"].Add(FilmTimesArray[2]);
                            }
                            else if (dag_UserInput == "Zaterdag")
                            {
                                DictOf["Zaterdag"].Add(FilmTimesArray[0]); DictOf["Zaterdag"].Add(FilmTimesArray[1]); DictOf["Zaterdag"].Add(FilmTimesArray[2]);
                            }
                            else
                            {
                                DictOf["Zondag"].Add(FilmTimesArray[0]); DictOf["Zondag"].Add(FilmTimesArray[1]); DictOf["Zondag"].Add(FilmTimesArray[2]);
                            }
                            // Hier worden de FilmObject attributes verandert naar de values die net zijn doorgevoerd in de console door de admin-user //
                            // this.FilmGenres = FilmGenresArray; this.FilmTitle = TitleofFilm; this.FilmTimes = FilmTimesArray; this.FilmRoom = RoomofFilm;
                            FilmObject = new Film.Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray, DictOf);
                            FilmObject.AddFilmtoDataBase(FilmObject); // Dit voegt het object toe aan de Json file
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                            //-------------------------------------------------------------------------------------------------------------------------//
                            // Nu wordt de volgende console input gecheckt door de UserInputMethod() function te callen // 
                            UserInput = Console.ReadLine();
                            UserInputMethod(UserInput);
                        }
                    }
                    catch
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("rood");
                        Console.WriteLine("U moet een getal invoeren! (1, 2, 3, 4, etc.");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("zwart");
                    }

                }
                catch
                {
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("rood");
                    Console.WriteLine("U moet een getal invoeren! (1, 2, 3, 4, etc.");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                }



            }

            UserInput = Console.ReadLine();
            UserInputMethod(UserInput);
        }

        public void AccountCreate(Gebruiker Object) // Eerst een object maken, dan hier als parameter in vullen om het te pushen naar de JSon file
        {
            List<Gebruiker> _data = new List<Gebruiker>();
            var AccountUsers = new WebClient().DownloadString(@"C:\Users\woute\source\repos\Esat-Aydin\Reservatie\Reservatie\AccountUsers.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path
            var AccountUsers_Gebruiker = JsonConvert.DeserializeObject<List<Gebruiker>>(AccountUsers);
            AccountUsers_Gebruiker.Add(Object);

            AccountUsers = JsonConvert.SerializeObject(AccountUsers_Gebruiker);
            File.WriteAllText(@"C:\Users\woute\source\repos\Esat-Aydin\Reservatie\Reservatie\AccountUsers.json", AccountUsers); // Net als AccountUsers de path veranderen als je hier errors krijgt!
        }
        public void ZoekOptie(string Gezochte_Film, dynamic DynamicFilmData)
        {

            ConsoleCommands.Textkleur("rood");
            List<string> Legelist = new List<string>();
            List<string> DagenvdWeek = new List<string>();
            List<string> Show_Tijden = new List<string>();
            Dictionary<string, Dictionary<string, List<string>>> DictofTimes = new();
            string Times = null;
            DagenvdWeek.Add("Maandag");
            DagenvdWeek.Add("Dinsdag");
            DagenvdWeek.Add("Woensdag");
            DagenvdWeek.Add("Donderdag");
            DagenvdWeek.Add("Vrijdag");
            DagenvdWeek.Add("Zaterdag");
            DagenvdWeek.Add("Zondag");
            int Count = 1;
            var table = new ConsoleTable("Dagen van de week", "Draaitijd");
            DictofTimes[Gezochte_Film] = new Dictionary<string, List<string>>();
            DictofTimes[Gezochte_Film]["Maandag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Dinsdag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Woensdag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Donderdag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Vrijdag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Zaterdag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Zondag"] = new List<string>();
            for (int i = 0; i < DynamicFilmData.Count; i++)
            {
                for (int j = 0; j < DagenvdWeek.Count; j++)
                {
                    if (DynamicFilmData[i]["FilmTitle"] == Gezochte_Film)
                    {
                        if (DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count > 0)
                        {
                           for (int x = 0; x < DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count; x++)
                            { 
                                Show_Tijden.Add(DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]][x].ToString());
                                DictofTimes[Gezochte_Film][DagenvdWeek[j]].Add(DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]][x].ToString());
                            }
                                if (DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count == 1)
                                {
                                    Times = DictofTimes[DynamicFilmData[i]["FilmTitle"].ToString()][DagenvdWeek[j]][0];
                                }
                                else if (DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count == 2)
                                {
                                    Times = DictofTimes[DynamicFilmData[i]["FilmTitle"].ToString()][DagenvdWeek[j]][0] + ", " + DictofTimes[DynamicFilmData[i]["FilmTitle"].ToString()][DagenvdWeek[j]][1];
                                }
                                else if (DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count == 3)
                                {
                                    Times = DictofTimes[DynamicFilmData[i]["FilmTitle"].ToString()][DagenvdWeek[j]][0] + ", " + DictofTimes[DynamicFilmData[i]["FilmTitle"].ToString()][DagenvdWeek[j]][1] + ", " + DictofTimes[DynamicFilmData[i]["FilmTitle"].ToString()][DagenvdWeek[j]][2];
                                }
                            table.AddRow(("Toets [" + (Count) + "] voor " + DagenvdWeek[j]), Times);
                            Count++;
                        }
                        else if (DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count <= 0)
                        {
                            for (int x = 0; x <= DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count; x++)
                            {
                                table.AddRow("Toets [" + (Count) + "] voor " + (DagenvdWeek[j]) + ": ", "Deze film draait niet op " + DagenvdWeek[j] + ".");
                                Count++;
                            }
                        }
                    }
                }
            }
            ConsoleCommands.Textkleur("wit");
            table.Write(Format.Alternative);
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            Console.WriteLine("Voor welke van de bovenstaande dagen zou u willen reserveren?");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            string DagenKeuze = Console.ReadLine();
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            static DateTime GetNextWeekday(DayOfWeek day)
            {
                DateTime result = DateTime.Now.AddDays(1);
                while (result.DayOfWeek != day)
                    result = result.AddDays(1);
                return result;
            }
            if (DagenKeuze == "1")
            {
                if (table.Rows[0][1].ToString() == "Deze film draait niet op Maandag.")
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Voor de geselecteerde dag draait de film niet.\nProbeer het opnieuw.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    ZoekOptie(Gezochte_Film, DynamicFilmData);
                }
                else
                {
                    string ConvertedToDate = GetNextWeekday(DayOfWeek.Monday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("U heeft gekozen voor Maandag, voor welk tijdslot zou u willen reserveren?");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    if (DictofTimes[Gezochte_Film]["Maandag"].Count == 1)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Maandag"][0]}\n");
                    }
                    else if (DictofTimes[Gezochte_Film]["Maandag"].Count == 2)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Maandag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Maandag"][1]}\n");
                    }
                    else if (DictofTimes[Gezochte_Film]["Maandag"].Count == 3)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Maandag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Maandag"][1]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(3); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Maandag"][2]}\n");
                    }                   
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    string tijdslot = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    if (tijdslot == "1")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Maandag"][0] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Maandag"][0], ConvertedToDate);
                    }
                    if (tijdslot == "2")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Maandag"][1] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Maandag"][1], ConvertedToDate);
                    }
                    if (tijdslot == "3")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Maandag"][2] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Maandag"][2], ConvertedToDate);
                    }
                }
            }
            else if (DagenKeuze == "2")
            {
                if (table.Rows[1][1].ToString() == "Deze film draait niet op Dinsdag.")
                {
                    Console.WriteLine("Voor de geselecteerde dag draait de film niet.\nProbeer het opnieuw.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    ZoekOptie(Gezochte_Film, DynamicFilmData);
                }
                else
                {
                    string ConvertedToDate = GetNextWeekday(DayOfWeek.Tuesday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                    Console.WriteLine("U heeft gekozen voor Dinsdag, voor welk tijdslot zou u willen reserveren?");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    if (DictofTimes[Gezochte_Film]["Dinsdag"].Count == 1)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Dinsdag"][0]}\n");
                    }
                    else if (DictofTimes[Gezochte_Film]["Dinsdag"].Count == 2)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Dinsdag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Dinsdag"][1]}\n");
                    }
                    else if (DictofTimes[Gezochte_Film]["Dinsdag"].Count == 3)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Dinsdag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Dinsdag"][1]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(3); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Dinsdag"][2]}\n");
                    }
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    string tijdslot = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    if (tijdslot == "1")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Dinsdag"][0] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Dinsdag"][0], ConvertedToDate);
                    }
                    if (tijdslot == "2")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Dinsdag"][1] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Dinsdag"][1], ConvertedToDate);
                    }
                    if (tijdslot == "3")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Dinsdag"][2] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Dinsdag"][2], ConvertedToDate);
                    }
                }
            }
            else if (DagenKeuze == "3")
            {
                if (table.Rows[2][1].ToString() == "Deze film draait niet op Woensdag.")
                {
                    Console.WriteLine("Voor de geselecteerde dag draait de film niet.\nProbeer het opnieuw.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    ZoekOptie(Gezochte_Film, DynamicFilmData);
                }
                else
                {
                    string ConvertedToDate = GetNextWeekday(DayOfWeek.Wednesday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                    Console.WriteLine("U heeft gekozen voor Woensdag, voor welk tijdslot zou u willen reserveren?");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    if (DictofTimes[Gezochte_Film]["Woensdag"].Count == 1)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Woensdag"][0]}\n");
                    }
                    else if (DictofTimes[Gezochte_Film]["Woensdag"].Count == 2)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Woensdag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Woensdag"][1]}\n");

                    }
                    else if (DictofTimes[Gezochte_Film]["Woensdag"].Count == 3)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Woensdag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Woensdag"][1]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(3); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Woensdag"][2]}\n");
                    }
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    string tijdslot = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    if (tijdslot == "1")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Woensdag"][0] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Woensdag"][0], ConvertedToDate);
                    }
                    if (tijdslot == "2")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Woensdag"][1] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Woensdag"][1], ConvertedToDate);
                    }
                    if (tijdslot == "3")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Woensdag"][2] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Woensdag"][2], ConvertedToDate);
                    }
                }
            }
            else if (DagenKeuze == "4")
            {
                if (table.Rows[3][1].ToString() == "Deze film draait niet op Donderdag.")
                {
                    Console.WriteLine("Voor de geselecteerde dag draait de film niet.\nProbeer het opnieuw.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    ZoekOptie(Gezochte_Film, DynamicFilmData);
                }
                else
                {
                    string ConvertedToDate = GetNextWeekday(DayOfWeek.Thursday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                    Console.WriteLine("U heeft gekozen voor Donderdag, voor welk tijdslot zou u willen reserveren?");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    if (DictofTimes[Gezochte_Film]["Donderdag"].Count == 1)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Donderdag"][0]}\n");
                    }
                    else if (DictofTimes[Gezochte_Film]["Donderdag"].Count == 2)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Donderdag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Donderdag"][1]}\n");

                    }
                    else if (DictofTimes[Gezochte_Film]["Donderdag"].Count == 3)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Donderdag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Donderdag"][1]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(3); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Donderdag"][2]}\n");
                    }
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    string tijdslot = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    if (tijdslot == "1")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Donderdag"][0] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Donderdag"][0], ConvertedToDate);
                    }
                    if (tijdslot == "2")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Donderdag"][1] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Donderdag"][1], ConvertedToDate);
                    }
                    if (tijdslot == "3")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Donderdag"][2] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Donderdag"][2], ConvertedToDate);
                    }
                }
            }
            else if (DagenKeuze == "5")
            {
                if (table.Rows[4][1].ToString() == "Deze film draait niet op Vrijdag.")
                {
                    Console.WriteLine("Voor de geselecteerde dag draait de film niet.\nProbeer het opnieuw.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    ZoekOptie(Gezochte_Film, DynamicFilmData);
                }
                else
                {
                    string ConvertedToDate = GetNextWeekday(DayOfWeek.Friday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                    Console.WriteLine("U heeft gekozen voor Vrijdag, voor welk tijdslot zou u willen reserveren?");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    if (DictofTimes[Gezochte_Film]["Vrijdag"].Count == 1)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Vrijdag"][0]}\n");
                    }
                    else if (DictofTimes[Gezochte_Film]["Vrijdag"].Count == 2)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Vrijdag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Vrijdag"][1]}\n");

                    }
                    else if (DictofTimes[Gezochte_Film]["Vrijdag"].Count == 3)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Vrijdag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Vrijdag"][1]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(3); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Vrijdag"][2]}\n");
                    }
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    string tijdslot = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    if (tijdslot == "1")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Vrijdag"][0] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Vrijdag"][0], ConvertedToDate);
                    }
                    if (tijdslot == "2")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Vrijdag"][1] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Vrijdag"][1], ConvertedToDate);
                    }
                    if (tijdslot == "3")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Vrijdag"][2] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Vrijdag"][2], ConvertedToDate);
                    }
                }
            }
            else if (DagenKeuze == "6")
            {
                if (table.Rows[5][1].ToString() == "Deze film draait niet op Zaterdag.")
                {
                    Console.WriteLine("Voor de geselecteerde dag draait de film niet.\nProbeer het opnieuw.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    ZoekOptie(Gezochte_Film, DynamicFilmData);
                }
                else
                {
                    string ConvertedToDate = GetNextWeekday(DayOfWeek.Saturday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                    Console.WriteLine("U heeft gekozen voor Zaterdag, voor welk tijdslot zou u willen reserveren?");
                    Console.WriteLine(DictofTimes[Gezochte_Film].Count);
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    if (DictofTimes[Gezochte_Film]["Zaterdag"].Count == 1)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zaterdag"][0]}\n");
                    }
                    else if (DictofTimes[Gezochte_Film]["Zaterdag"].Count == 2)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zaterdag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zaterdag"][1]}\n");

                    }
                    else if (DictofTimes[Gezochte_Film]["Zaterdag"].Count == 3)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zaterdag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zaterdag"][1]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(3); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zaterdag"][2]}\n");
                    }
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    string tijdslot = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    if (tijdslot == "1")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zaterdag"][0] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Zaterdag"][0], ConvertedToDate);

                    }
                    if (tijdslot == "2")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zaterdag"][1] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Zaterdag"][1], ConvertedToDate);
                    }
                    if (tijdslot == "3")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zaterdag"][2] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Zaterdag"][2], ConvertedToDate);
                    }
                }
            }
            else if (DagenKeuze == "7")
            {
                if (table.Rows[6][1].ToString() == "Deze film draait niet op Zondag.")
                {
                    Console.WriteLine("Voor de geselecteerde dag draait de film niet.\nProbeer het opnieuw.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    ZoekOptie(Gezochte_Film, DynamicFilmData);
                }
                else
                {
                    string ConvertedToDate = GetNextWeekday(DayOfWeek.Sunday).ToString();
                    ConvertedToDate = ConvertedToDate.Substring(0, 9);
                    Console.WriteLine(ConvertedToDate);
                    Console.WriteLine("U heeft gekozen voor Zondag, voor welk tijdslot zou u willen reserveren?");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    if (DictofTimes[Gezochte_Film]["Zondag"].Count == 1)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zondag"][0]}\n");
                    }
                    else if (DictofTimes[Gezochte_Film]["Zondag"].Count == 2)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zondag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zondag"][1]}\n");

                    }
                    else if (DictofTimes[Gezochte_Film]["Zondag"].Count == 3)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zondag"][0]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zondag"][1]}\n");
                        ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(3); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {DictofTimes[Gezochte_Film]["Zondag"][2]}\n");
                    }
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    string tijdslot = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    if (tijdslot == "1")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zondag"][0] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Zondag"][0], ConvertedToDate);
                    }
                    if (tijdslot == "2")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zondag"][1] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Zondag"][1], ConvertedToDate);
                    }
                    if (tijdslot == "3")
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zondag"][2] + " uur.");
                        SnacksOption();
                        ReserveerCodeMail(Gezochte_Film, DictofTimes[Gezochte_Film]["Zondag"][1], ConvertedToDate);
                    }
                }
            }




        }
        public static string DagKeuze(string DagenKeuze)
        {

            if (DagenKeuze == "1")
            {
                return "Maandag";
            }
            if (DagenKeuze == "2")
            {
                return "Dinsdag";
            }
            if (DagenKeuze == "1")
            {
                return "Woensdag";
            }
            if (DagenKeuze == "1")
            {
                return "Donderdag";
            }
            if (DagenKeuze == "1")
            {
                return "Vrijdag";
            }
            if (DagenKeuze == "1")
            {
                return "Zaterdag";
            }
            else            
            {
                return "Zondag";
            }
        }

        public static void Snacks()
        {
            string myJsonString = new WebClient().DownloadString(@"C:\Users\woute\source\repos\Esat-Aydin\Reservatie\Reservatie\snacksdrinks.json"); // Path moet nog veranderd worden
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            ConsoleCommands CommandLine = new ConsoleCommands();
            List<Object> Mandje = new List<Object>();


            ShoppingCart.Cart.WinkelMandje(DynamicData, Mandje);



        }
        public void SnacksOption() // Om deze te callen: Gebruiker.SnacksOption();
        {


            //Eventuele snacks tijdens het reserveren
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("Zou u ook alvast snacks willen bestellen voor bij de film?");
            Console.WriteLine("Door online de snacks te reserveren krijgt u 15% korting op het gehele bedrag.");
            Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] als u nu snacks wilt bestellen"); Console.Write("\nToets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("] als u nu geen snacks wilt bestellen\n");

            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            string Online_snacks = Console.ReadLine();
            string Online_snacks_secondchange = null;
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("wit");
            if (Online_snacks == "2")
            {
                Console.WriteLine("U heeft er voor gekozen om geen snacks te bestellen.");
                Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] om door te gaan"); Console.Write("\nToets ["); ConsoleCommands.Textkleur("zwart"); Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("] om alsnog snacks te bestellen\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                Online_snacks_secondchange = Console.ReadLine();
                Console.Clear();
                

            }
            else if (Online_snacks == "2" && Online_snacks_secondchange == "2")
            {


                Snacks();



            }
            else if (Online_snacks != "2" && Online_snacks != "1")
            {
                Console.WriteLine("U heeft de verkeerde input gegeven.");
                Console.WriteLine("Toets 'JA' om door te gaan en 'NEE' om het overzicht met snacks te bekijken.");
                Snacks();
            }


        }

    }
}
