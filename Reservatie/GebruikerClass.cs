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

namespace Gebruiker
{
    public class Gebruiker
    {
        public string Naam { get; set; }
        public string Email { get; set; }
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

        public void ReserveringBeheren()
        {
                // Inladen Json Module 
                var MyFilmsData = new WebClient().DownloadString(@"C:\Users\abdel\source\repos\Esat-Aydin\Reservatie\Reservatie\Filmsdata.json");
                string myJsonString = new WebClient().DownloadString(@"C:\Users\abdel\source\repos\Esat-Aydin\Reservatie\Reservatie\snacksdrinks.json");
                string myUserData = new WebClient().DownloadString(@"C:\Users\abdel\source\repos\Esat-Aydin\Reservatie\Reservatie\SampleLog.json");

                // Omzetten
                dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
                dynamic DynamicUserData = JsonConvert.DeserializeObject(myUserData);
                dynamic DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);
                Console.Clear();
                ConsoleCommands.Textkleur("wit");
                Console.Write("Voer hier uw reserverings code in:");
                ConsoleCommands.Textkleur("zwart");
                var Reservatie_code = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");


                ConsoleCommands.Textkleur("wit");
                for (int i = 0; i < DynamicUserData.Count; i++)
                {
                    string Res_code = (string)DynamicUserData[i]["Reservatie_code"];
                    if (Res_code == Reservatie_code)
                    {
                        Console.WriteLine("Uw reservering: ");
                        Gebruiker.Reservering_check(DynamicUserData, i);
                        //SnacksOption();
                        break;
                    }
                }
                ConsoleCommands.Textkleur("wit");






            
        }
        public void ReserveringMaken(string UserInput)
        {
            Film.Film FilmObject = new();
            MedewerkerClass.Medewerker admin = new();
            Gebruiker Klant = new();
            ConsoleCommands CommandLine = new();
            // Inladen Json Module 
            dynamic DynamicData = JsonData.JsonSerializer("Snacks");
            dynamic DynamicUserData = JsonData.JsonSerializer("Users");
            dynamic DynamicFilmData = JsonData.JsonSerializer("Films");
            Console.Clear(); ConsoleCommands.Textkleur("rood");
            Scherm.Screens.CinemaBanner();
            if (UserInput == "1")
            {
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Naar welke film bent u opzoek: ");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
                string Film_search = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                for (int i = 0; i < DynamicFilmData.Count; i++)
                {
                    string Film_zoeken = (string)DynamicFilmData[i]["FilmTitle"];
                    if (Film_search == Film_zoeken)
                    {
                        Scherm.Screens.CinemaBanner();
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("U heeft gezocht naar de volgende film:");
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("---------------------------------------------------------------------------------------------");
                        ConsoleCommands.Textkleur("rood");

                        FilmObject.Film_check(DynamicFilmData, i);
                    }
                }
                Klant.ZoekOptie(Film_search, DynamicFilmData);
            }
            else if (UserInput == "2")
            {
                Console.Clear();
                List<string> Show_films = new List<string>();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Op welke genre wilt u zoeken: ");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Toets [1] voor Action.\nToets [2] voor Comedy.\nToets [3] voor Thriller.\nToets [4] voor Romantiek.\nToets [5] voor Drama.\nToets [6] voor Sci-Fi.\nToets [7] voor Familie films. ");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
                var Genre_select = Console.ReadLine();
                Console.Clear();
                ConsoleCommands.Textkleur("wit");
                CommandLine.Genre(Genre_select);
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("We hebben deze film(s) gevonden onder de genre: ");
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
                int count = 1;
                for (int y = 0; y < Show_films.Count; y++)
                {

                    Console.Write("\nToets ["); ConsoleCommands.Textkleur("zwart"); Console.Write((count)); ConsoleCommands.Textkleur("wit"); Console.Write("] voor: " + Show_films[y] + "\n");
                    count++;
                }


                Console.WriteLine("\nVoor welke van de bovenstaande films zou u willen reserveren?");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
                string Chosen_film = Console.ReadLine();


                for (int i = 0; i < Show_films.Count + 1; i++)
                {
                    string film_showw = i.ToString();
                    if (Chosen_film == (film_showw))
                    {
                        Console.Clear();
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("---------------------------------------------------------------------------------------------");
                        ConsoleCommands.Textkleur("wit");

                        FilmObject.Films(Chosen_film, Show_films);
                        ConsoleCommands.Textkleur("wit");
                        Console.Write("\nU heeft gekozen voor: "); ConsoleCommands.Textkleur("rood"); Console.Write(Chosen_film + "\n\n");
                        string Chosen_date = " ";
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("Voer uw gewenste dag in (Bijvoorbeeld: Maandag): ");
                        Console.WriteLine("---------------------------------------------------------------------------------------------");
                        ConsoleCommands.Textkleur("groen");
                        Chosen_date = Console.ReadLine();
                        if (Chosen_date.Length > 10 | Chosen_date.Length < 5)
                        {
                            Console.WriteLine("Ongeldige datum.");
                        }
                        else { Klant.ReserveerCodeMail(); }

                    }
                }



            }
            
