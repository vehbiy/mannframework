﻿using MannFramework.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Tests.Garcia
{
    [TestClass]
    public class EntityManagerTests
    {
        [TestMethod]
        public void JoinGetItemsTest()
        {
            Project project = EntityManager.Instance.GetItem<Project>(1);
            ProjectSetting setting = project.GetProjectSetting();
            GarciaConfigurationManager.SetConfigurationValues(typeof(GarciaConfiguration), setting);
        }
    }
}
