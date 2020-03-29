using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pwnModel;
using pwnLogic;
using System.IO;
using System.Configuration;

namespace pwnUI
{
    public partial class pwnUI : Form
    {
        //Thijs
        private TimeSpan ts = TimeSpan.FromSeconds(10);
        private int counter = 1800; // 30 minuten
        int straftijd = 0;

        //Pogingen
        int pogingen = 0;

        //Settings
        int[] eindcode;
        int rebus;
        int[] pingpongbalcode;
        int dokterbibber;
        int getal1FF;
        int getal2FF;
        int antwoordFF;
        int[] watercode;
        string rebusAntwoord;

        public pwnUI()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        //Laad form applicatie
        private void pwnUI_Load(object sender, EventArgs e)
        {
            //hide other panels
            pnlSetTeamNaam.Hide();
            pnlIntroVideo.Hide();
            pnlTimer.Hide();
            pnlEindCijfers.Hide();
            pnlZeeReep.Hide();
            pnlSchadeEnVeiligheid.Hide();
            pnlFloraEnFauna.Hide();
            pnlWater.Hide();
            pnlToerismeEnRecreatie.Hide();
            pnlGezondheid.Hide();
            pnlIntroductie.Hide();
            pnlEindscherm.Hide();
            pnlErrorMessage.Hide();
            //start timer
            timerFlickeringScreen.Enabled = true;
            timerFlickeringScreen.Start();
        }

        private void btnPasswordLogin_Click(object sender, EventArgs e)
        {
            const string wachtwoordSpeler = "SPELER";
            const string wachtwoordAdmin = "ADMIN";

            if (tbWachtwoordLogin.Text.ToUpper() == wachtwoordAdmin)
            {
                //go to manegement
                this.Hide();
                adminUI adminUI = new adminUI();
                adminUI.Show();
            }
            else if (tbWachtwoordLogin.Text.ToUpper() == wachtwoordSpeler)
            {
                //Continue to escape room
                pnlLogin.Hide();
                pnlSetTeamNaam.Show();
            }
            else
            {
                //Verkeerd of geen wachtwoord ingevoerd.
                MessageBox.Show("Helaas, je hebt geen geldig wachtwoord ingevoerd.\r\nProbeer het nogmaals.", "Onjuist wachtwoord", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tbWachtwoordLogin.Clear();
        }

        //Voeg teamnaam toe aan DB en start de tijd
        private void btnPlayEscapeRoom_Click(object sender, EventArgs e)
        {
            //schrijf teamnaam naar DB
            Team_Service team_Service = new Team_Service();
            Team team = new Team();
            team.TeamNaam = tbTeamNaam.Text;
            team_Service.setTeam(team);

            //Hide panel
            pnlSetTeamNaam.Hide();

            //Speel introvideo
            PlayIntroVideo();
            timerForScore.Interval = 1000; // 1 second

            //Instellingen ophalen
            Instellingen_Service instellingen_Service = new Instellingen_Service();
            List<Instellingen> instelling = instellingen_Service.instellingen();

            foreach (Instellingen instellingen in instelling)
            {
                if (instellingen.IsEnabled == true)
                {
                    eindcode = Array.ConvertAll(instellingen.Eindcode.ToString().ToArray(), x => (int)x - 48);
                    rebus = instellingen.RebusRT;
                    pingpongbalcode = Array.ConvertAll(instellingen.PingpongbalSV.ToString().ToArray(), x => (int)x - 48);
                    dokterbibber = instellingen.DokterbibberG;
                    getal1FF = instellingen.Getal1FF;
                    getal2FF = instellingen.Getal2FF;
                    watercode = Array.ConvertAll(instellingen.CodeW.ToString().ToArray(), x => (int)x - 48);
                }
            }
        }

        //Show video
        public void PlayIntroVideo()
        {
            string bestandVideo = ConfigurationManager.AppSettings["introvideo"];
            pnlIntroVideo.Show();
            axWindowsMediaPlayerIntroVideo.URL = bestandVideo;
            axWindowsMediaPlayerIntroVideo.Ctlcontrols.play();
            axWindowsMediaPlayerIntroVideo.Ctlenabled = false;
        }

        //Stop video
        private void btnStopVideo_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayerIntroVideo.Ctlcontrols.stop();
        }

        //Go on when intro video stopped
        private void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // 8 = Media Ended
            if (e.newState == 8 || e.newState == 1)
            {
                pnlIntroVideo.Hide();
                ///show hint lbAanwijzing1 and question1
                pnlIntroductie.Show();
            }
        }


