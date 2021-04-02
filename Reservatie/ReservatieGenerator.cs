﻿using System;
using System.IO;
using System.Threading;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cinema
{
    class Data
    {



    }
    class Program
    {
        static void Main(string[] args)
        {
            // Inladen Json Module snacks
            string myJsonString = File.ReadAllText("C:\\Users\\woute\\Downloads\\snacksdrinks.json");
            var myJObject = JObject.Parse(myJsonString);
            //var MyJsonSnacks = JsonConvert.DeserializeObject<Data>(myJsonString);
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);

           
            // Startpagina applicatie
            Console.WriteLine("Welkom op de startpagina van de bioscoop.");
            Console.WriteLine("Selecteer '1' om te zoeken op genre.");
            Console.WriteLine("Selecteer '2' om te zoeken op een specifieke film.");
            Console.WriteLine("Selecteer '3' om uw reservering te bekijken.");
            var Start_options = Console.ReadLine();
            if (Start_options == "1") {
                Console.Write("Op welke genre wilt u zoeken: ");
                var Genre_search = Console.ReadLine();


            }
            else if (Start_options == "2") {
                Console.Write("Naar welke film bent u opzoek: ");
                var Film_search = Console.ReadLine();


            }
            else if (Start_options == "3") {
                Console.Write("Voer hier uw reservatie code in:");
                var Reservatie_code = Console.ReadLine();
                
            }
           
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
                    Console.WriteLine("Dranken:");
                    Console.WriteLine("-------------------------------------------------------------------------------");
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

        }
    }
}