            else if (UserInput == "3")
            {
                Console.Clear();
                var table = new ConsoleTable("Film Naam", "Film Genre 1", "Film Genre 2", "Film Genre 3", "zaal"); //Preset Table
                dynamic genres = DynamicFilmData[0]["FilmGenres"];
                List<string> All_Films = new List<string>();
                var zaal = DynamicFilmData[0]["FilmRoom"];
                for (int i = 0; i < DynamicFilmData.Count; i++)
                {
                    All_Films.Add(DynamicFilmData[i]["FilmTitle"].ToString()); //Lijst aangemaakt met alle films
                    All_Films.Sort(); //Lijst met films gesorteerd
                }
                ConsoleCommands.Textkleur("wit");
                for (int i = 0; i < DynamicFilmData.Count; i++)
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
                table.Write(Format.Alternative); //Format veranderen ivm "Counter"
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
                int choice = Int32.Parse(Console.ReadLine());
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("U heeft gekozen voor de volgende film:\t" + All_Films[choice - 1]);

            }
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
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            ConsoleCommands.Textkleur("wit");
            if (this.isAdmin == true && (UserInput == "!password"))
            {

                Console.WriteLine("Type nu het huidige admin wachtwoord in: ");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
                UserInput = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                if (UserInput == Password)
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Type nu het nieuwe wachtwoord in: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("zwart");
                    UserInput = Console.ReadLine();
                    Password = UserInput;
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Het wachtwoord is succesvol veranderd naar: " + Password);
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                }
            }
            if (this.isAdmin == true && (UserInput == "!help"))
            {
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Om het admin-wachtwoord opnieuw in te stellen, type: !password");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Om een film toe te voegen aan de database, type: !newfilm");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Om alle reserveringen te bekijken, type: !reserveringen");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Om reserveringen per zaal te zien, type: !zaalreserveringen");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
            }
            if (this.isAdmin == true && (UserInput == "!newfilm"))
            {
                Console.Clear();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Hoeveel genre's heeft de nieuwe film? Er is een maximum van drie genre's!");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
                var StringArrayGenreLength_Input = Console.ReadLine();
                try
                {
                    int StringArrayGenreLength = Int32.Parse(StringArrayGenreLength_Input);
                    string[] FilmGenresArray = new string[StringArrayGenreLength];
                    if (StringArrayGenreLength == 1)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("U kunt kiezen uit de volgende genres: "); ConsoleCommands.Textkleur("rood"); Console.Write("\n1. Action \n2. Comedy \n3. Thriller \n4. Romantic \n5. Horror \n6. Drama\n\n"); ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("Voer nu de genre van de film in: ");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[0] = Console.ReadLine();

                    }
                    else if (StringArrayGenreLength == 2)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("Voer nu de eerste genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[0] = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("Voer nu de tweede genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[1] = Console.ReadLine();
                    }
                    else if (StringArrayGenreLength == 3)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("Voer nu de eerste genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[0] = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("Voer nu de tweede genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
                        FilmGenresArray[1] = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("Voer nu de derde genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
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

                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Voer nu de titel van de nieuwe film in:");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
                    TitleofFilm = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Voer nu de zaal in van de film " + TitleofFilm + ": ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");

                    var FilmZaalInput = Console.ReadLine(); int RoomofFilm = Int32.Parse(FilmZaalInput); // Userinput (string) word hier verandert naar een int variabel

                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Voer nu de gewenste dag in"); ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("zwart");
                    var dag_UserInput = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------");
                    Console.WriteLine("Hoeveel tijdssloten wilt u beschikbaar stellen per dag? (maximaal 3): ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
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
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("Voer nu de tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
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
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
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
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("zwart");
                            FilmTimesArray[0] = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("Voer nu de tweede tijd van de film in: ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
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
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
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
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("zwart");
                            FilmTimesArray[0] = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("Voer nu de tweede tijd van de film in: ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("zwart");
                            FilmTimesArray[1] = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("Voer nu de derde tijd van de film in: ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
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
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
                            //-------------------------------------------------------------------------------------------------------------------------//
                            // Nu wordt de volgende console input gecheckt door de UserInputMethod() function te callen // 
                            UserInput = Console.ReadLine();
                            UserInputMethod(UserInput);
                        }
                    }
                    catch
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("rood");
                        Console.WriteLine("U moet een getal invoeren! (1, 2, 3, 4, etc.");
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        ConsoleCommands.Textkleur("zwart");
                    }

                }
                catch
                {
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("rood");
                    Console.WriteLine("U moet een getal invoeren! (1, 2, 3, 4, etc.");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("zwart");
                }



            }

            UserInput = Console.ReadLine();
            UserInputMethod(UserInput);
        }
        public void AdminConsole(bool adminConsoleChosen)
        {

            {
                Console.Clear();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Voer uw admin gebruikersnaam in:");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
                string input_name = Console.ReadLine();
                string adminInputName = input_name;
                Console.Clear();
                AccountCheck(adminInputName);

            }
        }
        public void AccountCheck(string Naam)
        {
            ConsoleCommands CommandLine = new ConsoleCommands();
            bool ReturnValue = false;
            dynamic AccountUsers_Gebruiker = JsonData.JsonSerializer("Users");
            List<string> ListofAccountsNames = new List<string>();
            List<string> ListofAccountsPasswords = new List<string>();
            List<string> ListofAccountsisAdmin = new List<string>();
            List<string> ListofAccountsEmails = new List<string>();

            for (int i = 0; i < AccountUsers_Gebruiker.Count; i++)
            {
                ListofAccountsNames.Add(AccountUsers_Gebruiker[i]["Naam"].ToString());
                ListofAccountsPasswords.Add(AccountUsers_Gebruiker[i]["Password"].ToString());
                ListofAccountsisAdmin.Add(AccountUsers_Gebruiker[i]["isAdmin"].ToString());
                ListofAccountsEmails.Add(AccountUsers_Gebruiker[i]["Email"].ToString());
                string StoredName = ListofAccountsNames[i];

                if (StoredName == Naam)
                {
                    ReturnValue = true;
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Welkom, " + Naam + ". Voer nu het ingestelde admin wachtwoord in: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("zwart");
                    string input_password = Console.ReadLine();
                    if (input_password == ListofAccountsPasswords[i])
                    {
                        if (ListofAccountsisAdmin[i] == "True")
                        {
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("U bent succesvol ingelogd als medewerker! Type !help voor een lijst aan commands.");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("zwart");
                            Gebruiker adminObject = new Gebruiker(Naam, ListofAccountsEmails[i], input_password, ReturnValue);
                            CommandLine.UserInput = Console.ReadLine();
                            adminObject.UserInputMethod(CommandLine.UserInput);
                        }
                    }

                    else
                    {
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("Het ingevoerde wachtwoord is incorrect, probeer het nogmaals: ");
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        ConsoleCommands.Textkleur("zwart");
                        CommandLine.UserInput = Console.ReadLine();

                        if (CommandLine.UserInput == ListofAccountsPasswords[i] && ListofAccountsisAdmin[i] == "True")
                        {
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("wit");
                            Gebruiker adminObject = new Gebruiker(Naam, ListofAccountsEmails[i], input_password, ReturnValue);
                            Console.WriteLine("U bent succesvol ingelogd als medewerker! Type !help voor een lijst aan commands.");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("zwart");
                            CommandLine.UserInput = Console.ReadLine();
                            adminObject.UserInputMethod(CommandLine.UserInput);
                        }
                        else
                        {
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("Het ingevoerde wachtwoord is incorrect, het programma wordt nu voor u afgesloten. ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("zwart");
                            CommandLine.RestartOption();
                            CommandLine.UserInput = Console.ReadLine();
                        }
                    }
                }
            }
            if (ReturnValue == false)
            {
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("We hebben geen account kunnen vinden met deze naam: " + Naam);
                ConsoleCommands.Textkleur("zwart");
                CommandLine.RestartOption(); // Dit sluit het programma af na twee verkeerde password inputs.
                ConsoleCommands.Textkleur("zwart");
                CommandLine.UserInput = Console.ReadLine();
                UserInputMethod(CommandLine.UserInput);
                CommandLine.UserInput = Console.ReadLine();
                UserInputMethod(CommandLine.UserInput);
            }













        }
        public void ShowSimplePercentage()
        {
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-------------------------------------------------------------------------------\n");
            ConsoleCommands.Textkleur("rood");
            for (int i = 0; i <= 100; i++)
            {
                Console.Write($"\rProgress: {i}%   ");
                Thread.Sleep(25);

            }
            Console.WriteLine("");
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-------------------------------------------------------------------------------\n");

        }
        public void AccountCreate(Gebruiker Object) // Eerst een object maken, dan hier als parameter in vullen om het te pushen naar de JSon file
        {
            List<Gebruiker> _data = new List<Gebruiker>();
            var AccountUsers = new WebClient().DownloadString(@"C:\Users\abdel\source\repos\Esat-Aydin\Reservatie\Reservatie\AccountUsers.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path
            var AccountUsers_Gebruiker = JsonConvert.DeserializeObject<List<Gebruiker>>(AccountUsers);
            AccountUsers_Gebruiker.Add(Object);

            AccountUsers = JsonConvert.SerializeObject(AccountUsers_Gebruiker);
            File.WriteAllText(@"C:\Users\abdel\source\repos\Esat-Aydin\Reservatie\Reservatie\AccountUsers.json", AccountUsers); // Net als AccountUsers de path veranderen als je hier errors krijgt!
        }
        public void ZoekOptie(string Gezochte_Film, dynamic DynamicMyFilmsData)
        {
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            ConsoleCommands.Textkleur("rood");
            List<string> Legelist = new List<string>();
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
            var table = new ConsoleTable("Dagen van de week", "Draaitijd");


            //int Genre_zoeken = (string)DynamicFilmData[i]["FilmGenres"][j];
            for (int i = 0; i < DynamicMyFilmsData.Count; i++)
            {
                //Genre_zoeken = DynamicMyFilmsData[i]["FilmDays"];
                for (int j = 0; j < DagenvdWeek.Count; j++)
                {

                    if (DynamicMyFilmsData[i]["FilmTitle"] == Gezochte_Film)
                    {


                        if (DynamicMyFilmsData[i]["FilmDays"][DagenvdWeek[j]].Count > 0)
                        {

                            for (int x = 0; x < DynamicMyFilmsData[i]["FilmDays"][DagenvdWeek[j]].Count; x++)
                            {

                                Show_Tijden.Add(DynamicMyFilmsData[i]["FilmDays"][DagenvdWeek[j]][x].ToString());


                                //Console.WriteLine("Toets [" + (Count) + "] voor "+ (DagenvdWeek[j]) +":" + Show_Tijden[x]);


                            }
                            string Times = Show_Tijden[0] + ", " + Show_Tijden[1] + ", " + Show_Tijden[2];
                            Count++;
                            table.AddRow(("Toets [" + (Count) + "] voor " + DagenvdWeek[j]), Times);

                        }
                        else if (DynamicMyFilmsData[i]["FilmDays"][DagenvdWeek[j]].Count <= 0)
                        {
                            for (int x = 0; x <= DynamicMyFilmsData[i]["FilmDays"][DagenvdWeek[j]].Count; x++)
                            {
                                table.AddRow("Toets [" + (Count) + "] voor " + (DagenvdWeek[j]) + ": ", "Deze film draait niet op " + DagenvdWeek[j] + ".");

                                Count++;

                            }
                        }
                    }

                }

            }
            table.Write(Format.Alternative);
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("Voor welke van de bovenstaande dagen zou u " + Gezochte_Film + " willen reserveren?");
            ConsoleCommands.Textkleur("zwart");
            DagKeuze();
        }
        public static void DagKeuze()
        {
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            string DagenKeuze = Console.ReadLine();
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            if (DagenKeuze == "1")
            {
                //Maandag
            }
            if (DagenKeuze == "1")
            {
                //Maandag
            }
            if (DagenKeuze == "1")
            {
                //Maandag
            }
            if (DagenKeuze == "1")
            {
                //Maandag
            }
            if (DagenKeuze == "1")
            {
                //Maandag
            }
            if (DagenKeuze == "1")
            {
                //Maandag
            }
            if (DagenKeuze == "1")
            {
                //Maandag
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
        public void ReserveerCodeMail() // Deze method regelt de reservering en mailt het vervolgens naar de gebruiker - Callen: Gebruiker.ReserveerCodeMail();
        {
            // informatie voor eventueel mailen reservatie code.
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-------------------------------------------------------------------------------");
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("Om te kunnen reserveren hebben wij uw naam en emailadres van u nodig.");
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-------------------------------------------------------------------------------\n");
            ConsoleCommands.Textkleur("rood");
            Console.Write("Naam: ");
            ConsoleCommands.Textkleur("zwart");
            string Naam_klant = Console.ReadLine();
            ConsoleCommands.Textkleur("rood");
            Console.Write("Email adress: ");
            ConsoleCommands.Textkleur("zwart");
            string Naam_email = Console.ReadLine();
            // Eventuele betaal methode?
            this.Naam = Naam_klant;
            this.Email = Naam_email;

            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-------------------------------------------------------------------------------");
            ConsoleCommands.Textkleur("wit");
            // Einde reserveren.
            Console.WriteLine("Bedankt voor het reserveren!");
            Console.WriteLine("Een ogenblik geduld alstublieft uw reservatie code wordt geladen.");
            Thread.Sleep(1000);
            ShowSimplePercentage();
            string GeneratedCode = this.ReserveringsCodeGenerator();
            // Random generator voor het maken van de reservatie code.


            ConsoleCommands.Textkleur("wit");
            Console.Write("Reserverings code: ");
            ConsoleCommands.Textkleur("rood");
            Console.WriteLine(GeneratedCode);
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("Zou u een bevestiging in uw mail willen ontvangen?");
            Console.WriteLine("Toets [JA] als u een mail-bevestinging wilt ontvangen of toets [NEE] als u geen mail-bevestiging .");
            // Email bevestiging.
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-------------------------------------------------------------------------------");
            ConsoleCommands.Textkleur("zwart");
            string Mail_Bevestiging = Console.ReadLine();

            if (Mail_Bevestiging == "JA")
            {

                try
                {
                    var message = new MimeMessage();
                    // Email verzender
                    message.From.Add(new MailboxAddress("ProjectB", "ProjectB1J@gmail.com"));
                    // Email geadresseerde
                    message.To.Add(new MailboxAddress(this.Naam, this.Email));
                    // Email onderwerp
                    message.Subject = "Bevestiging online reservatie.";
                    // Email text
                    message.Body = new TextPart("plain")
                    {
                        Text = @"Hallo " + this.Naam + @",
Bedankt voor het reserveren via onze bioscoop.
Hieronder vindt u de reservatie code.
Reservatie code: " + GeneratedCode

                    };


                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);

                        // authenticate smtp server
                        client.Authenticate("ProjectB1J@gmail.com", "Hogeschoolrotterdam");
                        // verzenden email
                        client.Send(message);
                        client.Disconnect(true);
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("-------------------------------------------------------------------------------");
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("De bevestiging is verstuurd per Email.");
                    };
                }
                catch
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-------------------------------------------------------------------------------");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Het versturen van de bevestiging is niet gelukt.");
                }
            }
            else if (Mail_Bevestiging == "NEE")
            {
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-------------------------------------------------------------------------------");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("U heeft gekozen om geen bevestiging in de mail te ontvangen.");
            }
            else
            {
                ReserveerCodeMail();
            }
            Console.WriteLine("Bedankt voor het online reserveren en we zien u graag binnenkort in onze bioscoop.");
            ConsoleCommands CommandLine = new ConsoleCommands();
            this.SnacksOption();

            // Data Reservering toevoegen.
            List<JsonData> _data = new List<JsonData>();
            var DataUser = File.ReadAllText(@"C:\Users\abdel\source\repos\Esat-Aydin\Reservatie\Reservatie\SampleLog.json"); //PATH VERANDEREN NAAR JOUW EIGEN BESTANDSLOCATIE ALS JE HIER EEN ERROR KRIJGT
            var JsonData = JsonConvert.DeserializeObject<List<JsonData>>(DataUser)
                      ?? new List<JsonData>();

            JsonData.Add(new JsonData()
            {
                Reservatie_code = GeneratedCode,
                Naam = this.Naam,
                Email = this.Email,
                //Film =
                //Zaal =
                //Stoel_num =

            });

            DataUser = JsonConvert.SerializeObject(JsonData);
            File.WriteAllText(@"C:\Users\abdel\source\repos\Esat-Aydin\Reservatie\Reservatie\SampleLog.json", DataUser);

        }
        public static void Reservering_check(dynamic dynamicUserData, int i)
        {
            ConsoleCommands CommandLine = new ConsoleCommands();
            Console.WriteLine("Naam: " + dynamicUserData[i]["Naam"]);
            Console.WriteLine("Email: " + dynamicUserData[i]["Email"]);
            Console.WriteLine("Reservatie code: " + dynamicUserData[i]["Reservatie_code"]);
            Console.WriteLine("Film: " + dynamicUserData[i]["Film"]);
            Console.WriteLine("Zaal: " + dynamicUserData[i]["Zaal"]);
            Console.WriteLine("Stoel nummer: " + dynamicUserData[i]["Stoel_num"]);
            CommandLine.RestartOption();


        }
        public static void Snacks()
        {
            string myJsonString = new WebClient().DownloadString(@"C:\Users\abdel\source\repos\Esat-Aydin\Reservatie\Reservatie\snacksdrinks.json"); // Path moet nog veranderd worden
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            ConsoleCommands CommandLine = new ConsoleCommands();
            List<Object> Mandje = new List<Object>();


            WinkelMandje(DynamicData, Mandje);



        }
        public void SnacksOption() // Om deze te callen: Gebruiker.SnacksOption();
        {


            //Eventuele snacks tijdens het reserveren
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("Zou u ook alvast snacks willen bestellen voor bij de film?");
            Console.WriteLine("Door online de snacks te reserveren krijgt u 15% korting op het gehele bedrag.");
            Console.WriteLine("Toets 'JA' als u online snacks wilt bestellen, toets 'NEE' als u dit niet wilt.");
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            ConsoleCommands.Textkleur("zwart");
            string Online_snacks = Console.ReadLine();
            string Online_snacks_secondchange = null;
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            ConsoleCommands.Textkleur("wit");
            if (Online_snacks == "NEE")
            {
                Console.WriteLine("U heeft er voor gekozen om geen snacks te bestellen.");
                Console.WriteLine("Weet u het zeker? Toets [JA] om door te gaan en [NEE] om het overzicht te bekijken met de snacks.");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
                Online_snacks_secondchange = Console.ReadLine();
                Console.Clear();
                Snacks();

            }
            else if (Online_snacks == "JA" || Online_snacks_secondchange == "NEE")
            {


                Snacks();



            }
            else if (Online_snacks != "NEE" && Online_snacks != "JA")
            {
                Console.WriteLine("U heeft de verkeerde input gegeven.");
                Console.WriteLine("Toets 'JA' om door te gaan en 'NEE' om het overzicht met snacks te bekijken.");
                Snacks();
            }


        }
        public static void WinkelMandje(dynamic DynamicData, List<Object> Mandje)
        {


            //Json file met alle snacks.
            Console.WriteLine("Toets [1] om de lijst met snacks te bekijken\nToets [2] om de lijst met dranken te bekijken.");
            ConsoleCommands.Textkleur("zwart");
            string SnackorDrinks = Console.ReadLine();
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            ConsoleCommands.Textkleur("wit");
            ConsoleCommands CommandLine = new ConsoleCommands();
            var tableDranken = new ConsoleTable("Dranken", "Prijs");
            var table = new ConsoleTable("Snacks", "Prijs");
            if (SnackorDrinks == "1")
            {
                for (int i = 0; i < DynamicData.snacks.Count; i++)
                {
                    table.AddRow("Toets [" + (i + 1) + "] " + DynamicData.snacks[i].Name, DynamicData.snacks[i].Price);
                }
                table.Write(Format.Alternative);
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
                var SnacksKeuze = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("wit");
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
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
                var DrankenKeuze = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("wit");
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
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            ConsoleCommands.Textkleur("zwart");
            string Meerbestellen = Console.ReadLine();

            if (Meerbestellen == "1")
            {

                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("wit");
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
