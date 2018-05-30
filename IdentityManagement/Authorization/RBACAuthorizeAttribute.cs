using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Xml;
namespace IdentityManagement.Authorization
{
    /// <summary>
    /// Check permittion follow by:
    /// User and Role  --> OR
    /// Permission , [Object|Operation]   --> AND
    /// Object|Operation is couple define a Permission
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RBACAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        string[] _Object_Operations;
        /// <summary>
        /// Obj1|Ope1,Obj2|Ope2,Obj3|Ope3 ... it is format to define Object and Operation in couple
        /// </summary>
        public string Object_Operations
        {
            get { return string.Join(",", _Object_Operations); }
            set { _Object_Operations = value.Split(','); }
        }

        string[] _Permissions;

        public string Permissions
        {
            get { return string.Join(",", _Permissions); }
            set { _Permissions = value.Split(','); }
        }

        string[] _Users;

        public string Users
        {
            get { return string.Join(",", _Users); }
            set { _Users = value.Split(','); }
        }

        string[] _Roles;

        public string Roles
        {
            get { return string.Join(",", _Roles); }
            set { _Roles = value.Split(','); }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {            
            if (filterContext.HttpContext.User == null || filterContext.HttpContext.User.Identity.IsAuthenticated == false)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            if (filterContext.HttpContext.User is FanxiPrincipal)
            {                
                FanxiPrincipal currentUser = filterContext.HttpContext.User as FanxiPrincipal;
                UserIdentity Id = currentUser.Identity as UserIdentity;
                if (_Users != null && _Users.Length > 0)
                {
                    if (!_Users.Contains(Id.Name)) { filterContext.Result = new HttpUnauthorizedResult(); return; }
                }
                if (_Roles != null && _Roles.Length > 0)
                {                    
                    IEnumerable<string> TempRoles = (from r in Id.Roles where _Roles.Contains(r) select r);
                    if (TempRoles == null || TempRoles.Count() == 0) { filterContext.Result = new HttpUnauthorizedResult(); return; }
                }
                if (_Permissions != null && _Permissions.Length > 0)
                {                    
                    string[] HasPermission = (from per in Id.Permissions select per.Name).ToArray();
                    string[] TempPer = (from per in _Permissions where (!HasPermission.ToArray().Contains(per)) select per).ToArray();
                    if (TempPer != null && TempPer.Length > 0) { filterContext.Result = new HttpUnauthorizedResult(); return; }
                }
                if (_Object_Operations != null && _Object_Operations.Length > 0)
                {
                    string[] HasPermission = (from per in Id.Permissions select per.RbacObject + "|" + per.RbacOperation).ToArray();
                    string[] TempPer = (from per in _Object_Operations where (!HasPermission.Contains(per)) select per).ToArray();
                    if (TempPer != null && TempPer.Length > 0) { filterContext.Result = new HttpUnauthorizedResult(); return; }
                }
            }
            else
                filterContext.Result = new HttpUnauthorizedResult();
        }
    }

}
