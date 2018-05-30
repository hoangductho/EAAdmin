using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teca.Admin.Controllers
{
    public class UploadStoreController : Controller
    {
        public FileResult Index(string path, int thumb = 0)
        {
            string fullpath = ConfigurationSettings.AppSettings.Get("PhysicalSiteDataDirectory") + @"\" + path;
            if (thumb > 0)
            {
                fullpath = ConfigurationSettings.AppSettings.Get("PhysicalSiteDataDirectory") + @"\_thumbs\" + path;
            }
         
            string filename = Path.GetFileName(fullpath);
            return File(fullpath, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
    }
}
