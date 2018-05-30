using FX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teca.Core;
using Teca.Core.IService;

namespace Teca.Admin.Controllers
{
    public class AjaxDataController : Controller
    {
        public ActionResult ArticlesSearch(string searchText)
        {
            IArticlesService artSrv = IoC.Resolve<IArticlesService>();
            int total = 0;
            var v = artSrv.GetbyFilter(searchText, 0, 10, out total).Select(c => new { c.NameVNI, c.NameENG, Url = URLManager.GetURL(c) }).ToList();
            return Json(v, JsonRequestBehavior.AllowGet);
        }
    }
}
