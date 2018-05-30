using System;
using System.Collections.Generic;
namespace IdentityManagement.Domain
{
    public class role
    {
        private int _roleid;
        private int _AppID;
        private string _name;
        public virtual int roleid
        {
            get { return _roleid; }
            set { _roleid = value; }
        }
        public virtual int AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
        }
        

        public virtual string name
        {
            get { return _name; }
            set { _name = value; }
        }
        IList<user> _Users;
        public virtual IList<user> Users
        {
            get { return _Users; }
            set { _Users = value; }
        }

        IList<session> _sessions;
        public virtual IList<session> Sessions
        {
            get { return _sessions; }
            set { _sessions = value; }
        }

        IList<permission> _Permissions;
        public virtual IList<permission> Permissions
        {
            get { return _Permissions; }
            set { _Permissions = value; }
        }

        public static string RootRole = "Roots";
    }
}
