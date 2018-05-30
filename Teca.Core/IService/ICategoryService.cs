using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;

namespace Teca.Core.IService
{
    public interface ICategoryService : IBaseService<Category, int>
    {
        IList<Category> GetByFilter(string keyword, int pageIndex, int pageSize, out int totalRecord);
    }
}
