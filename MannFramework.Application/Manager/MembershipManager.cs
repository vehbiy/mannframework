using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    /// <summary>
    /// Do not derive from this class
    /// </summary>
    public abstract class MembershipManagerBase
    {
        public abstract OperationResult InnerLogin(string userName, string password);
    }

    public abstract class MembershipManagerBase<T> : MembershipManagerBase
        where T : Member
    {
        public virtual OperationResult<T> Register(T member)
        {
            OperationResult<T> result = new OperationResult<T>();
            T existingMember = EntityManager.Instance.GetOne<T>("Email", member.Email);

            if (existingMember != null)
            {
                result.AddValidationResult("ExistingEmail");
                return result;
            }

            //member.HashedPassword = this.CreateHashedPassword(member.Password);
            OperationResult saveResult = EntityManager.Instance.Save(member);
            result = new OperationResult<T>(saveResult, member);
            return result;
        }

        public virtual OperationResult<T> Login(string email, string password)
        {
            string hashedPassword = CreateHashedPassword(password);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Email", email);
            parameters.Add("HashedPassword", hashedPassword);
            T member = EntityManager.Instance.GetOne<T>(parameters);
            return new OperationResult<T>(member, member == null ? "InvalidEmailOrPassword" : "");
        }

        public virtual string CreateHashedPassword(string plainPassword)
        {
            string hashedPassword = Helpers.CreateOneWayHash(plainPassword, HashAlgorithm.MD5);
            return hashedPassword;
        }

        public sealed override OperationResult InnerLogin(string userName, string password)
        {
            return this.Login(userName, password);
        }
    }

    public class MembershipManager : MembershipManagerBase<Member>
    {
        public static MembershipManager Instance = new MembershipManager();

        protected MembershipManager()
        {
        }

        public virtual OperationResult<Member> Register(string email, string password, string name, string surname)
        {
            Member member = new Member()
            {
                Email = email,
                Name = name,
                Surname = surname
            };

            return this.Register(member);
        }
    }
}
