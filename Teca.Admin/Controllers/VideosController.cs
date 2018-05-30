using FX.Core;
using FX.Utils.MvcPaging;
using IdentityManagement.Authorization;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teca.Admin.Models;
using Teca.Core.Domain;
using Teca.Core.IService;
using System.IO;
using FX.Context;
using NReco.VideoConverter;
using System.Threading;
using System.Net;
namespace Teca.Admin.Controllers
{
    public class ViewDataUploadFilesResult
    {
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string delete_url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_type { get; set; }
        public string url_video { get; set; }
    }
    public class VideosController : BaseController
    {
        private static float _uploadCount { get; set; }
        private static float _totalCount { get; set; }

        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();

        [RBACAuthorize(Permissions = "Video")]
        public ActionResult Index(VideoIndexModel model, int? page)
        {
            int defautPagesize = 10;
            int total = 0;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IVideosService videoSrv = IoC.Resolve<IVideosService>();
            IVideoTypeService videotypeSrv = IoC.Resolve<IVideoTypeService>();
            model.VideoTypes = videotypeSrv.Query.OrderBy(p => p.NameVNI).OrderBy(p => p.NameENG).ToList();
            string kw = String.IsNullOrWhiteSpace(model.Keyword) ? null : model.Keyword.Trim();
            IList<Videos> list = new List<Videos>();
            string username = HttpContext.User.Identity.Name;
            model.TypeID = string.IsNullOrWhiteSpace(model.TypeID) ? "0" : model.TypeID;
            int cID = 0;
            int.TryParse(model.TypeID, out cID);
            if (HttpContext.User.IsInRole("Root"))
                list = videoSrv.GetBySearch(kw, cID, currentPageIndex, defautPagesize, out total);
            else
                list = videoSrv.GetBySearch(kw, cID, username, currentPageIndex, defautPagesize, out total);

            model.Videos = new PagedList<Videos>(list, currentPageIndex, defautPagesize, total);
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemVideo")]
        public ActionResult Create()
        {
            IVideoTypeService videoTypeSrv = IoC.Resolve<IVideoTypeService>();
            VideoModels model = new VideoModels();
            model.Video = new Videos();
            model.Video.Active = true;
            model.VideoTypes = videoTypeSrv.Query.Where(p => p.Active).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
            return View(model);
        }
        [RBACAuthorize(Permissions = "ThemVideo")]
        public ActionResult ConfirmUpload(Videos video)
        {
            try
            {
                IVideosService videoSrv = IoC.Resolve<IVideosService>();
                video.LinkType = LinkType.Local;
                video.CreatedBy = HttpContext.User.Identity.Name;
                video.VideoPath = "/VideosStore/video/" + video.VideoPath;
                //Hình ảnh

                video.URLName = FX.Utils.Common.TextHelper.ToUrlFriendly(video.NameVNI);
                ConvertVideo conv = new ConvertVideo(video.VideoPath);
                if (video.VideoPath.IndexOf("mp4") > 0)
                {
                    Thread thread = new Thread(new ThreadStart(conv.ConvertVideoToWebM));
                    thread.Start();

                }
                else if (video.VideoPath.IndexOf("webm") > 0)
                {
                    Thread thread = new Thread(new ThreadStart(conv.ConvertVideoToMp4));
                    thread.Start();
                }


                string videoFull = video.VideoPath.Split('/')[3];
                string videoName = videoFull.Split('.')[0];
                string imageName = "/UploadStore/Videos/" + videoName + ".jpg";

                video.ImagePath = imageName;
                videoSrv.CreateNew(video);
                videoSrv.CommitChanges();
                Messages.AddFlashMessage("Thêm mới thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Video - Create", "Create Video Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                return new JsonResult() { Data = "Tải Video thành công!" };
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Video - Create", "Create Video Error : " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                return new JsonResult() { Data = "Có lỗi xảy ra vui lòng thực hiện lại." };
            }
        }
        [RBACAuthorize(Permissions = "ThemVideo")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Videos video)
        {
            try
            {
                IVideosService videoSrv = IoC.Resolve<IVideosService>();
                video.CreatedBy = HttpContext.User.Identity.Name;
                video.ImagePath = "/UploadStore/Videos/video.jpg";
                video.LinkType = LinkType.Youtube;
                //Kiểm tra video đường dẫn video chuẩn chưa . 
                if (video.VideoPath.IndexOf("embed") <= 0)
                {
                    string[] arr = video.VideoPath.Split('=');
                    string yId = arr[1];
                    if (yId.Length > 0)
                    {
                        string convertToEmbedLink = "https://www.youtube.com/embed/" + yId;
                        video.VideoPath = convertToEmbedLink;
                        //Định dạng chuẩn để xem video = thẻ iframe

                        WebClient cli = new WebClient();

                        var imgBytes = cli.DownloadData("http://img.youtube.com/vi/" + yId + "/0.jpg");

                        // System.IO.File.WriteAllBytes(ConfigurationSettings.AppSettings.Get("PhysicalSiteDataDirectory") + @"\_thumbs\Videos\" + yId + ".jpg", imgBytes);
                        video.ImagePath = "/UploadStore/Videos/" + yId + ".jpg";
                    }

                }

                video.URLName = FX.Utils.Common.TextHelper.ToUrlFriendly(video.NameVNI);
                videoSrv.CreateNew(video);
                videoSrv.CommitChanges();
                Messages.AddFlashMessage("Thêm mới thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Video - Create", "Create Video Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Video - Create", "Create Video Error : " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                IVideoTypeService videoTypeSrv = IoC.Resolve<IVideoTypeService>();
                VideoModels model = new VideoModels();
                model.Video = video;
                model.VideoTypes = videoTypeSrv.Query.Where(p => p.Active).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
                return View(model);
            }
        }

        [RBACAuthorize(Permissions = "SuaVideo")]
        public ActionResult Edit(int id)
        {
            IVideosService videoSrv = IoC.Resolve<IVideosService>();
            IVideoTypeService videoTypeSrv = IoC.Resolve<IVideoTypeService>();
            VideoModels model = new VideoModels();
            model.Video = videoSrv.Getbykey(id);
            model.VideoTypes = videoTypeSrv.Query.Where(p => p.Active).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]
        [RBACAuthorize(Permissions = "SuaVideo")]
        public ActionResult Edit(Videos video)
        {
            IVideosService videoSrv = IoC.Resolve<IVideosService>();
            //Videos oldVideo = videoSrv.Getbykey(video.Id);
            try
            {
                TryUpdateModel<Videos>(video);
                video.ModifiedBy = HttpContext.User.Identity.Name;
                video.ModifiedDate = DateTime.Now;
                //Kiểm trra người dùng có thực hiện đăng video mới thay thế hay không
                //if (video.VideoPath != oldVideo.VideoPath)
                //{
                if (video.LinkType == LinkType.Youtube)
                {
                    byte[] imgBytes;
                    string yId;
                    WebClient cli = new WebClient();
                    if (video.VideoPath.IndexOf("embed") <= 0)
                    {
                        string[] arr = video.VideoPath.Split('=');
                        yId = arr[1];        
                        string convertToEmbedLink = "https://www.youtube.com/embed/" + yId;
                        video.VideoPath = convertToEmbedLink;
                        imgBytes = cli.DownloadData("http://img.youtube.com/vi/" + yId + "/0.jpg");
                    }
                    else
                    {
                        string[] arr = video.VideoPath.Split('/');
                        yId = arr[arr.Length - 1];

                    }

                    imgBytes = cli.DownloadData("http://img.youtube.com/vi/" + yId + "/0.jpg");
                    System.IO.File.WriteAllBytes(ConfigurationSettings.AppSettings.Get("PhysicalSiteDataDirectory") + @"\_thumbs\Videos\" + yId + ".jpg", imgBytes);
                    video.ImagePath = "/UploadStore/Videos/" + yId + ".jpg";

                }
                else
                {
                    //Sửa video tự tự upload thì tiến hành convert video vừa sửa sang mp4 hoặc webm 
                    ConvertVideo conv = new ConvertVideo(video.VideoPath);
                    if (video.VideoPath.IndexOf("mp4") > 0)
                    {
                        Thread thread = new Thread(new ThreadStart(conv.ConvertVideoToWebM));
                        thread.Start();

                    }
                    else if (video.VideoPath.IndexOf("webm") > 0)
                    {
                        Thread thread = new Thread(new ThreadStart(conv.ConvertVideoToMp4));
                        thread.Start();
                    }
                    string videoFull = video.VideoPath.Split('/')[3];
                    string videoName = videoFull.Split('.')[0];
                    string imageName = "/UploadStore/Videos/" + videoName + ".jpg";
                    video.ImagePath = imageName;
                }
                //  }
                //

                video.URLName = FX.Utils.Common.TextHelper.ToUrlFriendly(video.NameVNI);
                videoSrv.Update(video);
                videoSrv.CommitChanges();
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Video - Edit : " + video.Id, "Edit Video Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddFlashMessage("Cập nhật thành công.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Video - Edit : " + video.Id, "Edit Video Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                IVideoTypeService videoCateSrv = IoC.Resolve<IVideoTypeService>();
                VideoModels model = new VideoModels();
                model.Video = video;
                model.VideoTypes = videoCateSrv.Query.Where(p => p.Active).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
                return View("Edit", model);
            }
        }

        [RBACAuthorize(Permissions = "XoaVideo")]
        public ActionResult Delete(int id)
        {
            try
            {
                IVideosService videoSrv = IoC.Resolve<IVideosService>();
                Videos video = videoSrv.Getbykey(id);
                string[] vpath = video.VideoPath.Split('/');
                var filePath = ConfigurationSettings.AppSettings.Get("PhysicalSiteDataDirectory") + "/video/" + vpath[vpath.Length - 1];

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                videoSrv.Delete(video);
                videoSrv.CommitChanges();
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Video - Delete : " + id, "Delete Video Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddFlashMessage("Xóa thành công.");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Video - Delete : " + id, "Delete Video Error : " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Chưa xóa được, vui lòng thực hiện lại.");
            }
            return RedirectToAction("Index");
        }


        //[HttpGet]
        //public void Delete(string id)
        //{
        //    var filename = id;
        //    var filePath = Path.Combine(Server.MapPath("~/Files"), filename);

        //    if (System.IO.File.Exists(filePath))
        //    {
        //        System.IO.File.Delete(filePath);
        //    }
        //}





    }
    public class ConvertVideo
    {
        public string inputVideo;

        public ConvertVideo(string inp)
        {
            this.inputVideo = inp;

        }
        public void ConvertVideoToWebM()
        {

            string sourceVideoFolder = ConfigurationSettings.AppSettings.Get("PhysicalSiteDataDirectory") + @"\video\";
            string videoFull = inputVideo.Split('/')[3];
            string videoName = videoFull.Split('.')[0];

            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();

            ffMpeg.ConvertMedia(sourceVideoFolder + videoFull, sourceVideoFolder + videoName + ".webm", Format.webm);

        }
        public void ConvertVideoToMp4()
        {

            string sourceVideoFolder = ConfigurationSettings.AppSettings.Get("PhysicalSiteDataDirectory") + @"\video\";
            string videoFull = inputVideo.Split('/')[3];
            string videoName = videoFull.Split('.')[0];

            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();

            ffMpeg.ConvertMedia(sourceVideoFolder + videoFull, sourceVideoFolder + videoName + ".mp4", Format.mp4);

        }

    }

}
