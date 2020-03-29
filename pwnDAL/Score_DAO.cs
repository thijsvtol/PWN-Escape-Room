using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pwnModel;
using MySql.Data.MySqlClient;


namespace pwnDAL
{
    public class Score_DAO : Base
    {
        //Stan
        public void AddScore(Score score)
        {
            string query = "INSERT INTO Scores(teamNaam,speelDatum, speelTijd, strafTijd, totaalTijd) VALUES('" + score.TeamNaam + "','" + score.Speeldatum + "','" + score.Speeltijd + "','" + score.Straftijd + "','" + score.Tijdover + "');";
            MySqlParameter[] sqlParameters = new MySqlParameter[0];
            OpenConnection();
            ExecuteEditQuery(query, sqlParameters);
        }

        public List<Score> GetScore()
        {
            string query = "SELECT * FROM Scores";
            MySqlParameter[] sqlParameters = new MySqlParameter[0];
            List<Score> scores = ReadTables(ExecuteSelectQuery(query, sqlParameters));
            return scores;
        }

        private List<Score> ReadTables(DataTable dataTable)
        {
            List<Score> score = new List<Score>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Score scores = new Score()
                {
                    ScoreID = (int)dr["scoreID"],
                    TeamNaam = (string)dr["teamNaam"],
                    Speeldatum = (DateTime)dr["speelDatum"],
                    Speeltijd = (TimeSpan)dr["speelTijd"],
                    Straftijd = (TimeSpan)dr["strafTijd"],
                    Tijdover = (TimeSpan)dr["totaalTijd"]
                };
                score.Add(scores);
            }
            return score;
        }
    }
}
