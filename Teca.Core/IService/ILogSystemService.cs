using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;

namespace Teca.Core.IService
{
    public interface ILogSystemService : IBaseService<LogData, int>
    {
        IList<LogData> GetByFilter(string Keyword, DateTime fromdate, DateTime todate, int pageIndex, int pageSize, out int total);
        void CreateNew(int userID, string mEvent, string comment, LogType type, string ipaddress, string browser);
       
    }
}
