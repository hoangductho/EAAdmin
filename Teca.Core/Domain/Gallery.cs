using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teca.Core.Domain
{
    public class Gallery
    {
        public virtual int Id { get; set; }        
        public virtual int TypeID { get; set; }
        public virtual string CreateBy { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual string ImgPath { get; set; }        
        public virtual bool Active { get; set; }
        public virtual GalleryType GalleryTypes { get; set; }
    }
}
