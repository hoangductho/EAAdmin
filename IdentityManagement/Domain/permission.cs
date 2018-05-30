using System;
using System.Collections.Generic;
namespace IdentityManagement.Domain
{
    public class permission
    {
        private int _permissionid;
        private int _AppID;
        private string _name;
        //private int _objectid;
        //private int _operationid;


        public virtual int permissionid
        {
            get { return _permissionid; }
            set { _permissionid = value; }
        }

        public virtual int AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
        }

        public virtual string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string Description { get; set; }
        //public virtual int objectid
        //{
        //    get { return _objectid; }
        //    set { _objectid = value; }
        //}

        //public virtual int operationid
        //{
        //    get { return _operationid; }
        //    set { _operationid = value; }
        //}

        IList<role> _Roles;
        public virtual IList<role> Roles
        {
            get { return _Roles; }
            set { _Roles = value; }
        }

        operation _Operation;

        public virtual operation Operation
        {
            get { return _Operation; }
            set { _Operation = value; }
        }

        objectRbac _ObjectRBAC;

        public virtual objectRbac ObjectRBAC
        {
            get { return _ObjectRBAC; }
            set { _ObjectRBAC = value; }
        }

        public static string Default = "Default";
    }
}
