using FX.Context;
using FX.Core;
using FX.Utils.MvcPaging;
using IdentityManagement.Authorization;
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
    public class GalleryTypeController : BaseController
    {
        private readonly IGalleryTypeService galleryTypeSrv;
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();
        public GalleryTypeController()
        {
            galleryTypeSrv = IoC.Resolve<IGalleryTypeService>();
        }

        [RBACAuthorize(Permissions = "HinhAnh")]
        public ActionResult Index(int? page)
        {
            int defautPagesize = 10;
            int total = 0;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            if (HttpContext.User.IsInRole("Root"))
            {
                var list = galleryTypeSrv.GetList(currentPageIndex, defautPagesize, out total);
                var model = new PagedList<GalleryType>(list, currentPageIndex, defautPagesize, total);
                return View(model);
            }
            else
            {
                var list = galleryTypeSrv.GetList(HttpContext.User.Identity.Name, currentPageIndex, defautPagesize, out total);
                var model = new PagedList<GalleryType>(list, currentPageIndex, defautPagesize, total);
                return View(model);
            }
        }

        [RBACAuthorize(Permissions = "HinhAnh")]
        public ActionResult IndexGallery(int? page, int TypeId = 0)
        {
            int defautPagesize = 10;
            int total = 0;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IGalleryService gallerySrv = IoC.Resolve<IGalleryService>();
            GalleryModels model = new GalleryModels();
            model.GalleryTypes = galleryTypeSrv.Query.OrderBy(p => p.TitleVNI).ToList();
            model.TypeID = TypeId == 0 ? model.GalleryTypes.FirstOrDefault().Id : TypeId;
            var lst = gallerySrv.GetbyType(TypeId, currentPageIndex, defautPagesize, out total);
            model.Galleries = new PagedList<Gallery>(lst, currentPageIndex, defautPagesize, total);
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemAnh")]
        [HttpGet]
        public ActionResult Create()
        {
            GalleryType model = new GalleryType();
            model.Galleries = new List<Gallery>();
            model.Active = true;
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemAnh")]
        [HttpPost]
        public ActionResult Create(GalleryType model, string ImageLib)
        {
            try
            {
                var c = galleryTypeSrv.Query.Where(p => p.TitleVNI == model.TitleVNI || p.TitleENG == model.TitleENG).Count();
                if (c > 0)
                {
                    Messages.AddErrorMessage("Tiêu đề ảnh đã tồn tại");

                    return View(model);
                }
                string Mess = "";
                model.CreateBy = HttpContext.User.Identity.Name;
                model.CreateDate = DateTime.Now;
                model.UrlName = FX.Utils.Common.TextHelper.ToUrlFriendly(model.TitleVNI);
                string[] str = ImageLib.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                IList<Gallery> galleries = new List<Gallery>();
                foreach (var s in str)
                {
                    var it = new Gallery();
                    it.ImgPath = s;
                    it.CreateBy = model.CreateBy;
                    it.CreateDate = DateTime.Now;
                    it.Active = true;
                    galleries.Add(it);
                }
                if (!galleryTypeSrv.CreateNew(model, galleries, out Mess))
                {
                    Messages.AddErrorMessage(Mess);
                    return View(model);
                }
                Messages.AddFlashMessage("Tạo mới thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "GalleryType - Create :" + model.Id, "Create GalleryType Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "GalleryType - Create :" + model.Id, "Create GalleryType Error : " + ex.Message, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
                return View(model);
            }
        }
        [RBACAuthorize(Permissions = "SuaAnh")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            GalleryType model = galleryTypeSrv.Getbykey(id);
            return View(model);
        }

        [RBACAuthorize(Permissions = "SuaAnh")]
        [HttpPost]
        public ActionResult Update(int id, string ImageLib)
        {
            string Mess = "";
            GalleryType model = galleryTypeSrv.Getbykey(id);
            try
            {
                TryUpdateModel<GalleryType>(model);
                var c = galleryTypeSrv.Query.Where(p => (p.Id != id) && (p.TitleVNI == model.TitleVNI || p.TitleENG == model.TitleENG)).Count();
                if (c > 0)
                {
                    Messages.AddErrorMessage("Tiêu đề ảnh đã tồn tại");
                    return View("Edit", model);
                }
                model.UrlName = FX.Utils.Common.TextHelper.ToUrlFriendly(model.TitleVNI);
                string[] str = ImageLib.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                IList<Gallery> galleries = new List<Gallery>();
                foreach (var s in str)
                {
                    var it = new Gallery();
                    it.ImgPath = s;
                    it.CreateBy = model.CreateBy;
                    it.CreateDate = DateTime.Now;
                    it.Active = true;
                    galleries.Add(it);
                }
                if (!galleryTypeSrv.Update(model, galleries, out Mess))
                {
                    Messages.AddErrorMessage(Mess);
                    return View(model);
                }
                Messages.AddFlashMessage("Sửa đổi thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "GalleryType - Edit :" + id, "Edit GalleryType Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "GalleryType - Edit :" + id, "Edit GalleryType Error " + e, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
                return View("Edit", model);
            }
        }

        [RBACAuthorize(Permissions = "XoaAnh")]
        public ActionResult Delete(int id)
        {
            try
            {
                GalleryType model = galleryTypeSrv.Getbykey(id);
                if (model.Galleries.Count > 0)
                {
                    Messages.AddErrorFlashMessage("Không thể xóa chuyên mục này.");
                    return RedirectToAction("Index");
                }
                galleryTypeSrv.Delete(model);
                galleryTypeSrv.CommitChanges();
                Messages.AddFlashMessage("Xóa thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "GalleryType - Delete :" + id, "Delete GalleryType Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "GalleryType - Delete :" + id, "Delete GalleryType Error : " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorFlashMessage("Không thể xóa");
            }
            return RedirectToAction("Index");
        }

        [RBACAuthorize(Permissions = "XoaAnh")]
        public ActionResult DeleteGallery(int id)
        {
            IGalleryService gallerySrv = IoC.Resolve<IGalleryService>();
            Gallery model = gallerySrv.Getbykey(id);
            try
            {
                gallerySrv.Delete(model);
                gallerySrv.CommitChanges();
                Messages.AddFlashMessage("Xóa thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Gallery - Delete :" + id, "Delete Gallery Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            catch (Exception ex)
            {
                Messages.AddErrorFlashMessage("Không thể xóa");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Gallery - Delete :" + id, "Delete Gallery Error", LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            return RedirectToAction("IndexGallery", new { TypeId = model.TypeID });
        }
    }
}
