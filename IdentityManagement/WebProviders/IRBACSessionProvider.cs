using System;


namespace IdentityManagement.WebProviders
{
    public interface IRBACSessionProvider
    {
        /// <summary>
        /// Creates a user session and provides the user with a default set of active roles
        /// </summary>
        /// <param name="mUser"></param>
        /// <param name="mSession"></param>
        void CreateSession(string mUser, string mSession);
        void DeleteSession(string mUser, string mSession);
        /// <summary>
        /// Adds a role as an active role for the current session
        /// </summary>
        /// <param name="mUser"></param>
        /// <param name="mSession"></param>
        /// <param name="mRole"></param>
        void AddActiveRole(string mUser, string mSession, string mRole);
        /// <summary>
        /// Deletes a role from the active role set for the current
        /// </summary>
        /// <param name="mUser"></param>
        /// <param name="mSession"></param>
        /// <param name="mRole"></param>
        void DropActiveRole(string mUser, string mSession, string mRole);
        /// <summary>
        /// Determines if the session subject has permission to perform the requested operation on an object.
        /// </summary>
        /// <param name="mSession"></param>
        /// <param name="mOperation"></param>
        /// <param name="mObject"></param>
        /// <returns></returns>
        bool CheckAccess(string mSession, string mOperation, string mObject);

        /// <summary>
        /// returns the set of active roles associated with a session;
        /// </summary>
        /// <param name="mSession"></param>
        /// <returns></returns>
        string[] SessionRoles(string mSession);
        /// <summary>
        /// returns the set of permissions available in the session (i.e., the union of all the permissions assigned to a session’s active roles);
        /// </summary>
        /// <param name="mSession"></param>
        string[] SessionPermissions(string mSession);
    }
}
