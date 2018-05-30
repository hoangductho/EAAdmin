using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FX.Data;
using IdentityManagement.Domain;
using IdentityManagement.Service;

namespace IdentityManagement.ImplementService
{
    public class roleService : BaseService<role, int>, IroleService
    {
        public roleService(string sessionFactoryConfigPath)
            : base(sessionFactoryConfigPath)
        { }

        public role GetByName(string RoleName, int AppID)
        {
            string hsql = "from role r where r.name = :rolename AND r.AppID = :AppID";
            List<role> _lst = GetbyHQuery(hsql, new SQLParam("rolename", RoleName), new SQLParam("AppID", AppID));
            return (_lst == null || _lst.Count == 0) ? null : _lst[0];
        }
        public IList<role> SearchRole(string RoleName, int AppID)
        {
            string hsql = "from role r where r.name Like :rolename AND r.AppID = :AppID";
            return GetbyHQuery(hsql, new SQLParam("rolename", "%" + RoleName +"%"), new SQLParam("AppID", AppID));
        }

        public IList<role> GetAll(int AppID)
        {
            string hsql = "from role r where r.AppID = :AppID";
            return GetbyHQuery(hsql, new SQLParam("AppID", AppID));
        }
    }

}