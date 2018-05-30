using System;
using System.Collections.Generic;
using System.Text;
using IdentityManagement.Domain;
namespace IdentityManagement.Service
{
    public interface IroleService : FX.Data.IBaseService<role, int>
    {
        role GetByName(string RoleName,int AppID);
        IList<role> SearchRole(string RoleName, int AppID);
        IList<role> GetAll(int AppID);
    }
}
