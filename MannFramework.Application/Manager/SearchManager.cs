using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;

namespace MannFramework.Application.Manager
{
    public abstract class SearchManager<T> : SingletonBase<T>
    {
        public abstract List<ISearchable> InnerSearch(string keyword);
    }

    public sealed class SearchManager : SearchManager<SearchManager>
    {
        public static List<ISearchable> Search(string keyword)
        {
            return SearchManager.Instance.InnerSearch(keyword);
        }

        public override List<ISearchable> InnerSearch(string keyword)
        {
            List<ISearchable> searchResult = new List<ISearchable>();
            List<Project> projects = EntityManager.Instance.Search<Project>(new Dictionary<string, object> { { "Name", keyword } });
            searchResult.AddRange(projects);
            List<Item> items = EntityManager.Instance.Search<Item>(new Dictionary<string, object> { { "Name", keyword } });
            searchResult.AddRange(items);
            List<ItemProperty> properties = EntityManager.Instance.Search<ItemProperty>(new Dictionary<string, object> { { "Name", keyword } });
            searchResult.AddRange(properties);
            return searchResult;
        }
    }
}
