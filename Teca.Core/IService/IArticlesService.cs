using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;

namespace Teca.Core.IService
{
    public interface IArticlesService : IBaseService<Articles, int>
    {
        IList<Articles> ListForApprove(bool approved, string title, int catId, int pageIndex, int pageSize, out int totalRecord);
        IList<Articles> GetBySearch(string username,string title, int catId, int pageIndex, int pageSize, out int totalRecord);
        IList<Articles> GetbyFilter(string title, int pageIndex, int pageSize, out int totalRecord);
        IList<Articles> GetByCateID(int CateID, string keyword, int pageIndex, int pageSize, out int total);
        IList<Articles> GetByCateID(int CateID, string keyword);
        IList<Articles> GetInCate(int[] CateIDs, string keyword, int pageIndex, int pageSize, out int total);
        IList<Articles> GetHotNews(string keyword, int pageIndex, int pageSize, out int total);
        IList<Articles> GetInCate(int CateID, string keyword, int pageIndex, int pageSize, out int total);
        IList<Articles> GetbyIds(int[] ids);
        bool Delete(Articles art, out string message);
    }
}
