using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace LibraryData
{
    public class BookRepository
    {
        private readonly string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        public void AddBook(Book b1)
        {
            SqlConnection conn=new SqlConnection(connectionstring);
            string query = "Insert into Books(Title,Author,Stock) values(@title, @author, @stock)";
            SqlCommand cmd=new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@title", b1.Title);
            cmd.Parameters.AddWithValue("@author", b1.Author);
            cmd.Parameters.AddWithValue("@stock", b1.Stock);
            conn.Open();
            int i=cmd.ExecuteNonQuery();
            if (i == 1)
            {
                System.Console.WriteLine("Written Successfully");
            }
        }
        public List<Book> readallbooks()
        {
            List<Book> book=new List<Book>();
            SqlConnection con=new SqlConnection(connectionstring);
            string query = "Select BookId, Title, Author, Stock from books";
            SqlCommand cmd=new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader=cmd.ExecuteReader();
            while (reader.Read())
            {
                Book bk = new Book
                {
                    BookId = Convert.ToInt32(reader["BookId"]),
                    Title = Convert.ToString(reader["Title"]),
                    Author = Convert.ToString(reader["Author"]),
                    Stock = Convert.ToInt32(reader["Stock"]),
                };
                book.Add(bk);

            }
            return book;
        }
        public void updatestock(int id, int stock)
        {
            SqlConnection con=new SqlConnection(connectionstring);
            string query = "Update Books set Stock=@stock where BookId=@id";
            SqlCommand cmd=new SqlCommand(query, con);
            cmd.Parameters.AddWithValue ("@id", id);
            cmd.Parameters.AddWithValue("@stock", stock);
            con.Open ();
            int i=cmd.ExecuteNonQuery();
            if (i == 1)
            {
                Console.WriteLine("Updated Successfully!");
            }
        }
        public void deletebook(int id)
        {
           SqlConnection conn=new SqlConnection(connectionstring);
            string query = "Delete from Books where BookId=@id";
            SqlCommand cmd= new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open ();
            int i=cmd.ExecuteNonQuery ();
            if (i == 1)
            {
                Console.WriteLine("Deleted Successfully");
            }
        }
        public bool issuebook(int bkid, int mbid)
        {
            SqlConnection conn=new SqlConnection (connectionstring);
            SqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string checkstock = "select Stock from Books where BookId=@bkid";
                SqlCommand checkcmd = new SqlCommand(checkstock, conn, trans);
                checkcmd.Parameters.AddWithValue("@bkid", bkid);
                object result = checkcmd.ExecuteScalar();
                if(result==null || Convert.ToInt32(result) <= 0)
                {
                    trans.Rollback();
                    return false;
                }
                //Issue Query
                string issuequery = "Insert into IssuedBooks(BookId, MemberId, IssueDate, ReturnDate) values(@bkid,@mbid,@issuedate,Null)";
                SqlCommand issuecmd = new SqlCommand(issuequery, conn, trans);
                issuecmd.Parameters.AddWithValue("@bkid", bkid);
                issuecmd.Parameters.AddWithValue("@mbid", mbid);
                issuecmd.Parameters.AddWithValue("@issuedate", DateTime.Now);
                issuecmd.ExecuteNonQuery();
                //Update Query
                string updatequery = "Update Books set Stock=Stock-1 where BookId=@bkid";
                SqlCommand updatecmd = new SqlCommand(updatequery, conn, trans);
                updatecmd.Parameters.AddWithValue("@bkid", bkid);
                updatecmd.ExecuteNonQuery();

                trans.Commit();
                return true;
            }
            catch(Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                    
                }
                return false;
            }
            finally
            {
                // Explicitly close the connection when using is not used
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        public bool returnbook(int issueid)
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            SqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string checkissued = "Select BookId from IssuedBooks where IssueId=@issueid AND ReturnDate IS NULL";
                SqlCommand checkcmd = new SqlCommand(checkissued, conn, trans);
                checkcmd.Parameters.AddWithValue("@issueid", issueid);
                object result = checkcmd.ExecuteScalar();
                if (result == null)
                {
                    trans.Rollback();
                    return false;
                }
                int bookid = Convert.ToInt32(result);
                string returnquery = "Update IssuedBooks set ReturnDate=@date where IssueId=@issueid";
                SqlCommand returncmd = new SqlCommand(returnquery, conn, trans);
                returncmd.Parameters.AddWithValue("@issueid", issueid);
                returncmd.Parameters.AddWithValue("@date", DateTime.Now);
                returncmd.ExecuteNonQuery();
                //UpdateBooksTable
                string updatebook = "update Books set Stock=Stock+1 where BookId=@bookid";
                SqlCommand updatecmd = new SqlCommand(updatebook, conn, trans);
                updatecmd.Parameters.AddWithValue("@bookid", bookid);
                updatecmd.ExecuteNonQuery();

                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return false;
            }
            finally
            {
                // Explicitly close the connection when using is not used
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

    }
}