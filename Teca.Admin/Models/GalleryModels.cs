using FX.Utils.MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teca.Core.Domain;

namespace Teca.Admin.Models
{
    public class GalleryModels
    {
        public IList<GalleryType> GalleryTypes { get; set; }
        public IPagedList<Gallery> Galleries { get; set; }
        public int TypeID { get; set; }
    }

    public class BannerModels
    {
        public BannerType Type { get; set; }
        public IPagedList<Banners> PageListBanners { get; set; }
    }

    public class MenusModels
    {
        public MenuListType Type { get; set; }
        public IPagedList<Menu> PageListMenus { get; set; }
    }
}