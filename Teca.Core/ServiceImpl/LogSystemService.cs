using FX.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;
using Teca.Core.IService;

namespace Teca.Core.ServiceImpl
{
    public class LogSystemService : BaseService<LogData, int>, ILogSystemService
    {
        ILog log = LogManager.GetLogger(typeof(LogSystemService));
        public LogSystemService(string sessionFactoryConfigPath)
            : base(sessionFactoryConfigPath)
        { }
        public IList<LogData> GetByFilter(string Keyword, DateTime fromdate, DateTime todate, int pageIndex, int pageSize, out int total)
        {
            var qr = Query;
            if (!string.IsNullOrEmpty(Keyword))
                qr = qr.Where(l => l.Event.Contains(Keyword) || l.Comment.Contains(Keyword) || l.User.username.Contains(Keyword));
            if (fromdate != DateTime.MinValue) qr = qr.Where(invc => invc.CreateDate >= fromdate);
            if (todate != DateTime.MaxValue) qr = qr.Where(invc => invc.CreateDate <= todate.AddDays(1));
            total = qr.Count();
            qr = qr.OrderByDescending(c => c.CreateDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }


        public void CreateNew(int userID, string mEvent, string comment, LogType type, string ipaddress, string browser)
        {

            try
            {
                LogData mLog = new LogData();
                mLog.AdminID = userID;
                mLog.Event = mEvent;
                mLog.Type = type;
                mLog.Comment = comment;
                mLog.IpAddress = ipaddress;
                mLog.Browser = browser;
                CreateNew(mLog);
                CommitChanges();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
       
    }
}
