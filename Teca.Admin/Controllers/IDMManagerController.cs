using FX.Context;
using FX.Core;
using FX.Utils.MvcPaging;
using IdentityManagement.Authorization;
using IdentityManagement.Domain;
using IdentityManagement.Service;
using IdentityManagement.WebProviders;
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
    public class IDMManagerController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(IDMManagerController));
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();
        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        public ActionResult Index(int? page)
        {
            IroleService _RoleSrc = IoC.Resolve<IroleService>();
            IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
            int defautPageSize = 10;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IList<role> model;
            var qr = _RoleSrc.Query.Where(c => c.AppID == _MemberShipProvider.Application.AppID);
            int total = qr.Count();
            model = qr.OrderBy(p => p.name).Skip(currentPageIndex * defautPageSize).Take(defautPageSize).ToList();
            IPagedList<role> LstRoles = new PagedList<role>(model, currentPageIndex, defautPageSize, total);
            return View(LstRoles);
        }

        [HttpGet]
        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        public ActionResult Create()
        {
            IpermissionService _PermissionSrc = IoC.Resolve<IpermissionService>();
            IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
            RoleModel model = new RoleModel();
            model.Permissions = new List<permission>();
            List<permission> lPermissions = _PermissionSrc.Query.Where(e => e.AppID == _MemberShipProvider.Application.AppID).OrderBy(p => p.Description).ToList<permission>();
            ViewData["Permissions"] = lPermissions;
            return View(model);
        }
        [HttpPost]
        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        public ActionResult Create(RoleModel model, string[] permissions)
        {
            IroleService _RoleSrc = IoC.Resolve<IroleService>();
            IpermissionService _PermissionSrc = IoC.Resolve<IpermissionService>();
            IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
            try
            {
                if (permissions == null || permissions.Count() == 0)
                {
                    Messages.AddErrorMessage("Cần chọn permission.");
                    List<permission> lPermissions = _PermissionSrc.Query.Where(a => a.AppID == _MemberShipProvider.Application.AppID).ToList<permission>();
                    ViewData["Permissions"] = lPermissions;
                    model.Permissions = new List<permission>();
                    return View("Create", model);
                }
                int cRole = _RoleSrc.Query.Where(p => p.AppID == _MemberShipProvider.Application.AppID && p.name.ToUpper() == model.name.Trim().ToUpper()).Count();
                if (cRole > 0)
                {
                    Messages.AddErrorMessage("Tên quyền này đã tồn tại trong hệ thống.");
                    List<permission> lPermissions = _PermissionSrc.Query.Where(a => a.AppID == _MemberShipProvider.Application.AppID).ToList<permission>();
                    ViewData["Permissions"] = lPermissions;
                    model.Permissions = new List<permission>();
                    return View("Create", model);
                }
                role omodel = new role();
                model.Permissions = permissions == null ? new List<permission>() : _PermissionSrc.Query.Where(p => permissions.Contains(p.name)).OrderBy(p => p.Description).ToList<permission>();
                //lay cac thong tin cho role
                omodel.name = model.name;
                omodel.Permissions = model.Permissions;
                omodel.AppID = _MemberShipProvider.Application.AppID;
                _RoleSrc.CreateNew(omodel);
                _RoleSrc.CommitChanges();
                Messages.AddFlashMessage("Tạo quyền thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Role - Create : " + omodel.roleid, "Create Role Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Role - Create", "Create Role Error : " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
                List<permission> lPermissions = _PermissionSrc.Query.Where(a => a.AppID == _MemberShipProvider.Application.AppID).OrderBy(p => p.Description).ToList<permission>();
                ViewData["Permissions"] = lPermissions;
                model.Permissions = new List<permission>();
                return View("Create", model);
            }
        }

        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        public ActionResult Edit(int id)
        {
            IroleService _RoleSrc = IoC.Resolve<IroleService>();
            IpermissionService _PermissionSrc = IoC.Resolve<IpermissionService>();
            IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
            RoleModel model = new RoleModel();
            model.Permissions = new List<permission>();
            List<permission> lPermissions = (from p in _PermissionSrc.Query where p.AppID == _MemberShipProvider.Application.AppID select p).OrderBy(p => p.Description).ToList();
            ViewData["Permissions"] = lPermissions;
            role orole = _RoleSrc.Getbykey(id);
            model.name = orole.name;
            model.Permissions = orole.Permissions.ToList<permission>();
            model.Id = id;
            return View(model);
        }
        [HttpPost]
        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        public ActionResult Edit(int roleid, string[] permissions)
        {
            if (roleid <= 0)
                throw new HttpRequestValidationException();
            IroleService _RoleSrc = IoC.Resolve<IroleService>();
            IpermissionService _PermissionSrc = IoC.Resolve<IpermissionService>();
            IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
            role omodel = _RoleSrc.Getbykey(roleid);
            try
            {
                
                TryUpdateModel<role>(omodel);
                if (permissions == null || permissions.Count() == 0)
                {
                    Messages.AddErrorMessage("Cần chọn permission.");
                    RoleModel model = new RoleModel();
                    model.Id = roleid;
                    model.name = omodel.name;
                    model.Permissions = omodel.Permissions.ToList<permission>();
                    List<permission> lPermissions = _PermissionSrc.Query.Where(a => a.AppID == _MemberShipProvider.Application.AppID).OrderBy(p => p.Description).ToList<permission>();
                    ViewData["Permissions"] = lPermissions;                    
                    return View("Edit", model);
                }
                if (omodel != null)
                {
                    omodel.Permissions = _PermissionSrc.Query.Where(p => permissions.Contains(p.name)).OrderBy(p => p.Description).ToList<permission>();
                    _RoleSrc.Update(omodel);
                    _RoleSrc.CommitChanges();
                    Messages.AddFlashMessage("Sửa role thành công.");
                    logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Role - Edit : " + omodel.roleid, "Edit Role Success : " + " Info--NameRole " + omodel.name, LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                    return RedirectToAction("Index");
                }
                else
                {
                    RoleModel model = new RoleModel();
                    model.Id = roleid;
                    model.name = omodel.name;
                    model.Permissions = omodel.Permissions.ToList<permission>();
                    List<permission> lPermissions = _PermissionSrc.Query.Where(a => a.AppID == _MemberShipProvider.Application.AppID).OrderBy(p => p.Description).ToList<permission>();
                    ViewData["Permissions"] = lPermissions;
                    Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
            
                    logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Role - Edit : " + omodel.roleid, "Edit Role Error :" + "Edit Role - role null", LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                    return View("Edit", model);
                }
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Role - Edit : " + omodel.roleid, "Edit Role Error :" + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                RoleModel model = new RoleModel();
                model.Id = roleid;
                model.name = omodel.name;
                model.Permissions = omodel.Permissions.ToList<permission>();
                List<permission> lPermissions = _PermissionSrc.Query.Where(a => a.AppID == _MemberShipProvider.Application.AppID).OrderBy(p => p.Description).ToList<permission>();
                ViewData["Permissions"] = lPermissions;
                Messages.AddErrorMessage("Có lỗi trong quá trình sửa role.");
                return View("Edit", model);
            }
        }

        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        public ActionResult Delete(int roleid)
        {
            if (roleid <= 0)
                throw new HttpRequestValidationException();
            IroleService _RoleSrc = IoC.Resolve<IroleService>();
            role model = _RoleSrc.Getbykey(roleid);
            if (model == null)
            {
                Messages.AddErrorFlashMessage("Role không tồn tại, không thể xóa.");
                return RedirectToAction("Index");
            }
            if (model.name == "Root")
            {
                Messages.AddErrorFlashMessage("Không thể xóa role này.");
                return RedirectToAction("Index");
            }
            try
            {

                _RoleSrc.Delete(model);
                _RoleSrc.CommitChanges();
                Messages.AddFlashMessage("Xóa role thành công.");
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Role - Delete : " + roleid, "Edit Role Success : " + " NameRole " + model.name, LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
            
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "Role - Delete : " + roleid, "Edit Role Error : " + ex  , LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                Messages.AddErrorFlashMessage("Có lỗi xảy ra, vui lòng thực hiện lại.");
            }
            return RedirectToAction("Index");
        }
    }
}
