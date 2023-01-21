using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.AspNet.Identity;

namespace MannFramework.Application
{
    public class StateManager<T> : SingletonBase<T>
    {
        protected HttpSessionState State
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public object this[string key]
        {
            get
            {
                return this.State[key];
            }
            set
            {
                this.State[key] = value;
            }
        }

        public K Read<K>(string key)
            where K : class
        {
            return this[key] as K;
        }

        public K Read<K>()
            where K : class
        {
            return Read<K>(typeof(K).Name);
        }

        public void Write<K>(string key, K value)
            where K : class
        {
            this[key] = value;
        }

        public void Write<K>(K value)
            where K : class
        {
            Write<K>(typeof(K).Name, value);
        }

        public string CultureCode { get { return Read<string>("CultureCode"); } set { Write<string>("CultureCode", value); } }

        public Member Member
        {
            get
            {
                Member member = Read<Member>();

                if (member == null && HttpContext.Current.User.Identity != null)
                {
                    IIdentity identity = HttpContext.Current.User.Identity;
                    string email = identity.GetUserName();

                    if (!string.IsNullOrEmpty(email))
                    {
                        member = EntityManager.Instance.GetOne<Member>("Email", email);
                        //member = new Member()
                        //{
                        //    Name = "Vehbi",
                        //    Surname = "Yurdakurban",
                        //    Email = "vehbiy@gmail.com",
                        //    ProfilePhoto = "",
                        //    DefaultLanguage = EntityManager.Instance.GetItem<Language>(2),
                        //    Gender = Gender.Male
                        //};

                        Write<Member>(member);
                    }
                    else
                    {
                        FormsAuthentication.SignOut();
                    }
                }

                return member;
            }
            set
            {
                Write<Member>(value);
            }
        }

        public Language Language
        {
            get
            {
                Language language = Read<Language>();

                if (language == null)
                {
                    //if (Member != null)
                    //{
                    //    language = Member.DefaultLanguage;
                    //}

                    if (language == null)
                    {
                        language = EntityManager.Instance.GetOne<Language>("IsDefault", true);
                    }
                }

                if (language != null)
                {
                    Language = language;
                    CultureCode = language.CultureCode;
                }

                //if (language == null)
                //{
                //    if (Member != null)
                //    {
                //        language = Member.DefaultLanguage;
                //    }

                //    if (language == null)
                //    {
                //        language = EntityManager.Instance.GetOne<Language>("IsDefault", true);
                //    }
                //}

                //if (language != null)
                //{
                //    Language = language;
                //}

                return language;
            }
            set
            {
                Write<Language>(value);

                if (value != null)
                {
                    CultureCode = value.CultureCode;
                }
            }
        }
    }

    public class StateManager : StateManager<StateManager>
    {
    }
}
