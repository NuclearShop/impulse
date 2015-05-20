using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ImpulseApp.Utilites
{
    public class FFMPEG
    {
        Process ffmpeg;
        string filepath;
        public FFMPEG(HttpServerUtility server)
        {
            filepath = server.MapPath("~/Videos/") + "ffmpeg.exe";
        }
        private void exec(string input, string output, string parameters)
        {
            ffmpeg = new Process();

            ffmpeg.StartInfo.Arguments = " -i " + input + (parameters != null ? " " + parameters : "") + " " + output;
            ffmpeg.StartInfo.FileName =  filepath;
            ffmpeg.StartInfo.UseShellExecute = false;
            ffmpeg.StartInfo.RedirectStandardOutput = true;
            ffmpeg.StartInfo.RedirectStandardError = true;
            ffmpeg.StartInfo.CreateNoWindow = true;

            ffmpeg.Start();
            ffmpeg.WaitForExit();
            ffmpeg.Close();    
        }

        public void ExtractThumbnail(string video, string jpg, string dimension)
        {
            if (dimension == null) dimension = "640x480";
            exec(video, jpg, "-s " + dimension);
        }
    }
}