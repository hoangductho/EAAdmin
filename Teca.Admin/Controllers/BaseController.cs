using FX.Utils.MVCMessage;
using IdentityManagement.Authorization;
using System;
using System.Web.Mvc;

namespace Teca.Admin.Controllers
{
    [MessagesFilter]    
    public class BaseController : Controller
    {        
        protected MessageViewData Messages
        {
            get
            {
                if (!ViewData.ContainsKey("Messages"))
                {
                    throw new InvalidOperationException("Messages are not available. Did you add the MessageFilter attribute to the controller?");
                }
                return (MessageViewData)ViewData["Messages"];
            }
        }
    }
}