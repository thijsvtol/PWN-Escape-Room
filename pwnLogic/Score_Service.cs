using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pwnDAL;
using pwnModel;

namespace pwnLogic
{
    public class Score_Service
    {
        //Stan
        Score_DAO Score_DAO = new Score_DAO();

        public List<Score> GetScore()
        {
            List<Score> scores = Score_DAO.GetScore();
            return scores;
        }

        public void setScore(Score s)
        {
            Score_DAO score_DAO = new Score_DAO();
            score_DAO.AddScore(s);
        }
    }
}
