using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
//using Microsoft.Owin.Security.Cookies;
//using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;

/// <summary>
/// Add [assembly: OwinStartup(typeof($namespace$.$startupclass$))] before namespace decleration
/// </summary>
namespace MannFramework.Application
{
    public abstract class GarciaApiStartup
    {
        protected abstract void Register(HttpConfiguration configuration);

        /// <summary>
        /// Should be overriden if a custom AuthorizationServerProvider will be used
        /// </summary>
        /// <returns></returns>
        protected virtual AuthorizationServerProvider CreateAuthorizationServerProvider()
        {
            return new AuthorizationServerProvider();
        }

        public virtual void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            ConfigureOAuth(appBuilder);
            this.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));
        }

        protected virtual void ConfigureOAuth(IAppBuilder appBuilder)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/api/token"), // token alacağımız path'i belirtiyoruz
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
                Provider = this.CreateAuthorizationServerProvider()
            };

            // AppBuilder'a token üretimini gerçekleştirebilmek için ilgili authorization ayarlarımızı veriyoruz.
            appBuilder.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);

            // Authentication type olarak ise Bearer Authentication'ı kullanacağımızı belirtiyoruz.
            // Bearer token OAuth 2.0 ile gelen standartlaşmış token türüdür.
            // Herhangi kriptolu bir veriye ihtiyaç duymadan client tarafından token isteğinde bulunulur ve server belirli bir expire date'e sahip bir access_token üretir.
            // Bearer token üzerinde güvenlik SSL'e dayanır.
            // Bir diğer tip ise MAC token'dır. OAuth 1.0 versiyonunda kullanılıyor, hem client'a, hemde server tarafına implementasyonlardan dolayı ek maliyet çıkartmaktadır. Bu maliyetin yanı sıra ise Bearer token'a göre kaynak alış verişinin biraz daha güvenli olduğu söyleniyor çünkü client her request'inde veriyi hmac ile imzalayıp verileri kriptolu bir şekilde göndermeleri gerektiği için.
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }

    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        protected MembershipManagerBase membershipManager = MembershipManager.Instance;
        // OAuthAuthorizationServerProvider sınıfının client erişimine izin verebilmek için ilgili ValidateClientAuthentication metotunu override ediyoruz.
        public override async System.Threading.Tasks.Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        protected virtual OperationResult Login(OAuthGrantResourceOwnerCredentialsContext context)
        {
            OperationResult loginResult = this.membershipManager.InnerLogin(context.UserName, context.Password);
            return loginResult;
        }

        // OAuthAuthorizationServerProvider sınıfının kaynak erişimine izin verebilmek için ilgili GrantResourceOwnerCredentials metotunu override ediyoruz.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // CORS ayarlarını set ediyoruz.
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            OperationResult loginResult = this.Login(context);

            if (loginResult.Success)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                context.Validated(identity);
            }
            else
            {
                string message = !string.IsNullOrEmpty(loginResult.LocalizedValidationMessage) ? loginResult.LocalizedValidationMessage : GarciaLocalizationManager.Localize("InvalidCredentials");
                context.SetError("invalid_grant", message);
            }

            //// Kullanıcının access_token alabilmesi için gerekli validation işlemlerini yapıyoruz.
            //if (context.UserName == "Gokhan" && context.Password == "123456")
            //{
            //    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            //    identity.AddClaim(new Claim("sub", context.UserName));
            //    identity.AddClaim(new Claim("role", "user"));

            //    context.Validated(identity);
            //}
            //else
            //{
            //    context.SetError("invalid_grant", "Kullanıcı adı veya şifre yanlış.");
            //}
        }
    }
}
