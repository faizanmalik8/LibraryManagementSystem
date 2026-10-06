using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author {  get; set; }
        public int Stock {  get; set; }

        public Book() { }
        public Book(string title, string author, int Stock)
        {
            this.Title = title;
            this.Author = author;
            this.Stock = Stock;
        }
        public void showBook()
        {
            Console.WriteLine($"BookId: {this.BookId}, Title: {this.Title}, Author: {this.Author}, Stock: {this.Stock}");
        }


    }
}
