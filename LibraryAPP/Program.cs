using System;
using System.IO;
using Microsoft.Data.SqlClient;
using LibraryData;
class Program
{
    static void Main()
    {
        while(true)
        {
            Console.WriteLine("1. Add a New Book");
            Console.WriteLine("2. View All Books");
            Console.WriteLine("3. Update Book Stock");
            Console.WriteLine("4. Delete a Book");
            Console.WriteLine("5. Add a New Member");
            Console.WriteLine("6. View All Members");
            Console.WriteLine("7. Issue a Book");
            Console.WriteLine("8. Return a Book");
            Console.WriteLine("9. Backup Books to JSON");
            Console.WriteLine("10. Restore Books from JSON");
            Console.WriteLine("0. Exit");
            Console.WriteLine("========================================");
            Console.Write("Enter your choice: ");
            int choice=int.Parse(Console.ReadLine());
            BookRepository b1 = new BookRepository();
            MemberRepository member = new MemberRepository();
            BackupHelper backup=new BackupHelper();
            switch (choice)
            {
                
                case 1:
                    Console.WriteLine("Enter Title:");
                    string title=Console.ReadLine();
                    Console.WriteLine("Enter Author:");
                    string author = Console.ReadLine();
                    Console.WriteLine("Enter Stock:");
                    int stock=int.Parse(Console.ReadLine());
                    Book bk1=new Book(title,author,stock);
                    b1.AddBook(bk1);

                    break;
                case 2:
                    List<Book> list = new List<Book>();
                    list = b1.readallbooks();
                    foreach (Book b in list)
                    {
                        b.showBook();
                    }
                    
                    break;
                case 3:
                    Console.WriteLine("Enter Book ID:");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Stock:");
                    int stock1 = int.Parse(Console.ReadLine());
                    b1.updatestock(id, stock1);
                    break;
                case 4:
                    Console.WriteLine("Enter Book ID to Delete:");
                    int id1 = int.Parse(Console.ReadLine());
                    b1.deletebook(id1);
                    break;
                case 5:
                    
                    Console.WriteLine("Enter Name of Member:");
                    string name = Console.ReadLine();
                    member.addmember(name);
                    break;
                case 6:
                    List<Member> list1=new List<Member>();
                    list1=member.readallmembers();
                    foreach (Member m in list1)
                    {
                        m.show();
                    }
                    break;
                case 7:
                    Console.WriteLine("Enter Book ID:");
                    int id2 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Member ID:");
                    int mid = int.Parse(Console.ReadLine());
                    bool flag=b1.issuebook(id2, mid);
                    if (flag == false)
                    {
                        Console.WriteLine("Unsuccessfull!");
                    }
                    else
                    {
                        Console.WriteLine("Book Issued Successfully!");
                    }
                    break;
                case 8:
                    Console.WriteLine("Enter Book Issue ID:");
                    int iid = int.Parse(Console.ReadLine());
                    bool flag2=b1.returnbook(iid);
                    if (flag2 == false)
                    {
                        Console.WriteLine("Unsuccessfull!");
                    }
                    else
                    {
                        Console.WriteLine("Book Returned Successfully!");
                    }
                    break;
                case 9:
                    List<Book> bklist= new List<Book>();
                    bklist=b1.readallbooks();
                    backup.converttojson(bklist, "backup.txt");
                    Console.WriteLine("Backup Created Successfully!");
                    break;
                case 10:
                    List<Book> bklist1= new List<Book>();
                    bklist1 = backup.Readfromjson("backup.txt");
                    foreach (Book bk in bklist1)
                    {
                        Console.WriteLine($"Title: {bk.Title}, Stock: {bk.Stock}");
                    }
                    break;
                case 0:
                    return;
                    break;








            }
        }
        
        Book book = new Book("Rich Dad Poor Dad","Faizan MAlik",1);
        BookRepository repository = new BookRepository();
        
    
        //repository.AddBook(book);
        //repository.updatestock(2, 4);
        //repository.deletebook(2);
        list=repository.readallbooks();
        
        /*foreach(Book b in list)
        {
            b.showBook();
        }*/
       // Member m1 = new Member(1, "FaizanMalik");
       // List<Member> memberslist = new List<Member>();
        MemberRepository mrepo= new MemberRepository();
        //mrepo.addmember("Faizan Malik");
        // memberslist=mrepo.readallmembers();
        // foreach(Member m in memberslist)
        //{
        //  m.show();
        //}
        /* bool check=repository.issuebook(1002, 1);
         if(check==false)
         {
             Console.WriteLine("Unsuccessfull!");
         }*/
        /*bool check1 = repository.returnbook(4);
        if (check1== false)
        {
            Console.WriteLine("Unsuccessfull!");
        }*/
        
    }

}