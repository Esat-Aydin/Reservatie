using System;
using System.Threading;
using MailKit.Net.Smtp;
using MimeKit;
namespace Reservering_bevestiging
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bedankt voor het reserveren!");
            Console.WriteLine("Een ogenblik geduld alstublieft uw reservatie code wordt geladen.");
            Thread.Sleep(3000);
            // Random generator voor het maken van de reservatie code.
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            
            var finalString = new String(stringChars);

            Console.WriteLine("reservatie code:" + finalString);
            Console.WriteLine("Zou je een bevestiging in je mail willen ontvangen?");
            Console.WriteLine("Toets 'JA' als je een bevestinging wil ontvangen toets 'NEE' als je geen bevestiging per mail wil ontvangen.");
            // Email bevestiging.
            string Mail_Bevestiging = Console.ReadLine();
           
            if (Mail_Bevestiging == "JA");
            {
                try
                {
                    var message = new MimeMessage();
                    // Email verzender
                    message.From.Add(new MailboxAddress("Wouter van Krugten", "wouterschiedam98@gmail.com"));
                    // Email geadresseerde
                    message.To.Add(new MailboxAddress("cor", "g.riedijk@gmail.com"));
                    // Email onderwerp
                    message.Subject = "Bevestiging online reservatie.";
                    // Email text
                    message.Body = new TextPart("plain")
                    {
                        Text = @"hallo,
Bedankt voor het reserveren via onze bioscoop applicatie.
Hieronder vind je de reservatie code.
Reservatie code: " + finalString
                    };


                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate("Wouterschiedam98@gmail.com", "Feyenoord1");

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
            if (Mail_Bevestiging == "NEE")
            {
                    Console.WriteLine("U heeft gekozen om geen bevestiging in de mail te ontvangen.");
                    Console.WriteLine("Bedankt voor het online reserveren en we zien u graag bij onze bioscoop.");
            



            }

            
        }
    }
}
