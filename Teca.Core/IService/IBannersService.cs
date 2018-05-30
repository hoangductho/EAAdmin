using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;

namespace Teca.Core.IService
{
    public interface IBannersService : IBaseService<Banners, int>
    {
        IList<Banners> GetbyList(BannerType type, int pageIndex, int pageSize, out int total);
        List<Banners> GetByPos(BannerType position, int count); 
    }
}
