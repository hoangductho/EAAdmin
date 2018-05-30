using System;

namespace IdentityManagement.DTO
{
    public class userDTO
    {
        private string _username;
        private string _password;
        private string _firstname;
        private string _familyname;
        private string _email;
        public virtual string username
        {
            get { return _username; }
            set { _username = value; }
        }

        public virtual string password
        {
            get { return _password; }
            set { _password = value; }
        }

        public virtual string firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        public virtual string familyname
        {
            get { return _familyname; }
            set { _familyname = value; }
        }

        public virtual string email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}
