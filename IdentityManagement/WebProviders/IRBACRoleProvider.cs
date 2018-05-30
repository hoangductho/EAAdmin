using System;
using IdentityManagement.Domain;
namespace IdentityManagement.WebProviders
{
   public interface IRBACRoleProvider
    {
       //RoleProvider member
       void AddUsersToRoles(string[] usernames, string[] roleNames);
       void UpdateUsersToRoles(int userid, string[] roleNames);
       void UpdateUsersToRoles(string username, string[] roleNames);
       void CreateRole(string roleName);
       bool DeleteRole(string roleName, bool throwOnPopulatedRole);
       string[] FindUsersInRole(string roleName, string usernameToMatch);
       string[] GetAllRoles();
       string[] GetRoleForOperation(string Operation, string Object);
       void RemoveUsersFromRoles(string[] usernames, string[] roleNames);
       bool RoleExists(string roleName);


        void GrantPermission(string mObject, string mOperation, string[] mRoles);
        void RevokePermission(string mOperation, string mObject, string[] mRoles);
        void UpdatePermission(string mObject, string mOperation, string[] mRoles);
        void AddInheritance(string r_asc, string r_desc);
        void DeleteInheritance(string r_asc, string r_desc);
        void AddAscendant(string r_asc, string r_desc);
        void AddDescendant(string r_asc, string r_desc);
        void InstallObject(string mObject, string[] mOperations);
        void InstallObject(IRbacObject mObject);
        void UnInstallObject(string mObject);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Object"></param>
        /// <returns>return all Operations in specify Object </returns>
        string[] RoleOperationsOnObject(string role, string Object);
        /// <summary>
        /// returns the set of permissions granted to a given role;
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        string[] RolePermissions(string role);
        /// <summary>
        /// returns the set of active roles associated with a session;
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        string[] SessionRoles(string session);
        /// <summary>
        ///  returns the set of roles assigned to a given user;
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string[] GetRolesForUser(string username);

        string[] GetRolesForUser(int userid);
        /// <summary>
        /// returns the set of users assigned to a given role;
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        string[] GetUsersInRole(string roleName);
        /// <summary>
        /// Assign an User for a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        void AssignUserToRole(string user, string role);
        void DeassignUserToRole(string user, string role);
        bool IsUserInRole(string username, string roleName);
    }
}
