using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MannFramework.Application;

namespace MannFramework.CodeGenerator
{
    public static class GarciaGeneratorConfiguration
    {
        //public static string ConnectionString { get; set; }
        /// <summary>
        /// Used for 1-n relationship. Example: Person->AddressList
        /// </summary>
        //public static string ListSuffix { get; set; }
        /// <summary>
        /// Used for 1-1 relationship. Example: Address->CityId
        /// </summary>
        //public static string IdSuffix { get; set; }
        public static bool UseInsertTime { get; set; }
        public static bool UseDeleteTime { get; set; }
        public static string BaseProviderName { get; set; }
        //public static bool EnableCachingInProviders { get; set; }
        //public static bool LazyLoad { get; set; }
        //public static string GetRestriction { get; set; }
        //public static string SetRestriction { get; set; }
        public static string BaseWebApiModelName { get; set; }
        public static string BaseWebApiControllerName { get; set; }
        public static string BaseMvcModelName { get; set; }
        public static string BaseMvcControllerName { get; set; }
        public static bool EnableCaching { get; set; }
        public static bool GenerateCreateEntity { get; set; }
        public static bool GenerateInitializeEntity { get; set; }
        public static bool GenerateGetCommonParameters { get; set; }
        public static bool UseAlias { get; set; }
        public static string BaseEntityName { get; set; }
        public static bool GenerateForeignKeyConstraint { get; set; }
        public static bool GenerateStoredProcedures { get; set; }
        //public static string WebApiNamespaceSuffix { get; set; }
        public static string WebApiControllerNamespaceSuffix { get; set; }
        public static string WebApiModelNamespaceSuffix { get; set; }
        //public static bool GenerateEntity { get; set; }
        //public static bool GenerateProvider { get; set; }
        //public static bool GenerateMssql { get; set; }
        public static bool GenerateWebApiModel { get; set; }
        //public static bool GenerateWebApiController { get; set; }
        public static bool GenerateMvcModel { get; set; }
        //public static bool GenerateMvcView { get; set; }
        //public static bool GenerateMvcController { get; set; }
        public static bool AddSaveLinksToBottom { get; set; }
        public static bool AddSaveLinksToTop { get; set; }
        public static bool LazyLoad { get; set; }
        public static FrameworkType FrameworkType { get; set; }
        public static OrmType OrmType { get; set; }
        public static bool GenerateWebApiGetAll { get; set; }
        public static bool GenerateWebApiGetOne { get; set; }
        public static bool GenerateWebApiPost { get; set; }
        public static bool GenerateWebApiDelete { get; set; }

        static GarciaGeneratorConfiguration()
        {
            GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaGeneratorConfiguration));
        }
    }
}
