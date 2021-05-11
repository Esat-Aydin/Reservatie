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

namespace Reservation
{
    public abstract class Reserveren
    {
        //public Gebruiker.Gebruiker GebruikerObject;
        //public Reserveren() 
        //{
            //this.GebruikerObject = new Gebruiker.Gebruiker();
        //}
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
            Scherm.Screens.CinemaBanner();
            ConsoleCommands.Textkleur("wit");
            Console.Write("\t\t\tVoer hier uw reserverings code in:\n");
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("zwart");
            var Reservatie_code = Console.ReadLine();
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");


            ConsoleCommands.Textkleur("wit");
            for (int i = 0; i < DynamicUserData.Count; i++)
            {
                string Res_code = (string)DynamicUserData[i]["Reservatie_code"];
                if (Res_code == Reservatie_code)
                {
                    Scherm.Screens.CinemaBanner();
                    Console.WriteLine("\t\tUw reservering: ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Reservering_check(DynamicUserData, i);
                    //SnacksOption();
                    break;
                }
            }
            ConsoleCommands.Textkleur("wit");







        }
        public void ReservationCodePercentage()
        {
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("rood");
            for (int i = 0; i <= 100; i++)
            {
                Console.Write($"\rProgress: {i}%   ");
                Thread.Sleep(25);

            }
            Console.WriteLine("");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");

        }
        public void ReserveringMaken(string UserInput)
        {
            Film.Film FilmObject = new();
            MedewerkerClass.Medewerker admin = new();
            Gebruiker.Gebruiker Klant = new();
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
                Console.WriteLine("\t\t\tNaar welke film bent u opzoek: ");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                string Film_search = Console.ReadLine();
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                for (int i = 0; i < DynamicFilmData.Count; i++)
                {
                    string Film_zoeken = (string)DynamicFilmData[i]["FilmTitle"];
                    if (Film_search == Film_zoeken)
                    {
                        Scherm.Screens.CinemaBanner();
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("U heeft gezocht naar de volgende film:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("rood");

                        FilmObject.Film_check(DynamicFilmData, i);
                    }
                }
                Klant.ZoekOptie(Film_search, DynamicFilmData);
            }
            else if (UserInput == "2")
            {
                Scherm.Screens.CinemaBanner();
                List<string> Show_films = new List<string>();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("\t\t\t\tKies een genre uit\t\t\t\t");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("wit");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("1"); ConsoleCommands.Textkleur("wit"); Console.Write("] Action\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("2"); ConsoleCommands.Textkleur("wit"); Console.Write("] Comedy\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("3"); ConsoleCommands.Textkleur("wit"); Console.Write("] Thriller\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("4"); ConsoleCommands.Textkleur("wit"); Console.Write("] Romantiek\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("5"); ConsoleCommands.Textkleur("wit"); Console.Write("] Drama\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("6"); ConsoleCommands.Textkleur("wit"); Console.Write("] Sci-Fi\n");
                Console.Write("["); Console.ForegroundColor = ConsoleColor.Black; Console.Write("7"); ConsoleCommands.Textkleur("wit"); Console.Write("] Familie\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                var Genre_select = Console.ReadLine();
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


                for (int i = 0; i < Show_films.Count + 1; i++)
                {
                    string film_showw = i.ToString();
                    if (Chosen_film == (film_showw))
                    {
                        Scherm.Screens.CinemaBanner();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("wit");

                        FilmObject.Films(Chosen_film, Show_films);
                        ConsoleCommands.Textkleur("wit");
                        Console.Write("\nU heeft gekozen voor: "); ConsoleCommands.Textkleur("rood"); Console.Write(Chosen_film + "\n\n");
                        string Chosen_date = " ";
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("Voer uw gewenste dag in (Bijvoorbeeld: Maandag): ");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("groen");
                        Chosen_date = Console.ReadLine();
                        if (Chosen_date.Length > 10 | Chosen_date.Length < 5)
                        {
                            Console.WriteLine("Ongeldige datum.");
                        }
                        //else { ReserveerCodeMail(); }

                    }
                }



            }

            else if (UserInput == "3")
            {
                Scherm.Screens.CinemaBanner();
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
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                int choice = Int32.Parse(Console.ReadLine());
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("U heeft gekozen voor de volgende film:\t" + All_Films[choice - 1]);

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
        public void ReserveerCodeMail(string Gezochte_Film, string Show_Tijden) // Deze method regelt de reservering en mailt het vervolgens naar de gebruiker - Callen: Gebruiker.ReserveerCodeMail();
        {
            Gebruiker.Gebruiker Klant = new Gebruiker.Gebruiker();
            // informatie voor eventueel mailen reservatie code.
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("groen");
            Console.WriteLine("Om te kunnen reserveren hebben wij uw naam en emailadres van u nodig.");
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("rood");
            Console.Write("Naam: ");
            ConsoleCommands.Textkleur("blauw");
            string Naam_klant = Console.ReadLine();
            ConsoleCommands.Textkleur("rood");
            Console.Write("Email adress: ");
            ConsoleCommands.Textkleur("blauw");
            string Naam_email = Console.ReadLine();
            // Eventuele betaal methode?
            Klant.Naam = Naam_klant;
            Klant.Email = Naam_email;
            Klant.Film = Gezochte_Film;
            Klant.Film_Time = Show_Tijden;
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("groen");
            // Einde reserveren.
            Console.WriteLine("Bedankt voor het reserveren!");
            Console.WriteLine("Een ogenblik geduld alstublieft uw reservatie code wordt geladen.");
            Thread.Sleep(1000);
            ReservationCodePercentage();
            string GeneratedCode = this.ReserveringsCodeGenerator();
            // Random generator voor het maken van de reservatie code.


            ConsoleCommands.Textkleur("groen");
            Console.Write("Reserverings code: ");
            ConsoleCommands.Textkleur("rood");
            Console.WriteLine(GeneratedCode);
            ConsoleCommands.Textkleur("groen");
            Console.WriteLine("Zou u een bevestiging in uw mail willen ontvangen?");
            Console.WriteLine("Toets [JA] als u een mail-bevestinging wilt ontvangen of toets [NEE] als u geen mail-bevestiging .");
            // Email bevestiging.
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            ConsoleCommands.Textkleur("blauw");
            string Mail_Bevestiging = Console.ReadLine();

            if (Mail_Bevestiging == "JA")
            {

                try
                {
                    var message = new MimeMessage();
                    // Email verzender
                    message.From.Add(new MailboxAddress("ProjectB", "ProjectB1J@gmail.com"));
                    // Email geadresseerde
                    message.To.Add(new MailboxAddress(Klant.Naam, Klant.Email));
                    // Email onderwerp
                    message.Subject = "Bevestiging online reservatie.";
                    // Email text
                    message.Body = new TextPart("plain")
                    {
                        Text = @"Hallo " + Klant.Naam + @",
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
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("De bevestiging is verstuurd per Email.");
                    };
                }
                catch
                {
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Het versturen van de bevestiging is niet gelukt.");
                }
            }
            else if (Mail_Bevestiging == "NEE")
            {
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("U heeft gekozen om geen bevestiging in de mail te ontvangen.");
            }
            else
            {
                ReserveerCodeMail(Gezochte_Film, Show_Tijden);
            }
            Console.WriteLine("Bedankt voor het online reserveren en we zien u graag binnenkort in onze bioscoop.");
            ConsoleCommands CommandLine = new ConsoleCommands();


            // Data Reservering toevoegen.
            List<JsonData> _data = new List<JsonData>();
            var DataUser = File.ReadAllText(@"C:\Users\woute\source\repos\Esat-Aydin\Reservatie\Reservatie\SampleLog.json"); //PATH VERANDEREN NAAR JOUW EIGEN BESTANDSLOCATIE ALS JE HIER EEN ERROR KRIJGT
            var JsonData = JsonConvert.DeserializeObject<List<JsonData>>(DataUser)
                      ?? new List<JsonData>();

            JsonData.Add(new JsonData()
            {
                Reservatie_code = GeneratedCode,
                Naam = Klant.Naam,
                Email = Klant.Email,
                Film = Klant.Film,
                FilmTime = Klant.Film_Time

                //Zaal =
                //Stoel_num =

            });

            DataUser = JsonConvert.SerializeObject(JsonData);
            File.WriteAllText(@"C:\Users\woute\source\repos\Esat-Aydin\Reservatie\Reservatie\SampleLog.json", DataUser);
            CommandLine.RestartOption();

        }
        public static void Reservering_check(dynamic dynamicUserData, int i)
        {
            ConsoleCommands CommandLine = new ConsoleCommands();
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            Console.WriteLine("Naam: " + dynamicUserData[i]["Naam"]);
            Console.WriteLine("Email: " + dynamicUserData[i]["Email"]);
            Console.WriteLine("Reservatie code: " + dynamicUserData[i]["Reservatie_code"]);
            Console.WriteLine("Film: " + dynamicUserData[i]["Film"]);
            Console.WriteLine("Zaal: " + dynamicUserData[i]["Zaal"]);
            Console.WriteLine("Stoel nummer: " + dynamicUserData[i]["Stoel_num"]);
            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
            CommandLine.RestartOption();


        }
    }
}
