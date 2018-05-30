using System;
using System.Collections.Generic;

namespace Teca.Core.Domain
{
    public class Menu
    {
        private int _Id;
        private int _ParentID = 0;
        private string _NameVNI;
        private string _NameENG;
        private string _NavigateUrl;
        private int _Priority;
        private Boolean _IsPub;        
        private string _Css;        
        private DateTime _CreatedDate = DateTime.Now;

        public virtual int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public virtual int ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        public virtual string NameVNI
        {
            get { return _NameVNI; }
            set { _NameVNI = value; }
        }

        public virtual string NameENG
        {
            get { return _NameENG; }
            set { _NameENG = value; }
        }

        public virtual string NavigateUrl
        {
            get { return _NavigateUrl; }
            set { _NavigateUrl = value; }
        }

        public virtual int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        public virtual Boolean IsPub
        {
            get { return _IsPub; }
            set { _IsPub = value; }
        }

        public virtual MenuListType Position { get; set; }

        public virtual string Css
        {
            get { return _Css; }
            set { _Css = value; }
        }        

        public virtual DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public virtual Menu ParentMenu { get; set; }

        private IList<Menu> _ChildMenus = new List<Menu>();
        public virtual IList<Menu> ChildMenus
        {
            get { return _ChildMenus; }
            set { _ChildMenus = value; }
        }

    }
}
