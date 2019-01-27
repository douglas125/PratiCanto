using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassifTreinoVoz;
using AudioComparer;
using System.IO;
using System.Net;

namespace PratiCanto
{
    public partial class frmSpeechRecognizer : Form
    {
        public frmSpeechRecognizer()
        {
            InitializeComponent();
        }
        private void btnClient_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowClientForm();
        }

        private void frmSelfListen_Load(object sender, EventArgs e)
        {
            delegateAddText = AddText;
            delegateUpdateQueue = UpdateQueue;

            //List mics
            List<string> mics = SampleAudio.RealTime.GetMicrophones();
            foreach (string s in mics) cmbInputDevice.Items.Add(s);
            if (mics.Count <= 1) cmbInputDevice.Visible = false;
            if (mics.Count >= 1) cmbInputDevice.SelectedIndex = 0;

            bgW.RunWorkerAsync();
        }
        public void SetClient()
        {
            if (PratiCantoForms.frmC == null || PratiCantoForms.frmC.IsDisposed) return;
            if (PratiCantoForms.frmC.picClientPic.Image != null)
            {
                Bitmap bmp = new Bitmap(btnClient.Width, btnClient.Height);
                Graphics g = Graphics.FromImage(bmp);

                g.DrawImage(PratiCantoForms.frmC.picClientPic.Image, 0, 0, bmp.Width, bmp.Height);

                btnClient.Image = bmp;
            }
            else btnClient.Image = PratiCantoForms.DefaultClientImg;

            lblNote.Text = PratiCantoForms.frmC.txtImportantNotes.Text;
        }


        #region Record
        //SampleAudioGL saGL;
        SampleAudio.RealTime rt;
        bool recording = false;
        SampleAudio curSample;
        string recFileName;
        private void btnRec_Click(object sender, EventArgs e)
        {
            if (!recording)
            {
                string UserFolder;
                if (Directory.Exists(PratiCantoForms.ClientFolder)) UserFolder = PratiCantoForms.ClientFolder;
                else UserFolder = Application.StartupPath + "\\" + PratiCantoForms.ClientFolder;

                if (!Directory.Exists(UserFolder)) Directory.CreateDirectory(UserFolder);

                string recFolder = PratiCantoForms.getRecFolder();
                if (!Directory.Exists(recFolder)) Directory.CreateDirectory(recFolder);

                recFileName = recFolder + "\\SustainedVowel" + DateTime.Now.Hour.ToString() + "h" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + "min" + DateTime.Now.Second.ToString().PadLeft(2, '0') + "s.wav";
                //saGL.ViewFormants = false;
                rt = new SampleAudio.RealTime();
                rt.freqAmost = 16000;
                rt.OnDataReceived = SoundReceived;
                rt.StartRecording(cmbInputDevice.SelectedIndex);

                btnRec.Image = picStop.Image;

                recording = true;
            }
            else
            {
                recording = false;
                rt.StopRecording();

                //saGL.ResetRealTimeDrawing();

                if (curSample != null) curSample.Dispose();
                curSample = new SampleAudio(rt);
                //saGL.SetSampleAudio(curSample);
                btnRec.Image = picRec.Image;

                //SaveFileDialog sfd = new SaveFileDialog();
                //sfd.Filter = "wav|*.wav";
                //if (sfd.ShowDialog() == DialogResult.OK)
                //{
                //    System.IO.FileStream str = new FileStream(sfd.FileName, FileMode.Create);

                //    rt.SavePart(0, rt.time[rt.time.Count - 1], str);
                //}
            }
        }

        private void SoundReceived()
        {
            //saGL.DrawRealTime(rt);
        }
        #endregion

        #region List of tasks
        /// <summary>Transcribe</summary>
        /// <param name="base64FileData">File data in base64 format</param>
        /// <param name="bitRate">Bit rate - 16000 or 44100</param>
        private void Transcribe(string base64FileData, string bitRate, string header)
        {
            GoogleTranscribeSpeechOBJ gtso = new GoogleTranscribeSpeechOBJ(base64FileData, int.Parse(bitRate), header);
            lstGTSO.Add(gtso);
        }

        List<GoogleTranscribeSpeechOBJ> lstGTSO = new List<GoogleTranscribeSpeechOBJ>();

        #endregion

        private delegate void voidDel();
        private voidDel delegateAddText;
        private voidDel delegateUpdateQueue;
        string txtToAdd;
        string timelog;
        float curIntens;
        private void AddText()
        {
            lblCurIntens.Text = curIntens.ToString();
            if (txtToAdd != "" || timelog != "") txtAns.Text = txtToAdd + timelog + txtAns.Text + "\r\n\r\n";
        }
        private void UpdateQueue()
        {
            lblInQueue.Text = lstGTSO.Count.ToString();
        }


