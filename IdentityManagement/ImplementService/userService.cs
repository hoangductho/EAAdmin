using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FX.Data;
using IdentityManagement.Domain;
using IdentityManagement.Service;

namespace IdentityManagement.ImplementService
{
    public class userService : BaseService<user, int>, IuserService
    {
        public userService(string sessionFactoryConfigPath)
            : base(sessionFactoryConfigPath)
        { }

        public user GetByName(string mUserName)
        {
            string hsql = "from user u where u.username = :username";
            List<user> _lst = GetbyHQuery(hsql, new SQLParam[] { new SQLParam("username", mUserName) });
            return (_lst == null || _lst.Count == 0) ? null : _lst[0];
        }
        public IList<user> SearchUser(string mUserName, string Email, int AppID)
        {
            string HQL = "select u from user u join u.ApplicationList app where u.username like :username AND u.email like :email AND app.AppID = :AppID  ";
            return GetbyHQuery(HQL, new SQLParam("username","%"+ mUserName+"%"), new SQLParam("email", "%"+Email+"%"), new SQLParam("AppID", AppID));          
        }

        public IList<user> SearchUser(string mUserName, string Email, string AppName)
        {
            string HQL = "select u from user u join u.ApplicationList app where u.username like :username AND u.email like :email AND app.AppName = :AppName  ";
            return GetbyHQuery(HQL, new SQLParam("username", "%" +mUserName +"%"), new SQLParam("email","%"+ Email+"%"), new SQLParam("AppName", AppName));
        }

        public user GetByName(string mUserName, string ApplicationName)
        {
            string HQL = "select u from user u join u.ApplicationList app where u.username = :username AND app.AppName = :AppName";
            IList<user> lst = GetbyHQuery(HQL, new SQLParam("username", mUserName), new SQLParam("AppName", ApplicationName));
            return (lst == null || lst.Count == 0) ? null : lst[0];
        }
        public user GetByName(string mUserName, int AppID)
        {
            string HQL = "select u from user u join u.ApplicationList app where u.username = :username AND app.AppID = :AppID ";
            IList<user> lst = GetbyHQuery(HQL, new SQLParam("username", mUserName), new SQLParam("AppID", AppID));
            return  (lst == null || lst.Count == 0)?null: lst[0];
        }
        public IList<user> GetAllInApplication(string ApplicationName)
        {
            string HQL = "select u from user u join u.ApplicationList app where app.AppName = :AppName  ";
            return  GetbyHQuery(HQL, new SQLParam("AppName", ApplicationName));
        }
        public IList<user> GetAllInApplication(int AppID)
        {
            string HQL = "select u from user u join u.ApplicationList app where app.AppID = :AppID  ";
            return  GetbyHQuery(HQL, new SQLParam("AppID", AppID));            
        }
    }

}