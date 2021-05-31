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
using SnackClass;

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


/*        public void AdminConsole(bool adminConsoleChosen)
        {

            {
                Scherm.Screens.CinemaBanner();
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("wit");
                Console.WriteLine("Voer uw admin gebruikersnaam in:");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                ConsoleCommands.Textkleur("zwart");
                string input_name = Console.ReadLine();
                string adminInputName = input_name;
                Console.Clear();
                AccountCheck(adminInputName);

            }
        }*/
      /*  public void AccountCheck(string Naam)
        {
            ConsoleCommands CommandLine = new ConsoleCommands();
            bool ReturnValue = false;
            
            var AccountUsers = new WebClient().DownloadString(@".\AccountUsers.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path
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

                if (StoredName == Naam)
                {
                    ReturnValue = true;
                    Scherm.Screens.CinemaBanner();
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("wit");
                    Console.WriteLine("Welkom, " + Naam + ". Voer nu het ingestelde admin wachtwoord in: ");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    ConsoleCommands.Textkleur("zwart");
                    string input_password = Console.ReadLine();
                    if (input_password == ListofAccountsPasswords[i])
                    {
                        if (ListofAccountsisAdmin[i] == "True")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("wit");
                            Console.WriteLine("U bent succesvol ingelogd als medewerker! Type help voor een lijst aan commands.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart");
                            Gebruiker.Gebruiker adminObject = new Gebruiker.Gebruiker(Naam, ListofAccountsEmails[i], input_password, ReturnValue);
                            CommandLine.UserInput = Console.ReadLine();
                            adminObject.UserInputMethod(CommandLine.UserInput);
                        }
                    }

                    else
                    {
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        Console.WriteLine("Het ingevoerde wachtwoord is incorrect, probeer het nogmaals: ");
                        ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                        ConsoleCommands.Textkleur("zwart");
                        CommandLine.UserInput = Console.ReadLine();

                        if (CommandLine.UserInput == ListofAccountsPasswords[i] && ListofAccountsisAdmin[i] == "True")
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("wit");
                            Gebruiker.Gebruiker adminObject = new Gebruiker.Gebruiker(Naam, ListofAccountsEmails[i], input_password, ReturnValue);
                            Console.WriteLine("U bent succesvol ingelogd als medewerker! Type !help voor een lijst aan commands.");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart");
                            CommandLine.UserInput = Console.ReadLine();
                            adminObject.UserInputMethod(CommandLine.UserInput);
                        }
                        else
                        {
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            Console.WriteLine("Het ingevoerde wachtwoord is incorrect, het programma wordt nu voor u afgesloten. ");
                            ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                            ConsoleCommands.Textkleur("zwart");
                            CommandLine.RestartOption();
                            CommandLine.UserInput = Console.ReadLine();
                        }
                    }
                }
            }
            if (ReturnValue == false)
            {
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.WriteLine("We hebben geen account kunnen vinden met deze naam: " + Naam);
                ConsoleCommands.Textkleur("zwart");
                CommandLine.RestartOption(); // Dit sluit het programma af na twee verkeerde password inputs.
                ConsoleCommands.Textkleur("zwart");
                CommandLine.UserInput = Console.ReadLine();
                UserInputMethod(CommandLine.UserInput);
                CommandLine.UserInput = Console.ReadLine();
                UserInputMethod(CommandLine.UserInput);
            }
*/












        //}

    }
}
