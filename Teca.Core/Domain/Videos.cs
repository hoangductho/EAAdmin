using System;

namespace Teca.Core.Domain
{
    public class Videos
    {
        public virtual int Id { get; set; }
        public virtual int VideoTypeID { get; set; }
        public virtual string NameVNI { get; set; }
        public virtual string NameENG { get; set; }
        public virtual string Summary { get; set; }
       
        public virtual string ImagePath { get; set; }
        public virtual string VideoPath { get; set; }
        public virtual int Priority { get; set; }
        public virtual string Css { get; set; }
        public virtual bool IsHot { get; set; }
        public virtual bool Active { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual string URLName { get; set; }
        public virtual LinkType LinkType { get; set; }    
        public virtual VideoType VideoTypes { get; set; }
        private DateTime _CreatedDate = DateTime.Now;
        private DateTime _ModifiedDate = DateTime.Now;

        public virtual DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public virtual DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }        
    }
}