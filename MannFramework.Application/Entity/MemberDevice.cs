using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MannFramework.Application
{
    [Serializable]
    public class MemberDevice : ApplicationEntity
    {
        public DeviceType DeviceType { get; set; }
        public string PushToken { get; set; }

        private Member member;
        internal int MemberId { get; set; }
        public Member Member
        {
            get
            {
                if (this.member == null)
                {
                    this.member = EntityManager.Instance.GetItem<Member>(this.MemberId);
                }

                return this.member;
            }

            set
            {
                this.member = value;

                if (value != null)
                {
                    this.MemberId = value.Id;
                }
            }
        }

        public MemberDevice(Member Member, DeviceType DeviceType, string PushToken)
        {
            this.Member = Member;
            this.DeviceType = DeviceType;
            this.PushToken = PushToken;
        }

        public MemberDevice()
        {
        }
    }
}