        //Stan
        private void btnAanDeSlag_Click(object sender, EventArgs e)
        {
            pnlIntroductie.Hide();
            pnlZeeReep.Show();
            pnlEindCijfers.Show();
            pnlTimer.Show();
            SettingTextboxes();

            //Start timer
            timerForScore.Start();
        }

        public void SettingTextboxes()
        {
            //Bepaal hoelang het antwoord is
            VragenZeereep_Service vragen = new VragenZeereep_Service();
            List<VragenZeereep> vragenlijst = vragen.GetVraag();
            string antwoordVraagZeereep;
            foreach (VragenZeereep vraag in vragenlijst)
            {
                antwoordVraagZeereep = vraag.AntwoordOpVraag;

                //Set the usefull textboxes
                if (antwoordVraagZeereep.Length == 4)
                {
                    tbAntwoordZeeReep5.Hide();
                    tbAntwoordZeeReep6.Hide();
                    tbAntwoordZeeReep7.Hide();
                    tbAntwoordZeeReep8.Hide();
                    tbAntwoordZeeReep9.Hide();
                    tbAntwoordZeeReep10.Hide();
                }
                else if (antwoordVraagZeereep.Length == 5)
                {
                    tbAntwoordZeeReep6.Hide();
                    tbAntwoordZeeReep7.Hide();
                    tbAntwoordZeeReep8.Hide();
                    tbAntwoordZeeReep9.Hide();
                    tbAntwoordZeeReep10.Hide();
                }
                else if (antwoordVraagZeereep.Length == 6)
                {
                    tbAntwoordZeeReep7.Hide();
                    tbAntwoordZeeReep8.Hide();
                    tbAntwoordZeeReep9.Hide();
                    tbAntwoordZeeReep10.Hide();
                }
                else if (antwoordVraagZeereep.Length == 7)
                {
                    tbAntwoordZeeReep8.Hide();
                    tbAntwoordZeeReep9.Hide();
                    tbAntwoordZeeReep10.Hide();
                }
                else if (antwoordVraagZeereep.Length == 8)
                {
                    tbAntwoordZeeReep9.Hide();
                    tbAntwoordZeeReep10.Hide();
                }
                else if (antwoordVraagZeereep.Length == 9)
                {
                    tbAntwoordZeeReep10.Hide();
                }
            }
        }

        //Thijs
        //Voorkom sluiten van scherm
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }


