using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    public class Image : ApplicationEntity
    {
        public string FileName { get; set; }

        public Image(string fileName)
        {
            this.FileName = fileName;
        }

        public Image()
        {
        }
    }
}
