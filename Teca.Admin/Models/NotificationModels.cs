using FX.Utils.MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teca.Core.Domain;

namespace Teca.Admin.Models
{
    public class NotificationModels
    {
        public IList<Notification> Notifications { get; set; } 
        public int totalNotification;
       
    }
   
     
}