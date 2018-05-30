using System;
using System.Collections.Generic;
using IdentityManagement.Domain;
using IdentityManagement.ImplementService;
using IdentityManagement.Service;
using FX.Data;
using log4net;
namespace IdentityManagement.WebProviders
{
    public class RBACProfileProvider : IProfileProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RBACMembershipProvider));
        #region  Implement IProfileProvider
        string _SessionFactoryConfigPath;
        public string SessionFactoryConfigPath
        {
            get { return _SessionFactoryConfigPath; }
            set { _SessionFactoryConfigPath = value; }
        }
        public RBACProfileProvider(string mSessionFactoryConfigPath)
        {
            _SessionFactoryConfigPath = mSessionFactoryConfigPath;
        }
        public int DeleteProfiles(List<UserProfile> Profiles)
        {
            IUserProfileService _service = new UserProfileService(_SessionFactoryConfigPath);
            int ret = 0;
            foreach (UserProfile _profile in Profiles)
            {
                if (_profile.ProfileID > 0)
                {
                    try
                    {
                        _service.Delete(_profile);
                        ret++;
                    }
                    catch (Exception ex) { }
                }
            }

            _service.CommitChanges();
            return ret;
        }
        public int DeleteProfiles(string usernames)
        {
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            user mUser = UserSrv.GetByName(usernames);
            if (mUser == null) return 0;
            string Hql = "Delete UserProfile where UserId =:UserId";
            return (int)UserSrv.ExcuteNonQuery(Hql, true, new SQLParam("UserId", mUser.userid));
        }
        public int DeleteProfiles(string[] usernames)
        {
            IuserService UserSrv = new userService(SessionFactoryConfigPath);
            int ret = 0;
            foreach (string UN in usernames)
            {
                user mUser = UserSrv.GetByName(UN);
                if (mUser != null)
                {
                    string Hql = "Delete UserProfile where UserId =:UserId";
                    ret += (int)UserSrv.ExcuteNonQuery(Hql, true, new SQLParam("UserId", mUser.userid));
                }
            }
            return ret;
        }
        public IDictionary<string, UserProfile> FindProfilesByUserName(string UserName)
        {
            IuserService _service = new userService(this.SessionFactoryConfigPath);
            user mUser = _service.GetByName(UserName);
            if (mUser == null) return null;
            else return mUser.UserProfiles;
        }
        public object GetProperty(string UserName, string PropertyName)
        {
            IDictionary<string, UserProfile> Profiles = FindProfilesByUserName(UserName);
            if (Profiles.ContainsKey(PropertyName))
            {
                UserProfile item = Profiles[PropertyName];
                if (item.PropertyValuesBinary != null) return item.PropertyValuesBinary;
                else return item.PropertyValuesString;
            }
            else return null;
        }
        public int CreateProfileForUser(string UserName, string[] PropertyNames, object[] PropertyValues)
        {
            return 0;
        }
        public int UpdateProfileForUser(string UserName, string[] PropertyNames, object[] PropertyValues)
        {
            IuserService _service = new userService(this.SessionFactoryConfigPath);
            user mUser = _service.GetByName(UserName);
            if (mUser != null)
                return UpdateProfileForUser(mUser, PropertyNames, PropertyValues);
            return 0;
        }
        public int UpdateProfileForUser(user mUser, string[] PropertyNames, object[] PropertyValues)
        {
            for (int i = 0; i < PropertyNames.Length; i++)
            {
                object value = i >= PropertyValues.Length ? "" : PropertyValues[i];
                if (mUser.UserProfiles.ContainsKey(PropertyNames[i]))
                {
                    if (value is byte[]) mUser.UserProfiles[PropertyNames[i]].PropertyValuesBinary = value as byte[];
                    else mUser.UserProfiles[PropertyNames[i]].PropertyValuesString = value.ToString();
                }
                else
                {
                    UserProfile uf = new UserProfile(PropertyNames[i], value);
                    mUser.UserProfiles.Add(PropertyNames[i], uf);
                }
            }
            IuserService _service = new userService(this.SessionFactoryConfigPath);
            try
            {
                _service.Update(mUser);
                return 1;
            }
            catch (Exception ex)
            {
                log.Error("Update profile Error.", ex);
                return -1;
            }
        }
        public int AddPorfileforUser(string UserName, string PropertyNames, object PropertyValues)
        { return 0; }

        #endregion
    }
}