using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teca.Core.Domain
{
    public class Introduction
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string TitleENG { get; set; }
        public virtual string Description { get; set; }
        public virtual string DescriptionENG { get; set; }
        public virtual string CreateBy { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual IntroductionType Type { get; set; }
        public virtual Boolean Active { get; set; }
    }
}
