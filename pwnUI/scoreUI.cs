using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pwnLogic;
using pwnModel;

namespace pwnUI
{
    public partial class scoreUI : Form
    {
        public scoreUI()
        {
            InitializeComponent();
        }


        private void scoreUI_Load(object sender, EventArgs e)
        {
            // clear the datagridview before filling it again
            dataGridViewScore.Rows.Clear();

            // fill the datagridview
            Score_Service score_Service = new Score_Service();
            List<Score> scoreLijst = score_Service.GetScore();

            foreach (Score r in scoreLijst)
            {
                dataGridViewScore.Rows.Add(r.TeamNaam, r.Speeltijd, r.Straftijd, r.Tijdover);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            pwnUI pwnUI = new pwnUI();
            pwnUI.Show();
        }
    }
}
