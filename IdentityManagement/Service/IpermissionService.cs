using System;
using System.Collections.Generic;
using System.Text;
using IdentityManagement.Domain;
namespace IdentityManagement.Service
{
    public interface IpermissionService : FX.Data.IBaseService<permission, int>
    {
        permission GetByName(string PermissionName, int AppID);
        IList<permission> SearchPermission(string permissionName , int AppID);
        permission GetPermission(string Object, string Operation, int AppID);        
    }
}
