using MannFramework.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MannFramework.Macondo.Desktop
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        static BaseForm()
        {
            List<Language> languages = EntityManager.Instance.GetItems<Language>();
            DependencyManager.Localizer = new GarciaApplicationLocalizer(languages.Select(x => x.CultureCode).ToList());
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.LocalizeControls();
        }

        protected void LocalizeControls()
        {
            this.Text = this.Localize(this.Text);

            foreach (Control control in this.Controls)
            {
                if (this.IsLocalizable(control))
                {
                    control.Text = this.Localize(control.Text);
                    Type type = control.GetType();
                    PropertyInfo autoSize = type.GetProperty("AutoSize");

                    if (autoSize != null)
                    {
                        autoSize.SetValue(control, true);
                    }
                }
            }
        }

        protected string Localize(string key)
        {
            return GarciaLocalizationManager.Localize(key);
        }

        protected bool IsLocalizable(Control control)
        {
            List<Type> nonLocalizableControls = new List<Type>()
            {
                typeof(TextBox),
                typeof(RichTextBox)
            };

            foreach (Type controlType in nonLocalizableControls)
            {
                if (control.GetType().IsAssignableFrom(controlType))
                {
                    return false;
                }
            }

            return true;
        }

        protected void OpenForm(Type formType)
        {
            Form form = (Form)Activator.CreateInstance(formType);
            form.Show();
            //this.Hide();
        }

        private void hashingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenForm(typeof(HashingForm));
        }

        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenForm(typeof(ProjectsForm));
        }
    }
}
