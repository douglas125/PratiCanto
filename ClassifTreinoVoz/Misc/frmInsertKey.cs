using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace PratiCanto
{
    public partial class frmInsertKey : Form
    {
        /// <summary>Was license retrieval successful?</summary>
        public bool retrievedLicense = false;

        /// <summary>Refresh buttons with license files</summary>
        public ClassifTreinoVoz.frmChooseModV2 frmChoose;

        public frmInsertKey()
        {

            InitializeComponent();
        }

        string machineId;

        /// <summary>Local IP</summary>
        string localIP = "";
        /// <summary>Messages for the user</summary>
        string[] msgs;
        private void frmInsertKey_Load(object sender, EventArgs e)
        {
            Adjustbtns();
            msgs = lblMsgs.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            machineId = AudioComparer.SoftwareKey.GetHardwareInfo().Replace("\r\n", "****");
            localIP = GetPublicIP();
        }
        #region Cosmetics
        private void panInfo_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(230, 230, 230), panInfo.Width, panInfo.Height, true);
        }
        void Adjustbtns()
        {
            LayoutFuncs.AdjustButtonGraphic(btnGetLicense, true);

            btnGetLicense.Image = LayoutFuncs.MakeDegrade(Color.FromArgb(218, 30, 30), Color.Black, btnGetLicense.Width, btnGetLicense.Height, true);

            LayoutFuncs.AdjustButtonGraphic(btnCancel, false);
        }
    

        #endregion

        public static string GetPublicIP()
        {
            try
            {
                string url = "http://checkip.dyndns.org";
                System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                string response = sr.ReadToEnd().Trim();
                string[] a = response.Split(':');
                string a2 = a[1].Substring(1);
                string[] a3 = a2.Split('<');
                string a4 = a3[0];
                return a4;
            }
            catch
            {
                return "";
            }
        }

        #region Request license from Azure

        private void btnGetLicense_Click(object sender, EventArgs e)
        {
            AzureIsLicenseValid();
        }

        private void AzureAcquireLicense(string reqData)
        {
            HttpWebRequest HWR_ReqLicense = null;

            HWR_ReqLicense = (HttpWebRequest)HttpWebRequest.Create("https://praticantoservlic.azurewebsites.net/api/AcquireLicensePratiCanto?code=MGod/mOqDDVUbMshmUCHO/9k9o9mAaSYrTC05U3Yf1sXPcM2HmnSTQ==");

            HWR_ReqLicense.Method = "POST";
            HWR_ReqLicense.ContentType = "application/json";
            //_HWR_SpeechToText.Credentials = CredentialCache.DefaultCredentials;

            using (var stream = HWR_ReqLicense.GetRequestStream())
            {
                var data = Encoding.ASCII.GetBytes(reqData);
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)HWR_ReqLicense.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().Replace("\"", "");


            string mainFileName = machineId.Split(new string[] { "****" }, StringSplitOptions.RemoveEmptyEntries)[1].Replace("|", "");

            string[] licModules = responseString.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);

            for (int k = 1; k < licModules.Length; k += 2)
            {
                string fileName = Application.StartupPath + "\\" + mainFileName + licModules[k] + ".hnflic";
                byte[] cryptoData = Convert.FromBase64String(licModules[k + 1]);
                File.WriteAllBytes(fileName, cryptoData);
            }


            frmChoose.RefreshBtnEnabled();
            this.Close();
            this.retrievedLicense = true;
        }

        private void AzureIsLicenseValid()
        {
            HttpWebRequest HWR_ReqLicense = null;

            HWR_ReqLicense = (HttpWebRequest)HttpWebRequest.Create("https://praticantoservlic.azurewebsites.net/api/ConfirmLicensePratiCanto?code=1XBaKpT2We1mGaOMFmEkKhtE/iugk1OI4MaonruJA0Z3ccaG6/hdKw==");

            HWR_ReqLicense.Method = "POST";
            HWR_ReqLicense.ContentType = "application/json";
            //_HWR_SpeechToText.Credentials = CredentialCache.DefaultCredentials;


            string contents = @"
{
    'UserName': 'TXTUSERNAME',
    'RowKey': 'SOFTWAREKEY',
    'MachineId':'MACHINEID'
}".Replace("SOFTWAREKEY", txtLicenseKey.Text).Replace("MACHINEID", machineId).Replace("TXTUSERNAME", txtUserName.Text + "|" + localIP + "|");


            using (var stream = HWR_ReqLicense.GetRequestStream())
            {
                var data = Encoding.ASCII.GetBytes(contents);
                stream.Write(data, 0, data.Length);
            }

            try
            {
                var response = (HttpWebResponse)HWR_ReqLicense.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd().Replace("\"", "");

                if (!responseString.StartsWith("Key is valid for:"))
                {
                    MessageBox.Show(msgs[2], this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string licensedPC = responseString.Split(':')[1].Trim();

                    if (licensedPC == "")
                    {
                        //license has not been used
                        DialogResult dr = MessageBox.Show(msgs[1], this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            //retrieve licenses
                            AzureAcquireLicense(contents);
                        }
                    }
                    else if (licensedPC == machineId)
                    {
                        //licensed for this PC, just download licenses
                        AzureAcquireLicense(contents);
                    }
                    else //license is for another PC
                    {
                        MessageBox.Show(msgs[3], this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show(msgs[0], this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
