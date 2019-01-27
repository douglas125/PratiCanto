using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ClassifTreinoVoz
{
    /// <summary>Show PratiCanto Forms and public items</summary>
    public static class PratiCantoForms
    {
        /// <summary>Parent form to hold software windows</summary>
        public static Form mdiParentForm;

        /// <summary>Voice training form</summary>
        static frmPractice frmPrac;
        /// <summary>Show Voice Training Form</summary>
        public static void ShowFormPractice()
        {
            if (frmPrac == null || frmPrac.IsDisposed) frmPrac = new frmPractice();
            if (mdiParentForm != null) frmPrac.MdiParent = mdiParentForm;
            frmPrac.Show();
            frmPrac.WindowState = FormWindowState.Minimized;
            frmPrac.WindowState = FormWindowState.Maximized;
            frmPrac.Activate();
        }

        /// <summary>Voice analyzer form</summary>
        static AudioComparer.frmAudioComparer frmAudioComp;
        /// <summary>Show voice analyzer form</summary>
        public static void ShowFormAnalyzer()
        {
            if (frmAudioComp == null || frmAudioComp.IsDisposed) frmAudioComp = new AudioComparer.frmAudioComparer();
            if (mdiParentForm != null) frmAudioComp.MdiParent = mdiParentForm;
            frmAudioComp.Show();
            frmAudioComp.WindowState = FormWindowState.Minimized;
            frmAudioComp.WindowState = FormWindowState.Maximized;

            frmAudioComp.Activate();
        }

        /// <summary>Sustained vowel</summary>
        static PratiCanto.SPPATHSustainedVowel.frmSustVowel frmVowel;
        /// <summary>Show sustained vowel form</summary>
        public static void ShowFormSustVowel()
        {
            if (frmVowel == null || frmVowel.IsDisposed) frmVowel = new PratiCanto.SPPATHSustainedVowel.frmSustVowel();
            if (mdiParentForm != null) frmVowel.MdiParent = mdiParentForm;
            frmVowel.Show();

            //frmVowel.WindowState = FormWindowState.Minimized;
            //frmVowel.WindowState = FormWindowState.Maximized;

            frmVowel.Activate();
        }

        /// <summary>Self listen</summary>
        static PratiCanto.SPPATHSelfListen.frmSelfListen frmSelfListen;
        /// <summary>Show sustained vowel form</summary>
        public static void ShowFormSelfListen()
        {
            if (frmSelfListen == null || frmSelfListen.IsDisposed) frmSelfListen = new PratiCanto.SPPATHSelfListen.frmSelfListen();
            if (mdiParentForm != null) frmSelfListen.MdiParent = mdiParentForm;
            frmSelfListen.Show();

            frmSelfListen.WindowState = FormWindowState.Normal;
            //frmSelfListen.WindowState = FormWindowState.Minimized;
            //frmSelfListen.WindowState = FormWindowState.Maximized;

            frmSelfListen.Activate();
        }

        /// <summary>Eloquence</summary>
        static PratiCanto.frmEloquence frmEloquence;
        /// <summary>Show sustained vowel form</summary>
        public static void ShowFormEloquence()
        {
            if (frmEloquence == null || frmEloquence.IsDisposed) frmEloquence = new PratiCanto.frmEloquence();
            if (mdiParentForm != null) frmEloquence.MdiParent = mdiParentForm;
            frmEloquence.Show();

            frmEloquence.WindowState = FormWindowState.Normal;
            //frmSelfListen.WindowState = FormWindowState.Minimized;
            //frmSelfListen.WindowState = FormWindowState.Maximized;

            frmEloquence.Activate();
        }

        /// <summary>Eloquence</summary>
        static PratiCanto.frmSpeechRecognizer frmSpeech;
        /// <summary>Show sustained vowel form</summary>
        public static void ShowFormSpeech()
        {
            if (frmSpeech == null || frmSpeech.IsDisposed) frmSpeech = new PratiCanto.frmSpeechRecognizer();
            if (mdiParentForm != null) frmSpeech.MdiParent = mdiParentForm;
            frmSpeech.Show();

            //frmSpeech.WindowState = FormWindowState.Normal;
            //frmSpeech.WindowState = FormWindowState.Minimized;
            frmSpeech.WindowState = FormWindowState.Maximized;

            frmSpeech.Activate();
        }

        /// <summary>Intonation</summary>
        static PratiCanto.frmIntonation frmIntonation;
        /// <summary>Show sustained vowel form</summary>
        public static void ShowFormIntonation()
        {
            if (frmIntonation == null || frmIntonation.IsDisposed) frmIntonation = new PratiCanto.frmIntonation();
            if (mdiParentForm != null) frmIntonation.MdiParent = mdiParentForm;
            frmIntonation.Show();

            //frmSpeech.WindowState = FormWindowState.Normal;
            //frmSpeech.WindowState = FormWindowState.Minimized;
            //frmIntonation.WindowState = FormWindowState.Maximized;

            frmIntonation.Activate();
        }
        
        /// <summary>Intonation</summary>
        static PratiCanto.frmComposer frmComposer;
        /// <summary>Show sustained vowel form</summary>
        public static void ShowFormComposer()
        {
            if (frmComposer == null || frmComposer.IsDisposed) frmComposer = new PratiCanto.frmComposer();
            if (mdiParentForm != null) frmComposer.MdiParent = mdiParentForm;
            frmComposer.Show();
            frmComposer.Activate();
        }

        /// <summary>Inspector</summary>
        static PratiCanto.frmInspector frmInspector;
        /// <summary>Show sustained vowel form</summary>
        public static void ShowFormInspector()
        {
            if (frmInspector == null || frmInspector.IsDisposed) frmInspector = new PratiCanto.frmInspector();
            if (mdiParentForm != null) frmInspector.MdiParent = mdiParentForm;
            frmInspector.Show();
            frmInspector.Activate();
        }

        #region Client form and folder
        /// <summary>Client form</summary>
        public static AudioComparer.frmClient frmC;

        /// <summary>Client folder</summary>
        public static string ClientFolder = Application.StartupPath;

        /// <summary>Default client image</summary>
        public static Bitmap DefaultClientImg;

        /// <summary>Show form to retrieve client data and information</summary>
        public static void ShowClientForm()
        {
            if (frmC == null || frmC.IsDisposed) frmC = new AudioComparer.frmClient();
            //if (mdiParentForm != null) frmC.MdiParent = mdiParentForm;

            frmC.ShowDialog();
            ClientFolder = frmC.GetClientFolder();
            
            if (frmAudioComp != null && !frmAudioComp.IsDisposed) frmAudioComp.SetClient();
            if (frmPrac != null && !frmPrac.IsDisposed) frmPrac.SetClient();
            if (frmVowel != null && !frmVowel.IsDisposed) frmVowel.SetClient();
            if (frmSelfListen != null && !frmSelfListen.IsDisposed) frmSelfListen.SetClient();
            if (frmEloquence != null && !frmEloquence.IsDisposed) frmEloquence.SetClient();
            if (frmIntonation != null && !frmIntonation.IsDisposed) frmIntonation.SetClient();
        }

        /// <summary>Retrieves folder to where audio recordings should be saved</summary>
        public static string getRecFolder()
        {
            string UserFolder;

            if (Directory.Exists(PratiCantoForms.ClientFolder)) UserFolder = PratiCantoForms.ClientFolder;
            else UserFolder = Application.StartupPath + "\\" + PratiCantoForms.ClientFolder;

            string dtString = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');

            return UserFolder + "\\" + dtString;
        }


        #endregion
    }
}
