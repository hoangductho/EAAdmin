using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;

namespace Teca.Core.IService
{
    public interface IGalleryTypeService : IBaseService<GalleryType, int>
    {
        IList<GalleryType> GetList(int pageIndex, int pageSize, out int total);
        IList<GalleryType> GetList(string username,int pageIndex, int pageSize, out int total);
        bool CreateNew(GalleryType mType, IList<Gallery> galleries, out string messages);
        bool Update(GalleryType mType, IList<Gallery> galleries, out string messages);
    }
}
