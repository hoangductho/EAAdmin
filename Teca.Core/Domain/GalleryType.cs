using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teca.Core.Domain
{
    public class GalleryType
    {
        public virtual int Id { get; set; }
        public virtual string TitleVNI { get; set; }
        public virtual string TitleENG { get; set; }       
        public virtual string ImgPath { get; set; }
        public virtual string UrlName { get; set; }
        public virtual string CreateBy { get; set; }
        public virtual int Priority { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual Boolean Active { get; set; }
        private IList<Gallery> _Galleries = new List<Gallery>();

        public virtual IList<Gallery> Galleries
        {
            get { return _Galleries; }
            set { _Galleries = value; }
        }
    }
}
