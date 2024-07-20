using BusinessObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    internal class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new MemberDAO();
                    return instance;
                }
            }
        }

        public IEnumerable<Member> GetList()
        {
            List<Member> members;
            try
            {
                var db = new SaleManagmentContext();
                members = db.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return members;
        }

        public Member GetByEmail(string email)
        {
            Member member;
            try
            {
                var db = new SaleManagmentContext();
                member = db.Members.FirstOrDefault(m => m.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return member;
        }

        public Member VerifyMember(Member member)
        {
            Member mb;
            try
            {
                var db = new SaleManagmentContext();
                mb = db.Members.FirstOrDefault(m => m.Email == member.Email && m.Password == member.Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return mb;
        }

        public bool IsAdmin(Member member)
        {
            try
            {
                var config = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json").Build();

                var defaultAdminAccount = config.GetSection("DefaultAdminAccount");

                if (member.Email == defaultAdminAccount["Email"] && member.Password == defaultAdminAccount["Password"])
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteById(int Id)
        {
            try
            {
                var db = new SaleManagmentContext();
                Member member = new() { MemberId = Id };
                db.Members.Remove(member);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Add(Member member)
        {
            try
            {
                var db = new SaleManagmentContext();

                db.Members.Add(member);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Member member)
        {
            try
            {
                var db = new SaleManagmentContext();

                db.Members.Update(member);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
