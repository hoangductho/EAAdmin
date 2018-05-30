using System;

namespace Teca.Core.Domain
{
    public class Articles
    {
        public virtual int Id { get; set; }
        public virtual int CategoryID { get; set; }
        public virtual string NameVNI { get; set; }
        public virtual string Summary { get; set; }
        public virtual string SummaryENG { get; set; }
        public virtual string Descriptions { get; set; }
        public virtual string DescriptionsENG { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual int Priority { get; set; }
        public virtual string Css { get; set; }
        public virtual bool IsHot { get; set; }
        public virtual bool Active { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual string NameENG { get; set; }
        public virtual string URLName { get; set; }
        public virtual string ApproveBy { get; set; }

        public virtual string Comment { get; set; }
        public virtual Boolean Approved { get; set; }

        public virtual Category Categories { get; set; }

        private DateTime _CreatedDate = DateTime.Now;
        private DateTime _ModifiedDate = DateTime.Now;
        public virtual Boolean IsEvent { get; set; }
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

        private DateTime _ApproveDate = new DateTime(1900, 1, 1);

        public virtual DateTime ApproveDate
        {
            get { return _ApproveDate; }
            set { _ApproveDate = value; }
        }
        public virtual DateTime StartDate
        {
            get;
            set;
        }
        public virtual DateTime EndDate
        {
            get;
            set;
        }
        public virtual byte[] AttachData { get; set; }
        public virtual string TypeData { get; set; }
    }
}