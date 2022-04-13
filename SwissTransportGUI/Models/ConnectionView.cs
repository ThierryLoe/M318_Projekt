using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissTransportGUI.Models
{
    public class ConnectionView
    {
        public ConnectionView(string depatureStation, DateTime time, string arrivalStation, string depaturePlatform, string arrivalPlatform)
        {
            Von = depatureStation;
            Zeit = time.ToString("dd.MM.yyyy HH:mm");
            Nach = arrivalStation;
            AbfahrtsGleis = depaturePlatform;
            AnkunftsGleis = arrivalPlatform;
          
        }
        public string Von { get; set; }
        public string Zeit { get; set; }   
        public string Nach { get; set; } 
        public string AbfahrtsGleis { get; set; }
        public string AnkunftsGleis { get; set; }
    }
}
