using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryConsoleApp.Models
{
    public class Library
    {
        public List<Book> Books { get; set; }

        public Library(List<Book> books) => Books = books;
        public void AddBook(string name, string author, string category, string language, DateTime publicationDate, string isbn)
        {
            if (Books.Where(Book => Book.Name == name).ToList().Count == 0)
            {
                Books.Add(new Book(name, author, category, language, publicationDate, isbn));
                Console.WriteLine("Book added to library");
            }
            else
            {
                Console.WriteLine("A book with that name has already been added");
            }
        }

        public void DeleteBook(string name)
        {
            Books.RemoveAll(Book => Book.Name == name);
        }

        public void PrintBooks(int choice) {
            Console.Clear();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Filter by what author:");
                    var author = Console.ReadLine();
                    PrintFunction(Books.Where(Book => Book.Author == author).ToList());
                    break;
                case 2:
                    Console.WriteLine("Filter by what category:");
                    var category = Console.ReadLine();
                    PrintFunction(Books.Where(Book => Book.Category == category).ToList());
                    break;
                case 3:
                    Console.WriteLine("Filter by what language:");
                    var language = Console.ReadLine();
                    PrintFunction(Books.Where(Book => Book.Language == language).ToList());
                    break;
                case 4:
                    Console.WriteLine("Filter by what isbn:");
                    var isbn = Console.ReadLine();
                    PrintFunction(Books.Where(Book => Book.ISBN == isbn).ToList());
                    break;
                case 5:
                    Console.WriteLine("Filter by what book name:");
                    var name = Console.ReadLine();
                    PrintFunction(Books.Where(Book => Book.Name == name).ToList());
                    break;
                case 6:
                    Console.WriteLine("Showing taken books:");
                    PrintFunction(Books.Where(Book => Book.OnLoan == true).ToList());
                    break;
                case 7:
                    Console.WriteLine("Showing available books:");
                    PrintFunction(Books.Where(Book => Book.OnLoan == false).ToList());
                    break;
                case 8:
                    PrintFunction(Books);
                    break;
            }
        }

        public void TakeBook(string userName, string bookName, int loanPeriod)
        {
        
            int currently_on_loan = 0;
            int how_many_loaned = Books.Where(Book => Book.OnLoanToWhom == userName).ToList().Count;

            foreach (var book in Books)
            {
                if(book.OnLoanToWhom == userName)
                {
                    currently_on_loan++;
                }
            }

            Console.WriteLine("current");
            Console.WriteLine(currently_on_loan);
            Console.WriteLine(how_many_loaned);
            if (currently_on_loan >= 3) {
                Console.WriteLine("User has already 3 or more books on loan");
                return;
            }

            if (loanPeriod > 2)
            {
                Console.WriteLine("Specified loan period is too long");
                return;
            }

            try
            {
                var book = Books.First(Book => Book.Name == bookName);

                if (book.OnLoan)
                {
                    Console.WriteLine("Book is already on loan");
                    return;
                }

                book.OnLoanToWhom = userName;
                book.OnLoanUntil = DateTime.Today.AddDays(loanPeriod*30); // month == 30 days
                book.OnLoan = true;
            }
            catch
            {
                Console.WriteLine("Book not found");
                return;
            }
            Console.WriteLine("Book taken");
        }

        public void ReturnBook(string bookName)
        {
            try
            {
                var bookSet = Books.First(Book => Book.Name == bookName);
                bookSet.OnLoan = false;
                bookSet.OnLoanToWhom = null;
            }
            catch
            {
                Console.WriteLine("Book not found");
                return;
            }
            var book = Books.First(Book => Book.Name == bookName);

            if(BookIsLate(book, DateTime.Now))
            {
                Console.WriteLine("Late return!");
            }
        }

        private static void PrintFunction(List<Book> bookList)
        {
            Console.WriteLine("Books in library:");
            Console.WriteLine("=======================");
            foreach (var book in bookList)
            {
                Console.WriteLine("Name: {0}", book.Name);
                Console.WriteLine("Author: {0}", book.Author);
                Console.WriteLine("Category: {0}", book.Category);
                Console.WriteLine("Language: {0}", book.Language);
                Console.WriteLine("Publication Date: {0}", book.PublicationDate);
                Console.WriteLine("ISBN: {0}", book.ISBN);
                Console.WriteLine("-----------------------");
            }
            Console.WriteLine("=======================");
        }

         public bool BookIsLate(Book book, DateTime date)
        {
            return book.OnLoanUntil < date;
        }
    }
}