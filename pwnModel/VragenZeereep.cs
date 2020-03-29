using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace pwnModel
{
    public class VragenZeereep
    {
        //Stan
        public int VraagID { get; set; } //VraagID
        public String Vraag { get; set; } //Vraag
        public String AntwoordOpVraag { get; set; } //Antwoord op de vraag
        public bool VraagIsEnabled { get; set; } //Vraag is enabled
    }
}
