using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;

namespace Teca.Core.IService
{
    public interface IVideosService : IBaseService<Videos, int>
    {
        IList<Videos> GetBySearch(int typeId, int pageIndex, int pageSize, out int totalRecord);
        IList<Videos> GetBySearch(string title, int typeId, string username, int pageIndex, int pageSize, out int totalRecord);
        IList<Videos> GetBySearch(string title, int typeId, int pageIndex, int pageSize, out int totalRecord);
        IList<Videos> GetByTypeID(int TypeID, string keyword, int pageIndex, int pageSize, out int total);
        IList<Videos> GetByTypeID(int TypeID, string keyword);
        IList<Videos> GetAllVideo(int pageIndex, int pageSize, out int totalRecord);
        IList<Videos> GetByRelate(int ID, int TypeID, int pageIndex, int pageSize, out int total);
    }
}
