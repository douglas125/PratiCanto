using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PratiCanto
{
    public partial class frmVoiceCommand : Form
    {
        public frmVoiceCommand()
        {
            InitializeComponent();
        }

        #region Cosmetics
        private void gbVoiceCmd_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        private void gbActions_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        private void gbMonitor_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        #endregion

        private void frmVoiceCommand_Load(object sender, EventArgs e)
        {
            #region Cosmetics
            btnOpen.FlatStyle = FlatStyle.Flat;
            btnOpen.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            #endregion
        }


    }
}
