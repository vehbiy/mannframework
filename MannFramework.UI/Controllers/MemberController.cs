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
using System.Web.Mvc;
using MannFramework.Application;

namespace MannFramework.Macondo.Controllers
{
    public partial class MemberController : GarciaEntityMvcController<Member>
    {
        public override ActionResult Edit(Member model)
        {
            //if (ModelState.IsValid)
            //{
            //    if (model.Password.Equals(model.ConfirmPassword))
            //    {
            //        ModelState.AddModelError("ConfirmPassword", GarciaLocalizationManager.Localize("PasswordsDontMatch"));
            //        return View(model);
            //    }
            //}

            return base.Edit(model);
        }
    }
}

