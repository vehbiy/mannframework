/*
	This file was generated automatically by MannFramework Framework. 
	Do not edit manually. 
	Add a new partial class with the same name if you want to add extra functionality.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MannFramework;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MannFramework.Application
{
    public partial class Project : ISearchable
    {
        public override bool CachingEnabled => true;

        public ProjectSetting GetProjectSetting()
        {
            ProjectSetting setting = null;

            if (this.Id != 0)
            {
                setting = EntityManager.Instance.GetOne<ProjectSetting>("ProjectId", this.Id);
            }

            if (setting == null)
            {
                setting = this.CreateDefaultProjectSetting();
            }

            return setting;
        }

        private ProjectSetting CreateDefaultProjectSetting()
        {
            ProjectSetting setting = new ProjectSetting()
            {
                Project = this,
                ProjectId = this.Id,
                BaseEntityName = "CustomEntity",
                BaseMvcControllerName = "Controller",
                BaseMvcModelName = "Model",
                BaseProviderName = "CustomProvider",
                BaseWebApiControllerName = "ApiController",
                BaseWebApiModelName = "Model",
                EnableCaching = true,
                GenerateCreateEntity = true,
                GenerateForeignKeyConstraint = true,
                GenerateGetCommonParameters = true,
                GenerateInitializeEntity = true,
                GenerateMvcModel = false,
                GenerateStoredProcedures = false,
                GenerateWebApiModel = false,
                UseDeleteTime = true,
                UseInsertTime = true,
                WebApiControllerNamespaceSuffix = "Controller",
                WebApiModelNamespaceSuffix = "Model",
                UseAlias = false,
                AddSaveLinksToBottom = false,
                AddSaveLinksToTop = true,

                UseModeSettings = true,
                CurrentMode = MannFrameworkModeType.Development,
                DisableCaching = false,
                DefaultDatabaseConnectionType = DatabaseConnectionType.DynamicSql,
                UpdateCacheAfterSave = true,
                DefaultConnectionStringName = "",

                ApplePushCertificateLocation = "",
                ApplePushCertificatePassword = "",
                ApplePushForProduction = false,
                AndroidPushToken = "",
                FileUploadPath = "",
                AWSAccessKey = "",
                AWSSecret = "",
                AWSBucketName = "",
                UploadFilesToAWS = false,
                UploadFilesToAzure = false,
                AzureConnectionString = "",
                AzureBlobContainer = "",
                SocketIOUrl = "",
                SocketIOApiKey = ""
            };

            return setting;
        }

        #region ISearchable
        public string Title { get { return this.Name; } }
        public string Description { get { return this.Name; } }
        public string Icon { get { return ""; } } 
        #endregion
    }
}

