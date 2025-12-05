using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek_Verwaltungssystem
{
    public class Buch
    {
        public string Titel { get; set; }
        public string Autor { get; set; }
        public string Status { get; set; }
        public string Ausleiher { get; set; }
        public DateTime? Ausleihdatum { get; set; }
        public bool Verlängert { get; set; } = false;

        public Buch()
        {
            Status = "Verfügbar";
            Verlängert = false;
        }
        public string StatusFarbe()
        {
            if (Status == "Verloren")
                return "VERLOREN";

            if (Status == "Verfügbar")
                return "VERFÜGBAR";

            if (!Ausleihdatum.HasValue)
                return "FEHLER";

            TimeSpan differenz = DateTime.Now - Ausleihdatum.Value;

            if (Verlängert)
            {
                if (differenz.TotalDays <= 44)
                    return "VERLÄNGERT";

                else
                    return "ÜBERFÄLLIG";
            }
            
            if (differenz.TotalDays <= 30)
                return "AUSGELIEHEN";
            else
                return "ÜBERFÄLLIG";




        }
    }
}
