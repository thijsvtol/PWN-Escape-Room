using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pwnDAL;
using pwnModel;

namespace pwnLogic
{
    public class Team_Service
    {
        //Thijs
        public void setTeam(Team t)
        {
            Teams_DAO teams_DAO = new Teams_DAO();
            teams_DAO.AddTeam(t);
        }
    }
}
