using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Management;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using System.Net;

namespace AudioComparer
{
    /// <summary>Generation and validation of software key</summary>
    public class SoftwareKey
    {
        /// <summary>Saves PC Information</summary>
        public static void SavePCInfo()
        {
            string hardInfo = GetHardwareInfo().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)[1].Replace("|", "");
            string infoFileName = hardInfo + ".hnf";

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = infoFileName;
            sfd.Filter = "Info|*.hnf";
            if (STAShowDialog(sfd) == DialogResult.OK)
            {
                SoftwareKey.SaveHardwareInfo(sfd.FileName);
            }
        }

        /// <summary>Find license text</summary>
        public static string lblFindLicenseTxt = "Find license?";
        /// <summary>Not licensed text</summary>
        public static string lblNotRegisteredTxt = "Not registered";


        /// <summary>Checks if license is valid</summary>
        /// <param name="header">Module name</param>
        /// <param name="silent">Show save license file dialog? silent = true => does not show</param>
        /// <returns></returns>
        public static bool CheckLicense(string header, bool silent)
        {
            //check license
            bool validLicense = false;
            string licfilename = "info.hnflic";
            string infoFileName = "info.hnf";

            try
            {
                //public key
                string key = @"<RSAKeyValue><Modulus>un326Vz7ejw1Cj15VWVPby2lZeMY9fiSXedbrfkNVaqzpRj52nXLy13VYjnruzskuuGL/uPszGbQPUw3K6pfIbPZMsohQSQrR/7h2bh3ZJiOFDkFcKO/vcDIAbXZaT5o7uKPYxtMPDC9lGqtJvQ3rPQyaNbVfwNVQXTpzfs1f38=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
                SoftwareKey sk = new SoftwareKey(key);


                string[] files = Directory.GetFiles(Application.StartupPath, "*.hnflic");
                //if (files.Length == 0)
                {
                    //if no files are found, try to grab from server
                    try
                    {
                        //using (WebClient client = new WebClient())
                        //{
                        //    string licserverPath = "http://www.cmsoft.com.br/AudioSofts/PratiCanto/licensesv2/";
                        //    string hardInfo = GetHardwareInfo().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)[1].Replace("|", "");
                        //    licfilename = hardInfo + header + ".hnflic";
                        //    infoFileName = hardInfo + header + ".hnf";

                        //    string localFilename = Application.StartupPath + "\\" + licfilename;
                        //    string serverFilename = licserverPath + licfilename;

                        //    if (!File.Exists(localFilename)) //File.Delete(localFilename);
                        //        client.DownloadFile(serverFilename, localFilename);
                        //}
                    }
                    catch
                    {
                    }
                    files = Directory.GetFiles(Application.StartupPath, "*.hnflic");
                }


                foreach (string s in files)
                {
                    if (sk.LicenseCheck(s, header))
                    {
                        validLicense = true;
                        break;
                    }
                }
            }
            catch
            {
            }

            if (!validLicense && !silent)
            {
                try
                {
                    if (MessageBox.Show(lblFindLicenseTxt, "License", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.FileName = licfilename;
                        ofd.Filter = "Info|*.hnflic";

                        if (STAShowDialog(ofd) == DialogResult.OK)
                        {
                            FileInfo f = new FileInfo(ofd.FileName);
                            File.Copy(ofd.FileName, Application.StartupPath + "\\" + f.Name + (f.Extension.ToLower() == ".hnflic" ? "" : ".hnflic"));

                            Application.Exit();
                            return false;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show(lblNotRegisteredTxt, "License", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                        {
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.FileName = infoFileName;
                            sfd.Filter = "Info|*.hnf";
                            if (STAShowDialog(sfd) == DialogResult.OK)
                            {
                                SoftwareKey.SaveHardwareInfo(sfd.FileName);
                            }
                        }
                    }
                }
                catch
                {
                }

                Application.Exit();
                return false;

            }

            return validLicense;
        }

        private static DialogResult STAShowDialog(FileDialog dialog)
        {
            DialogState state = new DialogState();
            state.dialog = dialog;
            System.Threading.Thread t = new System.Threading.Thread(state.ThreadProcShowDialog);
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            t.Join();
            return state.result;
        }
        /// <summary>Class to help show dialog in other context</summary>
        public class DialogState
        {
            /// <summary>Dialog result</summary>
            public DialogResult result;
            /// <summary>Dialog box</summary>
            public FileDialog dialog;
            /// <summary>Thread to show dialog</summary>
            public void ThreadProcShowDialog()
            {
                result = dialog.ShowDialog();
            }

        }


        #region Hardware information

        /// <summary>Saves hardware information to a file. Extension .hnf</summary>
        /// <param name="file">File to contain hardware info</param>
        public static void SaveHardwareInfo(string file)
        {
            File.WriteAllText(file, GetHardwareInfo());
        }

        /// <summary>Already retrieved hardware info</summary>
        public static string AllHardwareInfo = "";

        /// <summary>Retrieves relevant hardware information</summary>
        /// <returns></returns>
        public static string GetHardwareInfo()
        {
            if (AllHardwareInfo != "") return AllHardwareInfo;
            //string[] keys = searchkeys.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            //ManagementObjectCollection[] values = new ManagementObjectCollection[keys.Length];

            //for (int k = 0; k < keys.Length; k++)
            //{
            //    ManagementClass mc = new ManagementClass(keys[k]);
            //    values[k] = mc.GetInstances();
            //}

            string processor = GetProcessors();
            string hdSerial = GetHDs();

            string ans = "";
            ans += Application.ProductName;
            ans += Application.ProductVersion.Substring(0, 4) + "\r\n";
            ans += "Processors|" + processor + "\r\n" + "HD|" + hdSerial;
            AllHardwareInfo = ans;
            return ans;
        }

        /// <summary>Retrieves information about processors</summary>
        public static string GetProcessors()
        {
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            string processor = "";
            foreach (ManagementObject mo in moc)
            {
                if (processor != "") processor += "|";
                processor += mo.Properties["processorID"].Value.ToString();
            }

            return processor;
        }

        /// <summary>Retrieves information about HDs</summary>
        /// <returns></returns>
        public static string GetHDs()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            string hdSerial = "";
            foreach (ManagementObject strt in mcol)
            {
                string serial = Convert.ToString(strt["VolumeSerialNumber"]);
                if (serial != "")
                {
                    if (hdSerial != "") hdSerial += "|";
                    hdSerial += serial;
                }
                break;
            }

            return hdSerial;
        }

        #endregion

        #region Key generation

        /// <summary>Generates a new public-private key pair</summary>
        /// <param name="filePublicPrivate">File to store public and private keys</param>
        /// <param name="filePublic">File to store public key</param>
        public static void GenNewPrivatePublicRSAKeyPair(string filePublicPrivate, string filePublic)
        {
            RSACryptoServiceProvider newRSA = new RSACryptoServiceProvider();

            string keypublicprivate = newRSA.ToXmlString(true);
            string keypublic = newRSA.ToXmlString(false);

            File.WriteAllText(filePublicPrivate, keypublicprivate);
            File.WriteAllText(filePublic, keypublic);
        }

        #endregion

        /// <summary>Crypto service provider</summary>
        public RSACryptoServiceProvider csp;

        /// <summary>Initializes instance of softwarekey</summary>
        /// <param name="key">RSA parameters</param>
        public SoftwareKey(string key)
        {
            string keys = key;

            csp = new RSACryptoServiceProvider();
            csp.FromXmlString(keys);
        }

        /// <summary>Generate license file based on hardware info file</summary>
        /// <param name="hardwareInfoFile">Information about hardware. Extension: .hnf</param>
        /// <param name="licenseFile">Encrypted file. Extension: .lic</param>
        /// <returns></returns>
        public void GenLicenseFile(string hardwareInfoFile, string licenseFile)
        {
            string hardData = File.ReadAllText(hardwareInfoFile);
            byte[] byteHardData = System.Text.Encoding.UTF8.GetBytes(hardData);

            byte[] cryptoData = csp.SignData(byteHardData, CryptoConfig.MapNameToOID("SHA512"));


            File.WriteAllBytes(licenseFile, cryptoData);

        }

        /// <summary>Checks license validity for this computer</summary>
        public bool LicenseCheck(string licenseFile, string header)
        {
            try
            {
                byte[] cryptoData = File.ReadAllBytes(licenseFile);

                string hardData = GetHardwareInfo();
                byte[] byteHardData = System.Text.Encoding.UTF8.GetBytes(header + hardData);

                bool verif = csp.VerifyData(byteHardData, CryptoConfig.MapNameToOID("SHA512"), cryptoData);

                return verif;
            }
            catch
            {
                return false;
            }
        }


    }
}
