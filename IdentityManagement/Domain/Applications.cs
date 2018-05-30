using System;

namespace IdentityManagement.Domain
{
    public class Applications
    {
        private int _AppID;
        private string _AppName;
        private string _Description;
        private string _URL;


        public virtual int AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
        }

        public virtual string AppName
        {
            get { return _AppName; }
            set { _AppName = value; }
        }

        public virtual string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public virtual string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }


    }
}
