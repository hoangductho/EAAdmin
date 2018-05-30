using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teca.Core.Domain
{
    public class Contact
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Address { get; set; }
        public virtual string Descriptions { get; set; }
        private bool _Active = true;
        public virtual bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        private bool _Type = false;
        public virtual bool Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private DateTime _CreatedDate = DateTime.Now;
        public virtual DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
    }
}
