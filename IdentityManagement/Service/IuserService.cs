using System;
using System.Collections.Generic;
using System.Text;
using IdentityManagement.Domain;
namespace IdentityManagement.Service
{
    public interface IuserService : FX.Data.IBaseService<user, int>
    {
        user GetByName(string mUserName);
        user GetByName(string mUserName, string ApplicationName);
        user GetByName(string mUserName, int AppID);
        IList<user> GetAllInApplication(string ApplicationName);
        IList<user> GetAllInApplication(int AppID);
        IList<user> SearchUser(string mUserName, string Email, int AppID);
        IList<user> SearchUser(string mUserName, string Email, string AppName);
    }
}
