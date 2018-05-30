using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teca.Core.Domain
{
    public class Notification
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual string CreateBy { get; set; }
        public virtual string ApproveBy { get; set; }
        public virtual int ArticleId { get; set; }
        public virtual int Status { get; set; }
        private DateTime _CreateDate = DateTime.Now;

        public virtual DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
