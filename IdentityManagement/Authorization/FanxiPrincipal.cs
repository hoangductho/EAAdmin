using System;
using System.Collections;
using System.Security;
using System.Security.Principal;
using System.Linq;
using System.Web.Security;
namespace IdentityManagement.Authorization
{
    public class FanxiPrincipal : RolePrincipal, IFanxiPrincipal
    {
        private IIdentity _identity;

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public FanxiPrincipal(UserIdentity midentity)
            : base(midentity)
        {
            _identity = midentity;
        }

        public bool IsInRole(string roles)
        {
            UserIdentity midentity = _identity as UserIdentity;
            if (midentity != null)
            {
                return midentity.Roles.Contains(roles);
            }
            else return false;
        }

        public bool IsInRole(params string[] roles)
        {
            UserIdentity midentity = _identity as UserIdentity;
            if (midentity == null) return false;
            if (roles != null || roles.Count() > 0)
            {
                foreach (string role in roles)
                {
                    if (midentity.Roles.Contains(role.Trim()))
                        return true;
                }
                return false;
            }
            return false;
        }

        public bool IsInPermission(params string[] permissions)
        {
            UserIdentity midentity = _identity as UserIdentity;
            if (midentity == null) return false;
            if (permissions != null || permissions.Count() > 0)
            {
                foreach (string permission in permissions)
                {
                    if (midentity.Permissions.Where(p => p.Name == permission.Trim()).Count() > 0)
                        return true;
                }
                return false;
            }
            return false;
        }

        public bool IsInRole(string mObject, string mOperation)
        {
            UserIdentity midentity = _identity as UserIdentity;
            if (midentity != null)
            {
                foreach (FanxiPermission Fp in midentity.Permissions)
                {
                    if (Fp.RbacObject == mObject || Fp.RbacOperation == mOperation)
                        return true;
                }
                return false;
            }
            else return false;
        }
    }
}
