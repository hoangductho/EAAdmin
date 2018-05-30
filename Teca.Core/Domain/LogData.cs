using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityManagement.Domain;
namespace Teca.Core.Domain
{
    public class LogData
    {
        public virtual int Id { get; set; }
        public virtual int AdminID { get; set; }
        public virtual string Event { get; set; }
        public virtual LogType Type { get; set; }
        public virtual string Comment { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual string Browser { get; set; }

        private user _User;

        public virtual user User
        {
            get { return _User; }
            set { _User = value; }
        }

        private DateTime _CreateDate = DateTime.Now;
        public virtual DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
