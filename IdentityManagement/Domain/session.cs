using System;
using System.Collections.Generic;
namespace IdentityManagement.Domain
{
    public class session
    {
        private int _sessionid;
        private string _name;
        private DateTime _LastUpdate;


        public virtual int sessionid
        {
            get { return _sessionid; }
            set { _sessionid = value; }
        }

        public virtual string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual DateTime LastUpdate
        {
            get { return _LastUpdate; }
            set { _LastUpdate = value; }
        }

        IList<role> _Roles;

        public virtual IList<role> Roles
        {
            get { return _Roles; }
            set { _Roles = value; }
        }

        IList<user> _Users;
        public virtual IList<user> Users
        {
            get { return _Users; }
            set { _Users = value; }
        }

        

    }
}
