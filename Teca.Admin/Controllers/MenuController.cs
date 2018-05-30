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
    public class MenuController : BaseController
    {
        private readonly IMenuService menuBarSrv;
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();
        public MenuController()
        {
            menuBarSrv = IoC.Resolve<IMenuService>();
        }

        [RBACAuthorize(Permissions = "Menu")]
        public ActionResult Index(int? page, MenuListType type = MenuListType.Top)
        {
            MenusModels model = new MenusModels();
            int total = 0;
            int PageSize = 10;
            int CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IList<Menu> lst = menuBarSrv.GetList(type,CurrentPageIndex, PageSize, out total);
            model.Type = type;
            model.PageListMenus = new PagedList<Menu>(lst, CurrentPageIndex, PageSize, total);
            return View(model);
        }
        [RBACAuthorize(Permissions = "ThemMenu")]
        [HttpGet]
        public ActionResult Create()
        {
            Menu model = new Menu();
            model.IsPub = true;
            ViewData["MenuParents"] = menuBarSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ToList();
            return View(model);
        }
        [RBACAuthorize(Permissions = "ThemMenu")]
        [HttpPost]
        public ActionResult Create(Menu model)
        {
            try
            {
                var c = menuBarSrv.Query.Where(p => p.NameVNI == model.NameVNI || p.NameENG == model.NameENG || p.NavigateUrl == model.NavigateUrl).Count();
                if (c > 0)
                {
                     ViewData["MenuParents"] = menuBarSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ToList();
                     Messages.AddErrorMessage("Tiêu đề hoặc đường dẫn đã tồn tại");
                     return View(model);
                }
                if (model.ParentID != 0)
                {
                    Menu menubar = menuBarSrv.Getbykey(model.ParentID);
                    if (model.Position != menubar.Position)
                    {
                        model.Position = menubar.Position;
                    }   
                }
                model.CreatedDate = DateTime.Now;
                menuBarSrv.CreateNew(model);
                menuBarSrv.CommitChanges();
                Messages.AddFlashMessage("Tạo mới menu thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Menu - Create : "+ model.Id, "Create Menu Success ", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Menu - Create : " + model.Id, "Create Menu Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                ViewData["MenuParents"] = menuBarSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ToList();
                Messages.AddErrorMessage("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
                return View(model);
            }
        }
        [RBACAuthorize(Permissions = "SuaMenu")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Menu model = menuBarSrv.Getbykey(id);
            ViewData["MenuParents"] = menuBarSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ToList();
            return View(model);
        }

        [RBACAuthorize(Permissions = "SuaMenu")]
        [HttpPost]
        public ActionResult Update(int id, string ParentID)
        {
            var model = menuBarSrv.Getbykey(id);
            try
            {
                TryUpdateModel<Menu>(model);
                var c = menuBarSrv.Query.Where(p => (p.Id != id) && (p.NameVNI == model.NameVNI || p.NameENG == model.NameENG || p.NavigateUrl == model.NavigateUrl)).Count();
                if (c > 0)
                {
                    ViewData["MenuParents"] = menuBarSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ToList();
                    Messages.AddErrorMessage("Tiêu đề hoặc đường dẫn đã tồn tại");
                    return View("Edit", model);
                }
                model.ParentID = !string.IsNullOrWhiteSpace(ParentID) ? int.Parse(ParentID) : 0;
                if (model.ParentID != 0)
                {
                    Menu menubar = menuBarSrv.Getbykey(model.ParentID);
                    if (model.Position != menubar.Position)
                    {
                        model.Position = menubar.Position;
                    }
                }
                menuBarSrv.Save(model);
                menuBarSrv.CommitChanges();
                Messages.AddFlashMessage("Sửa đổi thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Menu - Update : " + id, "Update Menu Success ", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["MenuParents"] = menuBarSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ToList();
                Messages.AddErrorMessage("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Menu - Update : " + id, "Update Menu Error " +ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                return View("Edit", model);
            }
        }

        [RBACAuthorize(Permissions = "XoaMenu")]
        public ActionResult Delete(int id)
        {
            try
            {
                Menu model = menuBarSrv.Getbykey(id);
                if (model.ChildMenus.Count > 0)
                {
                    Messages.AddErrorFlashMessage("Không thể xóa menu cha khi chưa xóa hết menu con");
                    return RedirectToAction("Index");
                }
                menuBarSrv.Delete(model);
                menuBarSrv.CommitChanges();
                Messages.AddFlashMessage("Xóa menu thành công");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Menu - Delete : " + id, "Delete Menu Success ", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Menu - Delete : " + id, "Delete Menu Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                Messages.AddErrorFlashMessage("Không thể xóa menu này");
            }
            return RedirectToAction("Index");
        }
    }
}
