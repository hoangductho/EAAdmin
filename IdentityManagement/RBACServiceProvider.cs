using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FX.Data;
using IdentityManagement.Domain;
using IdentityManagement.Service;
using IdentityManagement.ImplementService;
namespace IdentityManagement
{
    public class RBACServiceProvider : IRBACService
    {

        private Applications App;
        
        IuserService UserSrv;
        IroleService RoleSrv;
        IoperationService OperationSrv;
        IobjectService ObjectSrv;
        IpermissionService PermissionSrv;
        public RBACServiceProvider(string mApplicationName, string mSessionFactoryConfigPath)
        {
            App = new ApplicationsService(mSessionFactoryConfigPath).GetByName(mApplicationName);
            UserSrv = new userService(mSessionFactoryConfigPath);
            RoleSrv = new roleService(mSessionFactoryConfigPath);
            OperationSrv = new operationService(mSessionFactoryConfigPath);
            ObjectSrv = new objectService(mSessionFactoryConfigPath);
            PermissionSrv = new permissionService(mSessionFactoryConfigPath);
        }
        #region Administrative Commands
        public void AddUser(user mUser) 
        {
            user TempUser = Mapping(mUser);
            if (TempUser == null)
            {
                mUser.ApplicationList = new List<Applications>();
                mUser.ApplicationList.Add(App);
                UserSrv.CreateNew(mUser);
            }
            else
            {
                if (!TempUser.ApplicationList.Contains(App)) 
                {
                    TempUser.ApplicationList.Add(App);
                }
            }
            UserSrv.CommitChanges();
        }
        public void DeleteUser(user mUser) 
        {
            user temp = Mapping(mUser);
            if (temp == null) throw new Exception("This user does not exist in system");
            else
            {
                if (temp.ApplicationList.Contains(App))
                {
                    temp.ApplicationList.Remove(App);
                    if (temp.ApplicationList.Count == 0) UserSrv.Delete(temp);
                    UserSrv.CommitChanges();
                }
            }

        }
        public void AddRole(role mRole) 
        {
            
            mRole.AppID  = App.AppID;
            RoleSrv.CreateNew(mRole);
            //throw new Exception("This method have not Implement."); 
        }
        public void DeleteRole(role mRole) 
        {
            //mapping Roles
            role TempRole = Mapping(mRole);
            if (TempRole != null)
            {
                RoleSrv.Delete(TempRole);
                RoleSrv.CommitChanges();
            }
        }
        public void AssignUser(user mUser, role mRole)
        {
            user TempUser = Mapping(mUser);       
            if (TempUser == null) throw new Exception("User does not exist in system.");
            role TempRole = Mapping(mRole);
            if (TempRole == null) throw new Exception("Role does not exist in system.");
            if (!TempUser.Roles.Contains(TempRole)) TempUser.Roles.Add(TempRole);
            UserSrv.CommitChanges();
        }
        public void DeassignUser(user mUser, role mRole) 
        {
            user TempUser = Mapping(mUser);
            if (TempUser == null) throw new Exception("User does not exist in system.");

            role TempRole = Mapping(mRole);
            if (TempRole == null) throw new Exception("Role does not exist in system.");

            if (TempUser.Roles.Contains(TempRole)) TempUser.Roles.Remove(TempRole);
            UserSrv.CommitChanges();
        }

        public void AddObject(objectRbac mObject)
        {
            mObject.AppID = App.AppID;
            ObjectSrv.CreateNew(mObject);
        }
        public void DeleteObject(objectRbac mObject)
        {
            objectRbac tempObject = Mapping(mObject) ;
            if (tempObject != null) ObjectSrv.Delete(tempObject);
            ObjectSrv.CommitChanges();
        }

        public void AddOperation(operation mOperation)
        {
            mOperation.AppID = App.AppID;
            OperationSrv.CreateNew(mOperation);
        }

        public void DeleteOperation(operation mOperation)
        {
            operation tempOperation = Mapping(mOperation);
            if (tempOperation != null) OperationSrv.Delete(tempOperation);
            OperationSrv.CommitChanges();
        }

