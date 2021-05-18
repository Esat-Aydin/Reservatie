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
            var SnackDataJson = File.ReadAllText(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\snacksdrinks.json");
            var SnackObjectJson = JsonConvert.DeserializeObject<SnackType>(SnackDataJson);
            if (isSnack)
                SnackObjectJson.Snacks.Add(SnackObject);
            else
                SnackObjectJson.Drinks.Add(SnackObject);
            SnackDataJson = JsonConvert.SerializeObject(SnackObjectJson);
            File.WriteAllText(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\snacksdrinks.json", SnackDataJson); 
        }
        public bool SnacksCheck(string SnackName)
        {
            string myJsonString = new WebClient().DownloadString(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\snacksdrinks.json");
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
                Console.WriteLine($"Wilt u {SnackName} toevoegen [1]JA of [2]NEE");
                string user_input = Console.ReadLine();
                if (user_input == "1")
                {
                    AddSnack(SnackObject, true);
                    Console.WriteLine($"Snack {SnackName} is succesvol toegevoegd");
                }
            }
        }

        public void SnacksRemove(string SnackName)
        {
            string myJsonString = new WebClient().DownloadString(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\snacksdrinks.json");
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
                    File.WriteAllText(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\snacksdrinks.json", UserData);
                    Console.WriteLine($"De {SnackName} is succesvol verwijderd");

                }

            }

        }

        public bool DrankenCheck(string SnackName)
        {
            string myJsonString = new WebClient().DownloadString(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\snacksdrinks.json");
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
                Console.WriteLine($"Wilt u {SnackName} toevoegen [1]JA of [2]NEE");
                string user_input = Console.ReadLine();
                if (user_input == "1")
                {
                    AddSnack(SnackObject, false);
                    Console.WriteLine($"Snack {SnackName} is succesvol toegevoegd");
                }
            }
        }

        public void DrankenRemove(string SnackName)
        {
            string myJsonString = new WebClient().DownloadString(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\snacksdrinks.json");
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
                    File.WriteAllText(@"C:\Users\esat6\source\repos\Reservatie\Reservatie\snacksdrinks.json", UserData);
                    Console.WriteLine($"De {SnackName} is verwijderd");

                }

            }

        }
    }
}
