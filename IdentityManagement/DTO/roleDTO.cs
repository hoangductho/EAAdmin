using System;

namespace IdentityManagement.DTO
{
    public class roleDTO
    {
        private string _name;
        public virtual string name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
