using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace ClassifTreinoVoz
{
    /// <summary>Self update routine</summary>
    public static class SelfUpdate
    {
        //information file = "latest.info"

        /// <summary>Need update?</summary>
        public static bool NeedsUpdate = false;

        /// <summary>Remote info file</summary>
        static string downloadedinfoFile = Application.StartupPath + "\\PratiCantoBuild.infosrv";

        /// <summary>Server path</summary>
        static string serverPath = "http://www.cmsoft.com.br/AudioSofts/PratiCanto/";

        /// <summary>Checks files to be downloaded</summary>
        /// <param name="updateAvailableStr">String: Update is available. Download?</param>
        public static void FetchBuild(string updateAvailableStr)
        {
            ShowLatestInfo();

            updtAvailableStr = updateAvailableStr;
            try
            {
                using (WebClient client = new WebClient())
                {
                    if (File.Exists(downloadedinfoFile)) File.Delete(downloadedinfoFile);
                    client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    client.DownloadFileAsync(new System.Uri(serverPath + "PratiCantoBuild.info"), downloadedinfoFile);
                }
            }
            catch
            {
            }

        }

        private static void ShowLatestInfo()
        {
            if (File.Exists(Application.StartupPath + "\\latest.info"))
            {
                string sMsg = File.ReadAllText(Application.StartupPath + "\\latest.info", Encoding.UTF8);
                MessageBox.Show(sMsg, "PratiCanto", MessageBoxButtons.OK);
                try
                {
                    File.Delete(Application.StartupPath + "\\latest.info");
                }
                catch { }
            }
        }

        /// <summary>String to ask for update</summary>
        static string updtAvailableStr;

        static void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                if (!File.Exists(downloadedinfoFile)) return;

                string downloadedBuildInfo = File.ReadAllText(downloadedinfoFile);
                //could not download from server
                if (downloadedBuildInfo == "") return;

                string localBuildInfoFile = Application.StartupPath + "\\PratiCantoBuild.info";
                string localBuildInfo = "";
                if (File.Exists(localBuildInfoFile))
                {
                    localBuildInfo = File.ReadAllText(localBuildInfoFile);
                    if (downloadedBuildInfo != localBuildInfo) NeedsUpdate = true;
                }
                else NeedsUpdate = true;

                if (NeedsUpdate)
                {
                    string[] files = new string[1];

                    try { files = downloadedBuildInfo.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries); }
                    catch { }
                    
                    if ((files.Length == 2) || (files.Length > 2 && MessageBox.Show(updtAvailableStr, "PratiCanto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) )
                    {
                        bool success = true;
                        for (int k = 1; k < files.Length; k++)
                        {
                            string serverFile = serverPath + files[k].Replace("\\", "/");
                            string localFile = Application.StartupPath + "\\" + files[k];
                            string tempFile = "";
                            try
                            {
                                using (WebClient client = new WebClient())
                                {
                                    tempFile = localFile + "tmp";
                                    if (File.Exists(tempFile)) File.Delete(tempFile);
                                    client.DownloadFile(serverFile, tempFile);

                                    if (File.Exists(localFile + "old")) File.Delete(localFile + "old");
                                    if (File.Exists(localFile)) File.Move(localFile, localFile + "old");
                                    new System.IO.FileInfo(localFile).Directory.Create();
                                    File.Move(tempFile, localFile);
                                }
                            }
                            catch (Exception ex)
                            {
                                if (File.Exists(tempFile)) File.Delete(tempFile);
                                MessageBox.Show("Fail: " + localFile + "\r\n" + ex.ToString());
                                success = false;
                            }
                        }


                        if (success)
                        {
                            //Replace current build information
                            File.Delete(localBuildInfoFile);
                            File.Move(downloadedinfoFile, localBuildInfoFile);
                            ShowLatestInfo();
                            if (files.Length > 2) Application.Exit();

                        }

                        
                    }
                }
            }
            catch
            {
                //can't know if update is needed
            }
        }
    }
}
