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
    public class NotificationController : BaseController
    {
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();

        private readonly INotificationService notiSrv;

        public NotificationController()
        {
            notiSrv = IoC.Resolve<INotificationService>();
        }
        [RBACAuthorize]
        public ActionResult Index(int? page)
        {
            string currentUser = HttpContext.User.Identity.Name;
            int defautPageSize = 15;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IQueryable<Notification> query = notiSrv.Query.Where(p => p.CreateBy == currentUser);
            int total = 0;
            total = query.Count();
            query = query.OrderByDescending(p => p.CreateDate);
            IList<Notification> lst = query.Skip(currentPageIndex * defautPageSize).Take(defautPageSize).ToList();
            IPagedList<Notification> model = new PagedList<Notification>(lst, currentPageIndex, defautPageSize, total);
            return View(model);
        }
        [RBACAuthorize]
        public ActionResult GetNotification()
        {
            NotificationModels model = new NotificationModels();
            string currentUser = HttpContext.User.Identity.Name;
            int totalNotif = notiSrv.Query.Where(p => p.CreateBy == currentUser && p.Status == 0).Count();
            IList<Notification> list = notiSrv.GetbyCurrent(currentUser, 15);
            model.Notifications = list;
            model.totalNotification = totalNotif;
            return View(model);
        }
        [RBACAuthorize]
        public ActionResult Clear()
        {
            string currentUser = HttpContext.User.Identity.Name;
            IList<Notification> list = notiSrv.Query.Where(p => p.CreateBy == currentUser && p.Status == 0).ToList();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.Status = 1;
                    notiSrv.Update(item);
                }
                notiSrv.CommitChanges();
                return new JsonResult() { Data = "Xóa thông báo thành công" };
            }
            else
            {
                return new JsonResult() { Data = "Có lỗi xảy ra." };
            }
        }
        [RBACAuthorize]
        public ActionResult Detail(int id)
        {

            string currentUser = HttpContext.User.Identity.Name;
            Notification notifi = notiSrv.Getbykey(id);
            try
            {
                if (notifi != null)
                {
                    if (notifi.CreateBy.ToLower().Equals(currentUser.ToLower()))
                    {
                        int articleId = notifi.ArticleId;
                        IArticlesService artSrv = IoC.Resolve<IArticlesService>();
                        ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
                        ArticleModels model = new ArticleModels();
                        model.Article = artSrv.Getbykey(id);
                        model.Categories = artCateSrv.Query.OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
                        model.Notification = notifi;
                        return View(model);
                    }
                    else
                    {
                        Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                        return View("Index");
                    }
                }
                else
                {
                    Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Notification - Details : " + id, "Details Notification Error : " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                return View("Index");
            }
        }
        [RBACAuthorize]
        public ActionResult Delete(int id)
        {
            try
            {
                Notification model = notiSrv.Getbykey(id);
                notiSrv.Delete(model);
                notiSrv.CommitChanges();
                Messages.AddFlashMessage("Xóa thông báo thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Notification - Delete : " + id, "Delete Notification Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Notification - Delete : " + id, "Delete Notification Error : " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);

                Messages.AddErrorFlashMessage("Không thể xóa thông báo này");
            }
            return RedirectToAction("Index");
        }
    }
}
