using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository, IBase
    {
        public IEnumerable<Member> GetList() => MemberDAO.Instance.GetList();

        public bool IsAdmin(Member member) => MemberDAO.Instance.IsAdmin(member);

        public Member GetMemberByEmail(string email) => MemberDAO.Instance.GetByEmail(email);

        public void DeleteById(int id) => MemberDAO.Instance.DeleteById(id);
        public Member VerifyMember(Member member) => MemberDAO.Instance.VerifyMember(member);
        public void Add(Member member) => MemberDAO.Instance.Add(member);
        public void Update(Member member) => MemberDAO.Instance.Update(member);

    }
}
