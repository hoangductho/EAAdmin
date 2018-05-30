using System;
using System.Collections.Generic;
using System.Web.Security;
using IdentityManagement.Domain;
using IdentityManagement.Service;
using IdentityManagement.ImplementService;
using System.Linq;
using FX.Data;
using System.Collections.Specialized;
using log4net;
using System.Configuration;
using System.Configuration.Provider;
namespace IdentityManagement.WebProviders
{
    public class FXRoleProvider : RoleProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RBACMembershipProvider));
        private bool pWriteExceptionsToEventLog = false;
        Applications _App;

        private Applications App
        {
            get
            {
                if (_App == null) _App = new ApplicationsService(_SessionFactoryConfigPath).GetByName(ApplicationName);
                return _App;
            }
        }
        string _SessionFactoryConfigPath;

        IuserService _UserSrv;

        private IuserService UserSrv
        {
            get 
            {
                if (_UserSrv == null) _UserSrv = new userService(_SessionFactoryConfigPath);
                return _UserSrv; 
            }
           
        }
        IroleService _RoleSrv;

        private IroleService RoleSrv
        {
            get
            {
                if (_RoleSrv == null) _RoleSrv = new roleService(_SessionFactoryConfigPath);
                return _RoleSrv;
            }
           
        }

        public string ConnectionStrings
        {
            get { return _SessionFactoryConfigPath; }
            set { _SessionFactoryConfigPath = value; }
        }

        string _ApplicationName;

        public override string ApplicationName
        {
            get { return _ApplicationName; }
            set { _ApplicationName = value; }
        }

        public override void Initialize(string name, NameValueCollection config)
        {

            //
            // Initialize values from web.config.
            //

            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "FXRoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Sample FX Role provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);


            if (config["applicationName"] == null || config["applicationName"].Trim() == "")
            {
                _ApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                _ApplicationName = config["applicationName"];
            }


            if (config["writeExceptionsToEventLog"] != null)
            {
                if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
                {
                    pWriteExceptionsToEventLog = true;
                }
            }


            //
            // Initialize FX Conect.
            //

            ConnectionStringSettings pConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if (pConnectionStringSettings == null || pConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            ConnectionStrings = pConnectionStringSettings.ConnectionString;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            if (_App == null) return;
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
                }
            }
            UserSrv.CommitChanges();
        }

        public override void CreateRole(string roleName)
        {
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
            IList<role> lst = RoleSrv.GetAll(_App.AppID);
            if (lst == null || lst.Count == 0) return new string[] { };
            return (from r in lst select r.name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (_App == null) return null;
            user mUser = UserSrv.GetByName(username, _App.AppID);
            if (mUser == null || mUser.Roles == null || mUser.Roles.Count == 0) return new string[] { };
            else return (from r in mUser.Roles where r.AppID == _App.AppID select r.name).ToArray();
        }
        public override string[] GetUsersInRole(string roleName)
        {
            if (_App == null) return null;
            role mRole = RoleSrv.GetByName(roleName, _App.AppID);
            if (mRole == null) return null;
            return (from u in mRole.Users select u.username).ToArray();

        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (_App == null) return false;
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
            return RoleSrv.GetByName(roleName, _App.AppID) != null;
        }
    }
}
