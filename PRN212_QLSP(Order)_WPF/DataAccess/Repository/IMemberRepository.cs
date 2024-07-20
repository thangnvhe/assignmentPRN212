using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetList();
        Member GetMemberByEmail(string email);
        bool IsAdmin(Member member);
        Member VerifyMember(Member member);
        void Add(Member member);
        void Update(Member member);
    }
}
