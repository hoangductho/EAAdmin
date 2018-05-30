using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Web.Security;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;
using System.Web;
using log4net;
using IdentityManagement.Domain;
namespace IdentityManagement.Authorization
{
    public class UserIdentity : IIdentity
    {
        string username;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserIdentity));
        IList<FanxiPermission> _Permissions = new List<FanxiPermission>();
        [XmlElementAttribute("UserIdentity-Permission")]
        public IList<FanxiPermission> Permissions
        {
            get { return _Permissions; }
            set { _Permissions = value; }
        }


        string[] _Roles = new string[] { };
        public string[] Roles
        {
            get { return _Roles; }
            set { _Roles = value; }
        }

        /// <summary>
        /// Gets or sets the UserID of the User
        /// </summary>
        public string Name
        {
            get { return username; }
        }

        string _AuthenticationType;
        /// <summary>
        /// Gets the type of authenticated identity.
        /// Returns:The type of authenticated identity.
        /// </summary>
        public string AuthenticationType { get { return _AuthenticationType; } }
        private bool _IsAuthenticate;
        public bool IsAuthenticated
        {
            get { return _IsAuthenticate; }
        }

        public UserIdentity(string mUsername, IList<FanxiPermission> mPermissions, string[] mRoles)
        {
            _IsAuthenticate = false;
            username = mUsername;
            _AuthenticationType = "FanxiAuthentications";
            _Roles = mRoles;
            Permissions = mPermissions;
        }

        public UserIdentity(string mUsername, IList<FanxiPermission> mPermissions, string[] mRoles, bool mIsAuthenticated, string mAuthenticationType)
        {
            _IsAuthenticate = mIsAuthenticated;
            username = mUsername;
            _AuthenticationType = mAuthenticationType;
            _Roles = mRoles;
            Permissions = mPermissions;
        }

        public UserIdentity(FormsAuthenticationTicket mTicket)
        {
            username = mTicket.Name;
            _IsAuthenticate = !mTicket.Expired;
            _AuthenticationType = "FanxiAuthentications";
            string userData = mTicket.UserData;
            if (userData == "InCache")
            {
                userData = HttpContext.Current.Cache[mTicket.Name] as string;
            }
            if (!String.IsNullOrEmpty(userData))
            {
                try
                {
                    XmlDocument Xdoc = new XmlDocument();
                    string XMLstr = "<Root>" + userData + "</Root>";
                    Xdoc.LoadXml(XMLstr);
                    string RoleStr = Xdoc.GetElementsByTagName("Roles")[0].InnerText;
                    Roles = RoleStr.Split(',');
                    foreach (XmlNode _node in Xdoc.GetElementsByTagName("Per"))
                    {
                        Permissions.Add(new FanxiPermission(_node.InnerText));
                    }
                }
                catch (Exception exception)
                {
                    log.Error(exception);
                    FormsAuthentication.SignOut();
                    HttpContext.Current.Cache.Remove(username);
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Cache.Remove(username);
            }
        }

        /// <summary>
        /// format of UserData is 
        /// <role>role1,role2,..</role><Per>Name,RbacObject,RbacOperation</Per><Per>Name,RbacObject,RbacOperation</Per>...<Per>Name,RbacObject,RbacOperation</Per>
        /// </summary>
        public string UserData
        {
            get
            {
                string mRet = "<Roles>" + string.Join(",", Roles) + "</Roles>";
                foreach (FanxiPermission per in Permissions)
                {
                    mRet += "<Per>" + per.ToString() + "</Per>";
                }
                return mRet;
            }
        }

        public bool PermissionAdmin()
        {
            var isTrue = (from per in Permissions where per.Name.Contains("Admin") select per).ToList();
            if(isTrue.Count() > 0)
                return true;
            return false;
        }        
    }
}

