/*
	This file was generated automatically by Garcia Framework. 
	Do not edit manually. 
	Add a new partial class with the same name if you want to add extra functionality.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MannFramework;
using System.ComponentModel.DataAnnotations;
using MannFramework.Application;

namespace MannFramework.Macondo.Models
{
    public partial class ItemModel : Model<Item>
    {
        public string ProjectName { get; set; }
        public string Name { get; set; }
        public bool IsEnum { get; set; }

        public ItemModel()
        {
        }

        public ItemModel(Item item)
        {
            this.ProjectName = item.ProjectName;
            this.Name = item.Name;
            this.IsEnum = item.IsEnum;
            this.Id = item.Id;
        }
    }
}

