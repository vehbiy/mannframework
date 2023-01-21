using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    public partial class GarciaConfiguration
    {
        public static bool UseModeSettings { get; set; }
        public static GarciaModeType CurrentMode { get; set; }
        public static bool DisableCaching { get; set; }
        //public static bool UseInsertTime { get; set; }
        public static bool UseDeleteTime { get; set; }
        public static DatabaseConnectionType DefaultDatabaseConnectionType { get; set; }
        //public static bool AllowDifferentDatabaseConnectionTypes { get; set; }
        //public static bool AllowNonSingletonProviders { get; set; }
        public static bool UpdateCacheAfterSave { get; set; }
        public static string DefaultConnectionStringName { get; set; }
        //public static bool UseApplicationFrameworkFeatures { get; set; }

        static GarciaConfiguration()
        {
            //CurrentMode = GarciaConfigurationManager.GetValue<GarciaModeType>("CurrentMode");
            GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaConfiguration));
        }
    }
}
