using pwnDAL;
using pwnLogic;
using pwnModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pwnUI
{
    public partial class adminUI : Form
    {
        public adminUI()
        {
            InitializeComponent();
        }

        //Stan
        private void btnSaveSetttings_Click(object sender, EventArgs e)
        {
            Instellingen_Service instellingen_Service = new Instellingen_Service();
            if (rbInstelling1.Checked == true)
            {
                instellingen_Service.SetInstellingen(1);
            }
            else if (rbInstelling2.Checked == true)
            {
                instellingen_Service.SetInstellingen(2);
            }
            else if (rbInstelling3.Checked == true)
            {
                instellingen_Service.SetInstellingen(3);
            }
            pwnUI pwnUI = new pwnUI();
            pwnUI.Show();
            this.Close();
        }

        private void adminUI_Load(object sender, EventArgs e)
        {
            //instellingen ophalen
            Instellingen_Service instellingen_Service = new Instellingen_Service();
            List<Instellingen> instelling = instellingen_Service.instellingen();

            foreach (Instellingen instellingen in instelling)
            {
                if (instellingen.InstellingID == 1)
                {
                    tbPingpong1.Text = instellingen.PingpongbalSV.ToString();
                    tbGetal1V1.Text = instellingen.Getal1FF.ToString();
                    tbGetal2V1.Text = instellingen.Getal2FF.ToString();
                    tbWatercode1.Text = instellingen.CodeW.ToString();
                    tbRebus1.Text = instellingen.RebusRT.ToString();
                    tbGram1.Text = instellingen.DokterbibberG.ToString();
                    tbEindcode1.Text = instellingen.Eindcode.ToString();
                }
                else if (instellingen.InstellingID == 2)
                {
                    tbPingpong2.Text = instellingen.PingpongbalSV.ToString();
                    tbGetal1V2.Text = instellingen.Getal1FF.ToString();
                    tbGetal2V2.Text = instellingen.Getal2FF.ToString();
                    tbWatercode2.Text = instellingen.CodeW.ToString();
                    tbRebus2.Text = instellingen.RebusRT.ToString();
                    tbGram2.Text = instellingen.DokterbibberG.ToString();
                    tbEindcode2.Text = instellingen.Eindcode.ToString();
                }
                else if (instellingen.InstellingID == 3)
                {
                    tbPingpong3.Text = instellingen.PingpongbalSV.ToString();
                    tbGetal1V3.Text = instellingen.Getal1FF.ToString();
                    tbGetal2V3.Text = instellingen.Getal2FF.ToString();
                    tbWatercode3.Text = instellingen.CodeW.ToString();
                    tbRebus3.Text = instellingen.RebusRT.ToString();
                    tbGram3.Text = instellingen.DokterbibberG.ToString();
                    tbEindcode3.Text = instellingen.Eindcode.ToString();
                }
            }
        }

        private void btnTerugNaarBegin_Click(object sender, EventArgs e)
        {
            this.Hide();
            pwnUI pwnUI = new pwnUI();
            pwnUI.Show();
        }

        private void btnSluit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnScore_Click(object sender, EventArgs e)
        {
            this.Hide();
            scoreUI scoreUI = new scoreUI();
            scoreUI.Show();
        }
    }
}