        public void GrantPermission(objectRbac mObject, operation mOperation, role mRole) 
        {
           
            role TempRole = Mapping(mRole);
            if (TempRole == null) throw new Exception("Role Does not exist in system.");
           
            objectRbac tempObject = Mapping(mObject);
            operation tempOperation = Mapping(mOperation);

            //Grant permission
            string hsql = "from permission per where per.ObjectRBAC = :ObjectRBAC AND  per.Operation = :Operation ";
            
            List<permission> _lst = PermissionSrv.GetbyHQuery(hsql, new SQLParam("ObjectRBAC", tempObject), new SQLParam("ObjectRBAC", tempOperation));
            permission TempPermission = (_lst == null || _lst.Count == 0) ? null : _lst[0];
            if (TempPermission == null)
            {
                TempPermission = new permission();
                TempPermission.AppID = App.AppID;
                TempPermission.name = tempObject.name + ":" + tempOperation.name;
                TempPermission.ObjectRBAC = tempObject;
                TempPermission.Operation = tempOperation;
                PermissionSrv.CreateNew(TempPermission);
                TempRole.Permissions.Add(TempPermission);
                RoleSrv.CommitChanges();
            }

            else
            {
                if (!TempRole.Permissions.Contains(TempPermission))
                {
                    TempRole.Permissions.Add(TempPermission);
                    RoleSrv.CommitChanges();
                }
            }
           
        }

        public void RevokePermission(operation mOperation, objectRbac mObject, role mRole) 
        {
            role TempRole = Mapping(mRole);
            if (TempRole == null) throw new Exception("Role Does not exist in system.");

            objectRbac tempObject = Mapping(mObject);
            operation tempOperation = Mapping(mOperation);
            if (tempOperation == null || tempObject == null) return;
            //Grant permission
            string hsql = "from permission per where per.ObjectRBAC = :ObjectRBAC AND  per.Operation = :Operation ";
           
            List<permission> _lst = PermissionSrv.GetbyHQuery(hsql, new SQLParam("ObjectRBAC", tempObject), new SQLParam("ObjectRBAC", tempOperation));
            permission TempPermission = (_lst == null || _lst.Count == 0) ? null : _lst[0];

            if (TempPermission != null)
            {
                if (TempRole.Permissions.Contains(TempPermission))
                {
                    TempRole.Permissions.Remove(TempPermission);
                    RoleSrv.CommitChanges();
                }
            }
        }

        public void AddInheritance(role r_asc, role r_desc) { throw new Exception("This method have not Implement."); }

        public void DeleteInheritance(role r_asc, role r_desc) { throw new Exception("This method have not Implement."); }

        public void AddAscendant(role r_asc, role r_desc) { throw new Exception("This method have not Implement."); }

        public void AddDescendant(role r_asc, role r_desc) { throw new Exception("This method have not Implement."); }
        #endregion

        #region System Functions(session)

        public void CreateSession(user mUser, session mSession) { throw new Exception("This method have not Implement."); }

        public void DeleteSession(user mUser, session mSession) { throw new Exception("This method have not Implement."); }

        public void AddActiveRole(user mUser, session mSession, role mRole) { throw new Exception("This method have not Implement."); }

        public void DropActiveRole(user mUser, session mSession, role mRole) { throw new Exception("This method have not Implement."); }

        public bool CheckAccess(session mSession, operation mOperation, objectRbac mObject) { throw new Exception("This method have not Implement."); }
        #endregion

        #region Review Functions

        public IList<user> AuthorizedUsers(role mRole) 
        {
            role TempRole = Mapping(mRole);
            if (TempRole != null)
            {
                return TempRole.Users;
            }
            else return null;
        }

        public IList<role> AuthorizedRoles(user mUser) 
        {
            user TempUser = Mapping(mUser);
            if (TempUser != null) return (from _role in TempUser.Roles where (_role.AppID == App.AppID) select _role).ToList<role>();
            else return null;
        }

        public IList<permission> RolePermissions(role mRole) 
        {
            role TempRole = Mapping(mRole);
            return TempRole.Permissions;
        }

        public IList<permission> UserPermissions(user mUser) 
        {
            IList<permission> ret = new List<permission>() ;
            IList<role> RoleList = AuthorizedRoles(mUser);
            foreach (role r in RoleList)
            {
                IList<permission> _permissionList = RolePermissions(r);
                foreach (permission per in _permissionList)
                {
                    if (!ret.Contains(per)) ret.Add(per);
                }
            }
            return ret;
        }

