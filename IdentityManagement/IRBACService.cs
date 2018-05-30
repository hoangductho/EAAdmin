using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IdentityManagement.Domain;
namespace IdentityManagement
{
    public interface IRBACService
    {
       
        #region Administrative Commands
        void AddUser(user mUser);
        void DeleteUser(user mUser);
        void AddRole(role mRole);
        void DeleteRole(role mRole);
        void AssignUser(user mUser, role mRole);
        void DeassignUser(user mUser, role mRole);
        void AddObject(objectRbac mObject);
        void DeleteObject(objectRbac mObject);
        void AddOperation(operation mOperation);
        void DeleteOperation(operation mOperation);
        void GrantPermission(objectRbac mObject, operation mOperation, role mRole);
        void RevokePermission(operation mOperation, objectRbac mObject, role mRole);
        /// <summary>
        /// establishes a new immediate inheritance relationship between two existing roles
        /// </summary>
        /// <param name="r_asc"></param>
        /// <param name="r_desc"></param>
        void AddInheritance(role r_asc, role r_desc);
        /// <summary>
        /// deletes an existing immediate inheritance relationship between two roles;
        /// </summary>
        /// <param name="r_asc"></param>
        /// <param name="r_desc"></param>
        void DeleteInheritance(role r_asc, role r_desc);
        /// <summary>
        /// creates a new role and adds it as an immediate ascendant of an existing role;
        /// </summary>
        /// <param name="r_asc"></param>
        /// <param name="r_desc"></param>
        void AddAscendant(role r_asc, role r_desc);
        /// <summary>
        /// creates a new role and adds it as an immediate descendant of an existing role.
        /// </summary>
        /// <param name="r_asc"></param>
        /// <param name="r_desc"></param>
        void AddDescendant(role r_asc, role r_desc) ;
        #endregion

        #region System Functions(session)
        /// <summary>
        /// Creates a user session and provides the user with a default set of active roles
        /// </summary>
        /// <param name="mUser"></param>
        /// <param name="mSession"></param>
        void CreateSession(user mUser, session mSession);

        void DeleteSession(user mUser, session mSession);
        /// <summary>
        /// Adds a role as an active role for the current session
        /// </summary>
        /// <param name="mUser"></param>
        /// <param name="mSession"></param>
        /// <param name="mRole"></param>
        void AddActiveRole(user mUser, session mSession, role mRole);
        /// <summary>
        /// Deletes a role from the active role set for the current
        /// </summary>
        /// <param name="mUser"></param>
        /// <param name="mSession"></param>
        /// <param name="mRole"></param>
        void DropActiveRole(user mUser, session mSession, role mRole);
        /// <summary>
        /// Determines if the session subject has permission to perform the requested operation on an object.
        /// </summary>
        /// <param name="mSession"></param>
        /// <param name="mOperation"></param>
        /// <param name="mObject"></param>
        /// <returns></returns>
        bool CheckAccess(session mSession, operation mOperation, objectRbac mObject);
        #endregion

        #region Review Functions
        /// <summary>
        /// returns the set of users assigned to a given role;
        /// </summary>
        /// <param name="mRole"></param>
        /// <returns></returns>
        IList<user> AuthorizedUsers(role mRole);
        /// <summary>
        /// returns the set of roles assigned to a given user;
        /// </summary>
        /// <param name="mUser"></param>
        /// <returns></returns>
        IList<role> AuthorizedRoles(user mUser);
        /// <summary>
        /// returns the set of permissions granted to a given role;
        /// </summary>
        /// <param name="mRole"></param>
        /// <returns></returns>
        IList<permission> RolePermissions(role mRole);
        /// <summary>
        /// returns the set of permissions a given user gets through his or her assigned roles;
        /// </summary>
        /// <param name="mUser"></param>
        /// <returns></returns>
        IList<permission> UserPermissions(user mUser);
        /// <summary>
        /// returns the set of operations a given role may perform on a given object;
        /// </summary>
        /// <param name="mRole"></param>
        /// <param name="mObject"></param>
        /// <returns></returns>
        IList<operation> RoleOperationsOnObject(role mRole, objectRbac mObject);
        /// <summary>
        /// returns the set of operations a given user may perform on a given object (obtained either directly or through his or her assigned roles)
        /// </summary>
        /// <param name="mUser"></param>
        /// <param name="mObject"></param>
        /// <returns></returns>
        IList<operation> UserOperationsOnObject(user mUser, objectRbac mObject);
        /// <summary>
        /// returns the set of active roles associated with a session;
        /// </summary>
        /// <param name="mSession"></param>
        /// <returns></returns>
        IList<role> SessionRoles(session mSession);
        /// <summary>
        /// returns the set of permissions available in the session (i.e., the union of all the permissions assigned to a session’s active roles);
        /// </summary>
        /// <param name="mSession"></param>
        IList<permission> SessionPermissions(session mSession);
        #endregion
    }
}