        //Timer voor flikkeren
        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int a = rnd.Next(250, 255);
            int r = rnd.Next(0, 10);
            int g = rnd.Next(0, 50);
            int b = rnd.Next(150, 255);
            pnlLogin.BackColor = Color.FromArgb(a, r, g, b);
        }

        //Timer voor tijdscore
        private void timerForScore_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter < 0)
            {
                timerForScore.Stop();
                pnlEindscherm.Show();
                MessageBox.Show("Helaas,\r\nJullie zijn er niet in geslaagd om te ontsnappen uit de Escape Room van PWN! Voor het verlaten van de kamer heb je de volgende code nodig: " + eindcode, "Melding", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (counter <= 1200 && counter > 600)
            {
                pnlTimer.BackColor = Color.Orange;
                lbTimer.BackColor = Color.Orange;
            }
            else if (counter <= 600 && counter > 0)
            {
                pnlTimer.BackColor = Color.Red;
                lbTimer.BackColor = Color.Red;
                //geluidje....
            }
            TimeSpan result = TimeSpan.FromSeconds(counter);
            lbTimer.Text = string.Format("{0:00}:{1:00}", result.Minutes, result.Seconds);

        }

        //Stan
       

        //Check vraag 1
        private void btnCheckVraag1_Click(object sender, EventArgs e)
        {
            //vraag en antwoord bepalen
            string antwoordVraagZeereep;
            bool antwoordVraagZeereepIsCorrect = false;

            VragenZeereep_Service vragen = new VragenZeereep_Service();
            List<VragenZeereep> vragenlijst = vragen.GetVraag();

            foreach (VragenZeereep vraag in vragenlijst)
            {
                antwoordVraagZeereep = vraag.AntwoordOpVraag;

                //checken of het antwoord goed is
                if (antwoordVraagZeereep.Length == 4)
                {
                    if (tbAntwoordZeeReep1.Text == antwoordVraagZeereep[0].ToString() && tbAntwoordZeeReep2.Text == antwoordVraagZeereep[1].ToString() && tbAntwoordZeeReep3.Text == antwoordVraagZeereep[2].ToString() && tbAntwoordZeeReep4.Text == antwoordVraagZeereep[3].ToString())
                    {
                        antwoordVraagZeereepIsCorrect = true;
                    }                
                }
                else if (antwoordVraagZeereep.Length == 5)
                {
                    if (tbAntwoordZeeReep1.Text == antwoordVraagZeereep[0].ToString() && tbAntwoordZeeReep2.Text == antwoordVraagZeereep[1].ToString() && tbAntwoordZeeReep3.Text == antwoordVraagZeereep[2].ToString() && tbAntwoordZeeReep4.Text == antwoordVraagZeereep[3].ToString() && tbAntwoordZeeReep5.Text == antwoordVraagZeereep[4].ToString())
                    {
                        antwoordVraagZeereepIsCorrect = true;
                    }
                }
                else if (antwoordVraagZeereep.Length == 6)
                {
                    if (tbAntwoordZeeReep1.Text == antwoordVraagZeereep[0].ToString() && tbAntwoordZeeReep2.Text == antwoordVraagZeereep[1].ToString() && tbAntwoordZeeReep3.Text == antwoordVraagZeereep[2].ToString() && tbAntwoordZeeReep4.Text == antwoordVraagZeereep[3].ToString() && tbAntwoordZeeReep5.Text == antwoordVraagZeereep[4].ToString() && tbAntwoordZeeReep6.Text == antwoordVraagZeereep[5].ToString())
                    {
                        antwoordVraagZeereepIsCorrect = true;
                    }
                }
                else if (antwoordVraagZeereep.Length == 7)
                {
                    if (tbAntwoordZeeReep1.Text == antwoordVraagZeereep[0].ToString() && tbAntwoordZeeReep2.Text == antwoordVraagZeereep[1].ToString() && tbAntwoordZeeReep3.Text == antwoordVraagZeereep[2].ToString() && tbAntwoordZeeReep4.Text == antwoordVraagZeereep[3].ToString() && tbAntwoordZeeReep5.Text == antwoordVraagZeereep[4].ToString() && tbAntwoordZeeReep6.Text == antwoordVraagZeereep[5].ToString() && tbAntwoordZeeReep7.Text == antwoordVraagZeereep[6].ToString())
                    {
                        antwoordVraagZeereepIsCorrect = true;
                    }
                }
                else if (antwoordVraagZeereep.Length == 8)
                {
                    if (tbAntwoordZeeReep1.Text == antwoordVraagZeereep[0].ToString() && tbAntwoordZeeReep2.Text == antwoordVraagZeereep[1].ToString() && tbAntwoordZeeReep3.Text == antwoordVraagZeereep[2].ToString() && tbAntwoordZeeReep4.Text == antwoordVraagZeereep[3].ToString() && tbAntwoordZeeReep5.Text == antwoordVraagZeereep[4].ToString() && tbAntwoordZeeReep6.Text == antwoordVraagZeereep[5].ToString() && tbAntwoordZeeReep7.Text == antwoordVraagZeereep[6].ToString() && tbAntwoordZeeReep8.Text == antwoordVraagZeereep[7].ToString())
                    {
                        antwoordVraagZeereepIsCorrect = true;
                    }
                }
                else if (antwoordVraagZeereep.Length == 9)
                {
                    if (tbAntwoordZeeReep1.Text == antwoordVraagZeereep[0].ToString() && tbAntwoordZeeReep2.Text == antwoordVraagZeereep[1].ToString() && tbAntwoordZeeReep3.Text == antwoordVraagZeereep[2].ToString() && tbAntwoordZeeReep4.Text == antwoordVraagZeereep[3].ToString() && tbAntwoordZeeReep5.Text == antwoordVraagZeereep[4].ToString() && tbAntwoordZeeReep6.Text == antwoordVraagZeereep[5].ToString() && tbAntwoordZeeReep7.Text == antwoordVraagZeereep[6].ToString() && tbAntwoordZeeReep8.Text == antwoordVraagZeereep[7].ToString() && tbAntwoordZeeReep9.Text == antwoordVraagZeereep[8].ToString())
                    {
                        antwoordVraagZeereepIsCorrect = true;
                    }
                }
                else if (antwoordVraagZeereep.Length == 10)
                {
                    if (tbAntwoordZeeReep1.Text == antwoordVraagZeereep[0].ToString() && tbAntwoordZeeReep2.Text == antwoordVraagZeereep[1].ToString() && tbAntwoordZeeReep3.Text == antwoordVraagZeereep[2].ToString() && tbAntwoordZeeReep4.Text == antwoordVraagZeereep[3].ToString() && tbAntwoordZeeReep5.Text == antwoordVraagZeereep[4].ToString() && tbAntwoordZeeReep6.Text == antwoordVraagZeereep[5].ToString() && tbAntwoordZeeReep7.Text == antwoordVraagZeereep[6].ToString() && tbAntwoordZeeReep8.Text == antwoordVraagZeereep[7].ToString() && tbAntwoordZeeReep9.Text == antwoordVraagZeereep[8].ToString() && tbAntwoordZeeReep10.Text == antwoordVraagZeereep[9].ToString())
                    {
                        antwoordVraagZeereepIsCorrect = true;
                    }
                }

                //Clearing textboxes if wrong
                ClearingTextboxesIfWrong();
            }


            //antwoord goed
            if (antwoordVraagZeereepIsCorrect)
            {
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Goed gedaan!\r\nJullie zijn nu bij het volgende onderdeel.";
                //Set pogingen to 0
                pogingen = 0;
                //show next question
                pnlSchadeEnVeiligheid.Show();
                pnlZeeReep.Hide();
                //show first number
                lblCijfer1.Text = eindcode[0].ToString();
            }

            //antwoord fout
            else
            {
                if (pogingen < 2)
                {       
                    
                    pnlErrorMessage.Show();
                    lblErrorMessage.Text = "Het antwoord is onjuist, jullie krijgen een andere vraag op je mobiel.";
                    //Get new question
                    GetNewQuestion();
                    //Setting textboxes
                    SettingTextboxes();
                    pogingen++;
                }
                else
                {
                    pnlErrorMessage.Show();
                    lblErrorMessage.Text = "Het antwoord was onjuist, jullie hebben 5 minuten straftijd gekregen.";
                    //Set pogingen to 0
                    pogingen = 0;
                    //show next question
                    pnlSchadeEnVeiligheid.Show();
                    pnlZeeReep.Hide();
                    //show first number
                    lblCijfer1.Text = eindcode[0].ToString();
                    //straftijd 5 min
                    straftijd += 300;
                    counter -= 300;
                }
            }
        }

        public void GetNewQuestion()
        {
            Random rnd = new Random();
            int vraagID = rnd.Next(0, 7);
            VragenZeereep_Service vragen = new VragenZeereep_Service();
            vragen.GetNewQuestion(vraagID);
        }

        public void ClearingTextboxesIfWrong()
        {
            VragenZeereep_Service vragen = new VragenZeereep_Service();
            List<VragenZeereep> vragenlijst = vragen.GetVraag();
            string antwoordVraagZeereep;
            foreach (VragenZeereep vraag in vragenlijst)
            {
                antwoordVraagZeereep = vraag.AntwoordOpVraag;
                if (pogingen == 1)
                {
                    //Geef 1e en 2de letter
                    tbAntwoordZeeReep1.Text = antwoordVraagZeereep[0].ToString();
                    tbAntwoordZeeReep2.Text = antwoordVraagZeereep[1].ToString();
                }
                else if (pogingen == 0)
                {
                    //Geef 1e letter
                    tbAntwoordZeeReep1.Text = antwoordVraagZeereep[0].ToString();
                    tbAntwoordZeeReep2.Clear();
                }
            }
            
            //Clearing textboxes
            tbAntwoordZeeReep3.Clear();
            tbAntwoordZeeReep4.Clear();
            tbAntwoordZeeReep5.Clear();
            tbAntwoordZeeReep6.Clear();
            tbAntwoordZeeReep7.Clear();
            tbAntwoordZeeReep8.Clear();
            tbAntwoordZeeReep9.Clear();
            tbAntwoordZeeReep10.Clear();
        }

        //Play video if needed
        private void btnPlayVideo_Click(object sender, EventArgs e)
        {
            pnlZeeReep.Hide();
            PlayIntroVideo();
        }

        //Check vraag 2
        private void btCheckAnswerSchadeEnVeiligheid_Click(object sender, EventArgs e)
        {
            pnlErrorMessage.Hide();
            //check cijfercode
            if (tbPingpongA1.Text == pingpongbalcode[0].ToString() && tbPingpongA2.Text == pingpongbalcode[1].ToString() && tbPingpongA3.Text == pingpongbalcode[2].ToString())
            {
                //if answer is true
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Goed gedaan!\r\nJullie zijn nu bij het volgende onderdeel.";
                //Set pogingen to 0
                pogingen = 0;
                //Show next question
                pnlSchadeEnVeiligheid.Hide();
                pnlFloraEnFauna.Show();
                //Show second number
                lblCijfer2.Text = eindcode[1].ToString();
            }
            else
            {
                //if answer is false
                if (pogingen < 2)
                {
                    pogingen++;
                    pnlErrorMessage.Show();
                    lblErrorMessage.Text = "Het antwoord is onjuist, jullie krijgen nog een kans.";
                }
                else
                {
                    pnlErrorMessage.Show();
                    lblErrorMessage.Text = "Het antwoord was onjuist, jullie hebben 5 minuten straftijd gekregen.";
                    //Set pogingen to 0
                    pogingen = 0;
                    //Show next question
                    pnlSchadeEnVeiligheid.Hide();
                    pnlFloraEnFauna.Show();
                    //Show second number
                    lblCijfer2.Text = eindcode[1].ToString();
                    //straftijd 5 min
                    straftijd += 300;
                    counter -= 300;
                }
            }

            //Thijs
            //Bepaal welke som er gedaan moet worden
            Random rnd = new Random();
            int var = rnd.Next(0, 3);
            if (var == 0)
            {
                antwoordFF = getal1FF + getal2FF;
                lblFloraEnFaunaVAR.Text = "+";
            }
            else if (var == 1)
            {
                antwoordFF = getal1FF - getal2FF;
                lblFloraEnFaunaVAR.Text = "-";
            }
            else
            {
                antwoordFF = getal1FF * getal2FF;
                lblFloraEnFaunaVAR.Text = "X";
            }
        }

        //Check vraag 3
        private void btnFloraEnFauna_Click(object sender, EventArgs e)
        {
            if (tbFloraEnFaunaG1.Text == getal1FF.ToString() && tbFloraEnFaunaG2.Text == getal2FF.ToString() && tbFloraEnFaunaG3.Text == antwoordFF.ToString())
            {
                //if answer is true
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Goed gedaan!\r\nJullie zijn nu bij het volgende onderdeel.";
                pogingen = 0;
                pnlFloraEnFauna.Hide();
                lblCijfer3.Text = eindcode[2].ToString();
                pnlWater.Show();
            }
            else
            {
                pogingen++;
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Het antwoord is onjuist, jullie krijgen nog een kans.";
            }

            if (pogingen >= 2)
            {
                //if answer is false
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Het antwoord was onjuist, jullie hebben 5 minuten straftijd gekregen.";
                pogingen = 0;
                //straftijd 5 min
                straftijd += 300;
                counter -= 300;
                //Show next question
                pnlFloraEnFauna.Hide();
                pnlWater.Show();
                //Show third number
                lblCijfer3.Text = eindcode[2].ToString();         
            }
        }

        //Check vraag 4
        private void btnWaterAntwoordCheck_Click(object sender, EventArgs e)
        {
            if (tbWaterA1.Text == watercode[0].ToString() && tbWaterA1.Text == watercode[0].ToString() && tbWaterA1.Text == watercode[0].ToString())
            {
                //if answer is true
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Goed gedaan!\r\nJullie zijn nu bij het volgende onderdeel.";
                pogingen = 0;
                pnlToerismeEnRecreatie.Show();
                pnlWater.Hide();
                lblCijfer4.Text = eindcode[3].ToString();
            }
            else
            {
                //if answer is false
                pogingen++;
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Het antwoord is onjuist, jullie krijgen nog een kans.";
            }

            if (pogingen >= 2)
            {
                //if answer is 2x false
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Het antwoord was onjuist, jullie hebben 5 minuten straftijd gekregen.";
                pogingen = 0;
                //Show next question
                pnlWater.Hide();
                pnlToerismeEnRecreatie.Show();
                //Straftijd 5 min
                counter -= 300;
                straftijd += 300;
                //Show fourth number
                lblCijfer4.Text = eindcode[3].ToString();
            }
            //hide all rebussen
            pbRebus1.Hide();
            pbRebus2.Hide();
            pbRebus3.Hide();
            pbRebus4.Hide();


        }

        private void InsertUSB()
        {
            string bestand = ConfigurationManager.AppSettings["rebus"];
            while (!File.Exists(bestand))
            {
                //doe niks
            }
        }

        //Check vraag 5
        private void btnToerismeEnRecreatieCheckAns_Click(object sender, EventArgs e)
        {
            pnlErrorMessage.Hide();
            if (tbToerismeEnRecreatie.Text.ToLower() == rebusAntwoord)
            {
                //if answer is true
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Goed gedaan!\r\nJullie zijn nu bij het volgende onderdeel.";
                pogingen = 0;
                //Show next question
                pnlToerismeEnRecreatie.Hide();
                pnlGezondheid.Show();
                //Show fifth number
                lblCijfer5.Text = eindcode[4].ToString();
            }
            else
            {
                //if answer is false
                pogingen++;
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Het antwoord is onjuist, jullie krijgen nog een kans.";
            }

            if (pogingen >= 3)
            {
                //if answer is false 3 times
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Het antwoord was onjuist, jullie hebben 5 minuten straftijd gekregen.";
                pogingen = 0;
                //Straftijd 5 min
                straftijd += 300;
                counter -= 300;
                //Show next question
                pnlToerismeEnRecreatie.Hide();
                pnlGezondheid.Show();
                //Show fifth number
                lblCijfer5.Text = eindcode[4].ToString();
            }
        }

        private void pnlToerismeEnRecreatie_MouseHover(object sender, EventArgs e)
        {
            //INSERT USB STICK
            //InsertUSB();

            if (rebus == 1)
            {
                rebusAntwoord = "water";
                pbRebus1.Show();
            }
            else if (rebus == 2)
            {
                rebusAntwoord = "parasol";
                pbRebus2.Show();
            }
            else if (rebus == 3)
            {
                rebusAntwoord = "strand";
                pbRebus3.Show();
            }
            else
            {
                rebusAntwoord = "duinen";
                pbRebus4.Show();
            }
        }

        //Check vraag 6 (laatste)
        private void btnGezondheid_Click(object sender, EventArgs e)
        {
            pnlErrorMessage.Hide();
            //check aantal gram met een marge van 1 gram.
            if (tbGezondheidAnswer.Text == dokterbibber.ToString() || tbGezondheidAnswer.Text == dokterbibber + 1.ToString() || tbGezondheidAnswer.Text == (dokterbibber - 1).ToString())
            {
                //if answer is true
                //Stop timer
                timerForScore.Stop();
                //Show sixth number
                lblCijfer6.Text = eindcode[5].ToString();


                //Set design
                lblTeamnaam.Text = tbTeamNaam.Text;
                TimeSpan result = TimeSpan.FromSeconds(counter);
                lblEindTimer.Text = string.Format("{0:00}:{1:00}", result.Minutes, result.Seconds);
                SetDesign();
                pnlEindCijfers.Show();

                //Show eindscherm
                pnlGezondheid.Hide();
                pnlEindscherm.Show();
            }
            else
            {
                //if answer is false
                pogingen++;
                pnlErrorMessage.Show();
                lblErrorMessage.Text = "Het antwoord is onjuist, jullie krijgen nog een kans.";
            }

            //if answer is 3 times wrong
            if (pogingen >= 3)
            {
                //Straftijd 5 min
                straftijd += 300;
                counter -= 300;
                //Stop timer
                timerForScore.Stop();
                //Show sixth number
                lblCijfer6.Text = eindcode[5].ToString();
                //Show eindscherm
                pnlErrorMessage.Hide();
                pnlTimer.Hide();
                pnlGezondheid.Hide();
                pnlEindscherm.Show();

                //Set design
                lblTeamnaam.Text = tbTeamNaam.Text;
                TimeSpan result = TimeSpan.FromSeconds(counter);
                lblEindTimer.Text = string.Format("{0:00}:{1:00}", result.Minutes, result.Seconds);
                SetDesign();
                pnlEindCijfers.Show();
            }
        }

        //Stan
        public void SetDesign()
        {
            pnlEindCijfers.Location = new Point(0, 953);
            pnlEindCijfers.Width = 1900;
            lblKluiscode.Location = new Point(500, 30);
            lblCijfer1.Location = new Point(800, 30);
            lblCijfer2.Location = new Point(870, 30);
            lblCijfer3.Location = new Point(940, 30);
            lblCijfer4.Location = new Point(1010, 30);
            lblCijfer5.Location = new Point(1080, 30);
            lblCijfer6.Location = new Point(1150, 30);
        }

        private void btnScore_Click(object sender, EventArgs e)
        {
            //Hide eindscherm
            pnlEindscherm.Hide();

            //Set score
            int tijdoverInt = 1800 - counter;
            TimeSpan tijdover = TimeSpan.FromSeconds(tijdoverInt);
            TimeSpan speeltijd = TimeSpan.FromSeconds(counter);
            TimeSpan strafT = TimeSpan.FromSeconds(straftijd);
            Score score = new Score();
            score.TeamNaam = tbTeamNaam.Text;
            score.Speeldatum = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            score.Speeltijd = tijdover;
            score.Straftijd = strafT;
            score.Tijdover = speeltijd;
            //Schrijf score naar Database
            Score_Service score_Service = new Score_Service();
            score_Service.setScore(score);

            //View score
            scoreUI scoreUI = new scoreUI();
            scoreUI.Show();
            this.Hide();
        }

        //Clearing textboxes if clicked on it and focus other textbox if text changed
        private void tbAntwoordZeeReep1_MouseClick(object sender, MouseEventArgs e)
        {
            tbAntwoordZeeReep1.Clear();
        }

        private void tbAntwoordZeeReep2_MouseClick(object sender, MouseEventArgs e)
        {
            tbAntwoordZeeReep2.Clear();
        }

        private void tbAntwoordZeeReep3_MouseClick(object sender, MouseEventArgs e)
        {
            tbAntwoordZeeReep3.Clear();
        }

        private void tbAntwoordZeeReep4_MouseClick(object sender, MouseEventArgs e)
        {
            tbAntwoordZeeReep4.Clear();
        }

        private void tbAntwoordZeeReep5_MouseClick(object sender, MouseEventArgs e)
        {
            tbAntwoordZeeReep5.Clear();
        }

        private void tbAntwoordZeeReep6_MouseClick(object sender, MouseEventArgs e)
        {
            tbAntwoordZeeReep6.Clear();
        }

        private void tbAntwoordZeeReep7_MouseClick(object sender, MouseEventArgs e)
        {
            tbAntwoordZeeReep7.Clear();
        }

        private void tbAntwoordZeeReep8_MouseClick(object sender, MouseEventArgs e)
        {
            tbAntwoordZeeReep8.Clear();
        }

        private void tbAntwoordZeeReep9_MouseClick(object sender, MouseEventArgs e)
        {
            tbAntwoordZeeReep9.Clear();
        }

        private void tbAntwoordZeeReep10_MouseClick(object sender, MouseEventArgs e)
        {
            tbAntwoordZeeReep10.Clear();
        }

        private void tbPingpongA1_MouseClick(object sender, MouseEventArgs e)
        {
            tbPingpongA1.Clear();
        }
        private void tbPingpongA2_MouseClick(object sender, MouseEventArgs e)
        {
            tbPingpongA2.Clear();
        }
        private void tbPingpongA3_MouseClick(object sender, MouseEventArgs e)
        {
            tbPingpongA3.Clear();
        }
        private void tbAntwoordZeeReep1_TextChanged(object sender, EventArgs e)
        {
            tbAntwoordZeeReep2.Focus();
        }

        private void tbAntwoordZeeReep2_TextChanged(object sender, EventArgs e)
        {
            tbAntwoordZeeReep3.Focus();
        }

        private void tbAntwoordZeeReep3_TextChanged(object sender, EventArgs e)
        {
            tbAntwoordZeeReep4.Focus();
        }

        private void tbAntwoordZeeReep4_TextChanged(object sender, EventArgs e)
        {
            tbAntwoordZeeReep5.Focus();
        }

        private void tbAntwoordZeeReep5_TextChanged(object sender, EventArgs e)
        {
            tbAntwoordZeeReep6.Focus();
        }

        private void tbAntwoordZeeReep6_TextChanged(object sender, EventArgs e)
        {
            tbAntwoordZeeReep7.Focus();
        }

        private void tbAntwoordZeeReep7_TextChanged(object sender, EventArgs e)
        {
            tbAntwoordZeeReep8.Focus();
        }

        private void tbAntwoordZeeReep8_TextChanged(object sender, EventArgs e)
        {
            tbAntwoordZeeReep9.Focus();
        }

        private void tbAntwoordZeeReep9_TextChanged(object sender, EventArgs e)
        {
            tbAntwoordZeeReep10.Focus();
        }

        private void tbPingpongA1_TextChanged(object sender, EventArgs e)
        {
            tbPingpongA2.Focus();
        }

        private void tbPingpongA2_TextChanged(object sender, EventArgs e)
        {
            tbPingpongA3.Focus();
        }

        private void tbFloraEnFaunaG1_MouseClick(object sender, MouseEventArgs e)
        {
            tbFloraEnFaunaG1.Clear();
        }

        private void tbFloraEnFaunaG2_MouseClick(object sender, MouseEventArgs e)
        {
            tbFloraEnFaunaG2.Clear();
        }

        private void tbFloraEnFaunaG3_MouseClick(object sender, MouseEventArgs e)
        {
            tbFloraEnFaunaG3.Clear();
        }

        private void tbWaterA1_TextChanged(object sender, EventArgs e)
        {
            tbWaterA2.Focus();
        }

        private void tbWaterA2_TextChanged(object sender, EventArgs e)
        {
            tbWaterA3.Focus();
        }

        private void tbWaterA1_MouseClick(object sender, MouseEventArgs e)
        {
            tbWaterA1.Clear();
        }

        private void tbWaterA2_MouseClick(object sender, MouseEventArgs e)
        {
            tbWaterA2.Clear();
        }

        private void tbWaterA3_MouseClick(object sender, MouseEventArgs e)
        {
            tbWaterA3.Clear();
        }
    }
}
