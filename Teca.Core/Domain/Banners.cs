using FX.Utils;
using System;

namespace Teca.Core.Domain
{
    public class Banners
    {
        public virtual int Id { get; set; }
        public virtual BannerType TypeID { get; set; }
        public virtual string NameVNI { get; set; }
        public virtual string NameENG { get; set; }
        public virtual string NavigateUrl { get; set; }
        public virtual int Priority { get; set; }
        public virtual Boolean Active { get; set; }
        public virtual DateTime FromDate { get; set; }
        public virtual string DescriptionsVNI { get; set; }
        public virtual string DescriptionsENG { get; set; }
        public virtual DateTime ToDate { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        private DateTime _ModifiedDate = DateTime.Today;

        public virtual DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        } 
        

        [IgnoreJsonMember]
        public virtual string[] DictValueVNI
        {
            get { return DescriptionsVNI.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); }
        }

        [IgnoreJsonMember]
        public virtual string[] DictValueENG
        {
            get { return DescriptionsENG.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); }
        }
    }
}
