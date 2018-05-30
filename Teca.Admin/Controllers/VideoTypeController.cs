using FX.Context;
using FX.Core;
using FX.Utils.MvcPaging;
using IdentityManagement.Authorization;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teca.Admin.Models;
using Teca.Core.Domain;
using Teca.Core.IService;

namespace Teca.Admin.Controllers
{
    public class VideoTypeController : BaseController
    {
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();

        [RBACAuthorize(Permissions = "DanhMucVideo")]
        public ActionResult Index(VideoTypeIndexModel model, int? page, int? PageSize)
        {
            int defautPagesize = PageSize.HasValue ? PageSize.Value : 10;
            int total = 0;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IVideoTypeService artTypeSrv = IoC.Resolve<IVideoTypeService>();
            string kw = String.IsNullOrWhiteSpace(model.Keyword) ? null : model.Keyword.Trim();
            var list = artTypeSrv.GetByFilter(kw, currentPageIndex, defautPagesize, out total);
            model.VideoTypes = new PagedList<VideoType>(list, currentPageIndex, defautPagesize, total);
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemDanhMucVideo")]
        public ActionResult Create()
        {
            IVideoTypeService artTypeSrv = IoC.Resolve<IVideoTypeService>();
            VideoTypeModels model = new VideoTypeModels();
            model.ArtVideoType = new VideoType();
            model.ArtVideoType.Active = true;
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemDanhMucVideo")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(VideoType type)
        {
            try
            {

                IVideoTypeService typeSrv = IoC.Resolve<IVideoTypeService>();
                var c = typeSrv.Query.Where(p => p.NameVNI == type.NameVNI || p.NameENG == type.NameENG ).Count();
                if (c > 0)
                {
                    Messages.AddErrorMessage("Tiêu đề tin tức đã tồn tại");
                    VideoTypeModels model = new VideoTypeModels();
                    model.ArtVideoType = type;
                    return View(model);
                }
                type.UrlName = FX.Utils.Common.TextHelper.ToUrlFriendly(type.NameVNI);
                type.CreatedBy = HttpContext.User.Identity.Name;
                typeSrv.CreateNew(type);
                typeSrv.CommitChanges();
                Messages.AddFlashMessage("Thêm mới thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "VideoType - Create", "Create VideoType Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "VideoType - Create", "Create VideoType Error : " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                VideoTypeModels model = new VideoTypeModels();
                model.ArtVideoType = type;
                return View(model);
            }
        }

        [RBACAuthorize(Permissions = "SuaDanhMucVideo")]
        public ActionResult Edit(int id)
        {
            IVideoTypeService artTypeSrv = IoC.Resolve<IVideoTypeService>();
            VideoTypeModels model = new VideoTypeModels();
            model.ArtVideoType = artTypeSrv.Getbykey(id);
            return View(model);
        }

        [RBACAuthorize(Permissions = "SuaDanhMucVideo")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(int id)
        {
            IVideoTypeService TypeSrv = IoC.Resolve<IVideoTypeService>();
            VideoType type = TypeSrv.Getbykey(id);
            try
            {
                TryUpdateModel<VideoType>(type);
                var c = TypeSrv.Query.Where(p => (p.Id != id) && (p.NameVNI == type.NameVNI || p.NameENG == type.NameENG)).Count();
                if (c > 0)
                {
                    Messages.AddErrorMessage("Tiêu đề tin tức đã tồn tại");
                    VideoTypeModels model = new VideoTypeModels();
                    model.ArtVideoType = type;

                    return View("Edit", model);
                } 
                type.UrlName = FX.Utils.Common.TextHelper.ToUrlFriendly(type.NameVNI);
                type.ModifiedBy = HttpContext.User.Identity.Name;
                type.ModifiedDate = DateTime.Now;
                TypeSrv.Update(type);
                TypeSrv.CommitChanges();
                Messages.AddFlashMessage("Cập nhật thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "VideoType - Update : " + id, "Update VideoType Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "VideoType - Update : " + id, "Update VideoType Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");

                VideoTypeModels model = new VideoTypeModels();
                model.ArtVideoType = type;

                return View("Edit", model);
            }
        }

        [RBACAuthorize(Permissions = "XoaDanhMucVideo")]
        public ActionResult Delete(int id)
        {
            try
            {
                IVideoTypeService typeSrv = IoC.Resolve<IVideoTypeService>();
                IVideosService videoSrv = IoC.Resolve<IVideosService>();
                int c = videoSrv.Query.Where(p => p.VideoTypeID == id).Count();
                if (c > 0)
                {
                    Messages.AddErrorFlashMessage("Không được xóa chuyên mục đang sử dụng.");
                    return RedirectToAction("Index");
                }
                VideoType type = typeSrv.Getbykey(id);
                typeSrv.Delete(type);
                typeSrv.CommitChanges();
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "VideoType - Delete : " + id, "Delete VideoType Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                Messages.AddFlashMessage("Xóa thành công.");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "VideoType - Delete : " + id, "Delete VideoType Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                Messages.AddErrorMessage("Chưa xóa được, vui lòng thực hiện lại.");
            }
            return RedirectToAction("Index");
        }
    }
}
