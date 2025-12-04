using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bibliothek_Verwaltungssystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Bibliothek methode = new Bibliothek();
            methode.Laden();
            while (true)
            {
                Console.WriteLine("---BIBLIOTHEKS-MANAGER---\n");
                Console.WriteLine("1. Bücher anzeigen");
                Console.WriteLine("2. Buch hinzufügen");
                Console.WriteLine("3. Buch entfernen");
                Console.WriteLine("4. Buch als verloren melden");
                Console.WriteLine("5. Buch ausleihen");
                Console.WriteLine("6. Ausleihe verlängern");
                Console.WriteLine("7. Rückgabe");
                Console.WriteLine("8. Beenden");
                Console.Write("\nWählen Sie eine Option: ");

                string eingabe = Console.ReadLine();

                switch (eingabe)
                {
                    case "1":
                        methode.Anzeigen();
                        break;
                    case "2":
                        methode.Hinzufügen();
                        break;
                    case "3":
                        methode.Entfernen();
                        break;

                }

                Console.WriteLine("\n Drücken Sie ENTER zurm Fortfahren.");
                Console.ReadLine();
                Console.Clear();


            }
        }
    }
}
