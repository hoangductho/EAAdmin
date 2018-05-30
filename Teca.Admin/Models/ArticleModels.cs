using FX.Utils.MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teca.Core.Domain;

namespace Teca.Admin.Models
{
    public class CategoryModels
    {
        public Category ArtCategory { get; set; }
        public IList<Category> ParentCategory { get; set; }

        public int articleNumbers { get; set; }
    }

    public class ArticleModels
    {
        public Articles Article { get; set; }
        public IList<Category> Categories { get; set; }
        public Notification Notification { get; set; }
    }

    public class CategoryIndexModel
    {
        public string Keyword { get; set; }
        public IPagedList<Category> Categories;
    }

    public class ArticleIndexModel
    {
        public string Keyword { get; set; }        
        public string CatID { get; set; }
        public IPagedList<Articles> Articles;
        public List<Category> Categories = new List<Category>();       
    }
}