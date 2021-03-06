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
using ShoppingCart;
using Chair;

namespace Gebruiker
{
    public class Gebruiker : Reserveren
    {
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Film { get; set; }
        public string Film_Time { get; set; }
        public string Film_Day { get; set; }
        public string[] Stoel_num { get; set; }
        public string Zaal { get; set; }
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
        public void AdminCommands()
        {
            Scherm.Screens.CinemaBanner();
            Console.WriteLine("\t\t\t ADMIN CONSOLE");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] Film toevoegen\n\n");
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("] Film verwijderen\n\n");
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("3"); ConsoleCommands.Textkleur("wit"); Console.Write("] Alle reserveringen bekijken\n\n");
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("4"); ConsoleCommands.Textkleur("wit"); Console.Write("] Reserveringen per zaal bekijken\n\n");
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("5"); ConsoleCommands.Textkleur("wit"); Console.Write("] Snacks & Dranken configureren\n\n");
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("6"); ConsoleCommands.Textkleur("wit"); Console.Write("] Reservering Controleren\n\n");

            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("0"); ConsoleCommands.Textkleur("wit"); Console.Write("] Terug gaan\n\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
        }
        public void UserInputMethod(string UserInput)
        {
            Film.Film FilmObject = new Film.Film();

            //Film.Film FilmObject = new Film.Film();
            SnackClass.Snacks Snacks1 = new SnackClass.Snacks();
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
            bool FilmRemoved = false;
                if (UserInput == "0")
                {
                    Scherm.Screens.AdminOrUserScreen();
                }

                if (UserInput == "1")

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
                                FilmObject.AddObject(FilmObject); // Dit voegt het object toe aan de Json file
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
                                FilmObject.AddObject(FilmObject); // Dit voegt het object toe aan de Json file
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
                                FilmObject.AddObject(FilmObject); // Dit voegt het object toe aan de Json file
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
                            Scherm.Screens.CustomError("U moet een getal invoeren!");
                        }

                    }
                    catch
                    {
                        Scherm.Screens.CustomError("U moet een getal invoeren!");
                    }



                }
                if (UserInput == "2")
                {
                    
                    while (FilmRemoved == false)
                    {
                        Scherm.Screens.CinemaBanner();
                        ConsoleCommands.Textkleur("wit");
                        Console.Write("\t\t\tHieronder vind u een lijst met alle films: \n\n\t\t\t\t ["); ConsoleCommands.Textkleur("zwart"); Console.Write("0"); ConsoleCommands.Textkleur("wit"); Console.Write("] Terug gaan\n");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("zwart");

                        var table = new ConsoleTable("Film Naam", "Film Genre 1", "Film Genre 2", "Film Genre 3", "Zaal"); //Preset Table
                        dynamic genres = DynamicFilmData[0]["FilmGenres"];
                        List<string> All_Films = new List<string>();
                        var zaal = DynamicFilmData[0]["FilmRoom"];
                        for (int i = 0; i < DynamicFilmData.Count; i++)
                        {
                            All_Films.Add(DynamicFilmData[i]["FilmTitle"].ToString()); //Lijst aangemaakt met alle films
                        }
                        ConsoleCommands.Textkleur("wit");
                        for (int i = 0; i < DynamicFilmData.Count; i++)
                        {
                            if (All_Films[i] == DynamicFilmData[i]["FilmTitle"].ToString())
                            {
                                genres = DynamicFilmData[i]["FilmGenres"]; //Juiste genres zoeken bij de films
                                zaal = DynamicFilmData[i]["FilmRoom"]; //Juiste zaal zoeken bij de films
                                table.AddRow($"Toets [{i + 1}] voor {All_Films[i]}", genres[0], genres[1], genres[2], zaal); //Tabel invullen met juiste data
                            }
                        }
                        table.Write(Format.Alternative); //Format veranderen ivm "Counter"
                        Console.WriteLine("Toets het nummer in van de film die u wilt verwijderen");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        bool x = true;
                        while (x == true)
                        {
                            ConsoleCommands.Textkleur("zwart"); string delete_movie = Console.ReadLine();
                            int int_movie = 0;
                            Int32.TryParse(delete_movie, out int_movie);
                            bool Bool = false;
                            while (Bool == false)
                            {
                                if (int_movie < All_Films.Count + 1)
                                {
                                    delete_movie = All_Films[int_movie - 1];
                                    Bool = true;
                                    if (FilmObject.Film_check2(delete_movie) == true)
                                    {
                                        FilmObject.RemoveObject(delete_movie);
                                        x = false;
                                        FilmRemoved = true;
                                    }
                                }
                                else
                                {
                                    Scherm.Screens.CustomError("We hebben die Film niet kunnen vinden. Probeer het opnieuw.");
                                    Bool = true;
                                }
                            }
                        }
                    }
                    Thread.Sleep(3000);
                    AdminCommands(); ConsoleCommands.Textkleur("zwart");
                    UserInput = Console.ReadLine(); UserInputMethod(UserInput);
                }
                if (UserInput == "3")

                {
                    while (true)
                    {
                        Scherm.Screens.CinemaBanner();
                        Console.WriteLine("\t\t\t RESERVERINGEN BEKIJKEN");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                        var Reserveringen = new WebClient().DownloadString(@".\SampleLog.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path

                        dynamic AlleReserveringen = JsonConvert.DeserializeObject(Reserveringen);

                        Console.WriteLine("Hieronder vind u de lijst van alle reserveringen.");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        var table = new ConsoleTable("Naam", "Reserverings code", "Film", "Datum", "Tijd", "Zaal");
                        int AantalReserveringen = 0;
                        for (int i = 0; i < AlleReserveringen.Count; i++)
                        {
                            table.AddRow(AlleReserveringen[i]["Naam"], AlleReserveringen[i]["Reservatie_code"], AlleReserveringen[i]["Film"], AlleReserveringen[i]["FilmDate"], AlleReserveringen[i]["FilmTime"], AlleReserveringen[i]["Zaal"]);
                            AantalReserveringen++;
                        }
                        table.Write(Format.Alternative);
                        if (AantalReserveringen == 1)
                        {
                            ConsoleCommands.Textkleur("wit"); Console.Write($"\nEr is "); ConsoleCommands.Textkleur("rood"); Console.Write($"{AantalReserveringen} "); ConsoleCommands.Textkleur("wit"); Console.Write("reservering.\n");
                        }
                        else if (AantalReserveringen == 0)
                        {
                            ConsoleCommands.Textkleur("wit"); Console.Write($"\nEr zijn geen reserveringen.\n");

                        }
                        else
                        {
                            ConsoleCommands.Textkleur("wit"); Console.Write($"\nEr zijn "); ConsoleCommands.Textkleur("rood"); Console.Write($"{AantalReserveringen} "); ConsoleCommands.Textkleur("wit"); Console.Write("reserveringen.\n");

                        }
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write("] Reservering annuleren.\n");
                        Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(0); ConsoleCommands.Textkleur("wit"); Console.Write("] Om het programma af te sluiten.\n");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("zwart"); string input = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        if (input == "1")
                        {
                            AnnulerenAdmin(AlleReserveringen);
                        }
                        if (input == "0")
                        {
                            Console.Clear();
                            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                            Environment.Exit(1);
                        }
                        else
                        {
                            Scherm.Screens.ErrorMessageInput();
                            Thread.Sleep(2000);
                        }
                    }
                }
                if (UserInput == "4")
                {
                    while (true)
                    {
                        Scherm.Screens.CinemaBanner();
                        Console.WriteLine("\t\t\t BIOSCOOP ZALEN");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                        var Reserveringen = new WebClient().DownloadString(@".\SampleLog.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path

                        dynamic AlleReserveringen = JsonConvert.DeserializeObject(Reserveringen);
                        for (int i = 1; i < 4; i++)
                        {
                            Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(i); ConsoleCommands.Textkleur("wit"); Console.Write("] voor zaal: "); ConsoleCommands.Textkleur("rood"); Console.Write(i + "\n"); ConsoleCommands.Textkleur("wit");
                        }
                        int x = 0;
                        bool CorrectInput = false;
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                        string Input = Console.ReadLine();
                        var table = new ConsoleTable("Naam", "Reserverings code", "Film", "Datum");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        int AantalReserveringen = 0;
                        while (CorrectInput != true)
                        {
                            if (Int32.TryParse(Input, out x))
                            {
                                if (x > 0 && x < 4)
                                {
                                    Scherm.Screens.CinemaBanner();
                                    Console.WriteLine($"Hieronder vind u de lijst van alle reserveringen voor zaal {x}.");
                                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                                    for (int i = 0; i < AlleReserveringen.Count; i++)
                                    {
                                        if (AlleReserveringen[i]["Zaal"] == x.ToString())
                                        {
                                            AantalReserveringen++;
                                            table.AddRow(AlleReserveringen[i]["Naam"], AlleReserveringen[i]["Reservatie_code"], AlleReserveringen[i]["Film"], AlleReserveringen[i]["FilmDate"]);
                                        }
                                    }
                                    CorrectInput = true;
                                }
                                else
                                {
                                    Scherm.Screens.ErrorMessageInput(); Input = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                                }
                            }
                            else
                            {
                                Scherm.Screens.ErrorMessageInput(); Input = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            }
                        }
                        table.Write(Format.Alternative);
                        if (AantalReserveringen == 1)
                        {
                            ConsoleCommands.Textkleur("wit"); Console.Write($"\nZaal {x} heeft "); ConsoleCommands.Textkleur("rood"); Console.Write($"{AantalReserveringen} "); ConsoleCommands.Textkleur("wit"); Console.Write("reservering.\n");
                        }
                        else if (AantalReserveringen == 0)
                        {
                            ConsoleCommands.Textkleur("wit"); Console.Write($"\nZaal {x} heeft geen reserveringen.\n");

                        }
                        else
                        {
                            ConsoleCommands.Textkleur("wit"); Console.Write($"\nZaal {x} heeft "); ConsoleCommands.Textkleur("rood"); Console.Write($"{AantalReserveringen} "); ConsoleCommands.Textkleur("wit"); Console.Write("reserveringen.\n");

                        }
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write("] Reservering annuleren.\n");
                        Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(0); ConsoleCommands.Textkleur("wit"); Console.Write("] Om het programma af te sluiten.\n");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("zwart"); string input = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        if (input == "1")
                        {
                            AnnulerenAdmin(AlleReserveringen);
                        }
                        if (input == "0")
                        {
                            Console.Clear();
                            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                            Environment.Exit(1);
                        }
                        else
                        {
                            Scherm.Screens.ErrorMessageInput();
                            Thread.Sleep(2000);
                        }
                    }
                }
                if (UserInput == "5")
                {
                    Scherm.Screens.CinemaBanner();


                    Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] Snack toevoegen\n\n");
                    Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("] Snack verwijderen\n\n");
                    Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("3"); ConsoleCommands.Textkleur("wit"); Console.Write("] Drank toevoegen\n\n");
                    Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("4"); ConsoleCommands.Textkleur("wit"); Console.Write("] Drank verwijderen\n\n");
                    Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("0"); ConsoleCommands.Textkleur("wit"); Console.Write("] Om het progamma af te sluiten.\n\n");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");

                    ConsoleCommands.Textkleur("zwart"); string user_input1 = Console.ReadLine();
                    bool CorrectInput = false;
                    bool ErrorShown = false;
                    while (CorrectInput == false)
                    {
                        if (user_input1 == "0")

                        {
                            Console.Clear();
                            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                            Environment.Exit(1);
                            CorrectInput = true;
                        }
                        else if (user_input1 == "1")
                        {
                            Scherm.Screens.CinemaBanner();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine("Hoe heet de snack die u wilt toevoegen?");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                            string user_input_name = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine($"Wat is de prijs van {user_input_name}?");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");

                            string user_input_price = Console.ReadLine();
                            var SnackObject = new SnackClass.Snacks(user_input_name, user_input_price);

                            Snacks1.AddObject(SnackObject, user_input_name);
                            Thread.Sleep(1500);
                            AdminCommands();
                            ConsoleCommands.Textkleur("zwart"); string UserInputNew = Console.ReadLine();
                            UserInputMethod(UserInputNew);
                            CorrectInput = true;
                        }
                        else if (user_input1 == "3")
                        {
                            Scherm.Screens.CinemaBanner();
                            Console.WriteLine("Hoe heet de drank die u wilt toevoegen?");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");

                            string user_input_name = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine($"Wat is de prijs van {user_input_name}?");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");

                            ConsoleCommands.Textkleur("zwart"); string user_input_price = Console.ReadLine();
                            SnackClass.Snacks SnackObject = new SnackClass.Snacks(user_input_name, user_input_price);
                            Snacks1.DrankenAdd(SnackObject, user_input_name);
                            Thread.Sleep(3000);
                            AdminCommands(); ConsoleCommands.Textkleur("zwart");
                        UserInput = Console.ReadLine(); UserInputMethod(UserInput);
                        CorrectInput = true;
                        }
                        else if (user_input1 == "2")
                        {
                            bool x = true;
                            string myJsonString = new WebClient().DownloadString(@"snacksdrinks.json");
                            dynamic DynamicData1 = JsonConvert.DeserializeObject(myJsonString);

                            Console.WriteLine("Welke snack wilt u verwijderen?");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                            ConsoleCommands.Textkleur("wit");

                            Scherm.Screens.CinemaBanner();
                            var table = new ConsoleTable("Snacks", "Prijs");
                            for (int i = 0; i < DynamicData1.snacks.Count; i++)
                            {

                                table.AddRow("Toets [" + (i + 1) + "] " + DynamicData1.snacks[i].Name, DynamicData1.snacks[i].Price);

                            }
                            table.Write(Format.Alternative);

                            
                            ConsoleCommands.Textkleur("zwart"); string delete_snack = Console.ReadLine();
                            int int_snack;
                            bool Bool = false;

                            while (Bool == false)
                            {
                                if (Int32.TryParse(delete_snack, out int_snack) & int_snack < DynamicData1.snacks.Count+1)
                                {

                                    string user_input_name = (string)DynamicData1["snacks"][int_snack - 1]["Name"];

                                    if (Snacks1.SnacksCheck(user_input_name))
                                    {
                                        Snacks1.RemoveObject(user_input_name);
                                        Thread.Sleep(3000);
                                        AdminCommands();
                                        string UserInputNew = Console.ReadLine();
                                        UserInputMethod(UserInputNew);
                                    }
                                    Bool = true;
                                }
                                else
                                {
                                    Scherm.Screens.CustomError("We hebben die Snack niet kunnen vinden!");
                                    Bool = true;
                                }
                            }
                            

                            CorrectInput = true;
                        }
                        else if (user_input1 == "4")
                        {
                            bool x = true;
                            string myJsonString = new WebClient().DownloadString(@"snacksdrinks.json");
                            dynamic DynamicData1 = JsonConvert.DeserializeObject(myJsonString);

                            Console.WriteLine("Welke drank wilt u verwijderen?");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");

                            ConsoleCommands.Textkleur("wit");

                            Scherm.Screens.CinemaBanner();
                            var table = new ConsoleTable("Dranken", "Prijs");
                            for (int i = 0; i < DynamicData.dranken.Count; i++)
                            {

                                table.AddRow("Toets [" + (i + 1) + "] " + DynamicData.dranken[i].Name, DynamicData.dranken[i].Price);



                            }
                            table.Write(Format.Alternative);
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            
                            
              

                            


                            while (x == true)
                            {
                                ConsoleCommands.Textkleur("zwart");
                                string user_input_name = Console.ReadLine();
                                ConsoleCommands.Textkleur("wit");
                                int int_drank;
                                
                                if (Int32.TryParse(user_input_name, out int_drank) & int_drank < DynamicData.dranken.Count+1)
                                {

                                    user_input_name = (string)DynamicData["dranken"][int_drank - 1]["Name"];
                                       
                                    if (Snacks1.DrankenCheck(user_input_name) == true)
                                    {
                                        Snacks1.DrankenRemove(user_input_name);
                                        Thread.Sleep(3000);
                                        x = false;
                                     

                                    }
                                    
                                }
                                else
                                {
                                    Scherm.Screens.CustomError("We hebben die Drank niet kunnen vinden!");
                                    user_input_name = Console.ReadLine();
                                }
                                
                            }
                            CorrectInput = true;
                        }
                        else 
                        {
                            
                            Screens.ErrorMessageInput();
                            user_input1 = Console.ReadLine();
                        }
                    }
                }
                if (UserInput == "6")
                {
                    while (true)
                    {
                        Scherm.Screens.CinemaBanner();
                        Console.WriteLine("\t\t\t RESERVERING CONTROLEREN");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                        var Reserveringen = new WebClient().DownloadString(@"SampleLog.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path

                        dynamic AlleReserveringen = JsonConvert.DeserializeObject(Reserveringen);

                        Console.Write("\nVoer de reserverings code in die u wilt controleren: "); ConsoleCommands.Textkleur("zwart"); string MedewerkerInput = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        var table = new ConsoleTable("Naam", "Reserverings code", "Film", "Datum", "Tijd", "Zaal");
                        int AantalReserveringen = 0;
                        for (int i = 0; i < AlleReserveringen.Count; i++)
                        {
                            if (AlleReserveringen[i]["Reservatie_code"] == MedewerkerInput)
                            {
                                table.AddRow(AlleReserveringen[i]["Naam"], AlleReserveringen[i]["Reservatie_code"], AlleReserveringen[i]["Film"], AlleReserveringen[i]["FilmDate"], AlleReserveringen[i]["FilmTime"], AlleReserveringen[i]["Zaal"]);
                                AantalReserveringen++;
                            }
                        }
                        if (AantalReserveringen == 1)
                        {
                            table.Write(Format.Alternative);
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("wit"); Console.Write($"\nDeze reservering "); ConsoleCommands.Textkleur("rood"); Console.Write("bestaat. \n"); ConsoleCommands.Textkleur("wit");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write("] Reservering annuleren.\n");
                            Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(0); ConsoleCommands.Textkleur("wit"); Console.Write("] Om het programma af te sluiten.\n");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart"); string input = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            bool correctInput = false;
                            while (correctInput == false)
                            {
                                if (input == "1")
                                {
                                    AnnulerenAdmin(AlleReserveringen);
                                    correctInput = true;
                                }
                                if (input == "0")
                                {
                                    Console.Clear();
                                    Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                                    Environment.Exit(1);
                                    correctInput = true;
                                }
                                else
                                {
                                    Scherm.Screens.ErrorMessageInput();
                                    input = Console.ReadLine();
                                }
                            }
                        }
                        else
                        {
                            ConsoleCommands.Textkleur("wit"); Console.Write($"\nDeze reservering "); ConsoleCommands.Textkleur("rood"); Console.Write("bestaat niet\n"); ConsoleCommands.Textkleur("wit");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(0); ConsoleCommands.Textkleur("wit"); Console.Write("] Om het programma af te sluiten.\n");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart"); string input = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            bool correctInput = false;
                            while (correctInput == false)
                            {
                                if (input == "0")
                                {
                                    Console.Clear();
                                    Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                                    Environment.Exit(1);
                                    correctInput = true;
                                }
                                else
                                {
                                    Scherm.Screens.ErrorMessageInput();
                                    input = Console.ReadLine();
                                }
                            }
                        }
                    }
                }
                if (UserInput != "1" && UserInput != "2" && UserInput != "3" && UserInput != "4" && UserInput != "5" && UserInput != "6" && UserInput != "0")
                {
                    Screens.ErrorMessageInput();
                    UserInput = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                }
            
            

        }
        public void AnnulerenAdmin(dynamic AlleReserveringen)
        {
            string FirstScreen()
            {
                Console.WriteLine("Voer de reserverings code hieronder in");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart"); string res_code = Console.ReadLine();
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                return res_code;
            }
            string SecondScreen() { 
                Console.WriteLine("Weet u zeker dat u deze reservering wilt annuleren?");
                Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write("] Reservering annuleren\n");
                Console.Write("\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write("] Progamma afsluiten\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart"); string Optie = Console.ReadLine();
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                return Optie;
            }
            string ResCode = FirstScreen();
            string OptieVar = SecondScreen();
            if (OptieVar == "1")
            {
                bool ReservationFound = false;
                for (int i = 0; i < AlleReserveringen.Count; i++)
                {
                    if (ResCode == AlleReserveringen[i]["Reservatie_code"].ToString())
                    {
                        ReservationFound = true;
                        AlleReserveringen.Remove(AlleReserveringen[i]);
                        dynamic UserData = JsonConvert.SerializeObject(AlleReserveringen);
                        File.WriteAllText(@".\SampleLog.json", UserData);
                        Console.WriteLine("De reservering is succesvol geannuleerd. De applicatie wordt automatisch afgesloten.");
                        Thread.Sleep(3000);
                        Console.Clear();
                        Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                        Environment.Exit(1);
                    }
                }
                if (ReservationFound == false)
                {
                    Scherm.Screens.CustomError("We hebben geen reservering met die code kunnen vinden. Probeer het nogmaals.");
                    Thread.Sleep(3000);
                    AnnulerenAdmin(AlleReserveringen);
                }
            }
            if (OptieVar == "2")
            {
                Console.Clear();
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Environment.Exit(1);
            }

        }
        public void AccountCreate(Gebruiker Object) // Eerst een object maken, dan hier als parameter in vullen om het te pushen naar de JSon file
        {
            List<Gebruiker> _data = new List<Gebruiker>();

            var AccountUsers = new WebClient().DownloadString(@".\AccountUsers.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path

            var AccountUsers_Gebruiker = JsonConvert.DeserializeObject<List<Gebruiker>>(AccountUsers);
            AccountUsers_Gebruiker.Add(Object);

            AccountUsers = JsonConvert.SerializeObject(AccountUsers_Gebruiker);

            File.WriteAllText(@".\AccountUsers.json", AccountUsers); // Net als AccountUsers de path veranderen als je hier errors krijgt!

        }
        public void ZoekOptie(string Gezochte_Film, dynamic DynamicFilmData)
        {

            ConsoleCommands.Textkleur("rood");
            List<string> Legelist = new List<string>();
            List<string> DagenvdWeek = new List<string>();
            List<string> Show_Tijden = new List<string>();
            Dictionary<string, Dictionary<string, List<string>>> DictofTimes = new Dictionary<string, Dictionary<string, List<string>>>();
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
            static void TableShow(ConsoleTable table)
            {
                ConsoleCommands.Textkleur("wit");
                table.Write(Format.Alternative);
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.WriteLine("Voor welke van de bovenstaande dagen zou u willen reserveren?");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
            }
            TableShow(table);
            string DagenKeuze = Console.ReadLine();
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            while (true)
            {
                if (DagenKeuze == "1" || DagenKeuze == "2" || DagenKeuze == "3" || DagenKeuze == "4" || DagenKeuze == "5" || DagenKeuze == "6" || DagenKeuze == "7")
                {
                    if (DagenKeuze == "1")
                    {
                        if (table.Rows[0][1].ToString() == "Deze film draait niet op Maandag.")
                        {
                            Scherm.Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                            Thread.Sleep(1500);
                            Console.Clear();
                            Scherm.Screens.CinemaBanner();
                            TableShow(table);
                            DagenKeuze = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
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
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Maandag"][0]);
                                stoelen.Chair();
                            }
                            if (tijdslot == "2")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Maandag"][1] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Maandag"][1]);
                                stoelen.Chair();

                            }
                            if (tijdslot == "3")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Maandag"][2] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Maandag"][2]);
                                stoelen.Chair();
                            }
                        }
                    }
                    else if (DagenKeuze == "2")
                    {
                        if (table.Rows[1][1].ToString() == "Deze film draait niet op Dinsdag.")
                        {
                            Scherm.Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                            Thread.Sleep(1500);
                            Console.Clear();
                            Scherm.Screens.CinemaBanner();
                            TableShow(table);
                            DagenKeuze = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
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
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Dinsdag"][0]);
                                stoelen.Chair();
                            }
                            if (tijdslot == "2")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Dinsdag"][1] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Dinsdag"][1]);
                                stoelen.Chair();
                            }
                            if (tijdslot == "3")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Dinsdag"][2] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Dinsdag"][2]);
                                stoelen.Chair();
                            }
                        }
                    }
                    else if (DagenKeuze == "3")
                    {
                        if (table.Rows[2][1].ToString() == "Deze film draait niet op Woensdag.")
                        {
                            Scherm.Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                            Thread.Sleep(1500);
                            Console.Clear();
                            Scherm.Screens.CinemaBanner();
                            TableShow(table);
                            DagenKeuze = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
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
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Woensdag"][0]);
                                stoelen.Chair();
                            }
                            if (tijdslot == "2")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Woensdag"][1] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Woensdag"][1]);
                                stoelen.Chair();
                            }
                            if (tijdslot == "3")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Woensdag"][2] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Woensdag"][2]);
                                stoelen.Chair();
                            }
                        }
                    }
                    else if (DagenKeuze == "4")
                    {
                        if (table.Rows[3][1].ToString() == "Deze film draait niet op Donderdag.")
                        {
                            Scherm.Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                            Thread.Sleep(1500);
                            Console.Clear();
                            Scherm.Screens.CinemaBanner();
                            TableShow(table);
                            DagenKeuze = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
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
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Donderdag"][0]);
                                stoelen.Chair();
                            }
                            if (tijdslot == "2")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Donderdag"][1] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Donderdag"][1]);
                                stoelen.Chair();
                            }
                            if (tijdslot == "3")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Donderdag"][2] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Donderdag"][2]);
                                stoelen.Chair();
                            }
                        }
                    }
                    else if (DagenKeuze == "5")
                    {
                        if (table.Rows[4][1].ToString() == "Deze film draait niet op Vrijdag.")
                        {
                            Scherm.Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                            Thread.Sleep(1500);
                            Console.Clear();
                            Scherm.Screens.CinemaBanner();
                            TableShow(table);
                            DagenKeuze = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
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
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Vrijdag"][0]);
                                stoelen.Chair();
                            }
                            if (tijdslot == "2")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Vrijdag"][1] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Vrijdag"][1]);
                                stoelen.Chair();
                            }
                            if (tijdslot == "3")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Vrijdag"][2] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Vrijdag"][2]);
                                stoelen.Chair();
                            }
                        }
                    }
                    else if (DagenKeuze == "6")
                    {
                        if (table.Rows[5][1].ToString() == "Deze film draait niet op Zaterdag.")
                        {
                            Scherm.Screens.CustomError("Voor de geselecteerde dag draait de film niet.\nProbeer het opnieuw.");
                            Thread.Sleep(1500);
                            Console.Clear();
                            Scherm.Screens.CinemaBanner();
                            TableShow(table);
                            DagenKeuze = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
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
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zaterdag"][0]);
                                stoelen.Chair();

                            }
                            if (tijdslot == "2")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zaterdag"][1] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zaterdag"][1]);
                                stoelen.Chair();
                            }
                            if (tijdslot == "3")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zaterdag"][2] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zaterdag"][2]);
                                stoelen.Chair();
                            }
                        }
                    }
                    else if (DagenKeuze == "7")
                    {
                        if (table.Rows[6][1].ToString() == "Deze film draait niet op Zondag.")
                        {
                            Scherm.Screens.CustomError("Voor de geselecteerde dag draait de film niet.\nProbeer het opnieuw.");
                            Thread.Sleep(1500);
                            Console.Clear();
                            Scherm.Screens.CinemaBanner();
                            TableShow(table);
                            DagenKeuze = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
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
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zondag"][0]);
                                stoelen.Chair();


                            }
                            if (tijdslot == "2")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zondag"][1] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zondag"][1]);
                                stoelen.Chair();


                            }
                            if (tijdslot == "3")
                            {
                                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                                Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zondag"][2] + " uur.");
                                Thread.Sleep(1500);
                                var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zondag"][2]);
                                stoelen.Chair();


                            }
                        }
                    }
                }
                else
                {
                    Screens.CustomError("U kunt alleen een getal kiezen tussen 1 en 7! Probeer het opnieuw:");
                    Thread.Sleep(1500);
                    Scherm.Screens.CinemaBanner();
                    TableShow(table);
                    DagenKeuze = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                }
            }




        }
        public void GenreOptie(List<string>Show_films, dynamic DynamicFilmData, string SearchedGenre)
        {
            string Gezochte_Film = null;
            List<string> DagenvdWeek = new List<string>();
            string Times = null;
            DagenvdWeek.Add("Maandag");
            DagenvdWeek.Add("Dinsdag");
            DagenvdWeek.Add("Woensdag");
            DagenvdWeek.Add("Donderdag");
            DagenvdWeek.Add("Vrijdag");
            DagenvdWeek.Add("Zaterdag");
            DagenvdWeek.Add("Zondag");
            Dictionary<string, Dictionary<string, List<string>>> DictofTimes = new Dictionary<string, Dictionary<string, List<string>>>();
            int count = 1;
            for (int y = 0; y < Show_films.Count; y++)
            {
                Console.Write("\nToets ["); ConsoleCommands.Textkleur("zwart"); Console.Write((count)); ConsoleCommands.Textkleur("wit"); Console.Write("] voor: " + Show_films[y] + "\n");
                count++;
            }
            Console.WriteLine("\nVoor welke van de bovenstaande films zou u willen reserveren?");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            string Chosen_film = Console.ReadLine();
            int isInt = 0;
            bool Break = true;

            while (Break)
            {
                if (!int.TryParse(Chosen_film, out isInt))
                {
                    Scherm.Screens.ErrorMessageInput();
                    Thread.Sleep(2000);
                    Console.Clear();
                    Scherm.Screens.CinemaBanner();
                    Console.Write("\t\t\tWe hebben deze films gevonden onder de genre "); ConsoleCommands.Textkleur("rood"); Console.Write(SearchedGenre); ConsoleCommands.Textkleur("wit"); Console.Write(":\n");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    GenreOptie(Show_films, DynamicFilmData, SearchedGenre);
                    Break = false;
                }
                else if(Int32.Parse(Chosen_film) > count - 1)
                {
                    Scherm.Screens.ErrorMessageInput();
                    Thread.Sleep(2000);
                    Console.Clear();
                    Scherm.Screens.CinemaBanner();
                    Console.Write("\t\t\tWe hebben deze films gevonden onder de genre "); ConsoleCommands.Textkleur("rood"); Console.Write(SearchedGenre); ConsoleCommands.Textkleur("wit"); Console.Write(":\n");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    GenreOptie(Show_films, DynamicFilmData, SearchedGenre);
                    Break = false;
                }
                else
                {
                    if(Chosen_film != "0")
                    {
                        for (int i = 0; i < Show_films.Count+1; i++)
                        {

                            if (Chosen_film == i.ToString())
                            {
                                Gezochte_Film = Show_films[i-1];
                                Break = false;
                            }
                            if (Show_films.Count == 1)
                            {
                                Gezochte_Film = Show_films[0];
                                Break = false;
                            }
                        }
                    }
                    else
                    {
                        Chosen_film = "";
                      
                    }


                }
            }

            var table = new ConsoleTable("Dagen van de week", "Draaitijd", "Week [1]", "Week [2]");
            DictofTimes[Gezochte_Film] = new Dictionary<string, List<string>>();
            DictofTimes[Gezochte_Film]["Maandag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Dinsdag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Woensdag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Donderdag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Vrijdag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Zaterdag"] = new List<string>();
            DictofTimes[Gezochte_Film]["Zondag"] = new List<string>();
            int Count = 1;
            for (int i = 0; i < DynamicFilmData.Count; i++)

            {
                for (int j = 0; j < DagenvdWeek.Count; j++)
                {
                    if (DynamicFilmData[i]["FilmTitle"] == Gezochte_Film)
                    {
                        if (DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count > 0)
                        {
                            var FirstWeek = GetNextWeekday(DayOfWeekConverter(DagenvdWeek[j])).ToString(); string FirstWeekDay = FirstWeek.Substring(0, 9);
                            var SecondWeek = GetTwoWeeksFromNow(DayOfWeekConverter(DagenvdWeek[j])).ToString(); string SecondWeekDay = SecondWeek.Substring(0, 9);
                            for (int x = 0; x < DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count; x++)
                            {
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
                            table.AddRow(("Toets [" + (Count) + "] voor " + DagenvdWeek[j]), Times, FirstWeekDay, SecondWeekDay);
                            Count++;
                        }
                        else if (DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count <= 0)
                        {
                            for (int x = 0; x <= DynamicFilmData[i]["FilmDays"][DagenvdWeek[j]].Count; x++)
                            {
                                table.AddRow("Toets [" + (Count) + "] voor " + (DagenvdWeek[j]) + ": ", "Deze film draait niet op " + DagenvdWeek[j] + ".", " ", " ");
                                Count++;
                            }
                        }
                    }
                }
            }
            static void TableShow(ConsoleTable Table)
            {
                ConsoleCommands.Textkleur("wit");
                Table.Write(Format.Alternative);
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.WriteLine("Voor welke van de bovenstaande dagen zou u willen reserveren?");

                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                
            }
            TableShow(table);
            string DagenKeuze = Console.ReadLine();

            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

            while (true)
            {
                if (!int.TryParse(DagenKeuze, out isInt))
                {
                    Scherm.Screens.ErrorMessageInput();
                    DagenKeuze = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                }
                if (DagenKeuze == "1")
                {
                    if (table.Rows[0][1].ToString() == "Deze film draait niet op Maandag.")
                    {
                        ConsoleCommands.Textkleur("wit");
                        Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                        Thread.Sleep(1500);
                        Screens.CinemaBanner();
                        TableShow(table);
                        DagenKeuze = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else
                    {
                        string ConvertedToDate = GetNextWeekday(DayOfWeek.Monday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                        string ConvertedDate = GetTwoWeeksFromNow(DayOfWeek.Monday).ToString(); ConvertedDate = ConvertedDate.Substring(0, 9);
                        string[] ArrayofDates = { ConvertedToDate, ConvertedDate };
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

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Maandag"][0], ArrayofDates);
                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Maandag"][0]);
                            stoelen.Chair();


                        }
                        if (tijdslot == "2")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Maandag"][1] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Maandag"][1], ArrayofDates);
                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Maandag"][1]);
                            stoelen.Chair();



                        }
                        if (tijdslot == "3")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Maandag"][2] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Maandag"][2], ArrayofDates);
                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Maandag"][2]);
                            stoelen.Chair();


                        }
                    }
                }
                else if (DagenKeuze == "2")
                {
                    if (table.Rows[1][1].ToString() == "Deze film draait niet op Dinsdag.")
                    {
                        Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                        Thread.Sleep(1500);
                        Screens.CinemaBanner();
                        TableShow(table);
                        DagenKeuze = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else
                    {
                        string ConvertedToDate = GetNextWeekday(DayOfWeek.Tuesday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                        string ConvertedDate = GetTwoWeeksFromNow(DayOfWeek.Tuesday).ToString(); ConvertedDate = ConvertedDate.Substring(0, 9);
                        string[] ArrayofDates = { ConvertedToDate, ConvertedDate };
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

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Dinsdag"][0], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Dinsdag"][0]);
                            stoelen.Chair();

                        }
                        if (tijdslot == "2")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Dinsdag"][1] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Dinsdag"][1], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Dinsdag"][1]);
                            stoelen.Chair();


                        }
                        if (tijdslot == "3")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Dinsdag"][2] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Dinsdag"][2], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Dinsdag"][2]);
                            stoelen.Chair();


                        }
                    }
                }
                else if (DagenKeuze == "3")
                {
                    if (table.Rows[2][1].ToString() == "Deze film draait niet op Woensdag.")
                    {
                        Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                        Thread.Sleep(1500);
                        Screens.CinemaBanner();
                        TableShow(table);
                        DagenKeuze = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else
                    {
                        string ConvertedToDate = GetNextWeekday(DayOfWeek.Wednesday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                        string ConvertedDate = GetTwoWeeksFromNow(DayOfWeek.Wednesday).ToString(); ConvertedDate = ConvertedDate.Substring(0, 9);
                        string[] ArrayofDates = { ConvertedToDate, ConvertedDate };
                        Screens.CustomError("U heeft gekozen voor Woensdag, voor welk tijdslot zou u willen reserveren?");
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

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Woensdag"][0], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Woensdag"][0]);
                            stoelen.Chair();


                        }
                        if (tijdslot == "2")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Woensdag"][1] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Woensdag"][1], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Woensdag"][1]);
                            stoelen.Chair();


                        }
                        if (tijdslot == "3")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Woensdag"][2] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Woensdag"][2], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Woensdag"][2]);
                            stoelen.Chair();

                        }
                    }
                }
                else if (DagenKeuze == "4")
                {
                    if (table.Rows[3][1].ToString() == "Deze film draait niet op Donderdag.")
                    {
                        Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                        Thread.Sleep(1500);
                        Screens.CinemaBanner();
                        TableShow(table);
                        DagenKeuze = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else
                    {
                        string ConvertedToDate = GetNextWeekday(DayOfWeek.Thursday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                        string ConvertedDate = GetTwoWeeksFromNow(DayOfWeek.Thursday).ToString(); ConvertedDate = ConvertedDate.Substring(0, 9);
                        string[] ArrayofDates = { ConvertedToDate, ConvertedDate };
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

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Donderdag"][0], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Donderdag"][0]);
                            stoelen.Chair();


                        }
                        if (tijdslot == "2")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Donderdag"][1] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Donderdag"][1], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Donderdag"][1]);
                            stoelen.Chair();


                        }
                        if (tijdslot == "3")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Donderdag"][2] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Donderdag"][2], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Donderdag"][2]);
                            stoelen.Chair();


                        }
                    }
                }
                else if (DagenKeuze == "5")
                {
                    if (table.Rows[4][1].ToString() == "Deze film draait niet op Vrijdag.")
                    {
                        Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                        Thread.Sleep(1500);
                        Screens.CinemaBanner();
                        TableShow(table);
                        DagenKeuze = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else
                    {
                        string ConvertedToDate = GetNextWeekday(DayOfWeek.Friday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                        string ConvertedDate = GetTwoWeeksFromNow(DayOfWeek.Friday).ToString(); ConvertedDate = ConvertedDate.Substring(0, 9);
                        string[] ArrayofDates = { ConvertedToDate, ConvertedDate };
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

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Vrijdag"][0], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Vrijdag"][0]);
                            stoelen.Chair();


                        }
                        if (tijdslot == "2")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Vrijdag"][1] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Vrijdag"][1], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Vrijdag"][1]);
                            stoelen.Chair();


                        }
                        if (tijdslot == "3")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Vrijdag"][2] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Vrijdag"][2], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Vrijdag"][2]);
                            stoelen.Chair();


                        }
                    }
                }
                else if (DagenKeuze == "6")
                {
                    if (table.Rows[5][1].ToString() == "Deze film draait niet op Zaterdag.")
                    {
                        Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                        Thread.Sleep(1500);
                        Screens.CinemaBanner();
                        TableShow(table);
                        DagenKeuze = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else
                    {
                        string ConvertedToDate = GetNextWeekday(DayOfWeek.Saturday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                        string ConvertedDate = GetTwoWeeksFromNow(DayOfWeek.Saturday).ToString(); ConvertedDate = ConvertedDate.Substring(0, 9);
                        string[] ArrayofDates = { ConvertedToDate, ConvertedDate };
                        Console.WriteLine("U heeft gekozen voor Zaterdag, voor welk tijdslot zou u willen reserveren?");
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

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Zaterdag"][0], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zaterdag"][0]);
                            stoelen.Chair();



                        }
                        if (tijdslot == "2")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zaterdag"][1] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Zaterdag"][1], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zaterdag"][1]);
                            stoelen.Chair();


                        }
                        if (tijdslot == "3")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zaterdag"][2] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Zaterdag"][2], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zaterdag"][2]);
                            stoelen.Chair();


                        }
                    }
                }
                else if (DagenKeuze == "7")
                {
                    if (table.Rows[6][1].ToString() == "Deze film draait niet op Zondag.")
                    {
                        Screens.CustomError("Voor de geselecteerde dag draait de film niet. Probeer het opnieuw.");
                        Thread.Sleep(1500);
                        Screens.CinemaBanner();
                        TableShow(table);
                        DagenKeuze = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    }
                    else
                    {
                        string ConvertedToDate = GetNextWeekday(DayOfWeek.Sunday).ToString(); ConvertedToDate = ConvertedToDate.Substring(0, 9);
                        string ConvertedDate = GetTwoWeeksFromNow(DayOfWeek.Sunday).ToString(); ConvertedDate = ConvertedDate.Substring(0, 9);
                        string[] ArrayofDates = { ConvertedToDate, ConvertedDate };
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

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Zondag"][0], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zondag"][0]);
                            stoelen.Chair();


                        }
                        if (tijdslot == "2")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zondag"][1] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Zondag"][1], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zondag"][1]);
                            stoelen.Chair();



                        }
                        if (tijdslot == "3")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

                            Console.WriteLine("U heeft gekozen voor " + DictofTimes[Gezochte_Film]["Zondag"][2] + " uur.");

                            var GekozenTijd = DateChoice(Gezochte_Film, DictofTimes[Gezochte_Film]["Zondag"][2], ArrayofDates);

                            var stoelen = new StoelKeuze(Gezochte_Film, ConvertedToDate, DictofTimes[Gezochte_Film]["Zondag"][2]);
                            stoelen.Chair();



                        }
                    }
                }
                else
                {
                    Screens.CustomError("U kunt alleen een getal kiezen tussen 1 en 7! Probeer het opnieuw:");
                    DagenKeuze = Console.ReadLine(); ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                }

            }

        }
        public static void Snacks(Gebruiker Klant)
        {
            string myJsonString = new WebClient().DownloadString(@".\snacksdrinks.json"); // Path moet nog veranderd worden
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            ConsoleCommands CommandLine = new ConsoleCommands();
            List<string> Mandje = new List<string>();


            Cart.WinkelMandje(DynamicData, Mandje, Klant);



        }
        public void SnacksOption(Gebruiker Klant = null) // Om deze te callen: Gebruiker.SnacksOption();
        {


            //Eventuele snacks tijdens het reserveren
            Console.WriteLine("Zou u ook alvast snacks willen bestellen voor bij de film?\n");
            Console.Write("Door online de snacks te reserveren krijgt u "); ConsoleCommands.Textkleur("rood"); Console.Write("15%");ConsoleCommands.Textkleur("wit");Console.Write(" korting op het gehele bedrag.\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            Console.Write("["); ConsoleCommands.Textkleur("zwart"); Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] Snacks bestellen"); Console.Write("\n\n["); ConsoleCommands.Textkleur("zwart"); Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("] Reservering afronden\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            string Online_snacks = Console.ReadLine();
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("wit");
            if (Online_snacks == "1")
            {
                Snacks(Klant);
            }
            else if (Online_snacks == "2")
            {
                Betaling(Klant);
            }




        }
        public static DayOfWeek DayOfWeekConverter(string DayName)
        {
            if (DayName == "Maandag")
            {
                return DayOfWeek.Monday;
            }
            else if (DayName == "Dinsdag")
            {
                return DayOfWeek.Tuesday;
            }
            else if (DayName == "Woensdag")
            {
                return DayOfWeek.Wednesday;
            }
            else if (DayName == "Donderdag")
            {
                return DayOfWeek.Thursday;
            }
            else if (DayName == "Vrijdag")
            {
                return DayOfWeek.Friday;
            }
            else if (DayName == "Zaterdag")
            {
                return DayOfWeek.Saturday;
            }
            else
            {
                return DayOfWeek.Sunday;
            }
        }
        public static DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime result = DateTime.Now;
            while (result.DayOfWeek != day)
                result = result.AddDays(1);
            return result;
        }
        public static DateTime GetTwoWeeksFromNow(DayOfWeek day)
        {
            DateTime result = DateTime.Now.AddDays(7);
            while (result.DayOfWeek != day)
                result = result.AddDays(1);
            return result;
        }
        public static string DateChoice(string Film, string FilmTime, string[] Dates)
        {
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {Dates[0]}\n\n");
            ConsoleCommands.Textkleur("wit"); Console.Write("Toets ["); ConsoleCommands.Textkleur("zwart"); Console.Write(2); ConsoleCommands.Textkleur("wit"); Console.Write($"] voor {Dates[1]}\n");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
            string UserInput = Console.ReadLine();
            while (UserInput != "1" && UserInput != "2")
            {
                Screens.ErrorMessageInput();
                UserInput = Console.ReadLine();
            }
            if (UserInput == "1")
            {
                Scherm.Screens.CinemaBanner();
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("wit"); Console.Write("U heeft gekozen voor: "); ConsoleCommands.Textkleur("rood"); Console.Write(Film); ConsoleCommands.Textkleur("wit"); Console.Write(" Datum: ");
                ConsoleCommands.Textkleur("rood"); Console.Write(Dates[0]); ConsoleCommands.Textkleur("wit"); Console.Write(" Tijd: "); ConsoleCommands.Textkleur("rood"); Console.Write(FilmTime + "\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                return Dates[0];
            }
            else
            {
                Scherm.Screens.CinemaBanner();
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("wit"); Console.Write("U heeft gekozen voor: "); ConsoleCommands.Textkleur("rood"); Console.Write(Film); ConsoleCommands.Textkleur("wit"); Console.Write(" Datum: ");
                ConsoleCommands.Textkleur("rood"); Console.Write(Dates[1]); ConsoleCommands.Textkleur("wit"); Console.Write(" Tijd: "); ConsoleCommands.Textkleur("rood"); Console.Write(FilmTime + "\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                return Dates[1];
            }
        }
    }
}
