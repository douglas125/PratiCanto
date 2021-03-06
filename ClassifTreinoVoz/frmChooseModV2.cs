using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Globalization;

namespace ClassifTreinoVoz
{
    public partial class frmChooseModV2 : Form
    {
        public frmChooseModV2()
        {
            //show splash screen
            splash = new PratiCanto.Misc.frmSplash();
            splash.Show();
            splash.Refresh();
            ModuleLicenses.CheckLicenses();

            InitializeComponent();
        }

        PratiCanto.Misc.frmSplash splash;
        private void Form1_Load(object sender, EventArgs e)
        {
            #region Cosmetics

            panTop.BackgroundImage = PratiCanto.LayoutFuncs.MakeDegrade(Color.White, Color.FromArgb(240, 240, 240), 1, panTop.Height, false);

            //border
            panLogo.BorderStyle = BorderStyle.None;
            panModules.BorderStyle = BorderStyle.None;
            panTop.BorderStyle = BorderStyle.None;
            panLogo.Paint += new PaintEventHandler(panTop_Paint);
            panModules.Paint += new PaintEventHandler(panTop_Paint);
            panTop.Paint += new PaintEventHandler(panTop_Paint);

            btnPractice.FlatAppearance.BorderSize = 0;

            Bitmap imgBtns = PratiCanto.LayoutFuncs.MakeDegrade(Color.White, Color.FromArgb(223, 223, 223), btnPractice.Width, btnPractice.Height, true);
            //btnVocalTrainer.Image = imgBtns;

            Bitmap imgResources = PratiCanto.LayoutFuncs.MakeDegrade(Color.FromArgb(218, 30, 30), Color.FromArgb(113, 40, 40), btnAbout.Width, btnAbout.Height, false);

            int offset = 25;
            foreach (Control c in panModules.Controls)
            {
                if (c is Button)
                {
                    Button b = (Button)c;
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;
                    if (!b.Text.EndsWith("."))
                    {
                        Bitmap btnImg = (Bitmap)imgBtns.Clone();
                        if (b.Image != null)
                        {
                            Graphics g = Graphics.FromImage(btnImg);
                            g.DrawImage(b.Image, offset, offset>>1, b.Width - 2*offset, b.Height - 2*offset);
                        }
                        b.Image = btnImg;
                    }
                    else b.Image = imgResources;

                    PratiCanto.LayoutFuncs.AnimateEnterLeaveBorder(b);
                }
            }
            #endregion

            //bool ValidLicense = AudioComparer.SoftwareKey.CheckLicense(lblFindLicense.Text, lblNotRegistered.Text);

            AdjustImgSizesToButtons(this.Controls);

            PratiCantoForms.DefaultClientImg = (Bitmap)picDefaultClientImage.Image;

            string version = String.Format("{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            lblVersion.Text += " " + version;

            //Initialize font
            frmPractice.InitFont();

            btnEditCreateMelody.Enabled = true;
            btnSustVowel.Enabled = false;
            btnSpeechToText.Enabled = false;
            btnPractice.Enabled = false;
            btnExtension.Enabled = false;
            btnVoiceAnalyzer.Enabled = false;
            btnIntonation.Enabled = false;
            btnInspector.Enabled = false;
            btnAudioAnnot.Enabled = false;

            try
            {
                if (File.Exists(agreeFile))
                {
                    chkAgree.Checked = true;
                }
            }
            catch { }

            //this.WindowState = FormWindowState.Minimized;
            //btnPractice_Click(sender, e);
            //btnVoiceAnalyzer_Click(sender, e);

            //Close splash screen
            PratiCantoForms.mdiParentForm.WindowState = FormWindowState.Maximized;
            splash.Close();
        }

        #region Enables/disables buttons depending on license

        /// <summary>Class to check licenses</summary>
        public static class ModuleLicenses
        {
            public static void CheckLicenses()
            {
                MelodyCreator = true; // AudioComparer.SoftwareKey.CheckLicense("MelodyCreator", true);
                VoiceAnalysis = true; // AudioComparer.SoftwareKey.CheckLicense("VoiceAnalysis", true);
                VoiceExtension = true; // AudioComparer.SoftwareKey.CheckLicense("VoiceExtension", true);
                VocalTrainer = true; // AudioComparer.SoftwareKey.CheckLicense("VocalTrainer", true);
                SustainedVowel = true; // AudioComparer.SoftwareKey.CheckLicense("SustainedVowel", true);
                SpeechToText = true; // AudioComparer.SoftwareKey.CheckLicense("SpeechToText", true);
                Intonation = true; // AudioComparer.SoftwareKey.CheckLicense("Intonation", true);
                Composer = true; // AudioComparer.SoftwareKey.CheckLicense("Composer", true);
                Inspector = true; // AudioComparer.SoftwareKey.CheckLicense("Inspector", true);

            }
            public static bool VoiceExtension = false;
            public static bool MelodyCreator = false;
            public static bool VocalTrainer = false;
            public static bool VoiceAnalysis = false;
            public static bool SustainedVowel = false;
            public static bool SpeechToText = false;
            public static bool Intonation = false;
            public static bool Composer = false;
            public static bool Inspector = false;

        }

        #endregion


        #region Agree to terms in disclaimer
        private void btnAbout_Click(object sender, EventArgs e)
        {
            (new AboutBox()).ShowDialog();
        }
        string agreeFile = Application.StartupPath + "\\agreedToLicense.bool";

        /// <summary>Refresh enabled buttons</summary>
        public void RefreshBtnEnabled()
        {
            ModuleLicenses.CheckLicenses();
            chkAgree_CheckedChanged(this, new EventArgs());
        }

        private void chkAgree_CheckedChanged(object sender, EventArgs e)
        {
            //btnEloquence.Enabled = chkAgree.Checked;
            //btnSelfListen.Enabled = chkAgree.Checked;

            btnEditCreateMelody.Enabled = chkAgree.Checked && ModuleLicenses.MelodyCreator;
            btnPractice.Enabled = chkAgree.Checked && ModuleLicenses.VocalTrainer;
            btnExtension.Enabled = chkAgree.Checked && ModuleLicenses.VoiceExtension;
            btnVoiceAnalyzer.Enabled = chkAgree.Checked && ModuleLicenses.VoiceAnalysis;
            btnSustVowel.Enabled = chkAgree.Checked && ModuleLicenses.SustainedVowel;
            btnSpeechToText.Enabled = chkAgree.Checked && ModuleLicenses.SpeechToText;
            btnIntonation.Enabled = chkAgree.Checked && ModuleLicenses.Intonation;

            btnAudioAnnot.Enabled = chkAgree.Checked && ModuleLicenses.Composer;
            btnInspector.Enabled = chkAgree.Checked && ModuleLicenses.Inspector;

            try
            {
                if (chkAgree.Checked && !File.Exists(agreeFile))
                {
                    File.WriteAllText(agreeFile, "Agreed in " + DateTime.Now.ToString());
                }
                else if (!chkAgree.Checked && File.Exists(agreeFile))
                {
                    File.Delete(agreeFile);
                }
            }
            catch { }
        }
        #endregion


        #region Buttons and images
        #region Border and panel paint events
        void panTop_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            drawRectangle(p.Width, p.Height, e.Graphics);
        }