        public IList<operation> RoleOperationsOnObject(role mRole, objectRbac mObject) 
        {
            role TempRole = Mapping(mRole);
            objectRbac TempObject = Mapping(mObject);
            if(TempRole.AppID != App.AppID || TempObject.AppID != App.AppID) return null ;
            return (from per in TempRole.Permissions where  (per.ObjectRBAC == TempObject) select per.Operation).ToList<operation>();
        }

        public IList<operation> UserOperationsOnObject(user mUser, objectRbac mObject) 
        {
            IList<operation> _ret = new List<operation>();
            objectRbac TempObject = Mapping(mObject);
            IList<role> Role_Lst = AuthorizedRoles(mUser);
            foreach (role r in Role_Lst)
            {
                List<operation> operationLst = (from per in r.Permissions where  (per.ObjectRBAC == TempObject) select per.Operation).ToList<operation>();
                foreach (operation op in operationLst)
                {
                    if (!_ret.Contains(op)) _ret.Add(op);
                }
            }
            return _ret;
        }

        public IList<role> SessionRoles(session mSession) { throw new Exception("This method have not Implement."); }

        public IList<permission> SessionPermissions(session mSession) { throw new Exception("This method have not Implement."); }
        #endregion

        #region Mapping to Persistant object
        //maping an object to a persistant object in Nhibernate
        private user Mapping(user mUser)
        {
            user temp;
            if (mUser.userid > 0) temp = UserSrv.Getbykey(mUser.userid);
            else
            {
                temp = UserSrv.GetByName(mUser.username);
            }
            return temp;
        }
        private role Mapping(role mRole)
        {
            role TempRole = null;
            if (mRole.roleid > 0) TempRole = RoleSrv.Getbykey(mRole.roleid);
            else
            {
                TempRole =RoleSrv.GetByName(mRole.name, App.AppID);
            }
            return TempRole;
        }

        private objectRbac Mapping(objectRbac mObject)
        {
            objectRbac tempObject;
            if (mObject.objectid > 0) tempObject = ObjectSrv.Getbykey(mObject.objectid);
            else
            {
                tempObject = ObjectSrv.GetByName(mObject.name, App.AppID);
            }
            return tempObject;
        }

        private operation Mapping(operation mOperation)
        {
            operation tempOperation;
            if (mOperation.operationid > 0) tempOperation = OperationSrv.Getbykey(mOperation.operationid);
            else
            {
                tempOperation = OperationSrv.GetByName(mOperation.name, App.AppID);
            }
            return tempOperation;
        }

        private session  Mapping(session _temp)
        {
            return null;
        }
        #endregion

        /// <summary>
        /// search user in current Application. Using [Like] command . if username and Email = string.empty then return all user in current Applications
        /// </summary>
        /// <param name="userName">if = string.Empty then not consider this conditions</param>
        /// <param name="Email">if = string.Empty then not consider this conditions</param>
        /// <returns></returns>
        public IList<user> SearchUser(string userName, string Email)
        {
            return UserSrv.SearchUser(userName, Email, App.AppID);
        }
        /// <summary>
        /// search Role in Current Applications
        /// </summary>
        /// <param name="RoleName">if = String.Empty then return all Role in the Application</param>
        /// <returns></returns>
        public IList<role> SearchRole(string RoleName)
        {
            return RoleSrv.SearchRole(RoleName, App.AppID);
        }

        public IList<permission> SearchPermission(string permissionName)
        {
            return PermissionSrv.SearchPermission(permissionName, App.AppID);
        }

        public IList<objectRbac> SearchObject(string ObjectName)
        {
            return ObjectSrv.SearchObject(ObjectName, App.AppID);  
        }

        public IList<objectRbac> SearchOperation(string ObjectName)
        { 
            return null; 
        }

        public permission AddPermission(objectRbac mObject, operation mOperation, string PermissionName)
        {
            throw new Exception("This method have not Implement.");
        }

        public void DeletePermission(permission mPermission)
        {
            throw new Exception("This method have not Implement.");
        }
    }
}
