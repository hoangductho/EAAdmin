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
    public class VideosService : BaseService<Videos, int>, IVideosService
    {
        public VideosService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }
        public IList<Videos> GetBySearch( int TypeID, int pageIndex, int pageSize, out int totalRecord)
        {
            //chỉ lấy video active
            var qr = Query.Where(p => p.Active);
            if (TypeID > 0)
                qr = qr.Where(a => a.VideoTypeID == TypeID);
            totalRecord = qr.ToList().Count;
            qr = qr.OrderByDescending(c => c.CreatedDate).ThenByDescending(c => c.ModifiedDate).ThenByDescending(c => c.NameVNI);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
        public IList<Videos> GetBySearch(string title, int TypeID, string username, int pageIndex, int pageSize, out int totalRecord)
        {   
            var qr = Query.Where(p=>p.CreatedBy.Equals(username));
            if (!string.IsNullOrEmpty(title))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(title.ToUpper()) || a.NameENG.ToUpper().Contains(title.ToUpper()));
            if (TypeID > 0)
                qr = qr.Where(a => a.VideoTypeID == TypeID);
            totalRecord = qr.ToList().Count;
            qr = qr.OrderByDescending(c => c.CreatedDate).ThenByDescending(c => c.ModifiedDate).ThenByDescending(c=>c.NameVNI);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
        public IList<Videos> GetBySearch(string title, int TypeID, int pageIndex, int pageSize, out int totalRecord)
        {      
            //lấy hết
            var qr = Query;
            if (!string.IsNullOrEmpty(title))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(title.ToUpper()) || a.NameENG.ToUpper().Contains(title.ToUpper()));
            if (TypeID > 0)
                qr = qr.Where(a => a.VideoTypeID == TypeID);
            totalRecord = qr.ToList().Count;
            qr = qr.OrderByDescending(c => c.CreatedDate).ThenByDescending(c => c.ModifiedDate).ThenByDescending(c => c.NameVNI);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
        public IList<Videos> GetAllVideo(int pageIndex, int pageSize, out int totalRecord)
        {
            var qr = Query.Where(p=>p.Active && p.VideoTypes.Active);
            
            totalRecord = qr.ToList().Count;
            qr = qr.OrderByDescending(c => c.CreatedDate).ThenByDescending(c => c.ModifiedDate).ThenByDescending(c => c.NameVNI);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
        public IList<Videos> GetByTypeID(int TypeID, string keyword, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(ar => ar.VideoTypeID == TypeID && ar.Active == true);
            if (!string.IsNullOrEmpty(keyword))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(keyword.ToUpper()) || a.NameENG.ToUpper().Contains(keyword.ToUpper()));
            qr = qr.OrderBy(ar => ar.ModifiedDate);
            total = qr.Count();
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public IList<Videos> GetByTypeID(int TypeID, string keyword)
        {
            var qr = Query.Where(ar => ar.Active == true);
            if (TypeID > 0)
                qr = Query.Where(ar => ar.VideoTypeID == TypeID && ar.Active == true);
            if (!string.IsNullOrEmpty(keyword))
                qr = qr.Where(a => a.NameVNI.ToUpper().Contains(keyword.ToUpper()) || a.NameENG.ToUpper().Contains(keyword.ToUpper()));
            qr = qr.OrderByDescending(ar => ar.ModifiedDate);
            return qr.ToList();
        }
        public IList<Videos> GetByRelate(int ID, int TypeID, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(p => p.Active && p.Id != ID && p.VideoTypeID == TypeID).OrderByDescending(p => p.Id);
            
            qr = qr.OrderByDescending(ar => ar.ModifiedDate);
            total = qr.Count();
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

      
    }
}
