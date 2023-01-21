using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public partial class Member
    {
        private Project defaultProject;
        public virtual Project DefaultProject
        {
            get
            {
                if (this.defaultProject == null)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("MemberId", this.Id);
                    parameters.Add("IsDefault", true);
                    this.defaultProject = EntityManager.Instance.GetOne<MemberProject>(parameters)?.Project;
                }

                if (this.defaultProject == null)
                {
                    this.defaultProject = EntityManager.Instance.GetItem<Project>(7);
                }

                return this.defaultProject;
            }
        }

        public string FullName
        {
            get
            {
                return new GarciaStringBuilder(this.Name, " ", this.Surname).ToString();
            }
        }

        [NotSaved]
        [NotSelected]
        [Compare("Password", ErrorMessage = "PasswordsDontMatch")]
        public string ConfirmPassword { get; set; }

        public List<MemberProject> GetProjects()
        {
            //return EntityManager.Instance.GetItems<Project>();
            //return this.Projects.Where(x => x.Project != null).Select(x => x.Project).ToList();
            return EntityManager.Instance.GetItems<MemberProject>("MemberId", this.Id).Where(x => x.Project != null).ToList();
        }

        public List<Role> GetRoles()
        {
            List<MemberInRole> memberInRoles = EntityManager.Instance.GetItems<MemberInRole>("MemberId", this.Id);
            return memberInRoles.Select(x => x.Role).ToList();
        }

        public override string ToString()
        {
            return this.FullName;
        }
    }
}
