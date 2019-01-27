using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClassifTreinoVoz
{
    public partial class frmEditNotes : Form
    {
        public frmEditNotes()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
