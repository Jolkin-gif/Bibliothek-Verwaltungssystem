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
                    
                    case "Grün":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    
                    case "Gelb":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    
                    case "Rot":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    
                    default:
                        Console.ResetColor();
                        break;
                }
                Console.WriteLine(status);
                Console.ResetColor();

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
            Anzeigen();
            Console.WriteLine("Welches Buch soll gelöscht werden?: ");
            
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
    }
} 
