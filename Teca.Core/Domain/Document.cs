using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teca.Core.Domain
{
    public class Document
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string NameENG { get; set; }
        public virtual Byte[] Data { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual string CreateBy { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Description { get; set; }
        public virtual string SignedBy { get; set; }
        public virtual string PublishBy { get; set; }
        public virtual string Serial { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual DateTime StartDate { get; set; }
    }
}
