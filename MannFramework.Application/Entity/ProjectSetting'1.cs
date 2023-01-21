using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public partial class ProjectSetting
    {
        private static ProjectSetting defaultSetting;
        public static ProjectSetting DefaultSetting
        {
            get
            {
                if (defaultSetting == null)
                {
                    lock (typeof(object))
                    {
                        if (defaultSetting == null)
                        {
                            defaultSetting = EntityManager.Instance.GetOne<ProjectSetting>("ProjectId", 0);
                        }
                    }
                }

                return defaultSetting;
            }
        }

        public override bool CachingEnabled => true;
    }
}
