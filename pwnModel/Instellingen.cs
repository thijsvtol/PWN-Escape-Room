using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pwnModel
{
    public class Instellingen
    {
        //Thijs
        public int InstellingID { get; set; } //VraagID
        public int PingpongbalSV { get; set; } //Vraag
        public int VragenSV { get; set; } //Antwoord op de vraag
        public int Getal1FF { get; set; } //Vraag is enabled
        public int Getal2FF { get; set; } //Vraag is enabled
        public int OmzettingstabelW { get; set; } //Vraag is enabled
        public int CodeW { get; set; }
        public int RebusRT { get; set; } //Vraag is enabled
        public int DokterbibberG { get; set; } //Vraag is enabled
        public int Eindcode { get; set; } //Vraag is enabled
        public bool IsEnabled { get; set; } //Vraag is enabled
    }
}
