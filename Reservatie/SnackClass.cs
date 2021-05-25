using Cinema;
using ConsoleTables;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Net;
using ConsoleTables;
using Cinema;
using Film;
using Scherm;
using Reservation;
using Gebruiker;
using Chair;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace SnackClass
{
    public class SnackType
    {
        [JsonProperty("dranken")]
        public List<Snacks> Drinks { get; set; }
        [JsonProperty("snacks")]
        public List<Snacks> Snacks { get; set; }
    }

    public class Snacks
    {
        public string Name;
        public string Price;
        
        public Snacks(string Name = null, string Price = null)
        {
            this.Name = Name;
            this.Price = Price;
        }

        public void AddSnack(Snacks SnackObject, bool isSnack)
        {
            List<Snacks> _data = new();
            var SnackDataJson = File.ReadAllText(@".\snacksdrinks.json");
            var SnackObjectJson = JsonConvert.DeserializeObject<SnackType>(SnackDataJson);
            if (isSnack)
                SnackObjectJson.Snacks.Add(SnackObject);
            else
                SnackObjectJson.Drinks.Add(SnackObject);
            SnackDataJson = JsonConvert.SerializeObject(SnackObjectJson);
            File.WriteAllText(@".\snacksdrinks.json", SnackDataJson); 
        }
        public bool SnacksCheck(string SnackName)
        {
            string myJsonString = new WebClient().DownloadString(@".\snacksdrinks.json");
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            int Index = 0;
            for (int i = 0; i < DynamicData["snacks"].Count; i++)
            {

                string Snack_code = (string)DynamicData["snacks"][i]["Name"];
                if (Snack_code == SnackName)
                {
                    Index = i;
                    return true;
                   
                }
            }
            return false;
        }
        public void SnacksAdd(Snacks SnackObject, string SnackName)
        {
            if (SnacksCheck(SnackName) == false)
            {
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.Write("\nWeet u zeker dat u "); ConsoleCommands.Textkleur("rood"); Console.Write(SnackName); ConsoleCommands.Textkleur("wit"); Console.Write(" wilt toevoegen? \n\n[");
                ConsoleCommands.Textkleur("zwart"); Console.Write(1); ConsoleCommands.Textkleur("wit"); Console.Write("] Ja\n\n["); ConsoleCommands.Textkleur("zwart"); Console.Write(2);
                ConsoleCommands.Textkleur("wit"); Console.Write("] Nee\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                string user_input = Console.ReadLine();
                if (user_input == "1")
                {
                    AddSnack(SnackObject, true);
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Console.Write("\nSnack "); ConsoleCommands.Textkleur("rood"); Console.Write(SnackName); ConsoleCommands.Textkleur("wit"); Console.Write(" is succesvol toegevoegd.\n\n");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                }
            }
        }

        public void SnacksRemove(string SnackName)
        {
            string myJsonString = new WebClient().DownloadString(@".\snacksdrinks.json");
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            int Index = 0;
            for (int i = 0; i < DynamicData["snacks"].Count; i++)
            {
                string Snack_code = (string)DynamicData["snacks"][i]["Name"];
                if (Snack_code == SnackName)
                {
                    Index = i;
                    DynamicData["snacks"].Remove(DynamicData["snacks"][Index]);
                    dynamic UserData = JsonConvert.SerializeObject(DynamicData);
                    File.WriteAllText(@".\snacksdrinks.json", UserData);
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Console.Write("\nSnack "); ConsoleCommands.Textkleur("rood"); Console.Write(SnackName); ConsoleCommands.Textkleur("wit"); Console.Write(" is succesvol verwijderd.\n\n");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");

                }

            }

        }

        public bool DrankenCheck(string SnackName)
        {
            string myJsonString = new WebClient().DownloadString(@".\snacksdrinks.json");
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            int Index = 0;
            for (int i = 0; i < DynamicData["dranken"].Count; i++)
            {
                string Snack_code = (string)DynamicData["dranken"][i]["Name"];
                if (Snack_code == SnackName)
                {
                    Index = i;
                    return true;

                }

            }
            return false;
        }
        public void DrankenAdd(Snacks SnackObject, string SnackName)
        {
            if (SnacksCheck(SnackName) == false)
            {
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                Console.Write("\nWilt u "); ConsoleCommands.Textkleur("rood");Console.Write(SnackName); ConsoleCommands.Textkleur("wit"); Console.Write(" toevoegen \n\n[");
                ConsoleCommands.Textkleur("zwart");Console.Write(1);ConsoleCommands.Textkleur("wit");Console.Write("] JA\n\n[");ConsoleCommands.Textkleur("zwart");Console.Write(2);
                ConsoleCommands.Textkleur("wit");Console.Write("]NEE\n");
                ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                string user_input = Console.ReadLine();
                if (user_input == "1")
                {
                    AddSnack(SnackObject, false);
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Console.Write("\nDrank "); ConsoleCommands.Textkleur("rood"); Console.Write(SnackName); ConsoleCommands.Textkleur("wit"); Console.Write(" is succesvol toegevoegd.\n\n");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                }
            }
        }

        public void DrankenRemove(string SnackName)
        {
            string myJsonString = new WebClient().DownloadString(@".\snacksdrinks.json");
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            int Index = 0;
            for (int i = 0; i < DynamicData["dranken"].Count; i++)
            {
                string Snack_code = (string)DynamicData["dranken"][i]["Name"];
                if (Snack_code == SnackName)
                {
                    Index = i;
                    DynamicData["dranken"].Remove(DynamicData["dranken"][Index]);
                    dynamic UserData = JsonConvert.SerializeObject(DynamicData);
                    File.WriteAllText(@".\snacksdrinks.json", UserData);
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n");
                    Console.Write("\nDrank "); ConsoleCommands.Textkleur("rood"); Console.Write(SnackName); ConsoleCommands.Textkleur("wit"); Console.Write(" is succesvol verwijderd.\n\n");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("_____________________________________________________________________________________________\n"); ConsoleCommands.Textkleur("zwart");
                }

            }

        }
    }
}
