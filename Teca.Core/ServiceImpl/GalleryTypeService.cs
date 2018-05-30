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
    public class GalleryTypeService: BaseService<GalleryType, int>, IGalleryTypeService
    {
        public GalleryTypeService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public IList<GalleryType> GetList(string username, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(p=>p.CreateBy == username);
            total = qr.Count();
            qr = qr.OrderByDescending(p => p.CreateDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();            
        }

        public bool CreateNew(GalleryType mType, IList<Gallery> galleries, out string messages)
        {
            messages = "";
            try
            {
                CreateNew(mType);
                IGalleryService gallerySrv = FX.Core.IoC.Resolve<IGalleryService>();
                foreach (var it in galleries)
                {
                    it.TypeID = mType.Id;
                    gallerySrv.CreateNew(it);
                }
                CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                messages = "Có lỗi xảy ra, vui lòng thực hiện lại.";
                return false;
            }
        }


        public bool Update(GalleryType mType, IList<Gallery> galleries, out string messages)
        {
            messages = "";
            BeginTran();
            try
            {                
                IGalleryService gallerySrv = FX.Core.IoC.Resolve<IGalleryService>();
                foreach (var ii in mType.Galleries)
                {
                    gallerySrv.Delete(ii);
                }
                foreach (var it in galleries)
                {
                    it.TypeID = mType.Id;
                    gallerySrv.CreateNew(it);
                }
                Save(mType);
                CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                RolbackTran();
                messages = "Có lỗi xảy ra, vui lòng thực hiện lại.";
                return false;
            }
        }

        public IList<GalleryType> GetList(int pageIndex, int pageSize, out int total)
        {
            var qr = Query;
            total = qr.Count();
            qr = qr.OrderBy(p => p.CreateDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();  
        }
    }
}
