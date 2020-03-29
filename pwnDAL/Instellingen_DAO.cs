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
    public class Instellingen_DAO : Base
    {
        //Thijs
        public List<Instellingen> GetSettings()
        {
            string query = "SELECT * FROM Instellingen;";
            MySqlParameter[] sqlParameters = new MySqlParameter[0];
            List<Instellingen> instellingen = ReadTables(ExecuteSelectQuery(query, sqlParameters));
            return instellingen;
        }

        private List<Instellingen> ReadTables(DataTable dataTable)
        {
            List<Instellingen> instellingen = new List<Instellingen>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Instellingen instelling = new Instellingen()
                {
                    InstellingID = (int)dr["instellingID"],
                    PingpongbalSV = (int)dr["pingpongbalSV"],
                    VragenSV = (int)dr["vragenSV"],
                    Getal1FF = (int)dr["getal1FF"],
                    Getal2FF = (int)dr["getal2FF"],
                    OmzettingstabelW = (int)dr["omzettingstabelW"],
                    CodeW = (int)dr["codeW"],
                    RebusRT = (int)dr["rebusRT"],
                    DokterbibberG = (int)dr["dokterbibberG"],
                    Eindcode = (int)dr["eindcode"],
                    IsEnabled = (bool)dr["isEnabled"]
                };
                instellingen.Add(instelling);
            }
            return instellingen;
        }

        //Stan
        public void UpdateInstellingen(int instellingID)
        {
            //set all instellingen to 0
            string querySetTo0 = "UPDATE Instellingen SET isEnabled = 0";
            MySqlParameter[] sqlParameters = new MySqlParameter[0];
            OpenConnection();
            ExecuteEditQuery(querySetTo0, sqlParameters);

            //set the instelling u use to 1
            string querySetTo1 = "UPDATE Instellingen SET isEnabled = 1 WHERE instellingID = " + instellingID;
            ExecuteEditQuery(querySetTo1, sqlParameters);
        }
    }
}
