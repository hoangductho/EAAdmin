using FX.Utils.MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teca.Core.Domain;

namespace Teca.Admin.Models
{
    public class  ContactIndexModels
    {
        public IPagedList<Contact> Contacts;
        public string Keyword { get; set; }

    }
}