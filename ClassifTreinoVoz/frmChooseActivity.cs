using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Threading;

namespace ClassifTreinoVoz
{
    public partial class frmChooseActivity : Form
    {
        public frmChooseActivity()
        {
            //show splash screen
            splash = new PratiCanto.Misc.frmSplash();
            splash.Show();
            splash.Refresh();

            //check licenses
            //disable buttons when user does not have the license for it
            ModuleLicenses.CheckLicenses();


            InitializeComponent();
        }

        /// <summary>Expiration date</summary>
        public DateTime ExpirationDate = new DateTime(2916, 11, 01);

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
                        imgDest.MakeTransparent(Color.White);
                        btn.Image = imgDest;
                    }
                }
            }
        }

        PratiCanto.Misc.frmSplash splash;
        private void frmChooseActivity_Load(object sender, EventArgs e)
        {
            
            //bool ValidLicense = AudioComparer.SoftwareKey.CheckLicense(lblFindLicense.Text, lblNotRegistered.Text);


            AudioComparer.SoftwareKey.lblFindLicenseTxt = lblFindLicense.Text;
            AudioComparer.SoftwareKey.lblNotRegisteredTxt = lblNotRegistered.Text;

            //this.Height = panModules.Top + panModules.Height + 50;
            //panModules.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            AdjustImgSizesToButtons(this.Controls);

            //new System.IO.FileInfo(Application.StartupPath + "\\es\\dum.txt").Directory.Create();

            PratiCantoForms.DefaultClientImg = (Bitmap)picDefaultClientImage.Image;

            ////delete previous versions
            //try
            //{
            //    if (File.Exists(Application.StartupPath + "\\PratiCanto.exeold")) File.Delete(Application.StartupPath + "\\PratiCanto.exeold");
            //    if (File.Exists(Application.StartupPath + "\\PratiCanto32.exeold")) File.Delete(Application.StartupPath + "\\PratiCanto32.exeold");
            //}
            //catch { }

            string version = String.Format("V {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            lblDevs.Text = lblDevs.Text.Replace("[SoftVers]", version);

            #region Expiration
            //if (ExpirationDate.Ticks != 0)
            //{
                
            //    if (ExpirationDate < DateTime.Now)
            //    {
            //        try
            //        {
            //            string pathExec = Application.StartupPath;
            //            DirectoryInfo di = new DirectoryInfo(pathExec);
            //            FileInfo[] licences = di.GetFiles("*.hnflic");

            //            File.Delete(pathExec + "\\publicKey.xml");
            //            File.Delete(pathExec + "\\NAudio.dll");
            //            File.Delete(pathExec + "\\OpenTK.dll");
            //            File.Delete(pathExec + "\\OpenCLTemplate.dll");

            //            foreach (FileInfo fi in licences)
            //            {
            //                File.Delete(fi.FullName);
            //            }
            //        }
            //        catch
            //        {
            //        }
            //        MessageBox.Show(lblExpired.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        Application.Exit();
            //        return;
            //    }
            //}
            //else 
            #endregion
            
            lblDevs.Text = lblDevs.Text.Replace("BETA", "");
            
            //Initialize font
            frmPractice.InitFont();

            bool ValidLicense = true;

            try
            {
                if (File.Exists(agreeFile))
                {
                    chkAgree.Checked = true;
                }
            }
            catch { }

            //Fetch current build
            if (ValidLicense)
            {
                //SelfUpdate.FetchBuild(lblUpdateAvailable.Text);
            }
            else
            {
                btnSpeechToText.Enabled = false;
                chkAgree.Enabled = false;
                btnEditCreateMelody.Enabled = false;
                btnPractice.Enabled = false;
                btnExtension.Enabled = false;
                btnVoiceAnalyzer.Enabled = false;
                btnSelfListen.Enabled = false;
                btnEloquence.Enabled = false;
            }

            //this.WindowState = FormWindowState.Minimized;
            //btnPractice_Click(sender, e);
            //btnVoiceAnalyzer_Click(sender, e);

            //Close splash screen
            PratiCantoForms.mdiParentForm.WindowState = FormWindowState.Maximized;
            splash.Close();
        }

        #region Enables/disables buttons depending on license

        /// <summary>Class to check licenses</summary>
        private static class ModuleLicenses
        {
            public static void CheckLicenses()
            {
                MelodyCreator = AudioComparer.SoftwareKey.CheckLicense("MelodyCreator", true);
                VoiceAnalysis = AudioComparer.SoftwareKey.CheckLicense("VoiceAnalysis", true);
                VoiceExtension = AudioComparer.SoftwareKey.CheckLicense("VoiceExtension", true);
                VocalTrainer = AudioComparer.SoftwareKey.CheckLicense("VocalTrainer", true);
                SustainedVowel = AudioComparer.SoftwareKey.CheckLicense("SustainedVowel", true);
                SpeechToText = AudioComparer.SoftwareKey.CheckLicense("SpeechToText", true);
                Intonation = AudioComparer.SoftwareKey.CheckLicense("Intonation", true);
            }
            public static bool VoiceExtension = false;
            public static bool MelodyCreator = false;
            public static bool VocalTrainer = false;
            public static bool VoiceAnalysis = false;
            public static bool SustainedVowel = false;
            public static bool SpeechToText = false;
            public static bool Intonation = false;
        }

        #endregion

        #region Show modules
        private void btnPractice_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormPractice();
            //this.WindowState = FormWindowState.Minimized;
        }

        private void btnSustVowel_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormSustVowel();
        }

        private void btnExtension_Click(object sender, EventArgs e)
        {
            //this.Hide();
            frmIdentifyVoice frmiv = new frmIdentifyVoice();
            frmiv.MdiParent = this.MdiParent;
            frmiv.Show();
        }

        private void btnEditCreateMelody_Click(object sender, EventArgs e)
        {
            //this.Hide();
            frmEditMelody frmEditMelody = new frmEditMelody();
            frmEditMelody.MdiParent = this.MdiParent;
            frmEditMelody.btnUpdate_Click(sender, e);
            frmEditMelody.Show();
        }

        private void btnVoiceAnalyzer_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormAnalyzer();
            //this.WindowState = FormWindowState.Minimized;
        }


        private void btnSelfListen_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormSelfListen();
        }
        private void btnEloquence_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormEloquence();
        }
        private void btnSpeechToText_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormSpeech();
        }
        private void btnIntonation_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormIntonation();
        }
        #endregion


        #region Agree to terms in disclaimer
        private void btnAbout_Click(object sender, EventArgs e)
        {
            (new AboutBox()).ShowDialog();
        }
        string agreeFile = Application.StartupPath + "\\agreedToLicense.bool";

        private void chkAgree_CheckedChanged(object sender, EventArgs e)
        {
            btnSelfListen.Enabled = chkAgree.Checked;
            btnEditCreateMelody.Enabled = chkAgree.Checked && ModuleLicenses.MelodyCreator;
            btnPractice.Enabled = chkAgree.Checked && ModuleLicenses.VocalTrainer;
            btnExtension.Enabled = chkAgree.Checked && ModuleLicenses.VoiceExtension;
            btnVoiceAnalyzer.Enabled = chkAgree.Checked && ModuleLicenses.VoiceAnalysis;
            btnSustVowel.Enabled = chkAgree.Checked && ModuleLicenses.SustainedVowel;
            btnEloquence.Enabled = chkAgree.Checked;
            btnSpeechToText.Enabled = chkAgree.Checked && ModuleLicenses.SpeechToText;
            btnIntonation.Enabled = chkAgree.Checked && ModuleLicenses.Intonation;
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
            frmChooseActivity frmC = new frmChooseActivity();
            frmC.MdiParent = this.MdiParent;
            frmC.Show();
            this.Close();
        }
        #endregion

        private void lblDevs_Click(object sender, EventArgs e)
        {

        }

        #region Links
        private void linkPratiCanto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkPratiCanto.Text);
        }

        private void linkYouTube_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UC5X8wCaVrjANG_6gmh14aqQ");
        }
        private void linkFacebook_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/praticanto/");
        }

        #endregion

        private void btnSavePCInfo_Click(object sender, EventArgs e)
        {
            AudioComparer.SoftwareKey.SavePCInfo();
        }










        





    }
}
