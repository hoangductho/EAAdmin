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
    public class GalleryService : BaseService<Gallery, int>, IGalleryService
    {
        public GalleryService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<Gallery> GetbyType(int typeId, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(p => p.TypeID == typeId);
            total = qr.Count();
            qr = qr.OrderByDescending(p => p.CreateDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
    }
}
