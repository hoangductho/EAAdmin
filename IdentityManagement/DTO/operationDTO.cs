using System;

namespace IdentityManagement.DTO
{
    public class operationDTO
    {
        private string _name;
        private Boolean _cancreate;
        private Boolean _canread;
        private Boolean _canupdate;
        private Boolean _candelete;
        private Boolean _islocked;

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
    }
}
