using System;
using System.Collections.Generic;
using System.Text;
using IdentityManagement.Domain;
namespace IdentityManagement.Service
{
    public interface IoperationService : FX.Data.IBaseService<operation, int>
    {
        operation GetByName(string OperationName, int AppID);
        IList<operation> SearchOperation(string OperationName, int AppID);
    }
}
