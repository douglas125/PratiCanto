using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AudioComparer;

namespace VocalGames
{
    public partial class frmChooseGame : Form
    {
        public frmChooseGame()
        {
            InitializeComponent();
        }

        private void btnFreqBird_Click(object sender, EventArgs e)
        {
            (new frmFreqBird(cmbInputDevice.SelectedIndex)).ShowDialog();
        }

        private void frmChooseGame_Load(object sender, EventArgs e)
        {
            //List mics
            List<string> mics = SampleAudio.RealTime.GetMicrophones();
            foreach (string s in mics) cmbInputDevice.Items.Add(s);
            if (mics.Count <= 1) cmbInputDevice.Visible = false;
            if (mics.Count >= 1) cmbInputDevice.SelectedIndex = 0;
        }


    }
}
