using FX.Core;
using FX.Utils.MvcPaging;
using IdentityManagement.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teca.Core.Domain;
using Teca.Core.IService;
using FX.Context;
namespace Teca.Admin.Controllers
{
    public class DocumentController : BaseController
    {

        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();
        private readonly IDocumentService documentSrv;

        public DocumentController()
        {
            documentSrv = IoC.Resolve<IDocumentService>();
        }

        [RBACAuthorize(Permissions = "Document")]
        public ActionResult Index(string name, int? page)
        {
            int total = 0;
            int PageSize = 10;
            int CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
            name = !string.IsNullOrWhiteSpace(name) ? name.Trim() : null;
            IList<Document> lst = documentSrv.GetForAdmin(name, CurrentPageIndex, PageSize, out total);
            var model = new PagedList<Document>(lst, CurrentPageIndex, PageSize, total);
            return View(model);
        }

        [RBACAuthorize(Permissions = "Document")]
        public ActionResult Create()
        {
            var model = new Document();
            model.PublishDate = DateTime.Now;
            model.StartDate = DateTime.Now;
            model.Active = true;
            model.CreateDate = DateTime.Now;
            model.PublishDate = DateTime.Now;
            model.StartDate = DateTime.Now;
            return View(model);
        }

        [RBACAuthorize(Permissions = "Document")]
        [HttpPost]
        public ActionResult Create(Document model, HttpPostedFileBase dataFile)
        {
            try
            {
                if (dataFile != null && dataFile.ContentLength > 0)
                {
                    byte[] bf = new byte[dataFile.ContentLength];
                    dataFile.InputStream.Read(bf, 0, dataFile.ContentLength);
                    model.Data = bf;
                    model.CreateBy = HttpContext.User.Identity.Name;
                    model.CreateDate = DateTime.Now;
                    documentSrv.CreateNew(model);
                    documentSrv.CommitChanges();
                    Messages.AddFlashMessage("Tạo mới văn bản pháp quy thành công");
                    logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Document - Create :" + model.Id, "Create Document Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                    return RedirectToAction("Index");
                }
                Messages.AddErrorFlashMessage("Chưa chọn file văn bản.");
                return View(model);
            }
            catch (Exception e)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Document - Create", "Create Document Error : " + e, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
                return View(model);
            }
        }

        [RBACAuthorize(Permissions = "Document")]
        public ActionResult Edit(int id)
        {
            var model = documentSrv.Getbykey(id);
            return View(model);
        }

        [RBACAuthorize(Permissions = "Document")]
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase dataFile)
        {
            var model = documentSrv.Getbykey(id);
            try
            {
                TryUpdateModel<Document>(model);
                if (dataFile != null && dataFile.ContentLength > 0)
                {
                    byte[] bf = new byte[dataFile.ContentLength];
                    dataFile.InputStream.Read(bf, 0, dataFile.ContentLength);
                    model.Data = bf;
                }
                model.CreateBy = HttpContext.User.Identity.Name;
                model.CreateDate = DateTime.Now;
                documentSrv.Save(model);
                documentSrv.CommitChanges();
                Messages.AddFlashMessage("Sửa văn bản pháp quy thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Document - Edit :" + id, "Edit Document Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Document - Edit :" + id, "Edit Document Error : " + e, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
                return View(model);
            }
        }

        [RBACAuthorize(Permissions = "Document")]
        public ActionResult Delete(int id)
        {
            try
            {
                var model = documentSrv.Getbykey(id);
                documentSrv.Delete(model);
                documentSrv.CommitChanges();
                Messages.AddFlashMessage("Xóa thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Document - Delete :" + id, "Delete Document Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Document - Delete :" + id, "Delete Document Error : " + ex.Message, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
            }
            return RedirectToAction("Index");
        }
    }
}
