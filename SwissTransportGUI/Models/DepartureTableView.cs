using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissTransportGUI.Models
{
    public class DepartureTableView
    {
        public DepartureTableView(string  departureStaion, string time, string departurePlatform) {
            Richtung = departureStaion;
            DatumZeit = time;
            Linie = departurePlatform;
        }

        public string Richtung { get; set; } = string.Empty;
        public string DatumZeit { get; set; } = string.Empty;
        public string Linie { get; set; } = string.Empty;
    }
}
