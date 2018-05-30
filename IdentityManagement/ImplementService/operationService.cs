using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FX.Data;
using IdentityManagement.Domain;
using IdentityManagement.Service;

namespace IdentityManagement.ImplementService
{
    public class operationService : BaseService<operation, int>, IoperationService
    {
        public operationService(string sessionFactoryConfigPath)
            : base(sessionFactoryConfigPath)
        { }

        public operation GetByName(string OperationName, int AppID)
        {
            string hsql = "from operation ope where ope.name = :OperationName AND ope.AppID = :AppID";
            List<operation> _lst = GetbyHQuery(hsql, new SQLParam("OperationName", OperationName), new SQLParam("AppID", AppID));
            return(_lst == null || _lst.Count == 0) ? null : _lst[0];
        }
        public IList<operation> SearchOperation(string OperationName, int AppID)
        {
            string hsql = "from operation ope where ope.name Like :OperationName AND ope.AppID = :AppID";
            return GetbyHQuery(hsql, new SQLParam("OperationName", "%" + OperationName + "%"), new SQLParam("AppID", AppID));
        }
    }

}