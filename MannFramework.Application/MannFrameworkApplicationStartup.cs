using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public class MannFrameworkApplicationStartup : Startup
    {
        public override void Start()
        {
            List<Language> languages = EntityManager.Instance.GetItems<Language>();
            DependencyManager.Localizer = new MannFrameworkApplicationLocalizer(languages.Select(x => x.CultureCode).ToList());
        }
    }
}
