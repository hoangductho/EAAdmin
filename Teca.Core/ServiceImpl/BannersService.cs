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
    public  class BannersService: BaseService<Banners, int>, IBannersService
    {
        public BannersService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public List<Banners> GetByPos(BannerType position, int count)
        {
            return Query.Where(p => p.TypeID == position && p.Active == true).OrderBy(p => p.Priority).Take(count).ToList();
        }

        public IList<Banners> GetbyList(BannerType type,int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(p => p.TypeID == type);
            total = qr.Count();
            qr = qr.OrderByDescending(p => p.CreatedDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
    }
}
