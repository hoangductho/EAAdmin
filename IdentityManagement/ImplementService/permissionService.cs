using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FX.Data;
using IdentityManagement.Domain;
using IdentityManagement.Service;

namespace IdentityManagement.ImplementService
{
    public class permissionService : BaseService<permission, int>, IpermissionService
    {
        public permissionService(string sessionFactoryConfigPath)
            : base(sessionFactoryConfigPath)
        { }

        public permission GetByName(string PermissionName, int AppID)
        {
            string hsql = "from permission per where per.name = :PermissionName AND per.AppID = :AppID";
            List<permission> _lst = GetbyHQuery(hsql, new SQLParam("PermissionName", PermissionName), new SQLParam("AppID", AppID));
            return (_lst == null || _lst.Count == 0) ? null : _lst[0];
        }
        public IList<permission> SearchPermission(string permissionName, int AppID)
        {
            string hsql = "from permission per where per.name Like :PermissionName AND per.AppID = :AppID";
            return GetbyHQuery(hsql, new SQLParam("PermissionName", "%" + permissionName + "%"), new SQLParam("AppID", AppID));
        }

        public permission GetPermission(string Object, string Operation, int AppID)
        {
            string hsql = "from permission per where per.Operation.name = :Operation AND per.ObjectRBAC.name = :Object AND per.AppID = :AppID";
            List<permission> _lst = GetbyHQuery(hsql, new SQLParam("Operation", Operation), new SQLParam("Object", Object), new SQLParam("AppID", AppID));
            return (_lst == null || _lst.Count == 0) ? null : _lst[0];
        }
    }

}