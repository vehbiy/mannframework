using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MannFramework.Application
{
    public class SearchResultModel
    {
        public string Keyword { get; set; }
        public List<SearchResultItemModel> Results { get; set; }

        public SearchResultModel(string keyword, List<SearchResultItemModel> results)
        {
            if (results == null)
            {
                results = new List<SearchResultItemModel>();
            }

            this.Keyword = keyword;
            this.Results = results;
        }

        public SearchResultModel()
        {
            this.Keyword = "";
            this.Results = new List<SearchResultItemModel>();
        }
    }

    public class SearchResultItemModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string FullUrl { get; set; }

        public SearchResultItemModel(ISearchable result)
        {
            this.Title = result.Title;
            this.Description = result.Description;
            this.Icon = result.Icon;
            this.Url = "/" + result.GetType().Name + "/Edit?Id=" + result.Id;
            this.FullUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "") + this.Url;
        }
    }
}