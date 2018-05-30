using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web;
using JockerSoft.Media;
namespace Teca.Admin.Upload
{
    public class FilesStatus
    {
        public const string HandlerPath = "/Upload/";
        private string fullName;
        private int p;
        private string fullPath;
        private string name1;

        public string group { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string url_video { get; set; }
        public string id { get; set; }
        public int size { get; set; }
        public string progress { get; set; }
        public string url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_url { get; set; }
        public string delete_type { get; set; }
        public string error { get; set; }

        public FilesStatus() { }

        public FilesStatus(FileInfo fileInfo) { SetValues(fileInfo.Name, (int)fileInfo.Length, fileInfo.FullName,url_video,id ); }

        public FilesStatus(string fileName, int fileLength, string fullPath,string url_video,string id) { SetValues(fileName, fileLength, fullPath,url_video,id ); }
      
        private void SetValues(string fileName, int fileLength, string fullPath,string url_video,string id )
        {
            name = fileName.Substring(0,fileName.LastIndexOf("."));
            this.id = id;
            type = "image/png";
            size = fileLength;
            progress = "1.0";
            url = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            delete_url = HandlerPath + "UploadHandler.ashx?f=" + fileName;
            delete_type = "DELETE";
            this.url_video = url_video;
           thumbnail_url = getImageFromVideo(id, url_video);
          //thumbnail_url = "/UploadStore/Videos/video.jpg";
        }

        public string getImageFromVideo(string name, string videopath)
        {
            string sourceVideoFolder = ConfigurationSettings.AppSettings.Get("PhysicalSiteDataDirectory") + @"\video\";
            string sourceVideoImageFolder = ConfigurationSettings.AppSettings.Get("PhysicalSiteDataDirectory") + @"\_thumbs\Videos\";
            
            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            ffMpeg.GetVideoThumbnail(sourceVideoFolder + url_video, sourceVideoImageFolder + name  + ".jpg");
            return "/UploadStore/Videos/"+name+".jpg";
        }

        private bool IsImage(string ext)
        {
            return ext == ".gif" || ext == ".jpg" || ext == ".png";
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}