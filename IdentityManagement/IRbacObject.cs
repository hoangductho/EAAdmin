using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityManagement
{
    public interface IRbacObject
    {
        string ObjectName { get; }
        string[] Operations { get; }
        string[] GetRoles(string Operations);
    }
}
