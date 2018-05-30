using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FX.Data;
using IdentityManagement.Domain;
using IdentityManagement.Service;

namespace IdentityManagement.ImplementService
{
    public class objectService : BaseService<objectRbac, int>, IobjectService
    {
        public objectService(string sessionFactoryConfigPath)
            : base(sessionFactoryConfigPath)
        { }

        public objectRbac GetByName(string ObjectName, int AppID)
        {
            string hsql = "from objectRbac obj where obj.name = :ObjectName AND obj.AppID = :AppID";
            List<objectRbac> _lst = GetbyHQuery(hsql, new SQLParam("ObjectName", ObjectName), new SQLParam("AppID", AppID));
            return (_lst == null || _lst.Count == 0) ? null : _lst[0];
        }
        public IList<objectRbac> SearchObject(string ObjectName, int AppID)
        {
            string hsql = "from objectRbac obj where obj.name Like :ObjectName AND obj.AppID = :AppID";
            return  GetbyHQuery(hsql, new SQLParam("ObjectName", "%" + ObjectName +"%"), new SQLParam("AppID", AppID));
        }
    }

}