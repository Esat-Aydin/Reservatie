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
using Gebruiker;

namespace Film
{
    public class Film // Object van deze class wordt toegevoegd aan de Json file die dan aan de gebruiker kan worden getoond (voor het toevoegen/verwijderen/bewerken van films)
    {
        public string[] FilmGenres { get; set; }
        public string FilmTitle { get; set; }
        public int FilmRoom { get; set; }
        public string[] FilmTimes { get; set; }
        public Dictionary<string, List<string>> FilmDays { get; set; }

        public Film(string[] FilmGenres = null, string FilmTitle = null, int FilmRoom = 0, string[] FilmTimes = null, Dictionary<string, List<string>> DictofData = null)
        {
            this.FilmGenres = FilmGenres;
            this.FilmTitle = FilmTitle;
            this.FilmRoom = FilmRoom;
            this.FilmTimes = FilmTimes;
            this.FilmDays = DictofData;
        }
        public void AddFilmtoDataBase(Film FilmObject) // Dit voegt de FilmObject object toe aan de Json file
        {
            List<Film> _data = new();
            var FilmDataJson = File.ReadAllText(@".\AccountUsers.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path
            var FilmObjectJson = JsonConvert.DeserializeObject<List<Film>>(FilmDataJson);
            FilmObjectJson.Add(FilmObject);
            FilmDataJson = JsonConvert.SerializeObject(FilmObjectJson);
            File.WriteAllText(@".\AccountUsers.json", FilmDataJson); // Net als FilmDataJson de path veranderen als je hier errors krijgt!
        }
        public void Film_check(dynamic DynamicFilmData, int i)
        {

            Console.WriteLine(DynamicFilmData[i]["FilmTitle"]);
        }
        public bool Film_check2(string FilmName)
        {

            string myJsonString = new WebClient().DownloadString(@".\Filmsdata.json");
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            for (int i = 0; i < DynamicData.Count; i++)
            {
                string FilmTitleObject = (string)DynamicData[i]["FilmTitle"];
                if (FilmTitleObject == FilmName)
                {
                    return true;
                }
            }
            return false;
        }
        public void Films(string Chosen_film, dynamic Show_films)
        {
            if (Chosen_film == "1")
            {
                Console.Write("U heeft gekozen voor de film: "); ConsoleCommands.Textkleur("rood"); Console.Write(Show_films[0] + "\n");
            }
            else if (Chosen_film == "2")
            {
                Console.Write("U heeft gekozen voor de film: "); ConsoleCommands.Textkleur("rood"); Console.Write(Show_films[1] + "\n");
            }
            else if (Chosen_film == "3")
            {
                Console.Write("U heeft gekozen voor de film: "); ConsoleCommands.Textkleur("rood"); Console.Write(Show_films[2] + "\n");
            }
            else if (Chosen_film == "4")
            {
                Console.Write("U heeft gekozen voor de film: "); ConsoleCommands.Textkleur("rood"); Console.Write(Show_films[3] + "\n");
            }
            else if (Chosen_film == "5")
            {
                Console.Write("U heeft gekozen voor de film: "); ConsoleCommands.Textkleur("rood"); Console.Write(Show_films[4] + "\n");
            }
            else if (Chosen_film == "6")
            {
                Console.Write("U heeft gekozen voor de film: "); ConsoleCommands.Textkleur("rood"); Console.Write(Show_films[5] + "\n");
            }

        }
        public void RemoveFilm(string FilmName)
        {
            string myJsonString = new WebClient().DownloadString(@".\Filmsdata.json");
            dynamic DynamicData = JsonConvert.DeserializeObject(myJsonString);
            int Index = 0;
            for (int i = 0; i < DynamicData.Count; i++)
            {
                string FilmTitleObject = (string)DynamicData[i]["FilmTitle"];
                Console.WriteLine("dasdasd");
                if (FilmTitleObject == FilmName)
                {
                    Index = i;
                    DynamicData.Remove(DynamicData[Index]);
                    dynamic UserData = JsonConvert.SerializeObject(DynamicData);
                    File.WriteAllText(@".\Filmsdata.json", UserData);
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("__\n");
                    Console.Write("\nFilm "); ConsoleCommands.Textkleur("rood"); Console.Write(FilmName); ConsoleCommands.Textkleur("wit"); Console.Write(" is succesvol verwijderd.\n\n");
                    ConsoleCommands.Textkleur("wit"); Console.WriteLine("__\n"); ConsoleCommands.Textkleur("zwart");

                }

            }
        }
    }
}
