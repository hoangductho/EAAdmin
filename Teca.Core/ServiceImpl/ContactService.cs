using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;
using Teca.Core.IService;

namespace Teca.Core.ServiceImpl
{
    class ContactService : BaseService<Contact, int>, IContactService
    {
        public ContactService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<Contact> GetList(bool type, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(p => p.Type == type);
            total = qr.Count();
            qr = qr.OrderByDescending(p => p.CreatedDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
    }
}
