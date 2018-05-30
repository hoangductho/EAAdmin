using System;
using System.Collections.Generic;
using IdentityManagement.Authorization;

namespace IdentityManagement.WebProviders
{
    interface IRBACPermissionProvider
    {
        void AddObject(string Object);
        void DeleteObject(string mObject);
        void AddOperation(string mOperation);
        void DeleteOperation(string mOperation);
        void CreatePermission(string Object, string Operation, string PermissionName);
        void DeletePermission(string PermissionName);
        /// <summary>
        /// returns the set of permissions a given user gets through his or her assigned roles;
        /// </summary>
        /// <param name="mUser"></param>
        /// <returns></returns>
        string[] UserPermissions(string mUser);
        /// <summary>
        /// returns the set of permissions granted to a given role;
        /// </summary>
        /// <param name="mRole"></param>
        /// <returns></returns>
        string[] RolePermissions(string mRole);
        /// <summary>
        /// returns the set of operations a given role may perform on a given object;
        /// </summary>
        /// <param name="mRole"></param>
        /// <param name="mObject"></param>
        /// <returns></returns>
        string[] RoleOperationsOnObject(string mRole, string mObject);
        /// <summary>
        /// returns the set of operations a given user may perform on a given object (obtained either directly or through his or her assigned roles)
        /// </summary>
        /// <param name="mUser"></param>
        /// <param name="mObject"></param>
        /// <returns></returns>
        string[] UserOperationsOnObject(string mUser, string mObject);

        IList<FanxiPermission> GetAllPermission();
        IList<FanxiPermission> SearchPermission(string PermissionName);
        FanxiPermission GetPermission(string Object, string Operation);
        string[] GetAllObject();
        string[] GetAllOperation();
    }
}
