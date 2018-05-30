using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Web.Caching;
namespace IdentityManagement.Authorization
{
    public abstract class FanxiAuthenticationBase
    {        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mUserName"></param>
        /// <param name="mPassword"></param>
        /// <returns>
        /// Return user with permmissions list
        /// If result = null then the Authen Faile 
        /// </returns>
        public abstract UserIdentity Authenticate(string mUserName, string mPassword);      

        public static void SetAuthCache(UserIdentity _user)
        {
            if (_user == null) return;
            double totalSeconds = FormsAuthentication.Timeout.TotalSeconds;            
            string userData = "InCache";
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,        // version
                                                                _user.Name,            // user name
                                                                DateTime.Now,          // create time
                                                                DateTime.Now.AddSeconds(totalSeconds), // expire time
                                                                false,                 // persistent
                                                                userData);             // user data
            string strEncryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strEncryptedTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
            HttpContext.Current.Cache.Insert(_user.Name, _user.UserData, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(totalSeconds));
        }
                
        //Check username, password and save to cache
        public bool LogOn(string mUserName, string mPassword)
        {
            UserIdentity tempId = Authenticate(mUserName, mPassword);
            if (tempId == null) return false;
            SetAuthCache(tempId);
            return true;
        }
        public void SigOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Cache.Remove(HttpContext.Current.User.Identity.Name);
        }
    }
}
