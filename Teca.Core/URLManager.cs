using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teca.Core.Domain;

namespace Teca.Core
{
    public static class URLManager
    {
        static string bannerURL = "{0}";
        static string articleURL = "/{1}/{0}.html";
        static string artCatURL = "/tin-tuc/{0}";
        static string videoCatURL = "/video/{0}";        
        static string videoURL = "/video/{0}/{1}.html";
        static string galleryCatURL = "/hinh-anh/{0}";
        static string menuURL = "/{0}";
        public static string GetURL(Banners item)
        {
            return string.Format(bannerURL, item.NavigateUrl);
        }
        public static string GetURL(Menu item)
        {
            return string.Format(menuURL, item.NavigateUrl);
        }
        public static string GetURL(Articles item)
        {
            return string.Format(articleURL, item.URLName, item.Categories.UrlName);
        }
        public static string GetURL(Category item)
        {
            return string.Format(artCatURL, item.UrlName);
        }
        public static string GetURL(GalleryType item)
        {
            return string.Format(galleryCatURL, item.UrlName);
        }
        public static string GetURL(VideoType item)
        {
            return string.Format(videoCatURL, item.UrlName);
        }
        public static string GetURL(Videos item)
        {
            return string.Format(videoURL, item.VideoTypes.UrlName, item.URLName);
        }   
    }

    public class Utils
    {
        public static string GetDescription(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
