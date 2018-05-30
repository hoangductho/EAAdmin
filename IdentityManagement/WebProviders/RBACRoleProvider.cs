using System;
using System.Collections.Generic;
using System.Web.Security;
using IdentityManagement.Domain;
using IdentityManagement.Service;
using IdentityManagement.ImplementService;
using System.Linq;
using log4net;
using FX.Data;
namespace IdentityManagement.WebProviders
{
    public class RBACRoleProvider : RoleProvider, IRBACRoleProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RBACMembershipProvider));
        Applications _App;
        string _SessionFactoryConfigPath;

        public override string ApplicationName
        {
            get { return _App != null ? _App.AppName : ""; }
            set
            {
                if (_App != null || _SessionFactoryConfigPath == string.Empty)
                {
                    throw new Exception("You cannot set Application for this provider because it had seted Application or SessionFactoryConfigPath is empty");
                }
                else
                {
                    _App = new ApplicationsService(SessionFactoryConfigPath).GetByName(value);
                }
            }
        }

        public string SessionFactoryConfigPath
        {
            get { return _SessionFactoryConfigPath; }
        }

        public RBACRoleProvider(string mApplicationName, string mSessionFactoryConfigPath)
        {
            _SessionFactoryConfigPath = mSessionFactoryConfigPath;
            ApplicationName = mApplicationName;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            if (_App == null) return;
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            foreach (string UN in usernames)
            {
                user mUser = UserSrv.GetByName(UN, _App.AppID);
                if (mUser != null)
                {
                    string[] currentRoles = (from r in mUser.Roles where r.AppID == _App.AppID select r.name).ToArray();
                    foreach (string r in roleNames)
                    {
                        if (!currentRoles.Contains(r))
                        {
                            role mRole = RoleSrv.GetByName(r, _App.AppID);
                            if (mRole != null) mUser.Roles.Add(mRole);
                        }
                    }
                    UserSrv.Save(mUser);
                }
            }
            UserSrv.CommitChanges();
        }

        private void updateRolesForUser(user mUser, string[] roleNames)
        {
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            if (mUser != null)
            {
                if (roleNames == null || roleNames.Length <= 0)
                {
                    if (mUser.Roles != null)
                        mUser.Roles.Clear();
                }
                else
                {
                    if (mUser.Roles == null) mUser.Roles = new List<role>();
                    string[] currentRoles = (from r in mUser.Roles where r.AppID == _App.AppID select r.name).ToArray();
                    string[] RemoveRoles = (from rl in currentRoles where !roleNames.Contains(rl) select rl).ToArray();
                    string[] InsertRoles = (from rl in roleNames where !currentRoles.Contains(rl) select rl).ToArray();
                    //remove role
                    foreach (string r in RemoveRoles)
                    {
                        role mRole = RoleSrv.GetByName(r, _App.AppID);
                        if (mRole != null) mUser.Roles.Remove(mRole);
                    }

                    foreach (string r in InsertRoles)
                    {
                        role mRole = RoleSrv.GetByName(r, _App.AppID);
                        if (mRole != null) mUser.Roles.Add(mRole);
                    }
                }
                UserSrv.Save(mUser);
                UserSrv.CommitChanges();
            }
        }

        public void UpdateUsersToRoles(int userid, string[] roleNames)
        {
            if (_App == null) return;
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user mUser = UserSrv.Getbykey(userid);
            updateRolesForUser(mUser, roleNames);
        }

        public void UpdateUsersToRoles(string username, string[] roleNames)
        {
            if (_App == null) return;
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            user mUser = UserSrv.GetByName(username, _App.AppID);
            updateRolesForUser(mUser, roleNames);
        }

        public override void CreateRole(string roleName)
        {
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            if (_App == null) return;
            role mRole = RoleSrv.GetByName(roleName, _App.AppID);
            if (mRole == null)
            {
                mRole = new role();
                mRole.AppID = _App.AppID;
                mRole.name = roleName;
                RoleSrv.CreateNew(mRole);
                RoleSrv.CommitChanges();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (_App == null) return false;
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            role mRole = RoleSrv.GetByName(roleName, _App.AppID);
            if (mRole == null) return false;
            try
            {
                RoleSrv.Delete(mRole);
                RoleSrv.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new Exception("this method have not implement.");
            //
            //if (_App == null) return null;
            //

        }

        public override string[] GetAllRoles()
        {
            if (_App == null) return null;
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            IList<role> lst = RoleSrv.GetAll(_App.AppID);
            if (lst == null || lst.Count == 0) return new string[] { };
            return (from r in lst select r.name).ToArray();
        }

        public string[] GetRolesForUser(int userid)
        {
            if (_App == null) return null;
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user mUser = UserSrv.Getbykey(userid);
            if (mUser == null || mUser.Roles == null || mUser.Roles.Count == 0) return new string[] { };
            else return (from r in mUser.Roles where r.AppID == _App.AppID select r.name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (_App == null) return null;
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user mUser = UserSrv.GetByName(username, _App.AppID);
            if (mUser == null || mUser.Roles == null || mUser.Roles.Count == 0) return new string[] { };
            else return (from r in mUser.Roles where r.AppID == _App.AppID select r.name).ToArray();
        }
        public override string[] GetUsersInRole(string roleName)
        {
            if (_App == null) return null;

            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            role mRole = RoleSrv.GetByName(roleName, _App.AppID);
            if (mRole == null) return null;
            return (from u in mRole.Users select u.username).ToArray();

        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (_App == null) return false;
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user mUser = UserSrv.GetByName(username, _App.AppID);
            if (mUser == null) return false;
            role mRole = (from r in mUser.Roles where r.AppID == _App.AppID && r.name == roleName select r).SingleOrDefault();
            return mRole != null;
        }
        /// <summary>
        /// Removes the specified user names from the specified roles for the configured applicationName
        /// </summary>
        /// <param name="usernames"> A string array of user names to be removed from the specified roles.</param>
        /// <param name="roleNames">A string array of role names to remove the specified user names from.</param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            if (_App == null) return;
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            string hql = "from user u where u.username in (:usernames)";
            IList<user> UserList = UserSrv.GetbyHQuery(hql, new SQLParam("usernames", usernames.ToString()));
            string hql2 = "from role r where r.name in (:roleNames) AND r.AppID = :AppID";
            IList<role> RoleList = RoleSrv.GetbyHQuery(hql2, new SQLParam("roleNames", roleNames.ToString()), new SQLParam("AppID", _App.AppID));
            foreach (user u in UserList)
            {
                foreach (role r in RoleList)
                {
                    if (u.Roles.Contains(r)) u.Roles.Remove(r);
                }
            }
            UserSrv.CommitChanges();
        }

        public override bool RoleExists(string roleName)
        {
            if (_App == null) return false;
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            return RoleSrv.GetByName(roleName, _App.AppID) != null;
        }

        #region Implement IRoleProvider

        /// <summary>
        /// Return All Role wich is assign permission for Action [Operation] on [Object]
        /// </summary>
        /// <param name="Operation"></param>
        /// <param name="Object"></param>
        /// <returns></returns>
        public string[] GetRoleForOperation(string mOperation, string mObject)
        {
            IpermissionService PermissionSrv = new permissionService(SessionFactoryConfigPath);
            permission _Per = PermissionSrv.GetPermission(mObject, mOperation, _App.AppID);
            if (_Per != null && _Per.Roles != null)
                return (from r in _Per.Roles select r.name).ToArray();
            else return new string[] { };
        }

        public void GrantPermission(string mObject, string mOperation, string[] mRoles)
        {
            if (_App == null) return;

            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            string HQL = "from role r where r.AppID = :AppID AND r.name in ({0})";
            string ParaStr = ":" + string.Join(",:", mRoles);
            HQL = string.Format(HQL, ParaStr);
            SQLParam[] paramList = new SQLParam[mRoles.Length + 1];
            paramList[0] = new SQLParam("AppID", _App.AppID);
            for (int i = 0; i < mRoles.Length; i++)
            {
                paramList[i + 1] = new SQLParam(mRoles[i], mRoles[i]);
            }
            List<role> RoleLst = RoleSrv.GetbyHQuery(HQL, paramList);
            if (RoleLst == null || RoleLst.Count == 0) return;
            IoperationService OperationSrv = new operationService(SessionFactoryConfigPath);
            IobjectService ObjectSrv = new objectService(SessionFactoryConfigPath);
            IpermissionService PermissionSrv = new permissionService(SessionFactoryConfigPath);
            //Grant permission
            permission TempPermission = PermissionSrv.GetPermission(mObject, mOperation, _App.AppID);
            if (TempPermission == null)
            {
                objectRbac tempObject = ObjectSrv.GetByName(mObject, _App.AppID);
                operation tempOperation = OperationSrv.GetByName(mOperation, _App.AppID);
                if (tempObject == null || tempOperation == null) return;
                TempPermission = new permission();
                TempPermission.AppID = _App.AppID;
                TempPermission.name = tempObject.name + ":" + tempOperation.name;
                TempPermission.ObjectRBAC = tempObject;
                TempPermission.Operation = tempOperation;
                TempPermission.Roles = new List<role>();
                foreach (role r in RoleLst)
                {
                    TempPermission.Roles.Add(r);
                }
                PermissionSrv.CreateNew(TempPermission);
                PermissionSrv.CommitChanges();
            }

            else
            {
                foreach (role r in RoleLst)
                {
                    if (!TempPermission.Roles.Contains(r))
                        TempPermission.Roles.Add(r);
                }
                PermissionSrv.CommitChanges();
            }
        }

        public void RevokePermission(string mObject, string mOperation, string[] mRoles)
        {
            if (_App == null) return;
            //string HQL = "from role r where r.name in (:rolenames) AND r.AppID = :AppID";
            //List<role> RoleLst = RoleSrv.GetbyHQuery(HQL, new SQLParam("rolenames", mRoles), new SQLParam("AppID", _App.AppID));            
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            string HQL = "from role r where r.AppID = :AppID AND r.name in ({0})";
            string ParaStr = ":" + string.Join(",:", mRoles);
            HQL = string.Format(HQL, ParaStr);
            SQLParam[] paramList = new SQLParam[mRoles.Length + 1];
            paramList[0] = new SQLParam("AppID", _App.AppID);
            for (int i = 0; i < mRoles.Length; i++)
            {
                paramList[i + 1] = new SQLParam(mRoles[i], mRoles[i]);
            }
            List<role> RoleLst = RoleSrv.GetbyHQuery(HQL, paramList);
            if (RoleLst == null || RoleLst.Count == 0) return;

            IpermissionService PermissionSrv = new permissionService(SessionFactoryConfigPath);
            permission TempPermission = PermissionSrv.GetPermission(mObject, mOperation, _App.AppID);

            if (TempPermission != null)
            {
                foreach (role r in RoleLst)
                {
                    // not using r.Permissions because amount of roles allway is less than amount of Permissions. ->because perfomance
                    if (TempPermission.Roles.Contains(r))
                    {
                        TempPermission.Roles.Remove(r);
                    }
                }
                PermissionSrv.CommitChanges();
            }
        }

        /// <summary>
        /// remove all oldrole assign for the permission and assign new [mRoles] for the permission
        /// </summary>
        /// <param name="mObject"></param>
        /// <param name="mOperation"></param>
        /// <param name="mRoles"></param>
        public void UpdatePermission(string mObject, string mOperation, string[] mRoles)
        {
            if (_App == null) return;

            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            string HQL = "from role r where r.AppID = :AppID AND r.name in ({0})";
            string ParaStr = ":" + string.Join(",:", mRoles);
            HQL = string.Format(HQL, ParaStr);
            SQLParam[] paramList = new SQLParam[mRoles.Length + 1];
            paramList[0] = new SQLParam("AppID", _App.AppID);
            for (int i = 0; i < mRoles.Length; i++)
            {
                paramList[i + 1] = new SQLParam(mRoles[i], mRoles[i]);
            }
            //List<role> RoleLst = RoleSrv.GetbyHQuery(HQL, new SQLParam("rolenames", string.Join(",",mRoles)), new SQLParam("AppID", _App.AppID));
            List<role> RoleLst = RoleSrv.GetbyHQuery(HQL, paramList);
            if (RoleLst == null || RoleLst.Count == 0) return;
            //Grant permission            
            IpermissionService PermissionSrv = new permissionService(SessionFactoryConfigPath);
            permission TempPermission = PermissionSrv.GetPermission(mObject, mOperation, _App.AppID);
            if (TempPermission != null)
            {
                List<role> TmpRolseLst = new List<role>();
                foreach (role r in TempPermission.Roles)
                    if (!RoleLst.Contains(r)) TmpRolseLst.Add(r);

                foreach (role r in TmpRolseLst) TempPermission.Roles.Remove(r);

                foreach (role r in RoleLst)
                    if (!TempPermission.Roles.Contains(r)) TempPermission.Roles.Add(r);

                PermissionSrv.CommitChanges();
            }
        }

        public void AddInheritance(string r_asc, string r_desc) { throw new Exception("This method have not Implement."); }
        public void DeleteInheritance(string r_asc, string r_desc) { throw new Exception("This method have not Implement."); }
        public void AddAscendant(string r_asc, string r_desc) { throw new Exception("This method have not Implement."); }
        public void AddDescendant(string r_asc, string r_desc) { throw new Exception("This method have not Implement."); }

        public void InstallObject(string mObject, string[] mOperations)
        {
            if (_App == null) return;
            IoperationService OperationSrv = new operationService(SessionFactoryConfigPath);
            IobjectService ObjectSrv = new objectService(SessionFactoryConfigPath);
            IpermissionService PermissionSrv = new permissionService(SessionFactoryConfigPath);
            objectRbac tempObject = ObjectSrv.GetByName(mObject, _App.AppID);
            if (tempObject != null) return;
            tempObject = new objectRbac();
            tempObject.AppID = _App.AppID;
            tempObject.name = mObject;
            ObjectSrv.CreateNew(tempObject);
            foreach (string ope in mOperations)
            {
                operation TempOpe = OperationSrv.GetByName(ope, _App.AppID);
                if (TempOpe == null)
                {
                    TempOpe = new operation();
                    TempOpe.name = ope;
                    TempOpe.AppID = _App.AppID;
                    OperationSrv.CreateNew(TempOpe);
                }

                permission TempPermission = new permission();
                TempPermission.AppID = _App.AppID;
                TempPermission.name = tempObject.name + ":" + TempOpe.name;
                TempPermission.ObjectRBAC = tempObject;
                TempPermission.Operation = TempOpe;
                PermissionSrv.CreateNew(TempPermission);
            }
            PermissionSrv.CommitChanges();
        }

        public void InstallObject(IRbacObject mObject)
        {
            InstallObject(mObject.ObjectName, mObject.Operations);
        }

        public void UnInstallObject(string mObject)
        {
            if (_App == null) return;
            IoperationService OperationSrv = new operationService(SessionFactoryConfigPath);
            IobjectService ObjectSrv = new objectService(SessionFactoryConfigPath);
            IpermissionService PermissionSrv = new permissionService(SessionFactoryConfigPath);
            objectRbac tempObject = ObjectSrv.GetByName(mObject, _App.AppID);
            if (tempObject == null) return;
            string HQL = "from permission per where per.ObjectRBAC = :ObjectRBAC";
            IList<permission> PerList = PermissionSrv.GetbyHQuery(HQL, new SQLParam("ObjectRBAC", tempObject));
            foreach (permission per in PerList)
            {
                per.Roles.Clear();
                PermissionSrv.Delete(per);
            }
            ObjectSrv.Delete(tempObject);
            ObjectSrv.CommitChanges();
        }

        public IList<operation> GetOperationsOnRoleObject(string mRole, string mObject)
        {
            if (_App == null) return null;
            IobjectService ObjectSrv = new objectService(SessionFactoryConfigPath);
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            role TempRole = RoleSrv.GetByName(mRole, _App.AppID);
            objectRbac TempObject = ObjectSrv.GetByName(mObject, _App.AppID);
            return (from per in TempRole.Permissions where (per.ObjectRBAC == TempObject) select per.Operation).ToList<operation>();
        }
        /// <summary>
        /// get All Operations on mObject in one role
        /// </summary>
        /// <param name="mRole"></param>
        /// <param name="mObject"></param>
        /// <returns></returns>
        public string[] RoleOperationsOnObject(string mRole, string mObject)
        {
            IList<operation> _lst = GetOperationsOnRoleObject(mRole, mObject);
            if (_lst == null) return null;
            return (from ope in _lst select ope.name).ToArray();
        }
        /// <summary>
        /// getAllPermissionName from Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public string[] RolePermissions(string role)
        {
            if (_App == null) return null;

            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            role TempRole = RoleSrv.GetByName(role, _App.AppID);
            if (TempRole == null) return null;
            else return (from per in TempRole.Permissions select per.name).ToArray();
        }
        public string[] SessionRoles(string session)
        { throw new Exception("This method have not Implement."); }
        public void AssignUserToRole(string mUser, string mRole)
        {
            if (_App == null) return;
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            user TempUser = UserSrv.GetByName(mUser, _App.AppID);
            if (TempUser == null) return;
            role TempRole = RoleSrv.GetByName(mRole, _App.AppID);
            if (TempRole == null) return;
            if (!TempUser.Roles.Contains(TempRole))
            {
                TempUser.Roles.Add(TempRole);
                RoleSrv.CommitChanges();
            }
        }
        public void DeassignUserToRole(string mUser, string mRole)
        {
            if (_App == null) return;
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            IroleService RoleSrv = new roleService(SessionFactoryConfigPath);
            user TempUser = UserSrv.GetByName(mUser, _App.AppID);
            if (TempUser == null) return;
            role TempRole = RoleSrv.GetByName(mRole, _App.AppID);
            if (TempRole == null) return;
            if (TempUser.Roles.Contains(TempRole)) TempUser.Roles.Remove(TempRole);
            UserSrv.CommitChanges();
        }
        #endregion

    }
}
