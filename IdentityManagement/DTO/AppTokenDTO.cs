using System;

namespace IdentityManagement.DTO
{
    public class AppTokenDTO
    {
        private string _Token;
        private string _AppName;
        private string _LoginID;
        private DateTime _CreatedTime;
        public virtual string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        public virtual string AppName
        {
            get { return _AppName; }
            set { _AppName = value; }
        }

        public virtual string LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        public virtual DateTime CreatedTime
        {
            get { return _CreatedTime; }
            set { _CreatedTime = value; }
        }
    }
}
