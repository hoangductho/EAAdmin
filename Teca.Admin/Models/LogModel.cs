using FX.Utils.MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teca.Core.Domain;

namespace Teca.Admin.Models
{

    public class LogIndexModel
    {
 
        public IPagedList<LogData> LogData;
       public string keyword { get; set; }
       public string fromdate { get; set; }
       public string todate { get; set; }

    }
}