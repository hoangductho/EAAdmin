using System;
using System.Web;
using System.Web.Security;
using IdentityManagement.Service;
using IdentityManagement.Domain;
using FX.Data;
using System.Collections.Generic;
using IdentityManagement.WebProviders;
using System.Linq;
namespace IdentityManagement.Authorization
{
    public class FanxiAuthentication : FanxiAuthenticationBase
    {
        IRBACMembershipProvider _membershipProvider;
        public FanxiAuthentication(IRBACMembershipProvider mMembershipProvider)
        {
            _membershipProvider = mMembershipProvider;
        }

        public FanxiAuthentication(string mApplicationName, string mSessionFactoryConfigPath)
        {
            _membershipProvider = new RBACMembershipProvider(mApplicationName, mSessionFactoryConfigPath);
        }

        public override UserIdentity Authenticate(string mUserName, string mPassword)
        {            
            user TempUser = _membershipProvider.AuthenUser(mUserName, mPassword);            
            if (TempUser != null)
            {
                IList<role> TempRoles = TempUser.Roles.Where<role>(r=>r.AppID == _membershipProvider.Application.AppID).ToList<role>();
                IList<FanxiPermission> _FPList = new List<FanxiPermission>();
                foreach (role r in TempRoles)
                {
                    foreach (permission per in r.Permissions)
                    {
                        if (_FPList.Where(i => i.Name == per.name).Count() > 0) continue;
                        string mObject = (per.ObjectRBAC != null) ? per.ObjectRBAC.name : "";
                        string mOperation = (per.Operation != null) ? per.Operation.name : "";
                        FanxiPermission _FP = new FanxiPermission(per.name, mObject, mOperation);
                        _FPList.Add(_FP);
                    }
                }
                return new UserIdentity(TempUser.username, _FPList, TempRoles.Select(p => p.name).ToArray());                
            }
            else return null;
        }
    }
}
