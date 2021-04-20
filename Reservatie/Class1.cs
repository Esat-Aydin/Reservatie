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

namespace Cinema
{
    public class Gebruiker : ConsoleCommands
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
        public void UserInputMethod(string UserInput)
        {
            string TitleofFilm = null;
            ConsoleCommands.Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            ConsoleCommands.Textkleur("groen");
            if (this.isAdmin == true && (UserInput == "!password"))
            {

                Console.WriteLine("Type nu het huidige admin wachtwoord in: ");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("blauw");
                UserInput = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                if (UserInput == Password)
                {
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Type nu het nieuwe wachtwoord in: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    UserInput = Console.ReadLine();
                    Password = UserInput;
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Het wachtwoord is succesvol veranderd naar: " + Password);
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                }
            }
            if (this.isAdmin == true && (UserInput == "!help"))
            {
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Om het admin-wachtwoord opnieuw in te stellen, type: !password");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Om een film toe te voegen aan de database, type: !newfilm");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Om alle reserveringen te bekijken, type: !reserveringen");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Om reserveringen per zaal te zien, type: !zaalreserveringen");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
            }
            if (this.isAdmin == true && (UserInput == "!newfilm"))
            {
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Hoeveel genre's heeft de nieuwe film? Er is een maximum van drie genre's!");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("blauw");
                var StringArrayGenreLength_Input = Console.ReadLine();
                try
                {
                    int StringArrayGenreLength = Int32.Parse(StringArrayGenreLength_Input);
                    string[] FilmGenresArray = new string[StringArrayGenreLength];
                    if (StringArrayGenreLength == 1)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("U kunt kiezen uit de volgende genres: "); ConsoleCommands.Textkleur("rood"); Console.Write("\n1. Action \n2. Comedy \n3. Thriller \n4. Romantic \n5. Horror \n6. Drama\n\n"); ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("Voer nu de genre van de film in: ");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                        FilmGenresArray[0] = Console.ReadLine();

                    }
                    else if (StringArrayGenreLength == 2)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("Voer nu de eerste genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                        FilmGenresArray[0] = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("Voer nu de tweede genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                        FilmGenresArray[1] = Console.ReadLine();
                    }
                    else if (StringArrayGenreLength == 3)
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("Voer nu de eerste genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                        FilmGenresArray[0] = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("Voer nu de tweede genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                        FilmGenresArray[1] = Console.ReadLine();
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("Voer nu de derde genre van de film in:");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                        FilmGenresArray[2] = Console.ReadLine();
                    }
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

                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de titel van de nieuwe film in:");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                    TitleofFilm = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de zaal in van de film " + TitleofFilm + ": ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");

                    var FilmZaalInput = Console.ReadLine(); int RoomofFilm = Int32.Parse(FilmZaalInput); // Userinput (string) word hier verandert naar een int variabel

                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de gewenste dag in"); ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    var dag_UserInput = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------");
                    Console.WriteLine("Hoeveel tijdssloten wilt u beschikbaar stellen per dag? (maximaal 3): ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
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
                            ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("Voer nu de tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("blauw");

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
                            Film FilmObject = new Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray, DictOf);
                            FilmObject.AddFilmtoDataBase(FilmObject); // Dit voegt het object toe aan de Json file
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
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
                            ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("blauw");
                            FilmTimesArray[0] = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("Voer nu de tweede tijd van de film in: ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("blauw");
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
                            Film FilmObject = new Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray, DictOf);
                            FilmObject.AddFilmtoDataBase(FilmObject); // Dit voegt het object toe aan de Json file
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
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
                            ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("blauw");
                            FilmTimesArray[0] = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("Voer nu de tweede tijd van de film in: ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("blauw");
                            FilmTimesArray[1] = Console.ReadLine();
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("Voer nu de derde tijd van de film in: ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("blauw");
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
                            Film FilmObject = new Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray, DictOf);
                            FilmObject.AddFilmtoDataBase(FilmObject); // Dit voegt het object toe aan de Json file
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
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
                        ConsoleCommands.Textkleur("blauw");
                    }
                    
                }
                catch
                {
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("rood");
                    Console.WriteLine("U moet een getal invoeren! (1, 2, 3, 4, etc.");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                }
                
                
                
            }
            UserInput = Console.ReadLine();
            UserInputMethod(UserInput);
        }
        public void AdminConsole(bool adminConsoleChosen)
        {

            {
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Voer hier uw naam in:");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("blauw");
                string input_name = Console.ReadLine();
                string adminInputName = input_name;
                AccountCheck(adminInputName);

            }
        }
        public void AccountCheck(string Naam)
        {
            bool ReturnValue = false;
            var AccountUsers = new WebClient().DownloadString(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\AccountUsers.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path
            dynamic AccountUsers_Gebruiker = JsonConvert.DeserializeObject(AccountUsers);
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
                Console.WriteLine(ListofAccountsNames[i]);

                if (StoredName == Naam)
                {
                    ReturnValue = true;
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Welkom, " + Naam + ". Voer nu het ingestelde admin wachtwoord in: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    string input_password = Console.ReadLine();
                    if (input_password == ListofAccountsPasswords[i])
                    {
                        if (ListofAccountsisAdmin[i] == "True")
                        {
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("U bent succesvol ingelogd als medewerker! Type !help voor een lijst aan commands.");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("blauw");
                            Gebruiker adminObject = new Gebruiker(Naam, ListofAccountsEmails[i], input_password, ReturnValue);
                            UserInput = Console.ReadLine();
                            adminObject.UserInputMethod(UserInput);
                        }
                    }

                    else
                    {
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("Het ingevoerde wachtwoord is incorrect, probeer het nogmaals: ");
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        ConsoleCommands.Textkleur("blauw");
                        UserInput = Console.ReadLine();

                        if (UserInput == ListofAccountsPasswords[i] && ListofAccountsisAdmin[i] == "True")
                        {
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("groen");
                            Gebruiker adminObject = new Gebruiker(Naam, ListofAccountsEmails[i], input_password, ReturnValue);
                            Console.WriteLine("U bent succesvol ingelogd als medewerker! Type !help voor een lijst aan commands.");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("blauw");
                            UserInput = Console.ReadLine();
                            adminObject.UserInputMethod(UserInput);
                        }
                        else
                        {
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("Het ingevoerde wachtwoord is incorrect, het programma wordt nu voor u afgesloten. ");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("blauw"); RestartOption();
                            UserInput = Console.ReadLine();
                        }
                    }
                }
            }
             if (ReturnValue == false)
            {
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("We hebben geen account kunnen vinden met deze naam: " + Naam);
                ConsoleCommands.Textkleur("blauw");
                RestartOption(); // Dit sluit het programma af na twee verkeerde password inputs.
                ConsoleCommands.Textkleur("blauw");
                UserInput = Console.ReadLine();
                UserInputMethod(UserInput);
                UserInput = Console.ReadLine();
                UserInputMethod(UserInput);
                }










            


        }

        public void AccountCreate(Gebruiker Object) // Eerst een object maken, dan hier als parameter in vullen om het te pushen naar de JSon file
        {
            List<Gebruiker> _data = new List<Gebruiker>();
            var AccountUsers = new WebClient().DownloadString(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\AccountUsers.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path
            var AccountUsers_Gebruiker = JsonConvert.DeserializeObject<List<Gebruiker>>(AccountUsers);
            AccountUsers_Gebruiker.Add(Object);
            AccountUsers = JsonConvert.SerializeObject(AccountUsers_Gebruiker);
            File.WriteAllText(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\AccountUsers.json", AccountUsers); // Net als AccountUsers de path veranderen als je hier errors krijgt!
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
            RestartOption();
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
        public static void Reservering_check(dynamic dynamicUserData, int i)
        {

            Console.WriteLine("Naam: " + dynamicUserData[i]["Naam"]);
            Console.WriteLine("Email: " + dynamicUserData[i]["Email"]);
            Console.WriteLine("Reservatie code: " + dynamicUserData[i]["Reservatie_code"]);
            Console.WriteLine("Film: " + dynamicUserData[i]["Film"]);
            Console.WriteLine("Zaal: " + dynamicUserData[i]["Zaal"]);
            Console.WriteLine("Stoel nummer: " + dynamicUserData[i]["Stoel_num"]);


        }
        public static void Snacks(dynamic DynamicData)
        {
            ConsoleCommands.Textkleur("groen");
            Console.WriteLine("Hieronder vindt u de lijst met de verkrijgbare snacks en dranken.");
            Textkleur("wit");
            Console.WriteLine("-----------------------------------------------------------------");
            Textkleur("groen");
            Console.WriteLine("Dranken:");
            Textkleur("wit");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Textkleur("groen");
            Console.WriteLine("Toets [1] voor: " + DynamicData.dranken[0].Name + "  Prijs: " + DynamicData.dranken[0].Price);
            Console.WriteLine("Toets [2] voor: " + DynamicData.dranken[1].Name + "  Prijs: " + DynamicData.dranken[1].Price);
            Console.WriteLine("Toets [3] voor: " + DynamicData.dranken[2].Name + "  Prijs: " + DynamicData.dranken[2].Price);
            Console.WriteLine("Toets [4] voor: " + DynamicData.dranken[3].Name + "  Prijs: " + DynamicData.dranken[3].Price);
            Console.WriteLine("Toets [5] voor: " + DynamicData.dranken[4].Name + "  Prijs: " + DynamicData.dranken[4].Price);
            Console.WriteLine("Toets [6] voor: " + DynamicData.dranken[5].Name + "  Prijs: " + DynamicData.dranken[5].Price);
            Console.WriteLine("Toets [7] voor: " + DynamicData.dranken[6].Name + "  Prijs: " + DynamicData.dranken[6].Price);
            Console.WriteLine("Toets [8] voor: " + DynamicData.dranken[7].Name + "  Prijs: " + DynamicData.dranken[7].Price);
            Console.WriteLine("Toets [9] voor: " + DynamicData.dranken[8].Name + "  Prijs: " + DynamicData.dranken[8].Price);
            Console.WriteLine("Toets [10] voor: " + DynamicData.dranken[9].Name + "  Prijs: " + DynamicData.dranken[9].Price);
            Console.WriteLine("Toets [11] voor: " + DynamicData.dranken[10].Name + "  Prijs: " + DynamicData.dranken[10].Price);
            Console.WriteLine("Toets [12] voor: " + DynamicData.dranken[11].Name + "  Prijs: " + DynamicData.dranken[11].Price);
            Textkleur("wit");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Textkleur("groen");
            Console.WriteLine("Snacks:");
            Textkleur("wit");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Textkleur("groen");
            Console.WriteLine("Toets [13] voor: " + DynamicData.snacks[0].Name + "  Prijs: " + DynamicData.snacks[0].Price);
            Console.WriteLine("Toets [14] voor: " + DynamicData.snacks[1].Name + "  Prijs: " + DynamicData.snacks[1].Price);
            Console.WriteLine("Toets [15] voor: " + DynamicData.snacks[2].Name + "  Prijs: " + DynamicData.snacks[2].Price);
            Console.WriteLine("Toets [16] voor: " + DynamicData.snacks[3].Name + "  Prijs: " + DynamicData.snacks[3].Price);
            Console.WriteLine("Toets [17] voor: " + DynamicData.snacks[4].Name + "  Prijs: " + DynamicData.snacks[4].Price);
            Console.WriteLine("Toets [18] voor: " + DynamicData.snacks[5].Name + "  Prijs: " + DynamicData.snacks[5].Price);
            Console.WriteLine("Toets [19] voor: " + DynamicData.snacks[6].Name + "  Prijs: " + DynamicData.snacks[6].Price);
            Textkleur("wit");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Textkleur("groen");


        }
        public void SnacksOption() // Om deze te callen: Gebruiker.SnacksOption();
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
                string myJsonString = new WebClient().DownloadString("https://stud.hosted.hr.nl/1010746/snacksdrinks.json"); // Path moet nog veranderd worden
                dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
                Snacks(DynamicData);


            }
        }
    }
    public class Film // Object van deze class wordt toegevoegd aan de Json file die dan aan de gebruiker kan worden getoond (voor het toevoegen/verwijderen/bewerken van films)
    {
        public string[] FilmGenres { get; set; }
        public string FilmTitle { get; set; }
        public int FilmRoom { get; set; }
        public string[] FilmTimes { get; set; }
        public Dictionary<string,List<string>> FilmDataTimes { get; set; }

        public Film(string[] FilmGenres = null, string FilmTitle = null, int FilmRoom = 0, string[] FilmTimes = null, Dictionary<string,List<string>> DictofData = null)
        {
            this.FilmGenres = FilmGenres;
            this.FilmTitle = FilmTitle;
            this.FilmRoom = FilmRoom;
            this.FilmTimes = FilmTimes;
            this.FilmDataTimes = DictofData; 
        }
        public void AddFilmtoDataBase(Film FilmObject) // Dit voegt de FilmObject object toe aan de Json file
        {

            List<Film> _data = new List<Film>();
            var FilmDataJson = File.ReadAllText(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\Filmsdata.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path
            var FilmObjectJson = JsonConvert.DeserializeObject<List<Film>>(FilmDataJson);
            FilmObjectJson.Add(FilmObject);
            FilmDataJson = JsonConvert.SerializeObject(FilmObjectJson);
            File.WriteAllText(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\Filmsdata.json", FilmDataJson); // Net als FilmDataJson de path veranderen als je hier errors krijgt!
        }
        public void Film_check(dynamic DynamicFilmData, int i)
        {
            Console.WriteLine(DynamicFilmData["Films"][i]["film"] + "\n");
        }


        public void Films(string Chosen_film, dynamic Show_films)
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



    }
    public class DataTijd
    {
        public string Data;
        public string Tijd;

    }
    public class ConsoleCommands
    {
        public string UserInput;
        public string Genre_search;
        public ConsoleCommands(string UserInput = null)
        {
            this.UserInput = UserInput;
        }
        public static void Textkleur(string kleur)
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
        public void RestartOption()
        {
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
        public void Genre(string Genre_select)
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
    }
    public class Medewerker : Gebruiker
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
            ConsoleCommands.Textkleur("groen");
            if (this.isAdmin == true && (UserInput == "!password"))
            {

                Console.WriteLine("Type nu het huidige admin wachtwoord in: ");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("blauw");
                UserInput = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                if (UserInput == this.Admin_Password)
                {
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Type nu het nieuwe wachtwoord in: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    UserInput = Console.ReadLine();
                    this.Admin_Password = UserInput;
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Het wachtwoord is succesvol veranderd naar: " + this.Admin_Password);
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                }
            }
            else if (this.isAdmin == true && (UserInput == "!help"))
            {
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Om het admin-wachtwoord opnieuw in te stellen, type: !password");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Om een film toe te voegen aan de database, type: !newfilm");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Om alle reserveringen te bekijken, type: !reserveringen");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Om reserveringen per zaal te zien, type: !zaalreserveringen");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
            }
            if (this.isAdmin == true && (UserInput == "!newfilm"))
            {
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Hoeveel genre's heeft de nieuwe film? Er is een maximum van drie genre's!");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("blauw");
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
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("U kunt kiezen uit de volgende genres: "); ConsoleCommands.Textkleur("rood"); Console.Write("\n1. Action \n2. Comedy \n3. Thriller \n4. Romantic \n5. Horror \n6. Drama\n\n"); ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de genre van de film in: ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                    FilmGenresArray[0] = Console.ReadLine();

                }
                else if (StringArrayGenreLength == 2)
                {
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de eerste genre van de film in:");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                    FilmGenresArray[0] = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de tweede genre van de film in:");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                    FilmGenresArray[1] = Console.ReadLine();
                }
                else if (StringArrayGenreLength == 3)
                {
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de eerste genre van de film in:");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                    FilmGenresArray[0] = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de tweede genre van de film in:");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                    FilmGenresArray[1] = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de derde genre van de film in:");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                    FilmGenresArray[2] = Console.ReadLine();
                }
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Voer nu de titel van de nieuwe film in:");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                TitleofFilm = Console.ReadLine();
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Voer nu de zaal in van de film " + TitleofFilm + ": ");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");

                var FilmZaalInput = Console.ReadLine(); int RoomofFilm = Int32.Parse(FilmZaalInput); // Userinput (string) word hier verandert naar een int variabel

                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Hoeveel tijdssloten wilt u beschikbaar stellen per dag? (maximaal 3): ");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");

                var tijdsSlotenInput = Console.ReadLine();
                int TimeSlots = Int32.Parse(tijdsSlotenInput); // Userinput (string) word hier verandert naar een int variabel
                string[] FilmTimesArray = new string[TimeSlots];

                if (TimeSlots == 1)
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    FilmTimesArray[0] = Console.ReadLine();
                }
                else if (TimeSlots == 2)
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    FilmTimesArray[0] = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de tweede tijd van de film in: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    FilmTimesArray[1] = Console.ReadLine();
                }
                else if (TimeSlots == 3)
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de eerste tijd van de film in (met de format UU:MM, voorbeeld: 12:15): ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    FilmTimesArray[0] = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de tweede tijd van de film in: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    FilmTimesArray[1] = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Voer nu de derde tijd van de film in: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    FilmTimesArray[2] = Console.ReadLine();
                }
                // Hier worden de FilmObject attributes verandert naar de values die net zijn doorgevoerd in de console door de admin-user //
                Film FilmObject = new Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray);
                FilmObject.AddFilmtoDataBase(FilmObject); // Dit voegt het object toe aan de Json file
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("groen");
                Console.WriteLine("De film: " + FilmObject.FilmTitle + " is succesvol toegevoegd aan de database.");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("-----------------------------------------------------------------"); ConsoleCommands.Textkleur("blauw");
                //-------------------------------------------------------------------------------------------------------------------------//
                // Nu wordt de volgende console input gecheckt door de UserInputMethod() function te callen // 
                UserInput = Console.ReadLine();
                UserInputMethod(UserInput);
            }
            UserInput = Console.ReadLine();
            UserInputMethod(UserInput);
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
        static void Main(string[] args)
        {
            // Voor de creation van de objects
            string[] FilmGenresArray = new string[3];
            FilmGenresArray[0] = "Action";
            FilmGenresArray[1] = "Comedy";
            FilmGenresArray[2] = "Thriller";
            string[] FilmTimesArray = new string[1];
            FilmTimesArray[0] = "12:00";
            int RoomofFilm = 3;
            string TitleofFilm = "John Wick";
            string DefaultAdmin_Password = "admin";


            // Objects
            Film FilmObject = new Film(FilmGenresArray, TitleofFilm, RoomofFilm, FilmTimesArray);
            Medewerker admin = new Medewerker(null, DefaultAdmin_Password);
            Gebruiker Klant = new Gebruiker();
            ConsoleCommands CommandLine = new ConsoleCommands();

            // Inladen Json Module 
            var MyFilmsData = new WebClient().DownloadString("https://stud.hosted.hr.nl/1010746/Filmsdata.json");
            string myJsonString = new WebClient().DownloadString("https://stud.hosted.hr.nl/1010746/snacksdrinks.json");
            string myUserData = new WebClient().DownloadString("https://stud.hosted.hr.nl/1010746/Samplelog.json");

            // Omzetten
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            dynamic DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            dynamic DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);

            // Startpagina applicatie

                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Welkom op de startpagina van de bioscoop.");
                Console.WriteLine("Selecteer '1' om te zoeken op genre.");
                Console.WriteLine("Selecteer '2' om te zoeken op een specifieke film.");
                Console.WriteLine("Selecteer '3' om uw reservering te bekijken.");
                Console.WriteLine("Selecteer '4' om in te loggen als Bioscoop Medewerker.");
                Console.WriteLine("Selecteer '5' om een account te maken.");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("blauw");
                var Start_options = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");

                if (Start_options == "1")
                {
                    List<string> Show_films = new List<string>();
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Op welke genre wilt u zoeken: ");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Toets [1] voor Action.\nToets [2] voor Comedy.\nToets [3] voor Thriller.\nToets [4] voor Romantiek.\nToets [5] voor Drama.\nToets [6] voor Sci-Fi.\nToets [7] voor Familie films. ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    var Genre_select = Console.ReadLine();
                    ConsoleCommands.Textkleur("groen");
                    CommandLine.Genre(Genre_select);
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("We hebben deze film(s) gevonden onder de genre: ");
                    for (int i = 0; i < DynamicFilmData["Films"].Count; i++)
                    {

                        for (int j = 0; j < DynamicFilmData["Films"][i]["genre"].Count; j++)
                        {
                            string Genre_zoeken = (string)DynamicFilmData["Films"][i]["genre"][j];
                            if (CommandLine.Genre_search == Genre_zoeken)
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
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    string Chosen_film = Console.ReadLine();


                    for (int i = 0; i < Show_films.Count + 1; i++)
                    {
                        string film_showw = i.ToString();
                        if (Chosen_film == (film_showw))
                        {

                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("-----------------------------------------------------------------");
                            ConsoleCommands.Textkleur("groen");

                            FilmObject.Films(Chosen_film, Show_films);

                            string Chosen_date = " ";
                            ConsoleCommands.Textkleur("groen");


                            Console.WriteLine("Voer uw gewenste dag in (Bijvoorbeeld: Maandag)");
                            Chosen_date = Console.ReadLine();
                            if (Chosen_date.Length > 10 | Chosen_date.Length < 5)
                            {
                                Console.WriteLine("Ongeldige datum.");
                            }
                            else { Klant.ReserveerCodeMail(); }

                        }
                    }


                }
                else if (Start_options == "2")
                {
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Naar welke film bent u opzoek: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    var Film_search = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    for (int i = 0; i < 5; i++)
                    {
                        string Film_zoeken = (string)DynamicFilmData["Films"][i]["film"];
                        if (Film_search == Film_zoeken)
                        {
                            ConsoleCommands.Textkleur("groen");
                            Console.WriteLine("U heeft gezocht de volgende film:");
                            FilmObject.Film_check(DynamicFilmData, i);
                        }
                    }

                }
                else if (Start_options == "3")
                {
                    ConsoleCommands.Textkleur("groen");
                    Console.Write("Voer hier uw reserverings code in:");
                    ConsoleCommands.Textkleur("blauw");
                    var Reservatie_code = Console.ReadLine();
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");


                    ConsoleCommands.Textkleur("groen");
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
                    ConsoleCommands.Textkleur("groen");
                    CommandLine.RestartOption();


                }
                else if (Start_options == "4")
                {
                    bool adminConsoleChosen = true;
                Klant.AdminConsole(adminConsoleChosen);
                }
                else if (Start_options == "5")
                {
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("rood");
                Console.WriteLine("***** A C C O U N T       C R E A T I O N *****");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Voer uw gewenste gebruikersnaam in: ");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("blauw");
                string KlantNaamInput = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("rood");
                Console.WriteLine("***** A C C O U N T       C R E A T I O N *****");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Voer uw email adres in: ");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("blauw");
                string KlantEmailInput = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("rood");
                Console.WriteLine("***** A C C O U N T       C R E A T I O N *****");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Voer uw wachtwoord in: ");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("blauw");
                string KlantPassInput = Console.ReadLine();
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("rood");
                Console.WriteLine("***** A C C O U N T       C R E A T I O N *****");
                ConsoleCommands.Textkleur("groen");
                Console.WriteLine("Voer uw wachtwoord nogmaals in: ");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("-----------------------------------------------------------------");
                ConsoleCommands.Textkleur("blauw");
                string KlantPassReInput = Console.ReadLine();
                if (KlantPassInput == KlantPassReInput)
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("rood");
                    Console.WriteLine("***** A C C O U N T       C R E A T I O N *****");
                    ConsoleCommands.Textkleur("rood");
                    Console.WriteLine("         ***** C O M P L E T E *****  ");
                    Gebruiker KlantObject = new Gebruiker(KlantNaamInput, KlantEmailInput, KlantPassInput);
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Uw gegevens: \n" + KlantNaamInput + "\n" + KlantEmailInput);
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    KlantObject.AccountCreate(KlantObject);
                }
                else
                {
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("rood");
                    Console.WriteLine("***** A C C O U N T       C R E A T I O N *****");
                    ConsoleCommands.Textkleur("groen");
                    Console.WriteLine("Dat is incorrect! Probeer het nogmaals: ");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("-----------------------------------------------------------------");
                    ConsoleCommands.Textkleur("blauw");
                    KlantPassReInput = Console.ReadLine();
                    if (KlantPassInput == KlantPassReInput)
                    {
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        ConsoleCommands.Textkleur("rood");
                        Console.WriteLine("***** A C C O U N T       C R E A T I O N *****");
                        ConsoleCommands.Textkleur("rood");
                        Console.WriteLine("    ***** C O M P L E T E *****  ");
                        Gebruiker KlantObject = new Gebruiker(KlantNaamInput, KlantEmailInput, KlantPassInput);
                        ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("Uw gegevens: \n" + KlantNaamInput + "\n" + KlantEmailInput);
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        ConsoleCommands.Textkleur("blauw");
                        KlantObject.AccountCreate(KlantObject);
                    }
                    else
                    {
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        ConsoleCommands.Textkleur("rood");
                        Console.WriteLine("***** A C C O U N T       C R E A T I O N *****");
                        ConsoleCommands.Textkleur("groen");
                        Console.WriteLine("Dat is incorrect! De console wordt nu afgesloten. ");
                        ConsoleCommands.Textkleur("wit");
                        Console.WriteLine("-----------------------------------------------------------------");
                        CommandLine.RestartOption();
                    }
                }
            }   





        }
    }
}