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

    public class JsonData
    {

        public string Naam { get; set; }
        public string Email { get; set; }
        public string Reservatie_code { get; set; }
        public string Film { get; set; }
        public string Zaal { get; set; }
        public string Stoel_num { get; set; }


    }
    //public class Gebruiker : Program // MOET NOG GEMAAKT WORDEN
    //{
    //public string Admin_Level = "Gebruiker";
    //}
    public class Medewerker : Program // MOET NOG GEMAAKT WORDEN
    {
        public string Name;
        public string Admin_Password;
        
        public Medewerker(string name, string AdminPass)
        {
            this.Name = name;
            this.Admin_Password = AdminPass;
        }

    }
    public class Program
    {

        public static void Main(string[] args)
        {

            // Inladen Json Module 
            var MyFilmsData = new WebClient().DownloadString("https://stud.hosted.hr.nl/1010746/Filmsdata.json");
            string myJsonString = new WebClient().DownloadString("https://stud.hosted.hr.nl/1010746/snacksdrinks.json");
            string myUserData = new WebClient().DownloadString("https://stud.hosted.hr.nl/1010746/Samplelog.json");

            // Omzetten
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            dynamic DynamicUserData = JsonConvert.DeserializeObject(myUserData);
            dynamic DynamicFilmData = JsonConvert.DeserializeObject(MyFilmsData);

            // Startpagina applicatie
            Console.WriteLine("Welkom op de startpagina van de bioscoop.");
            Console.WriteLine("Selecteer '1' om te zoeken op genre.");
            Console.WriteLine("Selecteer '2' om te zoeken op een specifieke film.");
            Console.WriteLine("Selecteer '3' om uw reservering te bekijken.");
            Console.WriteLine("Selecteer '4' om in te loggen als Bioscoop Medewerker.");
            var Start_options = Console.ReadLine();
            var UserInput = "To be declared";
            bool isAdmin = false;
            Medewerker admin = new Medewerker("admin", "admin");

            if (Start_options == "1")
            {
                List<string> Show_films = new List<string>();

                Console.WriteLine("Op welke genre wilt u zoeken: ");
                Textkleur("groen");
                Console.WriteLine("Toets [1] voor Action.\nToets [2] voor Comedy.\nToets [3] voor Thriller.\nToets [4] voor Romantiek.\nToets [5] voor Drama.\nToets [6] voor Sci-Fi.\nToets [7] voor Familie films. ");
                var Genre_search = Console.ReadLine();
                Console.WriteLine("We hebben deze film(s) gevonden onder het genre: ");
                for (int i = 0; i < DynamicFilmData["Films"].Count; i++)
                {

                    for (int j = 0; j < DynamicFilmData["Films"][i]["genre"].Count; j++)
                    {
                        string Genre_zoeken = (string)DynamicFilmData["Films"][i]["genre"][j];
                        if (Genre_search == Genre_zoeken)
                        {
                            Genre_check(DynamicFilmData, i);
                            Show_films.Add(DynamicFilmData["Films"][i]["film"].ToString());
                        }
                    }
                }
                Console.WriteLine("Voor welke van de bovenstaande films zou u willen reserveren?");
                string Chosen_film = Console.ReadLine();
                for (int i = 0; i < Show_films.Count; i++)
                {
                    if (Chosen_film == Show_films.ElementAt(i))
                    {
                        Console.WriteLine("U heeft gekozen voor de film:" + Chosen_film);
                        string Chosen_date = " ";
                        Console.WriteLine("Type uw gewenste datum in, om uw gekozen film te bekijken in onze bioscoop.\n dd/mm/jjjj");
                        Chosen_date = Console.ReadLine();
                        if (Chosen_date.Length != 10)
                        {
                            Console.WriteLine("Ongeldige datum gebruik dit patroon\n dd/mm/jjjj \n om uw datum in te voeren.");
                        }

                    }
                }


            }
            else if (Start_options == "2")
            {
                Console.Write("Naar welke film bent u opzoek: ");
                var Film_search = Console.ReadLine();
                for (int i = 0; i < 5; i++)
                {
                    string Film_zoeken = (string)DynamicFilmData["Films"][i]["film"];
                    if (Film_search == Film_zoeken)
                    {
                        Console.WriteLine("U heeft gezocht naar de volgende film:");
                        Film_check(DynamicFilmData, i);
                    }
                }

            }
            else if (Start_options == "3")
            {
                Console.Write("Voer hier uw reservatie code in:");
                var Reservatie_code = Console.ReadLine();
                for (int i = 0; i < 3; i++)
                {
                    string Res_code = (string)DynamicUserData[i]["Reservatie_code"];
                    if (Res_code == Reservatie_code)
                    {
                        Console.WriteLine("Uw reservering:");
                        Reservering_check(DynamicUserData, i);
                        SnacksOption();
                        break;
                    }
                }

                Console.WriteLine("Toets 'R' om het progamma opnieuw op te starten.");
                string restart = Console.ReadLine();
                if (restart.ToUpper() == "R")
                {
                    Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                    Environment.Exit(1);
                }


            }
            else if (Start_options == "4")
            {

                Console.WriteLine("Voer hier uw naam in:");
                string input_name = Console.ReadLine();
                Console.WriteLine("Welkom, " + input_name + ". Voer nu het ingestelde admin wachtwoord in: ");
                admin.Name = input_name;
                string input_password = Console.ReadLine();
                if (input_password == admin.Admin_Password)
                {
                    isAdmin = true;
                    Console.WriteLine("U bent succesvol ingelogd als medewerker! Type !help voor een lijst aan commands.");
                    UserInput = Console.ReadLine();
                    UserInputMethod(UserInput);

                }
                else
                {
                    Console.WriteLine("Het ingevoerde wachtwoord is incorrect, probeer het nogmaals: ");
                    UserInput = Console.ReadLine();
                    if (input_password == admin.Admin_Password)
                    {
                        isAdmin = true;
                        Console.WriteLine("U bent succesvol ingelogd als medewerker! Type !help voor een lijst aan commands.");
                        UserInput = Console.ReadLine();
                        UserInputMethod(UserInput);

                    }
                    else
                    {
                        Console.WriteLine("Dat is incorrect, het programma wordt nu voor u gesloten.");
                        return; // Dit sluit het programma af na twee verkeerde password inputs.
                    }
                }
            }
            UserInput = Console.ReadLine();
            UserInputMethod(UserInput);
            UserInput = Console.ReadLine();
            UserInputMethod(UserInput);


            void UserInputMethod(string UserInput) 
            {
                if (isAdmin == true && (UserInput == "!password"))
                {
                    Console.WriteLine("Type nu het huidige admin wachtwoord in: ");
                    UserInput = Console.ReadLine();
                    if (UserInput == admin.Admin_Password)
                    {
                        Console.WriteLine("Type nu het nieuwe wachtwoord in: ");
                        UserInput = Console.ReadLine();
                        admin.Admin_Password = UserInput;
                        Console.WriteLine("Het wachtwoord is succesvol veranderd naar: " + admin.Admin_Password);
                    }
                }
                if (isAdmin == true && (UserInput == "!help"))
                {
                    Console.WriteLine("Om het admin-wachtwoord opnieuw in te stellen type: !password");
                }

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
            
                
                


                // informatie voor eventueel mailen reservatie code.
                Console.WriteLine("Om te kunnen reserveren hebben wij uw naam en emailadres van u nodig.");
                Console.Write("Naam: ");
                string Naam_klant = Console.ReadLine();
                Console.Write("Email adress: ");
                string Naam_email = Console.ReadLine();
                // Eventuele betaal methode?


                // Einde reserveren.
                Console.WriteLine("Bedankt voor het reserveren!");
                Console.WriteLine("Een ogenblik geduld alstublieft uw reservatie code wordt geladen.");
                Thread.Sleep(3000);
                // Random generator voor het maken van de reservatie code.
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var stringChars = new char[16];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var Reservatiecode = new String(stringChars);


                Console.WriteLine("reservatie code:" + Reservatiecode);
                Console.WriteLine("Zou je een bevestiging in je mail willen ontvangen?");
                Console.WriteLine("Toets 'JA' als je een bevestinging wil ontvangen toets 'NEE' als je geen bevestiging per mail wil ontvangen.");
                // Email bevestiging.
                string Mail_Bevestiging = Console.ReadLine();

                if (Mail_Bevestiging == "JA")
                {

                    try
                    {
                        var message = new MimeMessage();
                        // Email verzender
                        message.From.Add(new MailboxAddress("ProjectB", "ProjectB1J@gmail.com"));
                        // Email geadresseerde
                        message.To.Add(new MailboxAddress(Naam_klant, Naam_email));
                        // Email onderwerp
                        message.Subject = "Bevestiging online reservatie.";
                        // Email text
                        message.Body = new TextPart("plain")
                        {
                            Text = @"Hallo,
Bedankt voor het reserveren via onze bioscoop.
Hieronder vind je de reservatie code.
Reservatie code: " + Reservatiecode

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
                else if (Mail_Bevestiging == "NEE")
                {
                    Console.WriteLine("U heeft gekozen om geen bevestiging in de mail te ontvangen.");
                }
                else
                {
                    Console.WriteLine("U heeft gekozen om geen bevestiging in de mail te ontvangen.");
                }
                Console.WriteLine("Bedankt voor het online reserveren en we zien u graag bij onze bioscoop.");
                // Data Reservering toevoegen.
                List<JsonData> _data = new List<JsonData>();
                var DataUser = File.ReadAllText(@"C:\Users\woute\SampleLog.json");
                var JsonData = JsonConvert.DeserializeObject<List<JsonData>>(DataUser)
                          ?? new List<JsonData>();

                JsonData.Add(new JsonData()
                {
                    Reservatie_code = Reservatiecode,
                    Naam = Naam_klant,
                    Email = Naam_email,
                    //Film =
                    //Zaal =
                    //Stoel_num =

                });

                DataUser = JsonConvert.SerializeObject(JsonData);
                File.WriteAllText(@"C:\Users\woute\SampleLog.json", DataUser);

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
