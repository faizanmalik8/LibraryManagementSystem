using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
    public class MemberRepository
    {
        private readonly string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        public void addmember(string name)
        {
            SqlConnection conn=new SqlConnection(connectionstring);
            string query = "Insert into Members(Name) values(@name)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue ("@name", name);
            conn.Open();
            int i=cmd.ExecuteNonQuery();
            if (i == 1)
            {
                Console.WriteLine("Member Added SuccessFully!");
            }
        }
        public List<Member> readallmembers()
        {
            List<Member> members = new List<Member>();
            SqlConnection conn = new SqlConnection(connectionstring);
            string query = "Select MemberId, Name from Members";
            SqlCommand cmd=new SqlCommand(query, conn);
            conn.Open ();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Member m1 = new Member
                {
                    MemberId = Convert.ToInt32(reader["MemberId"]),
                    Name = Convert.ToString(reader["Name"]),
                };
                members.Add(m1);
            }
            return members;
        }
    }
}
