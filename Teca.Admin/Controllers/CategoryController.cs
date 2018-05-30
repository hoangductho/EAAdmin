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
    public class CategoryController : BaseController
    {
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();
        private static ILog log = LogManager.GetLogger(typeof(CategoryController));
        [RBACAuthorize(Permissions = "DanhMucTin")]
        public ActionResult Index(CategoryIndexModel model, int? page)
        {
            int defautPagesize = 10;
            int total = 0;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
            string kw = String.IsNullOrWhiteSpace(model.Keyword) ? null : model.Keyword.Trim();
            var list = artCateSrv.GetByFilter(kw, currentPageIndex, defautPagesize, out total);
            model.Categories = new PagedList<Category>(list, currentPageIndex, defautPagesize, total);
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemDanhMuc")]
        public ActionResult Create()
        {
            ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
            CategoryModels model = new CategoryModels();            
            model.ArtCategory = new Category();
            model.ArtCategory.Active = true;
            model.ParentCategory = artCateSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
            return View(model);
        }

        [RBACAuthorize(Permissions = "ThemDanhMuc")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Category cate)
        {
            try
            {
                ICategoryService cateSrv = IoC.Resolve<ICategoryService>();

                int c = cateSrv.Query.Where(p => p.NameVNI == cate.NameVNI  || p.NameENG == cate.NameENG || p.UrlName == cate.UrlName).Count();
                if (c > 0)
                {
                    Messages.AddErrorMessage("Tiêu đề hoặc đường dẫn cho chuyên mục đã tồn tại.");
                    CategoryModels model = new CategoryModels();
                    model.ArtCategory = cate;
                    model.ParentCategory = cateSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
                    return View(model);
                }
                cate.CreatedBy = HttpContext.User.Identity.Name;
                cateSrv.CreateNew(cate);
                cateSrv.CommitChanges();
                Messages.AddFlashMessage("Thêm mới thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Category - Create :" + cate.Id, "Create Category Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Category - Create :" + cate.Id, "Create Category Error :" + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
                CategoryModels model = new CategoryModels();
                model.ArtCategory = cate;
                model.ParentCategory = artCateSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
                return View(model);
            }
        }

        [RBACAuthorize(Permissions = "SuaDanhMuc")]
        public ActionResult Edit(int id)
        {
            ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
            CategoryModels model = new CategoryModels();
            model.ArtCategory = artCateSrv.Getbykey(id);
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            model.articleNumbers = artSrv.Query.Where(p => p.CategoryID == id).Count();
            model.ParentCategory = artCateSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
            return View(model);
        }

        [RBACAuthorize(Permissions = "SuaDanhMuc")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(int id)
        {
            ICategoryService cateSrv = IoC.Resolve<ICategoryService>();
            Category cate = cateSrv.Getbykey(id);
            try
            {


                TryUpdateModel<Category>(cate); 
                int c = cateSrv.Query.Where(p => (p.Id != id) && (p.NameVNI == cate.NameVNI || p.NameENG == cate.NameENG || p.UrlName == cate.UrlName)).Count();
                if (c > 0)
                {
                    Messages.AddErrorMessage("Tiêu đề hoặc đường dẫn cho chuyên mục đã tồn tại.");
                    ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
                    CategoryModels model = new CategoryModels();
                    model.ArtCategory = cate;
                    IArticlesService artSrv = IoC.Resolve<IArticlesService>();
                    model.articleNumbers = artSrv.Query.Where(p => p.CategoryID == id).Count();
                    model.ParentCategory = artCateSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
                    return View("Edit", model);
                }
                cate.ModifiedBy = HttpContext.User.Identity.Name;
                cate.ModifiedDate = DateTime.Now;
                cateSrv.Update(cate);
                cateSrv.CommitChanges();
                Messages.AddFlashMessage("Cập nhật thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Category - Edit :" + id, "Edit Category Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Category - Edit :" + id, "Edit Category Error: " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                ICategoryService artCateSrv = IoC.Resolve<ICategoryService>();
                CategoryModels model = new CategoryModels();
                model.ArtCategory = cate;
                IArticlesService artSrv = IoC.Resolve<IArticlesService>();
                model.articleNumbers = artSrv.Query.Where(p => p.CategoryID == id).Count();
                model.ParentCategory = artCateSrv.Query.Where(p => p.ParentID == 0).OrderBy(p => p.NameVNI).ThenBy(p => p.NameENG).ToList();
                return View("Edit", model);
            }
        }

        [RBACAuthorize(Permissions = "XoaDanhMuc")]
        public ActionResult Delete(int id)
        {
            try
            {
                ICategoryService cateSrv = IoC.Resolve<ICategoryService>();
                IArticlesService artSrv = IoC.Resolve<IArticlesService>();
                int c = artSrv.Query.Where(p => p.CategoryID == id).Count();
                if (c > 0)
                {
                    Messages.AddErrorFlashMessage("Không được xóa chuyên mục đang sử dụng.");
                    logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Category - Xóa :" + id, "Không được xóa chuyên mục đang sử dụng", LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                    return RedirectToAction("Index");
                }
                Category cate = cateSrv.Getbykey(id);
                cateSrv.Delete(cate);
                cateSrv.CommitChanges();
                Messages.AddFlashMessage("Xóa thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Category - Delete :" + id, "Edit Category Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Category - Delete :" + id, "Delete Category Error: " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorFlashMessage("Chưa xóa được, vui lòng thực hiện lại.");
            }
            return RedirectToAction("Index");
        }
    }
}
