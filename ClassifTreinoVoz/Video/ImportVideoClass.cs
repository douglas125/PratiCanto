using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace PratiCanto.Video
{
    /// <summary>Class to import video</summary>
    public static class ImportVideoClass
    {
        /// <summary>Imports video</summary>
        /// <param name="filename">Video file name</param>
        /// <param name="displayControl">Control to display video in</param>
        /// <param name="audioFileName">Name of output audio file</param>
        /// <returns></returns>
        public static DxPlay.DxPlay ImportVideo(string filename, Control displayControl, out string audioFileName)
        {
            ImportVideo frmImpVideo = new ImportVideo();
            frmImpVideo.TopMost = true;
            frmImpVideo.Show();


            Application.DoEvents();
            System.Threading.Thread.Sleep(200);

            //extract audio
            frmImpVideo.lblExtractAudio.BackColor = Color.Yellow;
            audioFileName = DxPlay.DxPlay.ExtractAudioFromVideo(filename);
            if (audioFileName == "")
            {
                frmImpVideo.lblExtractAudio.BackColor = Color.Red;
                Application.DoEvents();
                System.Threading.Thread.Sleep(2000);
                frmImpVideo.Dispose();
                return null;
            }
            frmImpVideo.lblExtractAudio.BackColor = Color.Green;
            frmImpVideo.lblExtractAudio.Refresh();

            //open video
            frmImpVideo.lblOpenVideo.BackColor = Color.Yellow;
            frmImpVideo.lblOpenVideo.Refresh();
            Application.DoEvents();
            DxPlay.DxPlay videoPlayer = null;
            try
            {
                videoPlayer = new DxPlay.DxPlay(displayControl, filename);
            }
            catch
            {
                frmImpVideo.lblOpenVideo.BackColor = Color.Red;
                System.Threading.Thread.Sleep(2000);
                frmImpVideo.Dispose();
                return null;
            }

            frmImpVideo.lblOpenVideo.BackColor = Color.Green;
            Application.DoEvents();
            System.Threading.Thread.Sleep(200);

            frmImpVideo.Dispose();
            return videoPlayer;
        }
    }
}
