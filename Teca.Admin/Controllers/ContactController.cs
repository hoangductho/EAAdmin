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
    public class ContactController : BaseController
    {
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();
        private static ILog log = LogManager.GetLogger(typeof(CategoryController));
        [RBACAuthorize(Permissions = "LienHe")]
        public ActionResult Index(ContactIndexModels model, int? page)
        {
            IContactService contactSrv = IoC.Resolve<IContactService>();
            int defautPagesize = 10;
            int total = 0;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            string kw = String.IsNullOrWhiteSpace(model.Keyword) ? null : model.Keyword.Trim();
            var list = contactSrv.GetList(false, currentPageIndex, defautPagesize, out total);
            IPagedList<Contact> plist = new PagedList<Contact>(list, currentPageIndex, defautPagesize, total);
            return View(plist);
        }
        [RBACAuthorize(Permissions = "LienHe")]
        public ActionResult Info(int? page)
        {
            IContactService contactSrv = IoC.Resolve<IContactService>();
            int defautPagesize = 10;
            int total = 0;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var list = contactSrv.GetList(true, currentPageIndex, defautPagesize, out total);
            IPagedList<Contact> plist = new PagedList<Contact>(list, currentPageIndex, defautPagesize, total);
            return View(plist);
        }
         [RBACAuthorize(Permissions = "LienHe")]
        public ActionResult InfoCreate()
        {
            Contact model = new Contact();
            return View(model);
        }
         [RBACAuthorize(Permissions = "LienHe")]
        [HttpPost]
        public ActionResult InfoCreate(Contact model)
        {
            IContactService contactSrv = IoC.Resolve<IContactService>();
            try
            {
                model.Type = true;
                contactSrv.CreateNew(model);
                contactSrv.CommitChanges();
                Messages.AddFlashMessage("Thêm mới thông tin thành công.");
                return RedirectToAction("Info");
            }
            catch (Exception ex)
            {

                return View(model);
            }
        }
         [RBACAuthorize(Permissions = "LienHe")]
        public ActionResult InfoEdit(int id)
        {
            IContactService contactSrv = IoC.Resolve<IContactService>();
            Contact model = contactSrv.Getbykey(id);
            return View(model);
        }
         [RBACAuthorize(Permissions = "LienHe")]
        [HttpPost]
        public ActionResult InfoEdit(Contact model)
        {
            IContactService contactSrv = IoC.Resolve<IContactService>();
            try
            {
                model.Type = true;
                contactSrv.Update(model);
                contactSrv.CommitChanges();
                Messages.AddFlashMessage("Sửa thông tin thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Contact - Update ", "InfoEdit Contact Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Info");
            }
            catch (Exception ex)
            {

                return View(model);
            }
        }
         [RBACAuthorize(Permissions = "LienHe")]
        public ActionResult InfoDelete(int id)
        {
            try
            {
                IContactService contactSrv = IoC.Resolve<IContactService>();

                Contact contact = contactSrv.Getbykey(id);
                contactSrv.Delete(contact);
                contactSrv.CommitChanges();
                Messages.AddFlashMessage("Xóa thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Contact - Delete :" + id, "Delete Contact Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Contact - Delete :" + id, "Delete Contact Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorFlashMessage("Chưa xóa được, vui lòng thực hiện lại.");
            }
            return RedirectToAction("Info");
        }

        [RBACAuthorize(Permissions = "LienHe")]
        public ActionResult Detail(int id)
        {
            try
            {
                IContactService contactSrv = IoC.Resolve<IContactService>();
                Contact model = contactSrv.Getbykey(id);
                return View(model);

            }
            catch (Exception ex)
            {

                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Contact - Detail :" + id, "Detail Contact Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorFlashMessage("Có lỗi xảy ra");
                return RedirectToAction("Info");
            }
        }
        [RBACAuthorize(Permissions = "LienHe")]
        public ActionResult DetailInfo(int id)
        {
            try
            {
                IContactService contactSrv = IoC.Resolve<IContactService>();
                Contact model = contactSrv.Getbykey(id);
                return View(model);

            }
            catch (Exception ex)
            {

                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Contact - Detail :" + id, "Detail Contact Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorFlashMessage("Có lỗi xảy ra");
                return RedirectToAction("Info");
            }
        }

        [RBACAuthorize(Permissions = "LienHe")]
        public ActionResult Delete(int id)
        {
            try
            {
                IContactService contactSrv = IoC.Resolve<IContactService>();

                Contact contact = contactSrv.Getbykey(id);
                contactSrv.Delete(contact);
                contactSrv.CommitChanges();
                Messages.AddFlashMessage("Xóa thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Contact - Delete :" + id, "Delete Contact Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Contact - Delete :" + id, "Delete Contact Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorFlashMessage("Chưa xóa được, vui lòng thực hiện lại.");
            }
            return RedirectToAction("Index");
        }

    }
}
