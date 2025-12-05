using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bibliothek_Verwaltungssystem
{
    public class Bibliothek
    {
        public List<Buch> Bücher = new List<Buch>();

        public void Laden()
        {
            if (System.IO.File.Exists("bücher.json"))
            {
                string json = System.IO.File.ReadAllText("bücher.json");
                Bücher = JsonConvert.DeserializeObject<List<Buch>>(json);
            }
        }

        public void Speichern()
        {
            string json = JsonConvert.SerializeObject(Bücher, Formatting.Indented);
            System.IO.File.WriteAllText("bücher.json", json);
        }

        public DateTime? Fälligkeitsdatum(Buch buch)
        {
            if (!buch.Ausleihdatum.HasValue)
                return null;

            int tage = buch.Verlängert ? 44 : 30;
            return buch.Ausleihdatum.Value.AddDays(tage);
        }
        public void Anzeigen()
        {
            Console.WriteLine("---Alle Bücher---");

            foreach (Buch x in Bücher)
            {
                string status = x.StatusFarbe();

                Console.WriteLine(x.Titel + " von " + x.Autor + " | Status: ");



                switch (status)
                {
                    case "VERFÜGBAR":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;

                    case "VERLOREN":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;

                    case "AUSGELIEHEN":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;

                    case "VERLÄNGERT":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;

                    case "ÜBERZOGEN":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;

                    default:
                        Console.ResetColor();
                        break;
                }
                Console.WriteLine(status);
                Console.ResetColor();

                if (x.Status == "Ausgeliehen" && x.Ausleihdatum.HasValue)
                {
                    Console.WriteLine($" | Ausgeliehen am: {x.Ausleihdatum.Value:dd.MM.yyyy}");

                    DateTime? fällig = Fälligkeitsdatum(x);
                    if (fällig.HasValue)
                    {
                        Console.WriteLine($" | Fälligkeitsdatum: {fällig.Value:dd.MM.yyyy}");
                    }
                }
            }
        }

        public void Hinzufügen()
        {
            Console.WriteLine("Titel: ");
            string titel = Console.ReadLine();

            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();


            Bücher.Add(new Buch
            {
                Titel = titel,
                Autor = autor,
                Status = "Verfügbar",
                Ausleiher = null,
                Ausleihdatum = null,
                Verlängert = false
            });
            Speichern();

            Console.WriteLine("Das Buch wurde erfolgreich hinzugefügt!");
        }

        public void Entfernen()
        {
            Console.WriteLine("Welches Buch soll gelöscht werden?: ");
            Anzeigen();

            string titel = Console.ReadLine();

            Buch buch = Bücher.FirstOrDefault(x => x.Titel == titel);

            if (buch != null)
            {
                Bücher.Remove(buch);
                Console.WriteLine("Das Buch wurde erfolgreich entfernt!");
                Speichern();
            }
            else
            {
                Console.WriteLine("Das Buch existiert nicht!");
                return;
            }
        }

        public void Verloren()
        {
            Console.WriteLine("Welches Buch soll als verloren markiert werden?: ");
            Anzeigen();
            string titel = Console.ReadLine();

            Buch buch = Bücher.FirstOrDefault(x => x.Titel == titel);

            if (buch == null)
            {
                Console.WriteLine("Das Buch existiert nicht!");
                return;
            }

            buch.Status = "Verloren";
            Speichern();
            Console.WriteLine("Das Buch wurde erfolgreich als verloren markiert!");

        }
        public void Ausleihen()
        {
            Console.WriteLine("Welches Buch soll ausgeliehen werden?:");
            Anzeigen();

            string titel = Console.ReadLine();

            Buch buch = Bücher.FirstOrDefault(x => x.Titel == titel);

            if (buch == null || buch.Status != "Verfügbar")
            {
                Console.WriteLine("Das Buch ist nicht verfügbar!");
                return;
            }

            Console.WriteLine("Name des Ausleihers: ");
            string ausleiher = Console.ReadLine();

            buch.Status = "Ausgeliehen";
            buch.Ausleiher = ausleiher;
            buch.Ausleihdatum = DateTime.Now;
            buch.Verlängert = false;

            Speichern();
            Console.WriteLine("Das Buch wurde erfolgreich ausgeliehen!");


        }

        public void Verlängern()
        {
            Console.WriteLine("Welches ausgeliehene Buch soll verlängert werden?:");
            Anzeigen();

            string titel = Console.ReadLine();
            Buch buch = Bücher.FirstOrDefault(x => x.Titel == titel);

            if (buch == null || buch.Status != "Ausgeliehen")
            {
                Console.WriteLine("Das Buch ist nicht ausgeliehen!");
                return;
            }

            if (buch.Verlängert)
            {
                Console.WriteLine("Das Buch wurde bereits verlängert!");
                return;
            }

            if (Fälligkeitsdatum(buch).HasValue && Fälligkeitsdatum(buch).Value < DateTime.Now)
            {
                Console.WriteLine("Das Buch ist bereits überzogen und kann nicht verlängert werden!");
                return;
            }


            buch.Verlängert = true;

            Speichern();
            Console.WriteLine("Das Buch wurde erfolgreich verlängert! (14 Tage)");


        }

        public void Rückgabe()
        {
            Console.WriteLine("Welches Buch soll zurückgegeben werden?:");
            Anzeigen();
            string titel = Console.ReadLine();
            Buch buch = Bücher.FirstOrDefault(x => x.Titel == titel);
            if (buch == null || buch.Status != "Ausgeliehen")
            {
                Console.WriteLine("Das Buch ist nicht ausgeliehen!");
                return;
            }
            buch.Status = "Verfügbar";
            buch.Ausleiher = null;
            buch.Ausleihdatum = null;
            buch.Verlängert = false;
            Speichern();
            Console.WriteLine("Das Buch wurde erfolgreich zurückgegeben!");




        }
    }
} 
