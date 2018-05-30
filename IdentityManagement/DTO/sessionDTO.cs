using System;

namespace IdentityManagement.DTO
{
    public class sessionDTO
    {
        private string _name;
        private DateTime _LastUpdate;
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
    }
}
