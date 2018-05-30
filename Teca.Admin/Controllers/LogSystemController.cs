using FX.Core;
using FX.Utils.MvcPaging;
using IdentityManagement.Authorization;
using System;
using System.Web.Mvc;
using Teca.Admin.Models;
using Teca.Core.Domain;
using Teca.Core.IService;

namespace Teca.Admin.Controllers
{
    public class LogSystemController : BaseController
    {
        [RBACAuthorize(Roles = "Root")]
        public ActionResult Index(LogIndexModel model, int? page)
        {
            int total = 0;
            int pagesize = 10;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();
            string kw = !string.IsNullOrEmpty(model.keyword) ? model.keyword.Trim() : null;
            DateTime DateFrom = String.IsNullOrEmpty(model.fromdate) ? DateTime.MinValue : DateTime.ParseExact(model.fromdate, "dd/MM/yyyy", null);
            DateTime DateTo = String.IsNullOrEmpty(model.todate) ? DateTime.MaxValue : DateTime.ParseExact(model.todate, "dd/MM/yyyy", null);
            if (DateFrom == DateTo)
                DateTo = DateFrom.AddHours(12);
            var list = logSrv.GetByFilter(kw, DateFrom, DateTo, currentPageIndex, pagesize, out total);
            model.LogData = new PagedList<LogData>(list, currentPageIndex, pagesize, total);
            return View(model);
        }
    }
}
