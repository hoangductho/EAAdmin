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
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Teca.Admin.Models;
using Teca.Core.Domain;
using Teca.Core.IService;

namespace Teca.Admin.Controllers
{
    public class AccountController : BaseController
    {
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();
        private string ipSecurity = ConfigurationManager.AppSettings["ipSecurity"];
        public ActionResult Logon(LogOnModel model)
        {            
            model.lblErrorMessage = "";
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(LogOnModel _model, string captch)
        {
            string ip = Request.UserHostAddress == "::1"? "127.0.0.1" : Request.UserHostAddress;
            //if (!string.IsNullOrWhiteSpace(ipSecurity) && !ipSecurity.Contains(ip))
            //{
            //    _model.lblErrorMessage = "Địa chỉ của bạn không được truy cập vào trang web.";
            //    _model.Password = "";
            //    return View(_model);
            //}
            if (string.IsNullOrWhiteSpace(captch))
            {
                _model.lblErrorMessage = "Nhập đúng mã xác thực.";
                _model.Password = "";
                return View(_model);
            }
            bool cv = CaptchaController.IsValidCaptchaValue(captch);
            if (!cv)
            {
                _model.lblErrorMessage = "Nhập đúng mã xác thực.";
                _model.Password = "";
                return View(_model);
            }
            FanxiAuthenticationBase _authenticationService = IoC.Resolve<FanxiAuthenticationBase>();
            try
            {
                if (_model.UserName.Trim() != null && _model.Password != null)
                {
                    if (_authenticationService.LogOn(_model.UserName.Trim(), _model.Password.Trim()) == true)                    
                        return RedirectToAction("Index","Home");                                            
                    else
                    {
                        IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
                        user TempUser = _MemberShipProvider.GetUser(_model.UserName, true);
                        if (TempUser != null)
                        {
                            if (TempUser.FailedPasswordAttemptCount >= 5)
                                _model.lblErrorMessage = "Tài khoản đã bị khóa.";
                            else
                            {
                                TempUser.FailedPasswordAttemptCount++;
                                if (TempUser.FailedPasswordAttemptCount == 5)
                                    TempUser.IsLockedOut = true;
                                _model.lblErrorMessage = "Tài khoản hoặc mật khẩu đăng nhập không đúng";
                                _MemberShipProvider.UpdateUser(TempUser);
                            }
                            return View(_model);
                        }
                        _model.lblErrorMessage = "Tài khoản hoặc mật khẩu đăng nhập không đúng";
                        _model.Password = "";
                        return View(_model);
                    }
                }
                else
                {
                    _model.Password = "";
                    return View("LogOn", _model);
                }
            }
            catch (Exception ex)
            {
                _model.lblErrorMessage = "Tài khoản hoặc mật khẩu đăng nhập không đúng";
                _model.Password = "";
                return View("LogOn", _model);
            }

        }

        //logout khoi tai khoan
        public ActionResult Logout()
        {
			FanxiAuthenticationBase _authenticationService = IoC.Resolve<FanxiAuthenticationBase>();
            _authenticationService.SigOut();
            return RedirectToAction("LogOn");
        }

        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
                throw new HttpRequestValidationException();
            IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
            user model = _MemberShipProvider.GetUser(id, false);
            if (HttpContext.User.Identity.Name.ToUpper() == model.username.ToUpper())
            {
                Messages.AddErrorFlashMessage("Không thể xóa tài khoản đang sử dụng!");
                return RedirectToAction("index");
            }
            if (!_MemberShipProvider.DeleteUser(id, true))
                Messages.AddErrorFlashMessage("Chưa xóa được tài khoản.");
            else
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "User - Delete : " + string.Format("Delete: {0} by {1}", model.username, HttpContext.User.Identity.Name), "Delete User Success ", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                
                Messages.AddFlashMessage("Xóa tài khoản thành công!");
            }
            return RedirectToAction("index");
        }

        [RBACAuthorize]
        public ActionResult ChangePassword()
        {
            string username = HttpContext.User.Identity.Name;
            ChangePasswordModel model = new ChangePasswordModel();
            model.UserName = username;
            return View(model);
        }

