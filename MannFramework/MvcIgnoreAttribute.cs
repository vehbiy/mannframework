﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MvcIgnoreAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MvcListIgnoreAttribute : Attribute
    {

    }
}
