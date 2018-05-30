using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FX.Data;
using IdentityManagement.Domain;
using IdentityManagement.Service;

namespace IdentityManagement.ImplementService
{
    public class ApplicationsService : BaseService<Applications, int>, IApplicationsService
    {
        public ApplicationsService(string sessionFactoryConfigPath)
            : base(sessionFactoryConfigPath)
        { 
            
        }

        public Applications GetByName(string AppName)
        {
            string hsql = "from Applications app where app.AppName = :AppName";
            List<Applications> _lst = GetbyHQuery(hsql, new SQLParam[] { new SQLParam("AppName", AppName) });
            return (_lst == null || _lst.Count == 0) ? null : _lst[0];
        }
        public IList<Applications> SearchApplication(string AppName)
        {
            string hsql = "from Applications app where app.AppName Like :AppName";
            return  GetbyHQuery(hsql, new SQLParam[] { new SQLParam("AppName", "%" + AppName + "%") });
        }

        public Applications CreateNewApplication(string AppName, string AppDescription, string AppUrl, string username, string password)
        {
            Applications TempApp = GetByName(AppName);
            if (TempApp != null) throw new Exception("This Application is exist.");

            userService UserSrv = new userService(SessionFactoryConfigPath);
            roleService RoleSrv = new roleService(SessionFactoryConfigPath);
            objectService ObjectSrv = new objectService(SessionFactoryConfigPath);
            operationService OperationSrv = new operationService(SessionFactoryConfigPath);
            permissionService PermitSrv = new permissionService(SessionFactoryConfigPath);
           
            TempApp = new Applications();
            TempApp.AppName = AppName;
            TempApp.Description = AppDescription;
            TempApp.URL = AppUrl;
            user TemUser = UserSrv.GetByName(username);
            if (TemUser != null)
                throw new Exception("Root User is Exist in other Applications");
            TemUser = new user();
            TemUser.username = username;
            TemUser.password = password;

            role TemRole = new role();
            TemRole.name = role.RootRole;

            objectRbac TempObject = new objectRbac();
            TempObject.name = objectRbac.Default;

            operation TempOpe = new operation();
            TempOpe.name = operation.Default;
            TempOpe.canread = true;

            permission TemPermission = new permission();
            TemPermission.name = permission.Default;

            //begin transaction
            TempApp = CreateNew(TempApp);
            TempObject.AppID = TempApp.AppID;
            TempObject = ObjectSrv.CreateNew(TempObject);
            TempOpe.AppID = TempApp.AppID;
            TempOpe = OperationSrv.CreateNew(TempOpe);

            TemPermission.AppID = TempApp.AppID;
            TemPermission.ObjectRBAC = TempObject;
            TemPermission.Operation = TempOpe;
            TemPermission = PermitSrv.CreateNew(TemPermission);

            TemRole.Permissions = new List<permission>();
            TemRole.Permissions.Add(TemPermission);
            TemRole.AppID = TempApp.AppID;
            TemRole = RoleSrv.CreateNew(TemRole);

            TemUser.ApplicationList = new List<Applications>();
            TemUser.ApplicationList.Add(TempApp);
            TemUser.Roles = new List<role>();
            TemUser.Roles.Add(TemRole);
            TemUser = UserSrv.CreateNew(TemUser);
            CommitChanges();
            return TempApp;
        }
        public Applications CreateNewWithShareUser(string AppName, string AppDescription, string AppUrl, string username)
        {
            Applications TempApp = GetByName(AppName);
            if (TempApp != null) throw new Exception("This Application is exist.");

            userService UserSrv = new userService(SessionFactoryConfigPath);
            roleService RoleSrv = new roleService(SessionFactoryConfigPath);
            objectService ObjectSrv = new objectService(SessionFactoryConfigPath);
            operationService OperationSrv = new operationService(SessionFactoryConfigPath);
            permissionService PermitSrv = new permissionService(SessionFactoryConfigPath);

            TempApp = new Applications();
            TempApp.AppName = AppName;
            TempApp.Description = AppDescription;
            TempApp.URL = AppUrl;

            user TemUser = UserSrv.GetByName(username);
            if (TemUser == null)
                throw new Exception("Root User is not exist in other Applications");

            role TemRole = new role();
            TemRole.name = role.RootRole;

            objectRbac TempObject = new objectRbac();
            TempObject.name = objectRbac.Default;

            operation TempOpe = new operation();
            TempOpe.name = operation.Default;
            TempOpe.canread = true;

            permission TemPermission = new permission();
            TemPermission.name = permission.Default;

            //begin transaction
            TempApp = CreateNew(TempApp);
            TempObject.AppID = TempApp.AppID;
            TempObject = ObjectSrv.CreateNew(TempObject);
            TempOpe.AppID = TempApp.AppID;
            TempOpe = OperationSrv.CreateNew(TempOpe);

            TemPermission.AppID = TempApp.AppID;
            TemPermission.ObjectRBAC = TempObject;
            TemPermission.Operation = TempOpe;
            TemPermission = PermitSrv.CreateNew(TemPermission);

            TemRole.Permissions = new List<permission>();
            TemRole.Permissions.Add(TemPermission);
            TemRole.AppID = TempApp.AppID;
            TemRole = RoleSrv.CreateNew(TemRole);

            TemUser.ApplicationList.Add(TempApp);
            TemUser.Roles.Add(TemRole);
            TemUser = UserSrv.Update(TemUser);
            CommitChanges();
            return TempApp;
        }

        public Applications GetByURL(string AppURL)
        {
            throw new Exception("This method is not implement.");
        }

        public role RootRole
        {
            get { return null; }
        }
    }

}