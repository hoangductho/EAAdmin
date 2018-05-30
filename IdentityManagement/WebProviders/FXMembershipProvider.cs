using System;
using System.Collections.Generic;
using System.Web.Security;
using IdentityManagement.Domain;
using IdentityManagement.Service;
using IdentityManagement.ImplementService;
using FX.Data;
using log4net;
using System.Web.Configuration;
using System.Configuration;
using System.Configuration.Provider;
namespace IdentityManagement.WebProviders
{
    public class FXMembershipProvider : MembershipProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RBACMembershipProvider));
        private bool pWriteExceptionsToEventLog = false;
        private int newPasswordLength = 8;
        private MachineKeySection machineKey;
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

        #region "Property and Config"
        /// <summary>
        /// required implementation
        /// </summary>
        public override bool EnablePasswordReset
        {
            get { return _EnablePasswordReset; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public override bool EnablePasswordRetrieval
        {
            get { return _EnablePasswordRetrieval; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public override int MaxInvalidPasswordAttempts
        {
            get { return _MaxInvalidPasswordAttempts; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _MinRequiredNonalphanumericCharacters; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public override int MinRequiredPasswordLength
        {
            get { return _MinRequiredPasswordLength; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public override int PasswordAttemptWindow
        {
            get { return _PasswordAttemptWindow; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _PasswordFormat; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public override string PasswordStrengthRegularExpression
        {
            get { return _PasswordStrengthRegularExpression; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public override bool RequiresQuestionAndAnswer
        {
            get { return _RequiresQuestionAndAnswer; }
        }

        /// <summary>
        /// required implementation
        /// </summary>
        public override bool RequiresUniqueEmail
        {
            get { return _RequiresUniqueEmail; }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            //
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
            base.Initialize(name, config);

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


            //
            // Initialize OdbcConnection.
            //

            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            ConnectionStrings = ConnectionStringSettings.ConnectionString;


            // Get encryption and decryption key information from the configuration.

            Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");

            if (machineKey.ValidationKey.Contains("AutoGenerate"))
                if (PasswordFormat != MembershipPasswordFormat.Clear)
                    throw new ProviderException("Hashed or Encrypted passwords " +
                                                "are not supported with auto-generated keys.");
        }


        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }
        #endregion
        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">a username</param>
        /// <param name="oldPassword">original password</param>
        /// <param name="newPassword">new password</param>
        /// <returns>true or false</returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            log.Info("ChangePassword user: " + username + " in Application: " + _App.AppName);

            if (_App == null) return false;
            user TemUser = UserSrv.GetByName(username, _App.AppID);
            if (TemUser == null) return false;
            string OldPassWordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(oldPassword, "MD5");
            if (TemUser.password != OldPassWordHash) return false;

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);
            OnValidatingPassword(args);
            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");

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
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            log.Info("ChangePasswordQuestionAndAnswer user: " + username + " in Application: " + _App.AppName);

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
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            log.Info("Create new User: " + username + " in Application " + _App.AppName);
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

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
                return ConvertUserToMembership(TempUser);
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
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            user TempUser = UserSrv.GetByName(username, _App.AppID);
            if (TempUser == null) return false;

            try
            {
                UserSrv.Delete(TempUser);
                UserSrv.CommitChanges();
                log.Info("Delete User: " + username);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error  Delete User: " + username, ex);
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
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            log.Info("FindUsersByEmail Application " + _App.AppName);
            if (pageIndex <= 0 || pageSize <= 0)
            {
                totalRecords = 0;
                return null;
            }
            //string HQL = "select u from user u join u.ApplicationList app where u.email like %:email% AND app.AppID = :AppID  ";
            UserSrv.SetFetchPage((pageIndex - 1) * pageSize, pageSize);
            try
            {
                //IList<user> lst = UserSrv.GetbyHQuery(HQL,new SQLParam("email",emailToMatch),new SQLParam("AppID",_App.AppID));
                IList<user> lst = UserSrv.SearchUser(string.Empty, emailToMatch, _App.AppID);
                totalRecords = lst.Count;
                MembershipUserCollection ret = new MembershipUserCollection();
                foreach (user u in lst)
                {
                    ret.Add(ConvertUserToMembership(u));
                }
                return ret;
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
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            log.Info("FinUserByName Application " + _App.AppName);
            if (pageIndex <= 0 || pageSize <= 0)
            {
                totalRecords = 0;
                return null;
            }
            //string HQL = "select u from user u join u.ApplicationList app where u.username like %:username% AND app.AppID = :AppID  ";
            UserSrv.SetFetchPage((pageIndex - 1) * pageSize, pageSize);
            try
            {
                //IList<user> lst = UserSrv.GetbyHQuery(HQL, new SQLParam("username", usernameToMatch), new SQLParam("AppID", _App.AppID));
                IList<user> lst = UserSrv.SearchUser(usernameToMatch, "", _App.AppID);
                totalRecords = lst.Count;
                MembershipUserCollection ret = new MembershipUserCollection();
                foreach (user u in lst)
                {
                    ret.Add(ConvertUserToMembership(u));
                }
                return ret;
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
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            log.Info("GetAllUsers Application " + _App.AppName);
            if (pageIndex <= 0 || pageSize <= 0)
            {
                totalRecords = 0;
                return null;
            }
            UserSrv.SetFetchPage((pageIndex - 1) * pageSize, pageSize);
            try
            {
                // string HQL = "select u from user u join u.ApplicationList app where app.AppID = :AppID  ";
                // IList<user> lst = UserSrv.GetbyHQuery(HQL,new SQLParam("AppID",_App.AppID));
                IList<user> lst = UserSrv.GetAllInApplication(_App.AppID);
                totalRecords = lst.Count;
                MembershipUserCollection ret = new MembershipUserCollection();
                foreach (user u in lst)
                {
                    ret.Add(ConvertUserToMembership(u));
                }
                return ret;
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
        public override int GetNumberOfUsersOnline()
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
        public override string GetPassword(string username, string answer)
        {
            throw new Exception("have not implement.");
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">required implementation</param>
        /// <param name="userIsOnline">required implementation</param>
        /// <returns>required implementation</returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            log.Info("GetNumberOfUsersOnline Application: " + _App.AppName);

            try
            {
                user TempUser = UserSrv.GetByName(username, _App.AppID);
                return TempUser == null ? null : ConvertUserToMembership(TempUser);
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
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new Exception("have not implement.");
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="email">required implementation</param>
        /// <returns>required implementation</returns>
        public override string GetUserNameByEmail(string email)
        {
            log.Info("GetUserNameByEmail:" + email + " in Application: " + _App.AppName);
            //string HQL = "select u from user u join u.ApplicationList app where u.email = :email AND app.AppID = :AppID  ";
            try
            {
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
        public override string ResetPassword(string username, string answer)
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
            string newPassword = System.Web.Security.Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters);

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");

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
        public override bool UnlockUser(string userName)
        {
            log.Info("UnlockUser:" + userName + " in Application: " + _App.AppName);
            try
            {
                user us = UserSrv.GetByName(userName, _App.AppID);
                us.IsLockedOut = false;
                UserSrv.Update(us);
                UserSrv.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error UnlockUser: " + userName + " in Application: " + _App.AppName, ex);
                return false;
            }
            //string HQL = "update user u set u.IsLockedOut = false join u.Applications app where u.username = :username AND app.AppID=:AppID";
            //try
            //{
            //    object ret = UserSrv.ExcuteNonQuery(HQL, true, new SQLParam("username", userName), new SQLParam("AppID", _App.AppID));
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Error UnlockUser: " + userName + " in Application: " + _App.AppName, ex);
            //    return false;
            //}
            //throw new Exception("have not implement.");
        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="user">required implementation</param>
        public override void UpdateUser(MembershipUser muser)
        {
            log.Info("UpdateUser:" + muser.UserName + " in Application: " + _App.AppName);
            try
            {
                //string HQL = "select u from user u join u.ApplicationList app u.username = :username AND app.AppID = :AppID  ";
                //IList<user> lst = UserSrv.GetbyHQuery(HQL, new SQLParam("username", muser.UserName), new SQLParam("AppID", _App.AppID));
                //if (lst == null || lst.Count == 0) return ;
                user TempUser = ConvertMembershipToUser(muser);
                UserSrv.Update(TempUser);
                UserSrv.CommitChanges();
            }
            catch (Exception ex)
            {
                log.Error("Error UpdateUser: " + muser.UserName + " in Application: " + _App.AppName, ex);
                return;
            }

        }

        /// <summary>
        /// required implementation
        /// </summary>
        /// <param name="username">required implementation</param>
        /// <param name="password">required implementation</param>
        /// <returns>required implementation</returns>
        public override bool ValidateUser(string username, string password)
        {
            log.Info("ValidateUser:" + username + " in Application: " + _App.AppName);
            try
            {
                user TempUser = UserSrv.GetByName(username, _App.AppID);
                string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                if (TempUser != null && TempUser.password == pass) return true;
                else return false;
            }
            catch (Exception ex)
            {
                log.Error("Error ValidateUser: " + username + " in Application: " + _App.AppName, ex);
                return false;
            }
        }

        MembershipUser ConvertUserToMembership(user mUser)
        {
            return new MembershipUser(this.Name, mUser.username, mUser.userid, mUser.email, mUser.PasswordQuestion, "", mUser.IsApproved, mUser.IsLockedOut, mUser.CreateDate, mUser.LastLoginDate, mUser.CreateDate, mUser.LastPasswordChangedDate, mUser.LastLockoutDate);

        }
        user ConvertMembershipToUser(MembershipUser mMembership)
        {
            int userID = -1;
            if (mMembership.ProviderUserKey is int) userID = (int)mMembership.ProviderUserKey;
            user TempUser = _UserSrv.Getbykey(userID);
            if (TempUser == null) TempUser = new user();
            TempUser.username = mMembership.UserName;
            TempUser.email = mMembership.Email;
            TempUser.PasswordQuestion = mMembership.PasswordQuestion;
            TempUser.IsApproved = mMembership.IsApproved;
            TempUser.IsLockedOut = mMembership.IsLockedOut;
            TempUser.CreateDate = mMembership.CreationDate;
            TempUser.LastLoginDate = mMembership.LastLoginDate;
            TempUser.LastPasswordChangedDate = mMembership.LastPasswordChangedDate;
            TempUser.LastLockoutDate = mMembership.LastLockoutDate;
            return TempUser;
        }

        user UpdateUserFromMembership(MembershipUser mMembership, user TempUser)
        {
            TempUser.username = mMembership.UserName;
            TempUser.email = mMembership.Email;
            TempUser.PasswordQuestion = mMembership.PasswordQuestion;
            TempUser.IsApproved = mMembership.IsApproved;
            TempUser.IsLockedOut = mMembership.IsLockedOut;
            TempUser.CreateDate = mMembership.CreationDate;
            TempUser.LastLoginDate = mMembership.LastLoginDate;
            TempUser.LastPasswordChangedDate = mMembership.LastPasswordChangedDate;
            TempUser.LastLockoutDate = mMembership.LastLockoutDate;
            return TempUser;
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
