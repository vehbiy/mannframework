/*
	This file was generated automatically by Garcia Framework. 
	Do not edit manually. 
	Add a new partial class with the same name if you want to add extra functionality.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using MannFramework;

namespace MannFramework.Application
{
    public partial class ProjectSetting : ApplicationEntity
    {
        public Project Project { get { return Get(_projectId, ref _project); } set { Set(ref _project, ref _projectId, value); } }
        [NotSelected]
        [NotSaved]
        public int? ProjectId { get { return _projectId; } set { _projectId = value; } }
        [MvcListIgnore]
        [Required]
        public bool UseInsertTime { get; set; }
        [MvcListIgnore]
        [Required]
        public bool UseDeleteTime { get; set; }
        [MvcListIgnore]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string BaseProviderName { get; set; }
        [MvcListIgnore]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string BaseWebApiModelName { get; set; }
        [MvcListIgnore]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string BaseWebApiControllerName { get; set; }
        [MvcListIgnore]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string BaseMvcModelName { get; set; }
        [MvcListIgnore]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string BaseMvcControllerName { get; set; }
        [MvcListIgnore]
        [Required]
        public bool EnableCaching { get; set; }
        [MvcListIgnore]
        [Required]
        public bool GenerateCreateEntity { get; set; }
        [MvcListIgnore]
        [Required]
        public bool GenerateInitializeEntity { get; set; }
        [MvcListIgnore]
        [Required]
        public bool GenerateGetCommonParameters { get; set; }
        [MvcListIgnore]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string BaseEntityName { get; set; }
        [MvcListIgnore]
        [Required]
        public bool GenerateForeignKeyConstraint { get; set; }
        [MvcListIgnore]
        [Required]
        public bool GenerateStoredProcedures { get; set; }
        [MvcListIgnore]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string WebApiControllerNamespaceSuffix { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string WebApiModelNamespaceSuffix { get; set; }
        [Required]
        public bool GenerateWebApiModel { get; set; }
        [Required]
        public bool GenerateMvcModel { get; set; }
        [Required]
        public bool UseModeSettings { get; set; }
        [MvcListIgnore]
        [Required]
        public GarciaModeType CurrentMode { get; set; }
        [MvcListIgnore]
        [Required]
        public bool DisableCaching { get; set; }
        [MvcListIgnore]
        [Required]
        public DatabaseConnectionType DefaultDatabaseConnectionType { get; set; }
        [MvcListIgnore]
        [Required]
        public bool UpdateCacheAfterSave { get; set; }
        [MvcListIgnore]
        [StringLength(100, MinimumLength = 0)]
        public string DefaultConnectionStringName { get; set; }
        [MvcListIgnore]
        [StringLength(200, MinimumLength = 0)]
        public string ApplePushCertificateLocation { get; set; }
        [MvcListIgnore]
        [StringLength(100, MinimumLength = 5)]
        public string ApplePushCertificatePassword { get; set; }
        [MvcListIgnore]
        [Required]
        public bool ApplePushForProduction { get; set; }
        [MvcListIgnore]
        [StringLength(200, MinimumLength = 0)]
        public string AndroidPushToken { get; set; }
        [MvcListIgnore]
        [StringLength(200, MinimumLength = 0)]
        public string FileUploadPath { get; set; }
        [MvcListIgnore]
        [StringLength(200, MinimumLength = 0)]
        public string AWSAccessKey { get; set; }
        [MvcListIgnore]
        [StringLength(200, MinimumLength = 0)]
        public string AWSSecret { get; set; }
        [MvcListIgnore]
        [StringLength(200, MinimumLength = 0)]
        public string AWSBucketName { get; set; }
        [MvcListIgnore]
        [Required]
        public bool UploadFilesToAWS { get; set; }
        [MvcListIgnore]
        [Required]
        public bool UploadFilesToAzure { get; set; }
        [MvcListIgnore]
        [StringLength(200, MinimumLength = 0)]
        public string AzureConnectionString { get; set; }
        [MvcListIgnore]
        [StringLength(200, MinimumLength = 0)]
        public string AzureBlobContainer { get; set; }
        [MvcListIgnore]
        [StringLength(200, MinimumLength = 0)]
        public string SocketIOUrl { get; set; }
        [MvcListIgnore]
        [StringLength(100, MinimumLength = 0)]
        public string SocketIOApiKey { get; set; }
        [Required]
        public bool UseAlias { get; set; }
        [MvcListIgnore]
        [Required]
        public bool AddSaveLinksToBottom { get; set; }
        [MvcListIgnore]
        [Required]
        public bool AddSaveLinksToTop { get; set; }
        [MvcListIgnore]
        [Required]
        public SourceControlType SourceControlType { get; set; }
        [MvcListIgnore]
        [Required]
        public bool LazyLoad { get; set; }
        [MvcListIgnore]
        [Required]
        public OrmType OrmType { get; set; }
        [MvcListIgnore]
        [Required]
        public FrameworkType FrameworkType { get; set; }
        [MvcListIgnore]
        [Required]
        public bool GenerateWebApiGetAll { get; set; }
        [MvcListIgnore]
        [Required]
        public bool GenerateWebApiGetOne { get; set; }
        [MvcListIgnore]
        [Required]
        public bool GenerateWebApiPost { get; set; }
        [MvcListIgnore]
        [Required]
        public bool GenerateWebApiDelete { get; set; }

        #region Lazy load
        private Project _project;
        private int? _projectId;
        #endregion

        public ProjectSetting()
        {
        }
    }
}