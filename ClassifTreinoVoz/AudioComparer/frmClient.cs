using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AudioComparer
{
    public partial class frmClient : Form
    {
        /// <summary>Constructor</summary>
        public frmClient()
        {
            InitializeComponent();
        }

        /// <summary>Path to clients folder</summary>
        string clientsPath;
        private void frmClient_Load(object sender, EventArgs e)
        {
            //client Folder
            clientsPath = System.IO.Directory.GetParent(Application.StartupPath) + "\\Clients";
            if (!Directory.Exists(clientsPath)) clientsPath = Application.StartupPath + "\\Clients";

            //ficha associada: nome, data nascimento, foto?, observação, laudo do medico, observacoes muito importantes
            //homem/mulher/criança/profissinal da voz
            if (cmbVoiceType.SelectedIndex < 0) cmbVoiceType.SelectedIndex = 0;

            InitClientList();
        }

        private void dtBirth_ValueChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;

            int age = today.Year - dtBirth.Value.Year;

            if (dtBirth.Value > today.AddYears(-age))
                age--;

            lblAge.Text = age.ToString();
        }

        #region Client list


        /// <summary>Folders with client information</summary>
        List<DirectoryInfo> clientFolders = new List<DirectoryInfo>();

        /// <summary>Initialize client list</summary>
        void InitClientList()
        {
            lstClient.Items.Clear();
            clientFolders.Clear();
            DirectoryInfo di = new DirectoryInfo(clientsPath);
            if (di.Exists)
            {
                DirectoryInfo[] getclientFolders = di.GetDirectories();
                foreach (DirectoryInfo diClient in getclientFolders)
                {
                    lstClient.Items.Add(diClient.Name);
                    clientFolders.Add(diClient);
                }
            }
            else
            {
                Directory.CreateDirectory(clientsPath);
            }
        }

        #endregion

        #region Add/edit/remove users

        /// <summary>Retrieve selected client folder</summary>
        public string GetClientFolder()
        {
            string p = clientsPath + "\\" + txtClientFolder.Text;
            if (!Directory.Exists(p)) Directory.CreateDirectory(p);

            return p;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            txtClientFolder.Text = txtClientFolder.Text.Trim();
            string newClientPath = clientsPath + "\\" + txtClientFolder.Text;
            if (!Directory.Exists(newClientPath))
            {
                Directory.CreateDirectory(newClientPath);
                InitClientList();

                int idNewClient = -1;
                for (int k = 0; k < lstClient.Items.Count; k++)
                {
                    if (lstClient.Items[k].ToString() == txtClientFolder.Text)
                    {
                        idNewClient = k;
                        break;
                    }
                }

                if (idNewClient >= 0) lstClient.SelectedIndex = idNewClient;
                btnEditUser_Click(sender, e);
                txtName.Text = txtClientFolder.Text;
                txtName.Focus();
            }
            else
            {
                MessageBox.Show(lblClientExists.Text + txtClientFolder.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClientFolder.Focus();
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            string clientPath = clientsPath + "\\" + txtClientFolder.Text;

            if (Directory.Exists(clientPath))
            {
                gbDocs.Enabled = true;
                gbPic.Enabled = true;
                gbPersonalInfo.Enabled = true;

                if (txtName.Text == "") txtName.Text = txtClientFolder.Text;
            }
        }
        private void txtClientFolder_TextChanged(object sender, EventArgs e)
        {
            gbDocs.Enabled = false;
            gbPic.Enabled = false;
            gbPersonalInfo.Enabled = false;
        }
        private void lstClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstClient.SelectedIndex >= 0)
            {
                string selClient = lstClient.Items[lstClient.SelectedIndex].ToString();
                txtClientFolder.Text = selClient;

                DataSet cInfo = LoadClientInfo();
                if (cInfo != null) WriteClientInfo(cInfo.Tables["tblClientInfo"]);
                else
                {
                    txtName.Text = "";
                    txtNotes.Text = "";
                    txtImportantNotes.Text = "";
                }
            }
        }
        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            string newClientPath = clientsPath + "\\" + txtClientFolder.Text;
            if (Directory.Exists(newClientPath))
            {
                DialogResult res = MessageBox.Show(lblConfirmDelete.Text, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    Directory.Delete(newClientPath, true);
                    InitClientList();
                }
            }
        }
        #endregion

        #region Build/retrieve client information

        //Client picture
        private void picClientPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg;*.png;*.bmp;*.jpeg;*.tiff";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string clientPath = clientsPath + "\\" + txtClientFolder.Text;

                File.Copy(ofd.FileName, clientPath + "\\client" + txtClientFolder.Text + ".png");
                Bitmap bmp = new Bitmap(ofd.FileName);
                picClientPic.Image = bmp;
            }
        }

        /// <summary>Loads client information, picture and all that is available in the folder</summary>
        private DataSet LoadClientInfo()
        {
            string clientPath = clientsPath + "\\" + txtClientFolder.Text;
            try
            {
                //client documents
                DirectoryInfo di = new DirectoryInfo(clientPath);
                FileInfo[] clientDocs = di.GetFiles();
                lstClientDocs.Items.Clear();
                foreach (FileInfo ff in clientDocs) lstClientDocs.Items.Add(ff.Name);

                //client picture
                FileInfo[] fi = di.GetFiles("*.png");
                FileInfo fClientPic = fi.Where(i => i.Name.ToLower().StartsWith("client")).FirstOrDefault();
                if (fClientPic != null)
                {
                    Bitmap bmp = new Bitmap(fClientPic.FullName);
                    picClientPic.Image = bmp;
                }
                else picClientPic.Image = null;
                picClientPic.Refresh();

                //client information
                DataSet d = new DataSet();
                fi = di.GetFiles("*.xml");

                //load information
                FileInfo fClientInfo = fi.Where(i => i.Name.ToLower().StartsWith("client")).FirstOrDefault();
                if (fClientInfo != null)
                {
                    d.ReadXml(fClientInfo.FullName);
                    return d;
                }
                else return null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        /// <summary>Saves client information to a xml file in the folder</summary>
        private void SaveClientInfo()
        {
            string clientPath = clientsPath + "\\" + txtClientFolder.Text;
            try
            {
                if (Directory.Exists(clientPath))
                {
                    DataSet d = new DataSet();
                    d.Tables.Add(BuildClientInfo());
                    d.WriteXml(clientPath + "\\client" + txtClientFolder.Text + ".xml", XmlWriteMode.WriteSchema);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>Build client information from form</summary>
        private DataTable BuildClientInfo()
        {
            string[] fields = new string[] { "Name", "Birth", "intVoiceType", "ImportantNotes", "Notes" };
            DataTable tblClient = XMLFuncs.CreateNewTable("tblClientInfo", fields);

            DataRow r = tblClient.NewRow();
            tblClient.Rows.Add(r);
            r["Name"] = txtName.Text;
            r["Birth"] = dtBirth.Value.Ticks.ToString(); //read with new DateTime(ticks)
            r["intVoiceType"] = cmbVoiceType.SelectedIndex;
            r["ImportantNotes"] = txtImportantNotes.Text;
            r["Notes"] = txtNotes.Text;

            return tblClient;
        }

        /// <summary>Write client information to form</summary>
        private void WriteClientInfo(DataTable tblClient)
        {
            try
            {

                DataRow r = tblClient.Rows[0];

                txtName.Text = r["Name"].ToString();

                long ticks = long.Parse(r["Birth"].ToString());
                dtBirth.Value = new DateTime(ticks);

                int vtype = (int)r["intVoiceType"];
                cmbVoiceType.SelectedIndex = vtype;
                txtImportantNotes.Text = r["ImportantNotes"].ToString();
                txtNotes.Text = r["Notes"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Leave events to save information
        private void txtName_Leave(object sender, EventArgs e)
        {
            SaveClientInfo();
        } 
        private void txtImportantNotes_Leave(object sender, EventArgs e)
        {
            SaveClientInfo();
        }
        private void txtNotes_Leave(object sender, EventArgs e)
        {
            SaveClientInfo();
        }
        private void dtBirth_Leave(object sender, EventArgs e)
        {
            SaveClientInfo();
        }
        private void cmbVoiceType_Leave(object sender, EventArgs e)
        {
            SaveClientInfo();
        }
        #endregion

        #region Add and view documents
        private void btnAddDoc_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            string folder = GetClientFolder();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string s in ofd.FileNames)
                {
                    FileInfo fi = new FileInfo(s);
                    if (!File.Exists(folder + "\\" + fi.Name)) File.Copy(s, folder + "\\" + fi.Name);
                    else
                    {
                        string name = Path.GetFileNameWithoutExtension(fi.FullName);
                        string extension = Path.GetExtension(fi.FullName);
                        int ii = 1;
                        while (File.Exists(folder + "\\" + name + "(" + ii.ToString() + ")" + extension)) ii++;
                        File.Copy(s, folder + "\\" + name + "(" + ii.ToString() + ")" + extension);
                    }
                }
                LoadClientInfo();
            }
        }
        
        private void lstClientDocs_DoubleClick(object sender, EventArgs e)
        {
            if (lstClientDocs.SelectedIndex < 0) return;
            string file = lstClientDocs.Items[lstClientDocs.SelectedIndex].ToString();
            string folder = GetClientFolder();
            try
            {
                System.Diagnostics.Process.Start(folder + "\\" + file);
            }
            catch
            {
            }
        }
        #endregion

        private void btnViewFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string s = GetClientFolder();
                System.Diagnostics.Process.Start(s);
            }
            catch
            {
            }
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Has a valid client been selected?
            int idClient = -1;
            for (int k = 0; k < lstClient.Items.Count; k++)
            {
                if (lstClient.Items[k].ToString() == txtClientFolder.Text)
                {
                    idClient = k;
                    break;
                }
            }

            if (idClient >= 0) SaveClientInfo();
            else
            {
                e.Cancel = true;
                MessageBox.Show(lblSelectClientFromList.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void lstClient_DoubleClick(object sender, EventArgs e)
        {
            btnEditUser_Click(sender, e);
        }

        #region Cosmetics
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        private void gbPersonalInfo_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        private void gbPic_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }

        private void gbDocs_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        #endregion





    }
}
