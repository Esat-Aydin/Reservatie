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
using Film;
using Gebruiker;
using Cinema;

namespace MedewerkerClass
{
    public class Medewerker : Gebruiker.Gebruiker
    {
        public string Name { get; set; }
        public string Admin_Password { get; set; }

        public Medewerker(string name = null, string AdminPass = null)
        {
            this.Name = name;
            this.Admin_Password = AdminPass;

        }


        new public void UserInputMethod(string UserInput)
        {
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
                if (UserInput == this.Admin_Password)
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Type nu het nieuwe wachtwoord in: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("zwart");
                    UserInput = Console.ReadLine();
                    this.Admin_Password = UserInput;
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Het wachtwoord is succesvol veranderd naar: " + this.Admin_Password);
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                }
            }
            else if (this.isAdmin == true && (UserInput == "!help"))
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
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Hoeveel genre's heeft de nieuwe film? Er is een maximum van drie genre's!");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("zwart");
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
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Voer nu de titel van de nieuwe film in:");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
                TitleofFilm = Console.ReadLine();
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Voer nu de zaal in van de film " + TitleofFilm + ": ");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");

                var FilmZaalInput = Console.ReadLine(); int RoomofFilm = Int32.Parse(FilmZaalInput); // Userinput (string) word hier verandert naar een int variabel

                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Hoeveel tijdssloten wilt u beschikbaar stellen per dag? (maximaal 3): ");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");

                var tijdsSlotenInput = Console.ReadLine();
                int TimeSlots = Int32.Parse(tijdsSlotenInput); // Userinput (string) word hier verandert naar een int variabel
                string[] FilmTimesArray = new string[TimeSlots];

                if (TimeSlots == 1)
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("zwart");
                    FilmTimesArray[0] = Console.ReadLine();
                }
                else if (TimeSlots == 2)
                {
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
                }
                else if (TimeSlots == 3)
                {
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
                }
                // Hier worden de FilmObject attributes verandert naar de values die net zijn doorgevoerd in de console door de admin-user //
                Film.Film FilmObject = new Film.Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray);
                FilmObject.AddFilmtoDataBase(FilmObject); // Dit voegt het object toe aan de Json file
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("wit");
                Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("zwart");
                //-------------------------------------------------------------------------------------------------------------------------//
                // Nu wordt de volgende console input gecheckt door de UserInputMethod() function te callen // 
                UserInput = Console.ReadLine();
                UserInputMethod(UserInput);
            }
            UserInput = Console.ReadLine();
            UserInputMethod(UserInput);
        }
    }
}
