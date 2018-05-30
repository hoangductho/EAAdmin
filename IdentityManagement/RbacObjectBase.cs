using System;
using System.Collections;
using System.Text;
using IdentityManagement.WebProviders;
namespace IdentityManagement
{
    public abstract class RbacObjectBase:IRbacObject
    {
        private Hashtable Permissions = new Hashtable();
        public abstract string ObjectName { get; }
        public abstract string[] Operations { get; }
        public virtual string[] GetRoles(string mOperation)
        {
            if ( !Permissions.ContainsKey(mOperation) || Permissions[mOperation] == null)
            {
                Permissions[mOperation]  = RoleProvider.GetRoleForOperation(mOperation, ObjectName);
            }
            return Permissions[mOperation] as string[];
        }

        public abstract IRBACRoleProvider RoleProvider { get; } 

    }
}
