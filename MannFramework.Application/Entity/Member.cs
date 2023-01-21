using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public partial class Member : ApplicationEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Surname { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Email { get; set; }

        private string password;
        [NotSelected]
        [MvcIgnore]
        [MvcListIgnore]
        [StringLength(50, MinimumLength = 5)]
        [NotSaved]
        public string Password
        {
            get { return this.password;}
            set
            {
                this.password = value;

                if (!string.IsNullOrEmpty(this.password))
                {
                    this.HashedPassword = MembershipManager.Instance.CreateHashedPassword(this.password);
                }
            }
        }
        [MvcListIgnore]
        public DateTime? BirthDate { get; set; }
        [MvcListIgnore]
        [StringLength(50, MinimumLength = 0)]
        public string FacebookId { get; set; }
        [MvcListIgnore]
        [Required]
        public Gender Gender { get; set; }
        [StringLength(100, MinimumLength = 0)]
        public string ProfilePhoto { get; set; }
        public Language DefaultLanguage { get { return Get(_defaultLanguageId, ref _defaultLanguage); } set { Set(ref _defaultLanguage, ref _defaultLanguageId, value); } }
        [NotSelected]
        [NotSaved]
        public int? DefaultLanguageId { get { return _defaultLanguageId; } set { _defaultLanguageId = value; } }
        [Required]
        public virtual List<MemberProject> Projects { get { return Get(ref _projects); } set { Set(ref _projects, value); } }
        [Required]
        public virtual List<ProjectMapping> ProjectMappings { get { return Get(ref _projectMappings); } set { Set(ref _projectMappings, value); } }
        [Required]
        public List<MemberInRole> Roles { get { return Get(ref _roles); } set { Set(ref _roles, value); } }
        [NotUpdated]
        [MvcListIgnore]
        public string HashedPassword { get; internal set; }

        #region Lazy load
        private Language _defaultLanguage;
        private int? _defaultLanguageId;
        private List<MemberProject> _projects;
        private List<ProjectMapping> _projectMappings;
        private List<MemberInRole> _roles;
        #endregion

        public Member()
        {
            this.Projects = new List<MemberProject>();
            this.ProjectMappings = new List<ProjectMapping>();
            this.Roles = new List<MemberInRole>();
        }
    }

    public class Member2 : ApplicationEntity
    {
        public Gender Gender { get; set; }
        public Gender? Gender2 { get; set; }
        public List<Gender> Genders { get; set; }
    }
}
