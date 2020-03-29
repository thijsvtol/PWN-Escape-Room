using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pwnModel
{
    public class Score
    {
        //Stan
        public int ScoreID { get; set; } //scoreID
        public string TeamNaam { get; set; } //teamnaam
        public DateTime Speeldatum { get; set; } //speeldatum
        public TimeSpan Speeltijd { get; set; } //speeltijd
        public TimeSpan Straftijd { get; set; } //straftijd
        public TimeSpan Tijdover { get; set; } //tijd over na spelen van escape room
    }
}
