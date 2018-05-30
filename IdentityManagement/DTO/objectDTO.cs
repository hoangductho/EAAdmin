using System;

namespace IdentityManagement.DTO
{
    public class objectDTO
    {
        private string _name;
        private Boolean _locked;
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
    }
}
