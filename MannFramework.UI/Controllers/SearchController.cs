using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MannFramework.Application;
using MannFramework.Application.Manager;
using MannFramework.Macondo.Models;

namespace MannFramework.Macondo.Controllers
{
    public class SearchController : GarciaMvcController
    {
        [HttpPost]
        public ActionResult Result(string keyword)
        {
            this.SetTitle();
            List<SearchResultItemModel> models = new List<SearchResultItemModel>();
            List<ISearchable> results = SearchManager.Search(keyword);

            foreach (ISearchable result in results)
            {
                SearchResultItemModel model = new SearchResultItemModel(result);
                models.Add(model);
            }

            SearchResultModel resultModel = new SearchResultModel(keyword, models);
            StateManager.Instance.Write<SearchResultModel>(resultModel);
            return View(resultModel);
        }

        [HttpGet]
        public ActionResult Result()
        {
            SearchResultModel model = StateManager.Instance.Read<SearchResultModel>();

            if (model == null)
            {
                model = new SearchResultModel();
            }
            
            return View(model);
        }
    }
}