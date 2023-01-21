using MannFramework.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MannFramework.Macondo.Models
{
    public class CopyItemModel
    {
        //public Project Project { get; set; }
        //public Item Item { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? ProjectId { get; set; }
        [Required]
        public int? ItemId { get; set; }
    }
}