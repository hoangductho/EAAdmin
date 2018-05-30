using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace IdentityManagement.Authorization
{
    public interface IFanxiPrincipal : IPrincipal
    {
        bool IsInPermission(params string[] permissions);
        bool IsInRole(params string[] roles);
    }
}
