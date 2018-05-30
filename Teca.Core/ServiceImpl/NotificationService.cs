using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;
using Teca.Core.IService;

namespace Teca.Core.ServiceImpl
{
    public class NotificationService : BaseService<Notification, int>, INotificationService
    {
        public NotificationService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<Notification> GetbyCurrent(string username, int record)
        {
            return Query.Where(p => p.CreateBy == username && p.Status == 0).OrderByDescending(p => p.CreateDate).Take(record).ToList();
        }
    }
}
