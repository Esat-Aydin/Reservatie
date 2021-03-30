using System;
using System.Linq;
using System.Threading;
namespace Reservatie
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bedankt voor het reserveren!");
            Console.WriteLine("Een ogenblik geduld alstublieft uw reservatie code wordt geladen.");
            Thread.Sleep(1000);
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

            Console.WriteLine("Dit is uw reservatie code, neem deze mee wanneer u naar de film komt.");
        }
    }
}
