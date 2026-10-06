using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
    public class Member
    {
        public int MemberId {  get; set; }
        public string Name { get; set; }
        public Member() { }
        public Member(int id, string name)
        {
            this.MemberId = id;
            this.Name = name;
        }
        public void show()
        {
            Console.WriteLine($"MemberId: {this.MemberId}, Name: {this.Name}");
        }
    }
}
