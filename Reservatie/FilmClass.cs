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
            var FilmDataJson = File.ReadAllText(@"C:\Users\abdel\source\repos\Esat-Aydin\Reservatie\Reservatie\Filmsdata.json"); // even de full path kopieren en hier plakken  ---> in Solution Explorer --> rechter muisknop op FIlmsdata.json --> copy full path
            var FilmObjectJson = JsonConvert.DeserializeObject<List<Film>>(FilmDataJson);
            FilmObjectJson.Add(FilmObject);
            FilmDataJson = JsonConvert.SerializeObject(FilmObjectJson);
            File.WriteAllText(@"C:\Users\abdel\source\repos\Esat-Aydin\Reservatie\Reservatie\Filmsdata.json", FilmDataJson); // Net als FilmDataJson de path veranderen als je hier errors krijgt!
        }
        public void Film_check(dynamic DynamicFilmData, int i)
        {

            Console.WriteLine(DynamicFilmData[i]["FilmTitle"]);
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
}
