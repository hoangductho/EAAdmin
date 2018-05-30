using System;

namespace IdentityManagement.Domain
{
    public class operation
    {
        private int _operationid;
        private int _AppID;
        private string _name;
        private Boolean _cancreate;
        private Boolean _canread;
        private Boolean _canupdate;
        private Boolean _candelete;
        private Boolean _islocked;


        public virtual int operationid
        {
            get { return _operationid; }
            set { _operationid = value; }
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

        public virtual Boolean cancreate
        {
            get { return _cancreate; }
            set { _cancreate = value; }
        }

        public virtual Boolean canread
        {
            get { return _canread; }
            set { _canread = value; }
        }

        public virtual Boolean canupdate
        {
            get { return _canupdate; }
            set { _canupdate = value; }
        }

        public virtual Boolean candelete
        {
            get { return _candelete; }
            set { _candelete = value; }
        }

        public virtual Boolean islocked
        {
            get { return _islocked; }
            set { _islocked = value; }
        }

        public static string Default = "Default";
    }
}
