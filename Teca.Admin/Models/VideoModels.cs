using FX.Utils.MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teca.Core.Domain;

namespace Teca.Admin.Models
{
    public class VideoTypeModels
    {
        public VideoType ArtVideoType { get; set; }
       
    }

    public class VideoModels
    {
        public Videos Video { get; set; }
        public IList<VideoType> VideoTypes { get; set; }
    }

    public class VideoTypeIndexModel
    {
        public string Keyword { get; set; }
        public IPagedList<VideoType> VideoTypes;
    }

    public class VideoIndexModel
    {
        public string Keyword { get; set; }
        public string TypeID { get; set; }
        public IPagedList<Videos> Videos;
        public List<VideoType> VideoTypes = new List<VideoType>();
        public Dictionary<object, object> HotStatus = new Dictionary<object, object>();
        public Dictionary<object, object> PubStatus = new Dictionary<object, object>();
        public int CurrentType;
        public int CurrentHotStatus;
        public int CurrentPubStatus;
        public string ImageRootUrl;
        public string VideoRootUrl;
    }
}