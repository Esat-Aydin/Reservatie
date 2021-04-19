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

namespace Cinema
{
    public class Gebruiker
    {
        public string Naam;
        public string Email;
        public string Password;
        public bool isAdmin;

        public Gebruiker(string Naam = null, string Email = null, string Password = null, bool isAdmin = false) // Password en isAdmin worden alleen gebruikt als de user al een existing account heeft en admin level True assigned heeft gekregen!
        {
            this.Naam = Naam;
            this.Email = Email;
        }
        public string ReserveringsCodeGenerator()
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
        public void ReserveerCodeMail()
        {
            // informatie voor eventueel mailen reservatie code.
            Console.WriteLine("Om te kunnen reserveren hebben wij uw naam en emailadres van u nodig.");
            Console.Write("Naam: ");
            string Naam_klant = Console.ReadLine();
            Console.Write("Email adress: ");
            string Naam_email = Console.ReadLine();
            // Eventuele betaal methode?
            this.Naam = Naam_klant;
            this.Email = Naam_email;


            // Einde reserveren.
            Console.WriteLine("Bedankt voor het reserveren!");
            Console.WriteLine("Een ogenblik geduld alstublieft uw reservatie code wordt geladen.");
            Thread.Sleep(3000);
            string GeneratedCode = this.ReserveringsCodeGenerator();
            // Random generator voor het maken van de reservatie code.



            Console.WriteLine("Reserverings code: " + GeneratedCode);
            Console.WriteLine("Zou u een bevestiging in uw mail willen ontvangen?");
            Console.WriteLine("Toets (1)'JA' als u een mail-bevestinging wilt ontvangen of toets (2)'NEE' als u geen mail-bevestiging .");
            // Email bevestiging.
            string Mail_Bevestiging = Console.ReadLine();

            if (Mail_Bevestiging == "JA" || Mail_Bevestiging == "1")
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
                        Text = @"Hallo,
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

                        Console.WriteLine("De bevestiging is verstuurd per Email.");
                    };
                }
                catch
                {
                    Console.WriteLine("Het versturen van de bevestiging is niet gelukt.");
                }
            }
            else if (Mail_Bevestiging == "NEE" || Mail_Bevestiging == "2")
            {
                Console.WriteLine("U heeft gekozen om geen bevestiging in de mail te ontvangen.");
            }
            else
            {
                Console.WriteLine("U heeft gekozen om geen bevestiging in de mail te ontvangen.");
            }
            Console.WriteLine("Bedankt voor het online reserveren en we zien u graag binnenkort in onze bioscoop.");
            // Data Reservering toevoegen.
            List<JsonData> _data = new List<JsonData>();
            var DataUser = File.ReadAllText(@"https://stud.hosted.hr.nl/1010746/Samplelog.json"); //PATH VERANDEREN NAAR JOUW EIGEN BESTANDSLOCATIE ALS JE HIER EEN ERROR KRIJGT
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
            File.WriteAllText(@"C:\Users\woute\SampleLog.json", DataUser);

        }
    }
    public class Film : Program
    {
        public string[] FilmGenres { get; set; }
        public string FilmTitle { get; set; }
        public int FilmRoom { get; set; }
        public string[] FilmTimes { get; set; }

        public Film(string[] FilmGenres, string FilmTitle, int FilmRoom, string[] FilmTimes)
        {
            this.FilmGenres = FilmGenres;
            this.FilmTitle = FilmTitle;
            this.FilmRoom = FilmRoom;
            this.FilmTimes = FilmTimes;
        }



    }

    public class Medewerker : Program
    {
        public string Name;
        public string Admin_Password;
        public List<string> ListofAdmins;

        public Medewerker(string name, string AdminPass)
        {
            this.Name = name;
            this.Admin_Password = AdminPass;

        }

    }
    public class JsonData
    {

        public string Naam { get; set; }
        public string Email { get; set; }
        public string Reservatie_code { get; set; }
        public string Film { get; set; }
        public string Zaal { get; set; }
        public string Stoel_num { get; set; }



    }

    public class Program
    {
        private static string Genre_search;

        static void Main(string[] args)
        {
            string[] FilmGenresArray = new string[3];
            FilmGenresArray[0] = "Action";
            FilmGenresArray[1] = "Comedy";
            FilmGenresArray[2] = "Thriller";
            string[] FilmTimesArray = new string[1];
            FilmTimesArray[0] = "12:00";
            string TitleofFilm = "John Wick";
            int RoomofFilm = 3;
            Film FilmObject = new Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray);
            var UserInput = "To be declared";
            bool isAdmin = false;
            Medewerker admin = new Medewerker("admin", "admin");
            Gebruiker Klant = new Gebruiker();
            // Inladen Json Module 
            var MyFilmsData = new WebClient().DownloadString("https://stud.hosted.hr.nl/1010746/Filmsdata.json");
            string myJsonString = new WebClient().DownloadString("https://stud.hosted.hr.nl/1010746/snacksdrinks.json");
            string myUserData = new WebClient().DownloadString("https://stud.hosted.hr.nl/1010746/Samplelog.json");

            // Omzetten
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            dynamic DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            dynamic DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);

            // Startpagina applicatie
            Textkleur("groen");
            Console.WriteLine("Welkom op de startpagina van de bioscoop.");
            Console.WriteLine("Selecteer '1' om te zoeken op genre.");
            Console.WriteLine("Selecteer '2' om te zoeken op een specifieke film.");
            Console.WriteLine("Selecteer '3' om uw reservering te bekijken.");
            Console.WriteLine("Selecteer '4' om in te loggen als Bioscoop Medewerker.");
            Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            Textkleur("blauw");
            var Start_options = Console.ReadLine();
            Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            if (Start_options == "1")
            {
                List<string> Show_films = new List<string>();
                Textkleur("groen");
                Console.WriteLine("Op welke genre wilt u zoeken: ");
                Textkleur("groen");
                Console.WriteLine("Toets [1] voor Action.\nToets [2] voor Comedy.\nToets [3] voor Thriller.\nToets [4] voor Romantiek.\nToets [5] voor Drama.\nToets [6] voor Sci-Fi.\nToets [7] voor Familie films. ");
                Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                Textkleur("blauw");
                var Genre_select = Console.ReadLine();
                Genre(Genre_select);
                Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                Textkleur("groen");
                Console.WriteLine("We hebben deze film(s) gevonden onder de genre: ");
                for (int i = 0; i < DynamicFilmData["Films"].Count; i++)
                {

                    for (int j = 0; j < DynamicFilmData["Films"][i]["genre"].Count; j++)
                    {
                        string Genre_zoeken = (string)DynamicFilmData["Films"][i]["genre"][j];
                        if (Genre_search == Genre_zoeken)
                        {
                            //Genre_check(DynamicFilmData, i);
                            Show_films.Add(DynamicFilmData["Films"][i]["film"].ToString());

                        }
                    }

                }
                int count = 1;
                for (int y = 0; y < Show_films.Count; y++)
                {

                    Console.WriteLine("Toets [" + (count) + "] voor: " + Show_films[y]);
                    count++;
                }


                Console.WriteLine("Voor welke van de bovenstaande films zou u willen reserveren?");
                Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                Textkleur("blauw");
                string Chosen_film = Console.ReadLine();


                for (int i = 0; i < Show_films.Count + 1; i++)
                {
                    string film_showw = i.ToString();
                    if (Chosen_film == (film_showw))
                    {

                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Textkleur("groen");

                        Films(Chosen_film, Show_films);

                        string Chosen_date = " ";
                        Textkleur("groen");
                        Console.WriteLine("Type uw gewenste datum in, om uw gekozen film te bekijken in onze bioscoop.\ndd/mm/jjjj");
                        Chosen_date = Console.ReadLine();
                        if (Chosen_date.Length != 10)
                        {
                            Console.WriteLine("Ongeldige datum gebruik dit patroon\n dd/mm/jjjj \n om uw datum in te voeren.");
                        }
                        else { Klant.ReserveerCodeMail(); }

                    }
                }


            }
            else if (Start_options == "2")
            {
                Textkleur("groen");
                Console.WriteLine("Naar welke film bent u opzoek: ");
                Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                Textkleur("blauw");
                var Film_search = Console.ReadLine();
                Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                for (int i = 0; i < 5; i++)
                {
                    string Film_zoeken = (string)DynamicFilmData["Films"][i]["film"];
                    if (Film_search == Film_zoeken)
                    {
                        Textkleur("groen");
                        Console.WriteLine("U heeft gezocht de volgende film:");
                        Film_check(DynamicFilmData, i);
                    }
                }

            }
            else if (Start_options == "3")
            {
                Textkleur("groen");
                Console.Write("Voer hier uw reservatie code in:");
                Textkleur("blauw");
                var Reservatie_code = Console.ReadLine();
                Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");


                Textkleur("groen");
                for (int i = 0; i < DynamicUserData.Count; i++)
                {
                    string Res_code = (string)DynamicUserData[i]["Reservatie_code"];
                    if (Res_code == Reservatie_code)
                    {
                        Console.WriteLine("Uw reservering:");
                        Reservering_check(DynamicUserData, i);
                        //SnacksOption();
                        break;
                    }
                }
                Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                Textkleur("groen");
                Console.WriteLine("Toets 'R' om het progamma opnieuw op te starten.");
                Textkleur("blauw");
                string restart = Console.ReadLine();
                if (restart.ToUpper() == "R")
                {
                    Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                    Environment.Exit(1);
                }


            }
            else if (Start_options == "4")
            {
                Textkleur("groen");
                Console.WriteLine("Voer hier uw naam in:");
                Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                Textkleur("blauw");
                string input_name = Console.ReadLine();
                Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                Textkleur("groen");
                Console.WriteLine("Welkom, " + input_name + ". Voer nu het ingestelde admin wachtwoord in: ");
                admin.Name = input_name;
                Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                Textkleur("blauw");
                string input_password = Console.ReadLine();
                if (input_password == admin.Admin_Password)
                {
                    Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    isAdmin = true;
                    Textkleur("groen");
                    Console.WriteLine("U bent succesvol ingelogd als medewerker! Type !help voor een lijst aan commands.");
                    Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Textkleur("blauw");
                    UserInput = Console.ReadLine();
                    UserInputMethod(UserInput);

                }
                else
                {
                    Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Textkleur("groen");
                    Console.WriteLine("Het ingevoerde wachtwoord is incorrect, probeer het nogmaals: ");
                    Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Textkleur("blauw");
                    UserInput = Console.ReadLine();

                    if (UserInput == admin.Admin_Password)
                    {
                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Textkleur("groen");
                        isAdmin = true;
                        Console.WriteLine("U bent succesvol ingelogd als medewerker! Type !help voor een lijst aan commands.");
                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Textkleur("blauw");
                        UserInput = Console.ReadLine();
                        UserInputMethod(UserInput);

                    }
                    else
                    {
                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Textkleur("groen");
                        Console.WriteLine("Dat is incorrect, het programma wordt nu voor u gesloten.");
                        return; // Dit sluit het programma af na twee verkeerde password inputs.
                    }
                }
                Textkleur("blauw");
                UserInput = Console.ReadLine();
                UserInputMethod(UserInput);
                UserInput = Console.ReadLine();
                UserInputMethod(UserInput);


                void UserInputMethod(string UserInput)
                {
                    Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Textkleur("groen");
                    if (isAdmin == true && (UserInput == "!password"))
                    {

                        Console.WriteLine("Type nu het huidige admin wachtwoord in: ");
                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Textkleur("blauw");
                        UserInput = Console.ReadLine();
                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        if (UserInput == admin.Admin_Password)
                        {
                            Textkleur("groen");
                            Console.WriteLine("Type nu het nieuwe wachtwoord in: ");
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("blauw");
                            UserInput = Console.ReadLine();
                            admin.Admin_Password = UserInput;
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("groen");
                            Console.WriteLine("Het wachtwoord is succesvol veranderd naar: " + admin.Admin_Password);
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                        }
                    }
                    if (isAdmin == true && (UserInput == "!help"))
                    {
                        Textkleur("groen");
                        Console.WriteLine("Om het admin-wachtwoord opnieuw in te stellen, type: !password");
                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Textkleur("groen");
                        Console.WriteLine("Om een film toe te voegen aan de database, type: !newfilm");
                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Textkleur("groen");
                        Console.WriteLine("Om alle reserveringen te bekijken, type: !reserveringen");
                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Textkleur("groen");
                        Console.WriteLine("Om reserveringen per zaal te zien, type: !zaalreserveringen");
                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                    }
                    if (isAdmin == true && (UserInput == "!newfilm"))
                    {
                        string TitleFilm = null;
                        string GenreFilm = null;
                        Textkleur("groen");
                        Console.WriteLine("Hoeveel genre's heeft de nieuwe film? Er is een maximum van drie genre's!");
                        Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        Textkleur("blauw");
                        var StringArrayGenreLength_Input = Console.ReadLine();
                        int StringArrayGenreLength = Int32.Parse(StringArrayGenreLength_Input);
                        string[] FilmGenresArray = new string[StringArrayGenreLength];

                        //-Dictionary met alle genres-//
                        Dictionary<string, string> DictOfGenres = new Dictionary<string, string>()
                        {
                            {"Action", "1"},
                            {"Comedy", "2"},
                            {"Thriller", "3"},
                            {"Romantic", "4"},
                            {"Horror", "5"},
                            {"Drama", "6" }
                        };
                        //---------------------------// Moet nog worden ingecodeerd in de code hieronder //
                        if (StringArrayGenreLength == 1)
                        {
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("groen");
                            Console.WriteLine("U kunt kiezen uit de volgende genres: "); Textkleur("rood"); Console.Write("\n1. Action \n2. Comedy \n3. Thriller \n4. Romantic \n5. Horror \n6. Drama\n\n"); Textkleur("groen");
                            Console.WriteLine("Voer nu de genre van de film in: ");
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("blauw");
                            FilmGenresArray[0] = Console.ReadLine();

                        }
                        else if (StringArrayGenreLength == 2)
                        {
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("groen"); Console.WriteLine("Voer nu de eerste genre van de film in:");
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("blauw");
                            FilmGenresArray[0] = Console.ReadLine();
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("groen"); Console.WriteLine("Voer nu de tweede genre van de film in:");
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("blauw");
                            FilmGenresArray[1] = Console.ReadLine();
                        }
                        else if (StringArrayGenreLength == 3)
                        {
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("groen"); Console.WriteLine("Voer nu de eerste genre van de film in:");
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("blauw");
                            FilmGenresArray[0] = Console.ReadLine(); 
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------");Textkleur("groen"); Console.WriteLine("Voer nu de tweede genre van de film in:");
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("blauw");
                            FilmGenresArray[1] = Console.ReadLine();
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("groen"); Console.WriteLine("Voer nu de derde genre van de film in:");
                            Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("blauw");
                            FilmGenresArray[2] = Console.ReadLine();
                        }
                        Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("groen"); Console.WriteLine("Voer nu de titel van de nieuwe film in:");
                        Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("blauw");
                        TitleofFilm = Console.ReadLine();
                        Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("groen"); Console.WriteLine("Voer nu de zaal in van de film " + TitleofFilm + ": ");
                        Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("blauw");

                        var FilmZaalInput = Console.ReadLine(); int RoomofFilm = Int32.Parse(FilmZaalInput); // Userinput (string) word hier verandert naar een int variabel

                        Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("groen"); Console.WriteLine("Hoeveel tijdssloten wilt u beschikbaar stellen per dag? (maximaal 3): ");
                        Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("blauw");

                        var tijdsSlotenInput = Console.ReadLine(); 
                        int TimeSlots = Int32.Parse(tijdsSlotenInput); // Userinput (string) word hier verandert naar een int variabel
                        string[] FilmTimesArray = new string[TimeSlots];

                        if (TimeSlots == 1)
                        {
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("groen");
                            Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("blauw");
                            FilmTimesArray[0] = Console.ReadLine();
                        }
                        else if (TimeSlots == 2)
                        {
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("groen");
                            Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("blauw");
                            FilmTimesArray[0] = Console.ReadLine();
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("groen");
                            Console.WriteLine("Voer nu de tweede tijd van de film in: ");
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("blauw");
                            FilmTimesArray[1] = Console.ReadLine();
                        }
                        else if (TimeSlots == 3)
                        {
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("groen");
                            Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("blauw");
                            FilmTimesArray[0] = Console.ReadLine();
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("groen");
                            Console.WriteLine("Voer nu de tweede tijd van de film in: ");
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("blauw");
                            FilmTimesArray[1] = Console.ReadLine();
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("groen");
                            Console.WriteLine("Voer nu de derde tijd van de film in: ");
                            Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            Textkleur("blauw");
                            FilmTimesArray[2] = Console.ReadLine();
                        }
                        // Hier worden de FilmObject attributes verandert naar de values die net zijn doorgevoerd in de console door de admin-user //
                        FilmObject.FilmGenres = FilmGenresArray; FilmObject.FilmTitle = TitleofFilm; FilmObject.FilmTimes = FilmTimesArray; FilmObject.FilmRoom = RoomofFilm;
                        Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); Textkleur("groen");
                        Console.WriteLine("De film: " + TitleofFilm + " is succesvol toegevoegd aan de database.");
                        //-------------------------------------------------------------------------------------------------------------------------//
                        FilmDataBaseAdd(); // Dit voegt het object toe aan de Json file

                        // Nu wordt de volgende console input gecheckt door de UserInputMethod() function te callen // 
                        UserInput = Console.ReadLine();
                        UserInputMethod(UserInput);
                    }
                }

                void FilmDataBaseAdd() // Dit voegt de FilmObject object toe aan de Json file
                {

                    List<Film> _data = new List<Film>();
                    var FilmDataJson = File.ReadAllText(@"C:\Users\abdel\Source\Repos\Reservatie\Reservatie\Filmsdata.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path
                    var FilmObjectJson = JsonConvert.DeserializeObject<List<Film>>(FilmDataJson);
                    FilmObjectJson.Add(FilmObject);
                    FilmDataJson = JsonConvert.SerializeObject(FilmObjectJson);
                    File.WriteAllText(@"C:\Users\abdel\Source\Repos\Reservatie\Reservatie\Filmsdata.json", FilmDataJson);
                }

                void SnacksOption()
                {
                    //Eventuele snacks tijdens het reserveren
                    Console.WriteLine("Zou u ook alvast snacks willen bestellen voor bij de film?");
                    Console.WriteLine("Door online de snacks te reserveren krijgt u 15% korting op het gehele bedrag.");
                    Console.WriteLine("Toets 'JA' als u online snacks wilt bestellen, toets 'NEE' als u dit niet wilt.");
                    string Online_snacks = Console.ReadLine();
                    string Online_snacks_secondchange = null;

                    if (Online_snacks == "NEE")
                    {
                        Console.WriteLine("U heeft er voor gekozen om geen snacks te bestellen.");
                        Console.WriteLine("Weet u het zeker? Toets 'JA' om door te gaan en 'NEE' om het overzicht te bekijken met de snacks.");
                        Online_snacks_secondchange = Console.ReadLine();

                    }
                    else
                    {
                        Console.WriteLine("U heeft de verkeerde input gegeven.");
                        Console.WriteLine("Toets 'JA' om door te gaan en 'NEE' om het overzicht met snacks te bekijken.");
                        Online_snacks_secondchange = Console.ReadLine();
                    }
                    if (Online_snacks == "JA" || Online_snacks_secondchange == "NEE")
                    {
                        Console.WriteLine("Hieronder vindt u de lijst met de verkrijgbare snacks en dranken.");
                        //Json file met alle snacks.
                        Snacks(DynamicData);


                    }
                }

                
            }
            static void Genre_check(dynamic DynamicFilmData, int i)
            {

                Console.Write(DynamicFilmData["Films"][i]["film"] + "\n");

            }

            static void Film_check(dynamic DynamicFilmData, int i)
            {
                Console.WriteLine(DynamicFilmData["Films"][i]["film"] + "\n");
            }

            static void Reservering_check(dynamic dynamicUserData, int i)
            {

                Console.WriteLine("Naam: " + dynamicUserData[i]["Naam"]);
                Console.WriteLine("Email: " + dynamicUserData[i]["Email"]);
                Console.WriteLine("Reservatie code: " + dynamicUserData[i]["Reservatie_code"]);
                Console.WriteLine("Film: " + dynamicUserData[i]["Film"]);
                Console.WriteLine("Zaal: " + dynamicUserData[i]["Zaal"]);
                Console.WriteLine("Stoel nummer: " + dynamicUserData[i]["Stoel_num"]);


            }
            static void Textkleur(string kleur)
            {
                if (kleur == "groen")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (kleur == "blauw")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (kleur == "wit")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (kleur == "rood")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
            }
            static void Films(string Chosen_film, dynamic Show_films)
            {
                if (Chosen_film == "1")
                {
                    Console.WriteLine("U heeft gekozen voor de film: " + Show_films[0]);
                }
                else if (Chosen_film == "2")
                {
                    Console.WriteLine("U heeft gekozen voor de film: " + Show_films[1]);
                }
                else if (Chosen_film == "3")
                {
                    Console.WriteLine("U heeft gekozen voor de film: " + Show_films[2]);
                }
                else if (Chosen_film == "4")
                {
                    Console.WriteLine("U heeft gekozen voor de film: " + Show_films[3]);
                }
                else if (Chosen_film == "5")
                {
                    Console.WriteLine("U heeft gekozen voor de film: " + Show_films[4]);
                }
                else if (Chosen_film == "6")
                {
                    Console.WriteLine("U heeft gekozen voor de film: " + Show_films[5]);
                }

            }
            static void Genre(string Genre_select)
            {


                if (Genre_select == "1")
                {
                    Genre_search = "Action";
                }
                else if (Genre_select == "2")
                {
                    Genre_search = "Comedy";
                }
                else if (Genre_select == "3")
                {
                    Genre_search = "Thriller";
                }
                else if (Genre_select == "4")
                {
                    Genre_search = "Romantiek";
                }
                else if (Genre_select == "5")
                {
                    Genre_search = "Drama";
                }
                else if (Genre_select == "6")
                {
                    Genre_search = "Sci-Fi";
                }
                else if (Genre_select == "7")
                {
                    Genre_search = "Familie";
                }
            }
            static void Snacks(dynamic DynamicData)
            {
                Console.WriteLine(DynamicData.dranken[0].Name);
                Console.WriteLine(DynamicData.dranken[0].Price);
                Console.WriteLine(DynamicData.dranken[1].Name);
                Console.WriteLine(DynamicData.dranken[1].Price);
                Console.WriteLine(DynamicData.dranken[2].Name);
                Console.WriteLine(DynamicData.dranken[2].Price);
                Console.WriteLine(DynamicData.dranken[3].Name);
                Console.WriteLine(DynamicData.dranken[3].Price);
                Console.WriteLine(DynamicData.dranken[4].Name);
                Console.WriteLine(DynamicData.dranken[4].Price);
                Console.WriteLine(DynamicData.dranken[5].Name);
                Console.WriteLine(DynamicData.dranken[5].Price);
                Console.WriteLine(DynamicData.dranken[6].Name);
                Console.WriteLine(DynamicData.dranken[6].Price);
                Console.WriteLine(DynamicData.dranken[7].Name);
                Console.WriteLine(DynamicData.dranken[7].Price);
                Console.WriteLine(DynamicData.dranken[8].Name);
                Console.WriteLine(DynamicData.dranken[8].Price);
                Console.WriteLine(DynamicData.dranken[9].Name);
                Console.WriteLine(DynamicData.dranken[9].Price);
                Console.WriteLine(DynamicData.dranken[10].Name);
                Console.WriteLine(DynamicData.dranken[10].Price);
                Console.WriteLine(DynamicData.dranken[11].Name);
                Console.WriteLine(DynamicData.dranken[11].Price);
                Console.WriteLine("Snacks:");
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine(DynamicData.snacks[0].Name);
                Console.WriteLine(DynamicData.snacks[0].Price);
                Console.WriteLine(DynamicData.snacks[1].Name);
                Console.WriteLine(DynamicData.snacks[1].Price);
                Console.WriteLine(DynamicData.snacks[2].Name);
                Console.WriteLine(DynamicData.snacks[2].Price);
                Console.WriteLine(DynamicData.snacks[3].Name);
                Console.WriteLine(DynamicData.snacks[3].Price);
                Console.WriteLine(DynamicData.snacks[4].Name);
                Console.WriteLine(DynamicData.snacks[4].Price);
                Console.WriteLine(DynamicData.snacks[5].Name);
                Console.WriteLine(DynamicData.snacks[5].Price);
                Console.WriteLine(DynamicData.snacks[6].Name);
                Console.WriteLine(DynamicData.snacks[6].Price);

            }
        }
    }
}