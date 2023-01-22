using MannFramework.Application;
using MannFramework.Application.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;

namespace MannFramework.Application
{
    [AllowAnonymous]
    public class MannFrameworkAccountController : MannFrameworkMvcController
    {
        [HttpGet]
        public virtual ActionResult Login()
        {
            if (StateManager.Instance.Member != null)
            {
                return Redirect("~/");
            }

            return View();
        }

        [HttpPost]
        public virtual ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            OperationResult<Member> result = MembershipManager.Instance.Login(model.Email, model.Password);
           
            if (result.Success)
            {
                FormsAuthentication.SetAuthCookie(result.Item.Email, false);
                StateManager.Instance.Member = result.Item;
                StateManager.Instance.Language = StateManager.Instance.Member.DefaultLanguage;
                return Redirect("~/");
            }
            else
            {
                ModelState.AddModelError("Result", result.LocalizedValidationMessage);
            }

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Logout()
        {
            this.InnerLogout();
            return Redirect("~/");
        }

        [HttpPost]
        public virtual ActionResult LogoutPost()
        {
            this.InnerLogout();
            return Redirect("~/");
        }

        protected virtual void InnerLogout()
        {
            StateManager.Instance.Member = null;
            StateManager.Instance.Language = null;
            FormsAuthentication.SignOut();
        }

        [HttpGet]
        public virtual ActionResult ChangeLanguage(int id)
        {
            Language item = EntityManager.Instance.GetItem<Language>(id);

            if (item != null)
            {
                StateManager.Instance.Language = item;
            }

            return Redirect("~/");
        }

        [HttpGet]
        public ActionResult Back(int count = 1)
        {
            Uri uri = null;

            for (int i = 0; i < count; i++)
            {
                uri = ClickManager.Instance.Back(true);
                uri = uri.AddQueryStringValue("ref", "1");
            }

            return uri != null ? new RedirectResult(uri.AbsoluteUri) : this.RedirectToHome();
        }

        [HttpGet]
        public ActionResult Forward()
        {
            Uri uri = ClickManager.Instance.Forward(true);
            uri = uri.AddQueryStringValue("ref", "1");
            return new RedirectResult(uri.AbsoluteUri);
        }
    }
}
