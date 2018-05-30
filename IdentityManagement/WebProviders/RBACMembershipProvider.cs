using System;
using System.Collections.Generic;
using System.Web.Security;
using IdentityManagement.Domain;
using IdentityManagement.Service;
using IdentityManagement.ImplementService;
using FX.Data;
using log4net;
using System.Linq;
namespace IdentityManagement.WebProviders
{
    public class RBACMembershipProvider : IRBACMembershipProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RBACMembershipProvider));
        Applications _App;

        public Applications Application
        {
            get { return _App; }
        }
        string _SessionFactoryConfigPath;

        public string ApplicationName
        {
            get { return _App != null ? _App.AppName : ""; }
            set
            {
                if (_App != null || _SessionFactoryConfigPath == string.Empty)
                {
                    log.Error("You cannot set Application for this provider because it had seted Application or SessionFactoryConfigPath is empty");
                    throw new Exception("You cannot set Application for this provider because it had seted Application or SessionFactoryConfigPath is empty");
                }
                else
                {
                    _App = new ApplicationsService(_SessionFactoryConfigPath).GetByName(value);
                }
            }
        }
        public string SessionFactoryConfigPath
        {
            get { return _SessionFactoryConfigPath; }
        }

        public RBACMembershipProvider(string mApplicationName, string mSessionFactoryConfigPath)
        {
            _SessionFactoryConfigPath = mSessionFactoryConfigPath;
            ApplicationName = mApplicationName;

        }

        #region Override MembershipProvider
        public void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {

            // Initialize values from web.config.
            //

            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "OdbcMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Sample FX Membership provider");
            }

            // Initialize the abstract base class.
            // base.Initialize(name, config);

            //ApplicationName = GetConfigValue(config["applicationName"],System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _MaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _PasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            _MinRequiredNonalphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            _MinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            _PasswordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
            _EnablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            _EnablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            _RequiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            _RequiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
            //_WriteExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "true"));

            string temp_format = config["passwordFormat"];
            if (temp_format == null)
            {
                temp_format = "Hashed";
            }

            switch (temp_format)
            {
                case "Hashed":
                    _PasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    _PasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    _PasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new System.Configuration.Provider.ProviderException("Password format not supported.");
            }

        }


        //
        // A helper function to retrieve config values from the configuration file.
        //

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        private bool _EnablePasswordReset;
        private bool _EnablePasswordRetrieval;
        private int _MaxInvalidPasswordAttempts;
        private int _MinRequiredNonalphanumericCharacters;
        private int _MinRequiredPasswordLength;
        private int _PasswordAttemptWindow;
        private MembershipPasswordFormat _PasswordFormat = MembershipPasswordFormat.Hashed;
        private string _PasswordStrengthRegularExpression;
        private bool _RequiresQuestionAndAnswer;
        private bool _RequiresUniqueEmail;

        /// <summary>
        /// required implementation
        /// </summary>
        public bool EnablePasswordReset
        {
            get { return _EnablePasswordReset; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public bool EnablePasswordRetrieval
        {
            get { return _EnablePasswordRetrieval; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public int MaxInvalidPasswordAttempts
        {
            get { return _MaxInvalidPasswordAttempts; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public int MinRequiredNonAlphanumericCharacters
        {
            get { return _MinRequiredNonalphanumericCharacters; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public int MinRequiredPasswordLength
        {
            get { return _MinRequiredPasswordLength; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public int PasswordAttemptWindow
        {
            get { return _PasswordAttemptWindow; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public MembershipPasswordFormat PasswordFormat
        {
            get { return _PasswordFormat; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public string PasswordStrengthRegularExpression
        {
            get { return _PasswordStrengthRegularExpression; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public bool RequiresQuestionAndAnswer
        {
            get { return _RequiresQuestionAndAnswer; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public bool RequiresUniqueEmail
        {
            get { return _RequiresUniqueEmail; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">a username</param>
        /// <param name="oldPassword">original password</param>
        /// <param name="newPassword">new password</param>
        /// <returns>true or false</returns>
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            log.Info("ChangePassword user: " + username + " in Application: " + _App.AppName);
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            if (_App == null) return false;
            user TemUser = UserSrv.GetByName(username, _App.AppID);
            if (TemUser == null) return false;
            string OldPassWordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(oldPassword, "MD5");
            if (TemUser.password != OldPassWordHash) return false;
            string NewPassWordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "MD5");
            TemUser.password = NewPassWordHash;

            try
            {
                UserSrv.Update(TemUser);
                UserSrv.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("ERR in ChangePassword user: " + username + " in Application " + _App.AppName, ex);
                return false;
            }

        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">a username</param>
        /// <param name="password">the password</param>
        /// <param name="newPasswordQuestion">new question</param>
        /// <param name="newPasswordAnswer">new answer</param>
        /// <returns>true or false</returns>
        public bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            log.Info("ChangePasswordQuestionAndAnswer user: " + username + " in Application: " + _App.AppName);
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user TempUser = UserSrv.GetByName(username, _App.AppID);
            if (TempUser == null) return false;

            try
            {
                TempUser.password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                TempUser.PasswordQuestion = newPasswordQuestion;
                TempUser.PasswordAnswer = newPasswordAnswer;
                UserSrv.Update(TempUser);
                UserSrv.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("ERR in ChangePasswordQuestionAndAnswer user: " + username + " in Application " + _App.AppName, ex);
                return false;
            }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">required implementation</param>
        /// <param name="password">required implementation</param>
        /// <param name="email">required implementation</param>
        /// <param name="passwordQuestion">required implementation</param>
        /// <param name="passwordAnswer">required implementation</param>
        /// <param name="isApproved">required implementation</param>
        /// <param name="providerUserKey">required implementation</param>
        /// <param name="status">required implementation</param>
        /// <returns>a user object</returns>
        public user CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            log.Info("Create new User: " + username + " in Application " + _App.AppName);
            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user TempUser = UserSrv.GetByName(username);
            if (TempUser != null) { status = MembershipCreateStatus.DuplicateUserName; return null; }
            TempUser = new user();
            TempUser.username = username;
            TempUser.password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            TempUser.PasswordSalt = "MD5";
            TempUser.PasswordFormat = (int)PasswordFormat;
            TempUser.email = email;
            TempUser.PasswordQuestion = passwordQuestion;
            TempUser.PasswordAnswer = passwordAnswer;
            TempUser.IsApproved = isApproved;
            TempUser.ApplicationList = new List<Applications>();
            TempUser.ApplicationList.Add(_App);
            try
            {
                TempUser = UserSrv.CreateNew(TempUser);
                UserSrv.CommitChanges();
                status = MembershipCreateStatus.Success;
                return TempUser;
            }
            catch (Exception ex)
            {
                log.Error("CreateUser Error", ex);
                status = MembershipCreateStatus.ProviderError;
                return null;
            }


        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">required implementation</param>
        /// <param name="deleteAllRelatedData">required implementation</param>
        /// <returns>required implementation</returns>
        public bool DeleteUser(int userid, bool deleteAllRelatedData)
        {
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user TempUser = UserSrv.Getbykey(userid);
            if (TempUser == null) return false;

            try
            {
                UserSrv.Delete(TempUser);
                UserSrv.CommitChanges();
                log.Info("Delete User: " + TempUser.username);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error  Delete User: " + TempUser.username, ex);
                return false;
            }
        }

        public bool DeleteUser(user mUser, bool deleteAllRelatedData)
        {
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            if (mUser == null) return false;
            try
            {
                UserSrv.Delete(mUser);
                UserSrv.CommitChanges();
                log.Info("Delete User: " + mUser.username);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error  Delete User: " + mUser.username, ex);
                return false;
            }
        }
        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="emailToMatch">required implementation</param>
        /// <param name="pageIndex">required implementation</param>
        /// <param name="pageSize">required implementation</param>
        /// <param name="totalRecords">required implementation</param>
        /// <returns>required implementation</returns>
        public IList<user> FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            log.Info("FindUsersByEmail Application " + _App.AppName);
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            string hql = "select u from user u join u.ApplicationList app where u.email like %:email% AND app.AppID = :AppID  ";
            try
            {
                IList<user> lst = UserSrv.GetbyHQuery(hql, pageIndex, pageSize, out totalRecords, new SQLParam("email", emailToMatch), new SQLParam("AppID", _App.AppID));
                return lst;
            }
            catch (Exception ex)
            {
                log.Error("FindUserEmail Error Application " + _App.AppName, ex);
                totalRecords = 0;
                return null;
            }

        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="usernameToMatch">required implementation</param>
        /// <param name="pageIndex">required implementation</param>
        /// <param name="pageSize">required implementation</param>
        /// <param name="totalRecords">required implementation</param>
        /// <returns>required implementation</returns>
        public IList<user> FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            log.Info("FinUserByName Application " + _App.AppName);
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            string hql = "select u from user u join u.ApplicationList app where u.username like %:username% AND app.AppID = :AppID  ";

            try
            {
                IList<user> lst = UserSrv.GetbyHQuery(hql, pageIndex, pageSize, out totalRecords, new SQLParam("username", usernameToMatch), new SQLParam("AppID", _App.AppID));
                return lst;
            }
            catch (Exception ex)
            {
                log.Error("FindUsersByName Error Application " + _App.AppName, ex);
                totalRecords = 0;
                return null;
            }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="pageIndex">required implementation</param>
        /// <param name="pageSize">required implementation</param>
        /// <param name="totalRecords">required implementation</param>
        /// <returns>required implementation</returns>
        public IList<user> GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            log.Info("GetAllUsers Application " + _App.AppName);
            IuserService UserSrv = new userService(SessionFactoryConfigPath);

            try
            {
                string HQL = "select u from user u join u.ApplicationList app where app.AppID = :appid order by u.CreateDate ";
                IList<user> lst = UserSrv.GetbyHQuery(HQL, pageIndex, pageSize, out totalRecords, new SQLParam("appid", _App.AppID));
                return lst;
            }
            catch (Exception ex)
            {
                log.Error("GetAllUsers Error Application " + _App.AppName, ex);
                totalRecords = 0;
                return null;
            }

        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <returns>required implementation</returns>
        public int GetNumberOfUsersOnline()
        {
            //log.Info("GetNumberOfUsersOnline Application: " + _App.AppName);

            //try
            //{
            //    string HQL = "select count(u) from user u join u.ApplicationList app where u.IsLockedOut = False AND app.AppID = :AppID  ";
            //   return (int)UserSrv.ExcuteNonQuery(HQL, new SQLParam("AppID", _App.AppID));
            //}
            //catch (Exception ex)
            //{
            //    log.Error("GetNumberOfUsersOnline Error Application " + _App.AppName, ex);
            //    return 0;
            //}
            throw new Exception("This metho have not implement.");

        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">required implementation</param>
        /// <param name="answer">required implementation</param>
        /// <returns>required implementation</returns>
        public string GetPassword(string username, string answer)
        {
            throw new Exception("have not implement.");
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">required implementation</param>
        /// <param name="userIsOnline">required implementation</param>
        /// <returns>required implementation</returns>
        public user GetUser(string username, bool userIsOnline)
        {
            log.Info("GetNumberOfUsersOnline Application: " + _App.AppName);

            try
            {
                IuserService UserSrv = new userService(SessionFactoryConfigPath);
                user TempUser = UserSrv.GetByName(username, _App.AppID);
                return TempUser;
            }
            catch (Exception ex)
            {
                log.Error("GetNumberOfUsersOnline Error Application " + _App.AppName, ex);
                return null;
            }

        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="providerUserKey">required implementation</param>
        /// <param name="userIsOnline">required implementation</param>
        /// <returns>required implementation</returns>
        public user GetUser(object providerUserKey, bool userIsOnline)
        {
            if (providerUserKey is int)
            {
                IuserService UserSrv = new userService(SessionFactoryConfigPath);
                int userID = (int)providerUserKey;
                return UserSrv.Getbykey(userID);
            }
            else return null;
            //throw new Exception("have not implement.");
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="email">required implementation</param>
        /// <returns>required implementation</returns>
        public string GetUserNameByEmail(string email)
        {
            log.Info("GetUserNameByEmail:" + email + " in Application: " + _App.AppName);
            //string HQL = "select u from user u join u.ApplicationList app where u.email = :email AND app.AppID = :AppID  ";
            try
            {
                IuserService UserSrv = new userService(SessionFactoryConfigPath);
                //IList<user> lst = UserSrv.GetbyHQuery(HQL, new SQLParam("email", email), new SQLParam("AppID", _App.AppID));
                IList<user> lst = UserSrv.SearchUser("", email, _App.AppID);
                return (lst == null || lst.Count == 0) ? null : lst[0].username;
            }
            catch (Exception ex)
            {
                log.Error("Error GetUserNameByEmail: " + email + " in Application: " + _App.AppName, ex);
                return "";
            }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">required implementation</param>
        /// <param name="answer">required implementation</param>
        /// <returns>required implementation</returns>
        public string ResetPassword(string username, string answer)
        {
            log.Info("ResetPassword:" + username + " in Application: " + _App.AppName);

            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("Password reset is not enabled.");
            }

            if (answer == null && RequiresQuestionAndAnswer)
            {
                UpdateFailureCount(username, "passwordAnswer");
                throw new System.Configuration.Provider.ProviderException("Password answer required for password reset.");
            }
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user TempUser = UserSrv.GetByName(username, _App.AppID);
            if (TempUser.PasswordAnswer.ToUpper() != answer.ToUpper()) return "";
            else
            {
                string pass = CreateRandomPassword(MinRequiredPasswordLength > 7 ? MinRequiredPasswordLength : 7);
                TempUser.password = FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5");
                try
                {
                    UserSrv.Update(TempUser);
                    UserSrv.CommitChanges();
                    return pass;
                }
                catch (Exception ex)
                {
                    log.Error("Error ResetPassword: " + username + " in Application: " + _App.AppName, ex);
                    return "";
                }
            }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="userName">required implementation</param>
        /// <returns>required implementation</returns>
        public bool UnlockUser(string userName)
        {
            log.Info("UnlockUser:" + userName + " in Application: " + _App.AppName);
            string HQL = "update user u set u.IsLockedOut = false join u.Applications app where u.username = :username AND app.AppID=:AppID";
            try
            {
                IuserService UserSrv = new userService(SessionFactoryConfigPath);
                object ret = UserSrv.ExcuteNonQuery(HQL, true, new SQLParam("username", userName), new SQLParam("AppID", _App.AppID));
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error UnlockUser: " + userName + " in Application: " + _App.AppName, ex);
                return false;
            }
            //throw new Exception("have not implement.");
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="user">required implementation</param>
        public void UpdateUser(user muser)
        {
            log.Info("UpdateUser:" + muser.username + " in Application: " + _App.AppName);
            try
            {
                if (muser == null) return;
                IuserService UserSrv = new userService(SessionFactoryConfigPath);
                UserSrv.Update(muser);
                UserSrv.CommitChanges();
            }
            catch (Exception ex)
            {
                log.Error("Error UpdateUser: " + muser.username + " in Application: " + _App.AppName, ex);
                return;
            }

        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">required implementation</param>
        /// <param name="password">required implementation</param>
        /// <returns>required implementation</returns>
        public bool ValidateUser(string username, string password)
        {
            log.Info("ValidateUser:" + username + " in Application: " + _App.AppName);
            try
            {
                IuserService UserSrv = new userService(SessionFactoryConfigPath);
                user TempUser = UserSrv.GetByName(username, _App.AppID);
                string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                if (TempUser != null && TempUser.password == pass && TempUser.IsApproved && (!TempUser.IsLockedOut)) return true;
                else return false;
            }
            catch (Exception ex)
            {
                log.Error("Error ValidateUser: " + username + " in Application: " + _App.AppName, ex);
                return false;
            }
        }
        //System.Web.Profile.SqlProfileProvider
        #endregion
        public user CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out string status)
        {
            log.Info("Create new User: " + username + " in Application " + _App.AppName);
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user TempUser = UserSrv.GetByName(username);
            if (TempUser != null) { status = "DuplicateUserName"; return null; }
            TempUser = new user();
            TempUser.username = username;
            TempUser.password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            TempUser.PasswordSalt = "MD5";
            TempUser.PasswordFormat = (int)PasswordFormat;
            TempUser.email = email;
            TempUser.PasswordQuestion = passwordQuestion;
            TempUser.PasswordAnswer = passwordAnswer;
            TempUser.IsApproved = isApproved;
            TempUser.ApplicationList = new List<Applications>();
            TempUser.ApplicationList.Add(_App);
            try
            {
                TempUser = UserSrv.CreateNew(TempUser);
                UserSrv.CommitChanges();
                status = "Success";
                return TempUser;
            }
            catch (Exception ex)
            {
                log.Error("CreateUser Error", ex);
                status = "ProviderError";
                return null;
            }
        }

        public user CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, string groupName, out string status)
        {
            log.Info("Create new User: " + username + " in Application " + _App.AppName);
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user TempUser = UserSrv.GetByName(username);
            if (TempUser != null) { status = "DuplicateUserName"; return null; }
            TempUser = new user();
            TempUser.username = username;
            TempUser.password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            TempUser.PasswordSalt = "MD5";
            TempUser.PasswordFormat = (int)PasswordFormat;
            TempUser.email = email;
            TempUser.PasswordQuestion = passwordQuestion;
            TempUser.PasswordAnswer = passwordAnswer;
            TempUser.IsApproved = isApproved;
            TempUser.GroupName = groupName;
            TempUser.ApplicationList = new List<Applications>();
            TempUser.ApplicationList.Add(_App);
            try
            {
                TempUser = UserSrv.CreateNew(TempUser);
                UserSrv.CommitChanges();
                status = "Success";
                return TempUser;
            }
            catch (Exception ex)
            {
                log.Error("CreateUser Error", ex);
                status = "ProviderError";
                return null;
            }
        }

        public user AuthenUser(string mUserName, string mPassword)
        {
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user TempUser = UserSrv.GetByName(mUserName, _App.AppID);
            if (TempUser != null && TempUser.IsApproved && (!TempUser.IsLockedOut))
            {
                string _PassHash = FormsAuthentication.HashPasswordForStoringInConfigFile(mPassword, "MD5");
                if (TempUser.password == _PassHash) return TempUser;
            }
            return null;
        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }

            return new string(chars);
        }
        private void UpdateFailureCount(string username, string failureType) { }
    }
}
