using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pwnDAL;
using pwnModel;

namespace pwnLogic
{
    public class VragenZeereep_Service
    {
        //Stan
        VraagZeereep_DAO vraagZeereep = new VraagZeereep_DAO();

        public List<VragenZeereep> GetVraag()
        {
            List<VragenZeereep> vragen = vraagZeereep.GetQuestion();
            return vragen;
        }

        public void GetNewQuestion(int vraagID)
        {
            vraagZeereep.GetNewQuestion(vraagID);
        }
    }
}
