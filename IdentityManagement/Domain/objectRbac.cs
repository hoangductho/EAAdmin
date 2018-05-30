using System;

namespace IdentityManagement.Domain
{
    public class objectRbac
    {
        private int _objectid;
        private int _AppID;
        private string _name;
        private Boolean _locked;


        public virtual int objectid
        {
            get { return _objectid; }
            set { _objectid = value; }
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

        public virtual Boolean locked
        {
            get { return _locked; }
            set { _locked = value; }
        }

        public static string Default = "Default";
    }
}