        [RBACAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (model.UserName != HttpContext.User.Identity.Name)
                throw new HttpRequestValidationException();
            IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
            user oUser = _MemberShipProvider.GetUser(model.UserName, true);
            try
            {
                if (oUser.password == FormsAuthentication.HashPasswordForStoringInConfigFile(model.oldPassword, "MD5"))
                {
                    //kiem tra va luu vao csdl
                    if (model.NewPassword != model.oldPassword && model.NewPassword == model.RetypePassword)
                    {
                        oUser.password = FormsAuthentication.HashPasswordForStoringInConfigFile(model.NewPassword, "MD5");
                        _MemberShipProvider.UpdateUser(oUser);
                        Messages.AddFlashMessage("Thay đổi mật khẩu thành công!");
                    }
                    //truong hop pass moi va pass cu bang nhau
                    else if (model.NewPassword == model.oldPassword)
                    {
                        Messages.AddErrorMessage("Mật khẩu mới và mật khẩu cũ giống nhau");
                        return View(model);
                    }
                    //truong hop pass moi va pass go lai khong bang nhau
                    else if (model.NewPassword != model.RetypePassword)
                    {
                        Messages.AddErrorMessage("Mật khẩu mới và mật khẩu mới nhập lại không giống nhau");
                        return View(model);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Messages.AddErrorMessage("Nhập sai mật khẩu cũ !");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "User - ChangePassword", " User ChangePasword Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                Messages.AddErrorMessage("Có lỗi trong quá trình xử lý!");
                return View(model);
            }
        }

        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        public ActionResult Index(IndexAccountModel model, int? page)
        {
            IuserService _userService = IoC.Resolve<IuserService>();
            int defautPageSize = 10;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            IQueryable<user> query = _userService.Query.Where(u => u.GroupName == null || u.GroupName == "0");
            IList<user> lst;
            int total = 0;
            if (!string.IsNullOrWhiteSpace(model.username))
                query = query.Where(u => u.username.Contains(model.username.Trim()));
            total = query.Count();
            query = query.OrderByDescending(i => i.userid);
            lst = query.Skip(currentPageIndex * defautPageSize).Take(defautPageSize).ToList();
            model.PageListUser = new PagedList<user>(lst, currentPageIndex, defautPageSize, total);
            return View(model);
        }

        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        public ActionResult Edit(int id)
        {
            IRBACRoleProvider _RoleProvider = IoC.Resolve<IRBACRoleProvider>();
            IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
            user _model = _MemberShipProvider.GetUser(id, false);
            if (HttpContext.User.Identity.Name == _model.username)
            {
                Messages.AddErrorFlashMessage("Không được sửa tài khoản này.");
                return RedirectToAction("index");
            }
            AccountModels model = new AccountModels();
            user muser = _MemberShipProvider.GetUser(id, false);
            List<String> lst = new List<string>(_RoleProvider.GetAllRoles());
            model.RetypePassword = muser.password;
            model.AllRoles = lst.ToArray();
            model.UserRoles = _RoleProvider.GetRolesForUser(muser.username);
            model.tmpUser = _model;
            return View(model);
        }

        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update(int userid, string RetypePassword, string[] AssignRoles)
        {
            if (userid <= 0)
                throw new HttpRequestValidationException();
            IRBACRoleProvider _RoleProvider = IoC.Resolve<IRBACRoleProvider>();
            IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
            AccountModels model = new AccountModels();
            user _model = _MemberShipProvider.GetUser(userid, false);
            string oldpassHash = _model.password;

            //giu lai username khong cho sua
            string username = _model.username;
            try
            {
                TryUpdateModel(_model);
                if (_model.username != username)
                    throw new HttpRequestValidationException();
                AssignRoles = AssignRoles ?? new string[] { };
                if (_model.password != RetypePassword)
                {
                    List<String> lst = new List<string>(_RoleProvider.GetAllRoles());
                    model.RetypePassword = _model.password = oldpassHash;
                    model.AllRoles = lst.ToArray();
                    model.UserRoles = _RoleProvider.GetRolesForUser(_model.username);
                    model.tmpUser = _model;
                    Messages.AddErrorMessage("Nhập đúng mật khẩu của bạn.");
                    return View("Edit", model);
                }
                if (RetypePassword != oldpassHash) _model.password = FormsAuthentication.HashPasswordForStoringInConfigFile(RetypePassword, "MD5");
                _model.FailedPasswordAttemptCount = 0;
                _MemberShipProvider.UpdateUser(_model);
                _RoleProvider.UpdateUsersToRoles(_model.username, AssignRoles);
                Messages.AddFlashMessage("Cập nhật thông tin thành công.");

                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "User - Update : " + string.Format("Update: {0} by {1}", _model.username, HttpContext.User.Identity.Name), "Update User Success ", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "User - Update ", "Update User Error " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
              
                List<String> lst = new List<string>(_RoleProvider.GetAllRoles());
                model.RetypePassword = _model.password = oldpassHash;
                model.AllRoles = lst.ToArray();
                model.UserRoles = _RoleProvider.GetRolesForUser(_model.username);
                model.tmpUser = _model;
                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại!");
                return View("Edit", model);
            }
        }

        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        public ActionResult New()
        {
            IRBACRoleProvider _RoleProvider = IoC.Resolve<IRBACRoleProvider>();
            IuserService _userService = IoC.Resolve<IuserService>();
            user _model = new user();
            _model.IsApproved = true;
            AccountModels model = new AccountModels();
            List<String> lst = new List<string>(_RoleProvider.GetAllRoles());
            model.RetypePassword = _model.password = "";
            model.AllRoles = lst.ToArray();
            model.UserRoles = new string[] { };
            model.tmpUser = _model;
            return View(model);
        }

        [RBACAuthorize(Roles = "Root,BanGiamDoc")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(user _model, string RetypePassword, string[] AssignRoles)
        {
            IRBACRoleProvider _RoleProvider = IoC.Resolve<IRBACRoleProvider>();
            IuserService _userService = IoC.Resolve<IuserService>();
            IRBACMembershipProvider _MemberShipProvider = IoC.Resolve<IRBACMembershipProvider>();
            AccountModels model = new AccountModels();
            if (string.IsNullOrWhiteSpace(_model.username))
            {
                Messages.AddErrorMessage("Cần nhập những thông tin bắt buộc.");
                List<String> lst = new List<string>(_RoleProvider.GetAllRoles());
                model.RetypePassword = _model.password = "";
                model.AllRoles = lst.ToArray();
                model.UserRoles = AssignRoles ?? new string[] { };
                model.tmpUser = _model;
                return View("New", model);
            }
            try
            {
                string status = "";
                AssignRoles = AssignRoles ?? new string[] { };
                if (!_model.password.Equals(RetypePassword))
                {
                    Messages.AddErrorMessage("Nhập đúng mật khẩu của bạn.");
                    List<String> lst = new List<string>(_RoleProvider.GetAllRoles());
                    model.RetypePassword = _model.password = "";
                    model.AllRoles = lst.ToArray();
                    model.UserRoles = new string[] { };
                    model.tmpUser = _model;
                    return View("New", model);
                }
                else
                {
                    _MemberShipProvider.CreateUser(_model.username, _model.password, _model.email, _model.PasswordQuestion, _model.PasswordAnswer, _model.IsApproved, null, out status);
                    if (status != "Success")
                    {
                        List<String> lst = new List<string>(_RoleProvider.GetAllRoles());
                        model.RetypePassword = _model.password = "";
                        model.AllRoles = lst.ToArray();
                        model.UserRoles = new string[] { };
                        model.tmpUser = _model;
                        Messages.AddErrorMessage("Tài khoản đã có trên hệ thống hoặc dữ liệu không hợp lệ.");
                        return View("New", model);
                    }
                    if (AssignRoles == null)
                    {
                        Messages.AddFlashMessage("Bạn tạo tài khoản thành công nhưng chưa phân quyền!");
                        return RedirectToAction("index");
                    }
                    _RoleProvider.UpdateUsersToRoles(_model.username, AssignRoles);
                    Messages.AddFlashMessage("Tạo tài khoản thành công.");

                    logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "User - Create : " + string.Format("Create: {0} by {1}", _model.username, HttpContext.User.Identity.Name), "Create User Success ", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
               
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "User - Create ", "Create User Error: " + ex, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
               
                Messages.AddErrorMessage("Chưa tạo được người dùng.");
                List<String> lst = new List<string>(_RoleProvider.GetAllRoles());
                model.RetypePassword = _model.password = "";
                model.AllRoles = lst.ToArray();
                model.UserRoles = new string[] { };
                model.tmpUser = _model;
                return View("New", model);
            }
        }
    }
}
