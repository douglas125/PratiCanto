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
    public partial class frmMDIParent : Form
    {
        public frmMDIParent()
        {
            InitializeComponent();
        }

        private void frmMDIParent_Load(object sender, EventArgs e)
        {
            //senha Google praticantodsr@gmail.com: e pagseguro praticanto1@praticanto.com.br
            //QualiVoz2017

            PratiCantoForms.mdiParentForm = this;
            //bool ValidLicense = AudioComparer.SoftwareKey.CheckLicense(lblFindLicense.Text, lblNotRegistered.Text);

            Form frmChoose = new frmChooseModV2(); // frmChooseActivity();

            frmChoose.MdiParent = this;
            frmChoose.Show();
        }

  
    }
}
