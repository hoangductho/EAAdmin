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
    public class ArticleController : BaseController
    {
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();

        [RBACAuthorize(Permissions = "TinTuc")]
        public ActionResult Index(ArticleIndexModel model, int? page, bool isEn = false)
        {
            int defautPagesize = 10;
            int total = 0;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            ICategoryService categorySrv = IoC.Resolve<ICategoryService>();

            string kw = String.IsNullOrWhiteSpace(model.Keyword) ? null : model.Keyword.Trim();
            if (string.IsNullOrEmpty(model.CatID)) model.CatID = "0";
            int cID = int.Parse(model.CatID);
            var list = artSrv.GetBySearch(HttpContext.User.Identity.Name, kw, cID, currentPageIndex, defautPagesize, out total);
            model.Categories = categorySrv.Query.Where(p => p.Active).OrderBy(p => p.NameVNI).ToList();
            model.Articles = new PagedList<Articles>(list, currentPageIndex, defautPagesize, total);
            if (isEn)
                return View("Indexen", model);
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemTin")]
        public ActionResult Create()
        {
            ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
            ArticleModels model = new ArticleModels();
            model.Article = new Articles();
            model.Article.StartDate = DateTime.Now;
            model.Article.EndDate = DateTime.Now;
            model.Categories = artCateSrv.Query.Where(p => p.Active).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemTin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Articles art, HttpPostedFileBase dataFile)
        {
            try
            {
                IArticlesService artSrv = IoC.Resolve<IArticlesService>();
                art.CreatedBy = HttpContext.User.Identity.Name;
                art.URLName = FX.Utils.Common.TextHelper.ToUrlFriendly(art.NameVNI);
                if (!art.IsEvent)
                {
                    art.StartDate = DateTime.Now;
                    art.EndDate = DateTime.Now;
                }
                if (dataFile != null && dataFile.ContentLength > 0)
                {
                    art.TypeData = dataFile.FileName.Substring(dataFile.FileName.LastIndexOf("."));
                    byte[] bf = new byte[dataFile.ContentLength];
                    dataFile.InputStream.Read(bf, 0, dataFile.ContentLength);
                    art.AttachData = bf;
                }
                artSrv.CreateNew(art);
                // tu động approved
                art.Approved = true;
                art.ApproveBy = HttpContext.User.Identity.Name;
                art.Active = true;

                artSrv.CommitChanges();
                Messages.AddFlashMessage("Thêm mới thành công.");

                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Article - Create:" + art.Id, "Create Article complete", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Article - Create:" + art.Id, "Create Error" + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
                ArticleModels model = new ArticleModels();
                model.Article = art;
   
                model.Categories = artCateSrv.Query.Where(p => p.Active).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
                return View(model);
            }
        }

        [RBACAuthorize(Permissions = "SuaTin")]
        public ActionResult Edit(int id, bool isEn = false)
        {
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
            var art = artSrv.Getbykey(id);
            if (art == null)
            {
                Messages.AddErrorFlashMessage("Bài viết không tồn tại hoặc đã bị xóa");
                return RedirectToAction("Index", new { isEn = isEn });
            }
            ArticleModels model = new ArticleModels();
            model.Article = art;
            model.Categories = artCateSrv.Query.Where(p => p.Active).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
            if (isEn)
                return View("Editen", model);
            return View(model);
        }

        [RBACAuthorize(Permissions = "SuaTin")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(int id, HttpPostedFileBase dataFile, bool isEn = false)
        {
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            Articles art = artSrv.Getbykey(id);
            try
            {
                TryUpdateModel<Articles>(art);
                art.ModifiedBy = HttpContext.User.Identity.Name;
                art.ModifiedDate = DateTime.Now;
                art.URLName = FX.Utils.Common.TextHelper.ToUrlFriendly(art.NameVNI);
                if (!art.IsEvent)
                {
                    art.StartDate = DateTime.Now;
                    art.EndDate = DateTime.Now;
                }
                if (dataFile != null && dataFile.ContentLength > 0)
                {
                    art.TypeData = dataFile.FileName.Substring(dataFile.FileName.LastIndexOf("."));
                    byte[] bf = new byte[dataFile.ContentLength];
                    dataFile.InputStream.Read(bf, 0, dataFile.ContentLength);
                    art.AttachData = bf;
                }
                artSrv.Update(art);
                artSrv.CommitChanges();
                Messages.AddFlashMessage("Cập nhật thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Article - Edit:" + art.Id, "Edit complete", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                if (isEn)
                    return View("Indexen");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Article - Edit:" + art.Id, "Edit error : " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
                ArticleModels model = new ArticleModels();
                model.Article = artSrv.Getbykey(id);
                model.Categories = artCateSrv.Query.Where(p => p.Active).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
                if (isEn)
                    return View("Editen", model);
                return View("Edit", model);
            }
        }

        [RBACAuthorize(Permissions = "DuyetTin")]
        public ActionResult ApproveIndex(ArticleIndexModel model, int? page, bool approved = false)
        {
            int defautPagesize = 10;
            int total = 0;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            ICategoryService categorySrv = IoC.Resolve<ICategoryService>();
            model.Categories = categorySrv.Query.Where(p => p.Active).OrderBy(p => p.NameVNI).ToList();
            string kw = String.IsNullOrWhiteSpace(model.Keyword) ? null : model.Keyword.Trim();
            if (string.IsNullOrEmpty(model.CatID)) model.CatID = model.Categories != null ? model.Categories.FirstOrDefault().Id.ToString() : "0";
            int cID = int.Parse(model.CatID);
            var list = artSrv.ListForApprove(approved, kw, cID, currentPageIndex, defautPagesize, out total);
            model.Articles = new PagedList<Articles>(list, currentPageIndex, defautPagesize, total);
            ViewData["approved"] = approved;

            return View(model);
        }

        [RBACAuthorize(Permissions = "DuyetTin")]
        public ActionResult EditArt(int id)
        {
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            var model = artSrv.Getbykey(id);
            return View(model);
        }

        [HttpPost]
        [RBACAuthorize(Permissions = "DuyetTin")]
        public ActionResult UpdateArt(int id)
        {
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            Articles model = artSrv.Getbykey(id);
            try
            {
                TryUpdateModel<Articles>(model);
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = HttpContext.User.Identity.Name;
                artSrv.Save(model);
                artSrv.CommitChanges();
                Messages.AddFlashMessage("Tin đã được chỉnh sửa thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Article - UpdateArt : " + model.Id, "Edit complete", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("ApproveIndex", new { CatID = model.CategoryID });
            }
            catch (Exception ex)
            {

                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Article - UpdateArt :" + model.Id, "Edit Error:" + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorFlashMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                return View("EditArt", model);
            }
        }

        [RBACAuthorize(Permissions = "DuyetTin")]
        public ActionResult ViewAttachment(int id)
        {
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            Articles model = artSrv.Getbykey(id);
            byte[] data = model.AttachData;
            if (data != null)
            {
                string typeData = model.TypeData;
                string contentType = "";
                if (typeData.Equals(".pdf"))
                    contentType = "application/pdf";
                if (typeData.Equals(".doc"))
                    contentType = "application/msword";
                if (typeData.Equals(".docx"))
                    contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return File(data, contentType);
            }
            else
            {
                return Redirect("/Article/ApproveIndex");
            }
        }
        [RBACAuthorize(Permissions = "DuyetTin")]
        public ActionResult Approve(int id)
        {
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            Articles model = artSrv.Getbykey(id);
            model.Active = true;
            Messages.AddFlashMessage("Tin đã được chỉnh sửa thành công.");
            return View(model);
        }

        [HttpPost]
        [RBACAuthorize(Permissions = "DuyetTin")]
        public ActionResult Approved(int id, bool error = false)
        {
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            Articles model = artSrv.Getbykey(id);
            try
            {
                TryUpdateModel<Articles>(model);
                if (error)
                {
                    INotificationService notiSrv = IoC.Resolve<INotificationService>();
                    Notification notifi = new Notification();
                    notifi.ApproveBy = HttpContext.User.Identity.Name;
                    notifi.CreateBy = model.CreatedBy;
                    notifi.CreateDate = DateTime.Now;
                    notifi.ArticleId = id;
                    notifi.Description = model.Comment;
                    notiSrv.CreateNew(notifi);
                    notiSrv.CommitChanges();
                    Messages.AddErrorFlashMessage("Yêu cầu sửa lỗi đã được gửi.");
                    return RedirectToAction("Index");
                }
                else
                {
                    model.Approved = true;
                    model.ApproveBy = HttpContext.User.Identity.Name;
                    model.ApproveDate = DateTime.Now;
                    model.ModifiedDate = DateTime.Now;
                    model.URLName = string.Format("{0}-{1}", model.URLName, model.Id);
                    artSrv.Save(model);
                    artSrv.CommitChanges();
                    Messages.AddFlashMessage("Tin đã được phê duyệt thành công.");
                    logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Tin tức - Duyệt :" + model.Id, "Approve Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Tin tức - Duyệt :" + model.Id, "Approve Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorFlashMessage("Tin chưa được duyệt, vui lòng thực hiện lại.");
                return View("Approve", model);
            }
        }

        [RBACAuthorize(Permissions = "XoaTin")]
        public ActionResult Delete(int id)
        {
            string message = "";
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            Articles art = artSrv.Getbykey(id);
            if (artSrv.Delete(art, out message))
            {
                Messages.AddFlashMessage("Xóa thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Article - Delete :" + id, "Delete Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            else
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Article - Delete :" + id, "Delete Error : " + message, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Chưa xóa được, vui lòng thực hiện lại.");
            }
            return RedirectToAction("Index");
        }
    }
}
