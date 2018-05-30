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
    public class MenuService : BaseService<Menu, int>, IMenuService
    {
        public MenuService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<Menu> GetList(MenuListType type, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(p => p.Position == type);
            total = qr.Count();
            qr = qr.OrderByDescending(p => p.CreatedDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
    }
}
