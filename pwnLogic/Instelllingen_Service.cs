using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pwnDAL;
using pwnModel;

namespace pwnLogic
{
    public class Instellingen_Service
    {
        //Thijs
        Instellingen_DAO instellingen_DAO = new Instellingen_DAO();

        public List<Instellingen> instellingen()
        {
            try
            {
                List<Instellingen> instelling = instellingen_DAO.GetSettings();
                return instelling;
            }
            catch
            {
                List<Instellingen> instelling = new List<Instellingen>();
                Instellingen i = new Instellingen();
                i.InstellingID = 1;
                i.PingpongbalSV = 395;
                i.VragenSV = 1;
                i.Getal1FF = 44;
                i.Getal2FF = 13;
                i.RebusRT = 2;
                i.OmzettingstabelW = 1;
                i.DokterbibberG = 12;
                i.Eindcode = 469172;
                i.IsEnabled = true;
                return instelling;
            }
        }

        public void SetInstellingen(int instellingID)
        {
            Instellingen_DAO instellingen_DAO = new Instellingen_DAO();
            instellingen_DAO.UpdateInstellingen(instellingID);   
        }
    }
}

