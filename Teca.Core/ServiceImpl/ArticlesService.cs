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
    public class ArticlesService : BaseService<Articles, int>, IArticlesService
    {
        public ArticlesService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<Articles> ListForApprove(bool approved, string title, int catId, int pageIndex, int pageSize, out int totalRecord)
        {
            var qr = Query.Where(a => a.CategoryID == catId);
            if (!string.IsNullOrEmpty(title))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(title.ToUpper()) || a.NameENG.ToUpper().Contains(title.ToUpper()));
            if (approved)
                qr = qr.Where(a => a.Approved);
            else
                qr = qr.Where(p => !p.Approved);
            totalRecord = qr.Count();
            qr = qr.OrderByDescending(c => c.CreatedDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public IList<Articles> GetBySearch(string username, string title, int catId, int pageIndex, int pageSize, out int totalRecord)
        {
            var qr = Query.Where(p => p.CreatedBy == username);
            if (!string.IsNullOrEmpty(title))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(title.ToUpper()) || a.NameENG.ToUpper().Contains(title.ToUpper()) || a.Summary.Contains(title) || a.SummaryENG.Contains(title));
            if (catId > 0)
                qr = qr.Where(a => a.CategoryID == catId);
            totalRecord = qr.Count();
            qr = qr.OrderByDescending(c => c.CreatedDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public IList<Articles> GetbyFilter(string title, int pageIndex, int pageSize, out int totalRecord)
        {
            var qr = Query;
            if (!string.IsNullOrEmpty(title))
                qr = qr.Where(a => (a.NameVNI.ToUpper().Contains(title.ToUpper()) || a.NameENG.ToUpper().Contains(title.ToUpper())) && a.Approved && a.Active);
            totalRecord = qr.Count();
            qr = qr.OrderByDescending(c => c.ApproveDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public IList<Articles> GetByCateID(int CateID, string keyword, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(ar => ar.CategoryID == CateID && ar.Active == true && ar.Approved);
            if (!string.IsNullOrEmpty(keyword))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(keyword.ToUpper()) || a.NameENG.ToUpper().Contains(keyword.ToUpper()));
            total = qr.Count();
            qr = qr.OrderByDescending(ar => ar.ApproveDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public IList<Articles> GetByCateID(int CateID, string keyword)
        {
            var qr = Query.Where(ar => ar.Active == true);
            if (CateID > 0)
                qr = Query.Where(ar => ar.CategoryID == CateID && ar.Active == true);
            if (!string.IsNullOrEmpty(keyword))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(keyword.ToUpper()) || a.NameENG.ToUpper().Contains(keyword.ToUpper()));
            qr = qr.OrderByDescending(ar => ar.ApproveDate);
            return qr.ToList();
        }

        public IList<Articles> GetInCate(int[] CateIDs, string keyword, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(ar => CateIDs.Contains(ar.CategoryID) && ar.Active == true && ar.Approved);
            if (!string.IsNullOrEmpty(keyword))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(keyword.ToUpper()) || a.NameENG.ToUpper().Contains(keyword.ToUpper()));            
            total = qr.Count();
            if (CateIDs != null && CateIDs.Count() > 0)
                qr = qr.OrderBy(ar => ar.ApproveDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
        public IList<Articles> GetHotNews(string keyword, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(ar => ar.Active && ar.Approved && ar.IsHot);
            if (!string.IsNullOrEmpty(keyword))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(keyword.ToUpper()) || a.NameENG.ToUpper().Contains(keyword.ToUpper()));
            total = qr.Count();
            qr = qr.OrderByDescending(ar => ar.ApproveDate).ThenBy(ar => ar.Priority);            
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
        public IList<Articles> GetInCate(int CateID, string keyword, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(ar => ar.Active == true);
            if (!string.IsNullOrEmpty(keyword))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(keyword.ToUpper()) || a.NameENG.ToUpper().Contains(keyword.ToUpper()));
            if (CateID > 0)
                qr = qr.Where(ar => CateID == ar.CategoryID);
            total = qr.Count();
            qr = qr.OrderByDescending(ar => ar.ApproveDate).ThenBy(ar => ar.Priority);
            
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public IList<Articles> GetbyIds(int[] ids)
        {
            return Query.Where(p => ids.Contains(p.Id)).ToList();
        }


        public bool Delete(Articles art, out string message)
        {
            message = "";
            INotificationService notifiSrv = FX.Core.IoC.Resolve<INotificationService>();
            try
            {
                List<Notification> notifis = notifiSrv.Query.Where(p => p.ArticleId == art.Id).ToList();
                foreach (var it in notifis)
                {
                    notifiSrv.Delete(it);
                }
                Delete(art);
                CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