        void drawRectangle(int w, int h, Graphics g) 
        {
            g.DrawRectangle(Pens.LightGray, 0, 0, w, h);
        }
        #endregion






        private void AdjustImgSizesToButtons(Control.ControlCollection ctrColl)
        {
            foreach (Control ctrl in ctrColl)
            {
                if (ctrl is GroupBox || ctrl is FlowLayoutPanel || ctrl is Panel)
                {
                    AdjustImgSizesToButtons(ctrl.Controls);
                }
                else if (ctrl is Button)
                {
                    Button btn = (Button)ctrl;
                    Bitmap imgOrig = (Bitmap)btn.Image;
                    if (imgOrig != null)
                    {
                        Bitmap imgDest = new Bitmap(imgOrig, new Size(btn.Width, btn.Height));
                        //imgDest.MakeTransparent(Color.White);
                        btn.Image = imgDest;
                    }
                }
            }
        }


        #endregion

        private void btnSaveInfoLic_Click(object sender, EventArgs e)
        {
            PratiCanto.frmInsertKey frmInsKey = new PratiCanto.frmInsertKey();
            frmInsKey.frmChoose = this;
            //frmInsKey.MdiParent = this.MdiParent;
            frmInsKey.StartPosition = FormStartPosition.CenterScreen;
            frmInsKey.ShowDialog();

            if (!frmInsKey.retrievedLicense) AudioComparer.SoftwareKey.SavePCInfo();
        }

        private void btnAbout_Click_1(object sender, EventArgs e)
        {
            (new AboutBox()).ShowDialog();

        }


        #region Links
        private void picYoutube_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UC5X8wCaVrjANG_6gmh14aqQ");
        }
        private void lblYoutube_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UC5X8wCaVrjANG_6gmh14aqQ");
        }

        private void picFacebook_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/praticanto/");
        }
        private void lblFacebook_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/praticanto/");
        }
        #endregion




        #region Choose language
        private void btnPtBr_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            ShowNewFormMain();
        }
        private void btnEnUS_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            ShowNewFormMain();
        }
        private void btnES_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
            ShowNewFormMain();
        }
        private void ShowNewFormMain()
        {
            this.Hide();
            frmChooseModV2 frmC = new frmChooseModV2();
            frmC.MdiParent = this.MdiParent;
            frmC.Show();
            this.Close();
        }
        #endregion



        #region Show modules
        private void btnSustVowel_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormSustVowel();
        }
        private void btnPractice_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormPractice();
        }
        
        private void btnVoiceAnalyzer_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormAnalyzer();
        } 
        private void btnExtension_Click(object sender, EventArgs e)
        {
            //this.Hide();
            frmIdentifyVoice frmiv = new frmIdentifyVoice();
            frmiv.MdiParent = this.MdiParent;
            frmiv.WindowState = FormWindowState.Normal;
            frmiv.Show();
        }
        private void btnSpeechToText_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormSpeech();
        }
        private void btnAudioAnnot_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormComposer();
        }
        private void btnEditCreateMelody_Click(object sender, EventArgs e)
        {
            //this.Hide();
            frmEditMelody frmEditMelody = new frmEditMelody();
            frmEditMelody.MdiParent = this.MdiParent;
            frmEditMelody.btnUpdate_Click(sender, e);
            frmEditMelody.WindowState = FormWindowState.Normal;
            frmEditMelody.Show();
        }
        private void btnIntonation_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormIntonation();
        }
        private void btnInspector_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormInspector();
        }
        #endregion

        private void lblVersion_Click(object sender, EventArgs e)
        {
            PratiCanto.frmVoiceCommand frmVoiceCmd = new PratiCanto.frmVoiceCommand();
            frmVoiceCmd.Show();
 
        }
















    }
}
