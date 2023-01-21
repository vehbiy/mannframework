using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application.Manager
{
    public class ClickManager : StateManager<ClickManager>
    {
        public List<Uri> Urls
        {
            get
            {
                List<Uri> urls = Read<List<Uri>>("Urls");

                if (urls == null)
                {
                    urls = new List<Uri>();
                    Write<List<Uri>>("Urls", urls);
                }

                return urls;
            }
            set
            {
                Write<List<Uri>>("Urls", value);
            }
        }

        protected string IndexString { get { return Read<string>("Index"); } set { Write<string>("Index", value); } }
        protected int Index { get { return Helpers.GetValueFromObject<int>(this.IndexString); } set { this.IndexString = value.ToString(); } }

        public ClickManager()
        {
        }

        public void SaveClick(Uri url)
        {
            if (!string.IsNullOrEmpty(url.AbsolutePath) && !url.AbsolutePath.Equals(this.Urls.LastOrDefault()?.AbsolutePath))
            {
                this.Urls.Add(url);
                this.Index = this.Urls.Count - 1;
            }
        }

        public Uri Back(bool navigate = false)
        {
            if (this.Index == 0)
            {
                return this.Urls.FirstOrDefault();
            }

            Uri url = this.Urls[this.Index - 1];

            if (navigate)
            {
                //this.Urls.Remove(url);
                this.Urls.RemoveAt(this.Index);
                this.Index--;
            }

            return url;
        }

        public Uri Forward(bool navigate = false)
        {
            if (this.Index == this.Urls.Count - 1)
            {
                return this.Urls.LastOrDefault();
            }

            Uri url = this.Urls[this.Index + 1];

            if (navigate)
            {
                this.Index++;
            }

            return url;
        }
    }
}
