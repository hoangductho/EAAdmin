using System;
using System.Collections.Generic;
using System.Text;
using IdentityManagement.Domain;
namespace IdentityManagement.Service
{
    public interface IobjectService : FX.Data.IBaseService<objectRbac, int>
    {
        objectRbac GetByName(string ObjectName, int AppID);
        IList<objectRbac> SearchObject(string ObjectName, int AppID);
    }
}
