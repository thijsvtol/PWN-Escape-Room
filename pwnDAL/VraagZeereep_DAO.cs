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
    public class VraagZeereep_DAO : Base
    {
        //Stan
        public List<VragenZeereep> GetQuestion()
        {
            string query = "SELECT * FROM VragenZeeReep WHERE vraagIsEnabled = 1;";
            MySqlParameter[] sqlParameters = new MySqlParameter[0];
            List<VragenZeereep> vragen = ReadTables(ExecuteSelectQuery(query, sqlParameters));
            return vragen;
        }

        private List<VragenZeereep> ReadTables(DataTable dataTable)
        {
            List<VragenZeereep> vragen = new List<VragenZeereep>();

            foreach (DataRow dr in dataTable.Rows)
            {
                VragenZeereep vraag = new VragenZeereep()
                {
                    VraagID = (int)dr["vraagID"],
                    Vraag = (String)dr["vraag"],
                    AntwoordOpVraag = (String)dr["antwoord"],
                    VraagIsEnabled = (bool)dr["vraagIsEnabled"]
            };
                vragen.Add(vraag);
            }
            return vragen;
        }

        public void GetNewQuestion(int vraagID)
        {
            //set all vragen to 0
            string querySetTo0 = "UPDATE VragenZeeReep SET vraagIsEnabled = 0";
            MySqlParameter[] sqlParameters = new MySqlParameter[0];
            //OpenConnection();
            ExecuteEditQuery(querySetTo0, sqlParameters);

            //set the instelling u use to 1
            MySqlParameter[] sqlParameters1 = new MySqlParameter[0];
            string querySetTo1 = "UPDATE VragenZeeReep SET vraagIsEnabled = 1 WHERE vraagID = " + vraagID;
            ExecuteEditQuery(querySetTo1, sqlParameters1);
        }
    }
}
