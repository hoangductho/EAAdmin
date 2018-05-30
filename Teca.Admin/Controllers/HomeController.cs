using FX.Core;
using IdentityManagement.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teca.Core.Domain;
using Teca.Core.IService;
using FX.Context;
namespace Teca.Admin.Controllers
{
    public class HomeController : BaseController
    {
        ILogSystemService logSrv = IoC.Resolve<ILogSystemService>();
        [RBACAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [RBACAuthorize(Permissions = "TinTuc")]
        public ActionResult AddIntruction(IntroductionType type = IntroductionType.LichSu)
        {
            IIntroductionService introducSrv = IoC.Resolve<IIntroductionService>();
            Introduction model = introducSrv.Query.Where(p => p.Type == type).FirstOrDefault() ?? new Introduction();
            model.Type = type;
            model.Active = true;
            return View(model);
        }

        [RBACAuthorize(Permissions = "TinTuc")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveIntruction(Introduction model)
        {
            IIntroductionService introducSrv = IoC.Resolve<IIntroductionService>();
            try
            {
                model.CreateDate = DateTime.Now;
                model.CreateBy = HttpContext.User.Identity.Name;
                introducSrv.Save(model);
                introducSrv.CommitChanges();
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "SaveIntruction - Save : " + model.Id, "Save SaveIntruction Success", LogType.Success, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                return RedirectToAction("Details", new { id = model.Id});
            }
            catch (Exception ex)
            {
                logSrv.CreateNew(FXContext.Current.CurrentUser.userid, "SaveIntruction - Save", "Save SaveIntruction Error : " +ex.Message, LogType.Error, HttpContext.Request.UserHostAddress, HttpContext.Request.Browser.Browser);
                Messages.AddErrorMessage("Có lỗi xảy ra, vui lòng thực hiện lại");
                return View("AddIntruction", model);
            }
        }

        [RBACAuthorize(Permissions = "TinTuc")]
        public ActionResult Details(int id)
        {
            IIntroductionService introducSrv = IoC.Resolve<IIntroductionService>();
            var model = introducSrv.Getbykey(id);
            return View(model);
        }

        public ActionResult UnAuthorized()
        {
            Messages.AddErrorFlashMessage("Bạn không có quyền sử dụng chức năng này");
            return Redirect("/");
        }

        public ActionResult ErrorPage()
        {
            Messages.AddErrorFlashMessage("Không tin thấy trang, vui lòng xem lại đường dẫn.");
            return View();
        }

        public ActionResult PotentiallyError()
        {
            ViewBag.Message = "Dữ liệu không hợp lệ hoặc có chứa mã gây nguy hiểm tiềm tàng cho hệ thống.";
            return View();
        }
    }
}
