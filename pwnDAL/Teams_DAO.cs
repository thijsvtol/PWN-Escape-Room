using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using pwnModel;

namespace pwnDAL
{
    public class Teams_DAO : Base
    {
        //Thijs
        public void AddTeam(Team team)
        {
            string query = "INSERT INTO Teams (teamNaam) VALUES('" + team.TeamNaam + "');";
            MySqlParameter[] sqlParameters = new MySqlParameter[0];
            OpenConnection();
            ExecuteEditQuery(query, sqlParameters);
        }
    }
}
