﻿using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;

namespace Teca.Core.IService
{
    public interface IContactService : IBaseService<Contact, int>
    {
        IList<Contact> GetList(bool type, int pageIndex, int pageSize, out int total);
    }
}
