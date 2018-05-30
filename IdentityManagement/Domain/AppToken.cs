using System;

namespace IdentityManagement.Domain
{
    public class AppToken
    {
        private int _AppTokenID;
        private string _Token;
        private string _AppName;
        private string _LoginID;
        private int _UserID;
        private int _AppID;
        private DateTime _CreatedTime;


        public virtual int AppTokenID
        {
            get { return _AppTokenID; }
            set { _AppTokenID = value; }
        }

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

        public virtual int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public virtual int AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
        }

        public virtual DateTime CreatedTime
        {
            get { return _CreatedTime; }
            set { _CreatedTime = value; }
        }


    }
}
