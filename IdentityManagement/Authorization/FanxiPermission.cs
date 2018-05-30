using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityManagement.Authorization
{
    public class FanxiPermission
    {
        string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        string _RbacObject;
        public string RbacObject
        {
            get { return _RbacObject; }
        }

        string _RbacOperation;
        public string RbacOperation
        {
            get { return _RbacOperation; }
        }
        public FanxiPermission(string mName, string mObject, string mOperation)
        {
            _name = mName;
            _RbacObject = mObject;
            _RbacOperation = mOperation;
        }

        /// <summary>
        /// create fanxipermission from an string decriptions for set of (permissionName,ObjectName,OpenrationName)
        /// </summary>
        /// <param name="POO">string format is [permissionName],[ObjectName],[OpenrationName]</param>
        public FanxiPermission(string POO)
        {
            string[] strArr = POO.Split(',');
            if (strArr.Length > 0) _name = strArr[0];
            if (strArr.Length > 1) _RbacObject = strArr[1];
            if (strArr.Length > 2) _RbacOperation = strArr[2];
        }

        public override string ToString()
        {
            return _name + "," + _RbacObject + "," + _RbacOperation;
        }
    }    
}