        private void btnFromAudio_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "wav|*.wav";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fName in ofd.FileNames)
                    {
                        //string fName = ofd.FileName;

                        //byte[] file = File.ReadAllBytes(ofd.FileName);
                        //string fileData = System.Convert.ToBase64String(File.ReadAllBytes(ofd.FileName));
                        string fileData = System.Convert.ToBase64String(File.ReadAllBytes(fName));

                        Transcribe(fileData, "44100", fName + ":");

                        lblInQueue.Text = lstGTSO.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        float threshVal = 1000;

        private void bgW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    this.Invoke(delegateUpdateQueue);
                    System.Threading.Thread.Sleep(1200);
                    for (int k = 0; k < lstGTSO.Count; k++)
                    {
                        if (!lstGTSO[k].IsProcessing) lstGTSO[k].StartProcessing();

                        if (!lstGTSO[k].DoneProcessing)
                        {
                            List<string> transcript;
                            if (lstGTSO[k].TryRetrieveResults(out transcript))
                            {
                                txtToAdd = GoogleTranscribeSpeechOBJ.concat(transcript);
                                this.Invoke(delegateAddText);

                                lstGTSO.RemoveAt(k);
                                k--;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void numIntensThresh_ValueChanged(object sender, EventArgs e)
        {
            threshVal = (float)numIntensThresh.Value;
        }

        #region Push-to-talk
        float tStart = 0, tStop = 0;
        private void btnPushToTalk_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void btnPushToTalk_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void btnPushToTalk_MouseDown(object sender, MouseEventArgs e)
        {
            if (rt != null && rt.time.Count > 0)
            {
                tStart = rt.time[rt.time.Count - 1];
            }
        }
        private void btnPushToTalk_MouseUp(object sender, MouseEventArgs e)
        {
            if (rt != null && rt.time.Count > 0)
            {
                tStop = rt.time[rt.time.Count - 1];
                TranscribePTT();
            }
        }
        void TranscribePTT()
        {
            try
            {
                string fName = Application.StartupPath + "\\tempSpeechRecog.wavTemp";
                if (File.Exists(fName)) File.Delete(fName);

                //SaveFileDialog sfd = new SaveFileDialog();
                //sfd.Filter = "wav|*.wav";
                //if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(fName, FileMode.Create))
                    {
                        rt.SavePart(tStart, tStop, fs);
                    }
                    

                    //byte[] file = File.ReadAllBytes(ofd.FileName);
                    //string fileData = System.Convert.ToBase64String(File.ReadAllBytes(ofd.FileName));

                    string fileData = System.Convert.ToBase64String(File.ReadAllBytes(fName));
                    if (File.Exists(fName)) File.Delete(fName);

                    Transcribe(fileData, "16000", Math.Round(tStart, 2).ToString() + "s até " + Math.Round(tStop, 2).ToString() + "s:");
                }
            }
            catch (Exception ex)
            {
            }
        }


        #endregion

        /// <summary>Google transcription object</summary>
        public class GoogleTranscribeSpeechOBJ
        {
            /// <summary>Concatenate multiple strings</summary>
            public static string concat(List<string> lst)
            {
                string s = "";
                foreach (string ss in lst) s += ss;
                return s;
            }

            /// <summary>Google speech api key</summary>
            public static string GOOGLEAPIKEY = "AIzaSyCX01Jm6k4Jm6IFXf8Yh98HBG4FHXnuEM0";
            /// <summary>How many seconds to wait before next retry?</summary>
            public static int SECONDSTORETRY = 35;
            /// <summary>How many seconds to wait before try?</summary>
            public static int MINSECONDSTOTRY = 25;

            /// <summary>Creates a transcription object</summary>
            /// <param name="base64audiodata">Audio data in base64</param>
            /// <param name="bitRate">Bit rate - usually 44100 or 16000</param>
            /// <param name="header">Optional header to identify output string</param>
            public GoogleTranscribeSpeechOBJ(string base64audiodata, int bitRate, string header)
            {
                this.Base64AudioData = base64audiodata;
                this.BitRate = bitRate;
                this.Header = header;
            }

            /// <summary>Header</summary>
            private string Header;

            /// <summary>Audio bitrate</summary>
            private int BitRate;

            /// <summary>Audio data in Base64 format</summary>
            private string Base64AudioData;

            /// <summary>Google operation name</summary>
            private string OperationName;


            /// <summary>Starts processing this data using Speech API</summary>
            public void StartProcessing()
            {
                if (isProcessing || doneProcessing) return;
                isProcessing = true;
                try
                {
                    string contents = @"
{ 
  'config': 
  { 
    'encoding': 'LINEAR16', 
    'sampleRate': 'AUDIOBITRATE', 
    'languageCode': 'pt-BR'
  }, 
  'audio': 
  { 
    'content': 'AUDIOCONTENT'
  }
}".Replace("AUDIOCONTENT", Base64AudioData).Replace("AUDIOBITRATE", BitRate.ToString());


                    HttpWebRequest _HWR_SpeechToText = null;

                    _HWR_SpeechToText =
                                (HttpWebRequest)HttpWebRequest.Create(
                                    "https://speech.googleapis.com/v1beta1/speech:asyncrecognize?&key=" + GOOGLEAPIKEY);

                    _HWR_SpeechToText.Method = "POST";
                    _HWR_SpeechToText.ContentType = "application/json";
                    //_HWR_SpeechToText.Credentials = CredentialCache.DefaultCredentials;


                    using (var stream = _HWR_SpeechToText.GetRequestStream())
                    {
                        var data = Encoding.ASCII.GetBytes(contents);
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)_HWR_SpeechToText.GetResponse();

                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    this.OperationName = responseString.Split(new string[] { "\n", "\"name\":", "{", "}", " ", "\"" }, StringSplitOptions.RemoveEmptyEntries)[0];

                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }

            #region Retrieve results

            /// <summary>Control request times</summary>
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            /// <summary>Time of last attempt in seconds</summary>
            double lastAttemptTime = double.NegativeInfinity;

            /// <summary>Attempt to retrieve processed results</summary>
            public bool TryRetrieveResults(out List<string> Transcripts)
            {
                Transcripts = null;


                //preliminaries - control retry time, do not ask for results if they are available
                if (doneProcessing)
                {
                    Transcripts = processResult(this.Header, this.responseString);
                    return true;
                }
                if (!sw.IsRunning) sw.Start();
                if (sw.Elapsed.TotalSeconds < MINSECONDSTOTRY || sw.Elapsed.TotalSeconds - lastAttemptTime < SECONDSTORETRY)
                    return false;

                lastAttemptTime = sw.Elapsed.TotalSeconds;

                try
                {
                    HttpWebRequest _HWR_SpeechToText = null;

                    _HWR_SpeechToText =
                                (HttpWebRequest)HttpWebRequest.Create(
                                    "https://speech.googleapis.com/v1beta1/operations/" + OperationName + "?key=" + GOOGLEAPIKEY);

                    _HWR_SpeechToText.Method = "GET";

                    var response = (HttpWebResponse)_HWR_SpeechToText.GetResponse();
                    this.responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    if (responseString.Contains("\"done\": true"))
                    {
                        doneProcessing = true;
                        isProcessing = false;
                        Transcripts = processResult(this.Header, this.responseString);
                    }

                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                return doneProcessing;
            }

            /// <summary>Returns list of transcripts</summary>
            private static List<string> processResult(string header, string responseString)
            {
                List<string> Transcripts = new List<string>();

                //results should have 2 entries - header, transcript and confidence value
                string[] results = responseString.Split(new string[] { "\"transcript\":", "\"confidence\":" },
                    StringSplitOptions.RemoveEmptyEntries);

                for (int k = 1; k < results.Length; k += 2) Transcripts.Add(results[k].Replace("\"", "").Replace("\n", "\r\n").Replace(",", ""));

                if (Transcripts.Count >= 1) Transcripts[0] = header + Transcripts[0];
                return Transcripts;
            }

            /// <summary>Raw response string</summary>
            private string responseString = "";

            /// <summary>Retrieves raw response string</summary>
            public string GetResponseString
            {
                get
                {
                    return responseString;
                }
            }
            #endregion

            #region Status
            /// <summary>Processing audio?</summary>
            private bool isProcessing;

            /// <summary>Done processing this audio?</summary>
            private bool doneProcessing;

            /// <summary>Exception string</summary>
            private Exception exception;

            /// <summary>Is this audio being processed?</summary>
            public bool IsProcessing
            {
                get
                {
                    return isProcessing;
                }
            }

            /// <summary>Done processing this audio?</summary>
            public bool DoneProcessing
            {
                get
                {
                    return doneProcessing;
                }
            }

            /// <summary>Returns exception if one was generated during requests</summary>
            public Exception GetException
            {
                get
                {
                    return exception;
                }
            }

            #endregion
        }

        private void btnPushToTalk_Click(object sender, EventArgs e)
        {

        }




    }
}
