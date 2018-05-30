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
    public class BannersController : BaseController
    {
        private readonly IBannersService bannerSrv;
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();
        public BannersController()
        {
            bannerSrv = IoC.Resolve<IBannersService>();
        }

        [RBACAuthorize(Permissions = "Banner")]
        public ActionResult Index(int? page, BannerType type = BannerType.Top)
        {
            BannerModels model = new BannerModels(); 
            int total = 0;
            int PageSize = 10;
            int CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IList<Banners> lst = bannerSrv.GetbyList(type, CurrentPageIndex, PageSize, out total);
            model.PageListBanners = new PagedList<Banners>(lst, CurrentPageIndex, PageSize, total);
            model.Type = type;
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemBanner")]
        [HttpGet]
        public ActionResult Create()
        {
            Banners model = new Banners();
            model.Active = true;
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemBanner")]
        [HttpPost]
        public ActionResult Create(Banners model)
        {
            try
            {
                model.CreatedBy = HttpContext.User.Identity.Name;
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.FromDate = DateTime.Now;
                model.ToDate = DateTime.MaxValue;
                bannerSrv.CreateNew(model);
                bannerSrv.CommitChanges();
                Messages.AddFlashMessage("Tạo mới banner thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Banner - Create :" + model.Id, "Create Banner Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Banner - Create :" + model.Id, "Create Banner Error: " + e.Message, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
                return View(model);
            }
        }

        [RBACAuthorize(Permissions = "SuaBanner")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Banners model = bannerSrv.Getbykey(id);
            return View(model);
        }

        [RBACAuthorize(Permissions = "SuaBanner")]
        [HttpPost]
        public ActionResult Update(int id)
        {
            Banners model = bannerSrv.Getbykey(id);
            try
            {
                TryUpdateModel<Banners>(model);
                model.ModifiedBy = HttpContext.User.Identity.Name;
                model.ModifiedDate = DateTime.Now;
                bannerSrv.Save(model);
                bannerSrv.CommitChanges();
                Messages.AddFlashMessage("Sửa đổi thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Banner - Edit :" + id, "Edit Banner Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Banner - Edit :" + id, "Edit Banner Error :" + e.Message, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorFlashMessage("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
                return View("Edit", model);
            }
        }

        [RBACAuthorize(Permissions = "XoaBanner")]
        public ActionResult Delete(int id)
        {
            try
            {
                Banners model = bannerSrv.Getbykey(id);
                bannerSrv.Delete(model);
                bannerSrv.CommitChanges();
                Messages.AddFlashMessage("Xóa Banner thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Banner - Delete :" + id, "Delete Banner Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            catch (Exception ex)
            {
                Messages.AddErrorFlashMessage("Không thể xóa banner này");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Banner - Delete :" + id, "Delete Banner Error : " + ex.Message, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            return RedirectToAction("Index");
        }
    }
}
