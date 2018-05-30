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
    public class CategoryService : BaseService<Category, int>, ICategoryService
    {
        public CategoryService(string sessionFactoryConfigPath)
            : base(sessionFactoryConfigPath)
        { }
        public IList<Category> GetByFilter(string keyword, int pageIndex, int pageSize, out int totalRecord)
        {
            var qr = Query;
            if (!string.IsNullOrEmpty(keyword))
                qr = qr.Where(a => a.NameVNI.Contains(keyword) || a.NameENG.Contains(keyword));
            totalRecord = qr.Count();
            qr = qr.OrderByDescending(p => p.CreatedDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
    }
}
