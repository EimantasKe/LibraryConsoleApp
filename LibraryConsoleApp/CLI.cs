using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LibraryConsoleApp.Models;

namespace LibraryConsoleApp
{
    interface IFunction
    {
        string Description { get; }
        void Run(Library books);
    }

    class AddBook : IFunction
    {
        public string Description => "Add a book";
        public void Run(Library books)
        {
            Console.WriteLine("Enter book name:");
            string BookName = Console.ReadLine();

            Console.WriteLine("Enter author name:");
            string Author = Console.ReadLine();

            Console.WriteLine("Enter category:");
            string Category = Console.ReadLine();

            Console.WriteLine("Enter language:");
            string Language = Console.ReadLine();

            Console.WriteLine("Enter publication date:");
            string dateInput = Console.ReadLine();
            try
            {
                var parsedDateTest = DateTime.Parse(dateInput);
            }
            catch
            {
                Console.Write("Bad date format");
                return;
            }
            var parsedDate = DateTime.Parse(dateInput);
            Console.WriteLine();

            Console.WriteLine("Enter ISBN:");
            string ISBN = Console.ReadLine();

            books.AddBook(BookName, Author, Category, Language, parsedDate, ISBN);
        }
    }

    class TakeBook : IFunction
    {
        public string Description => "Take a book";
        public void Run(Library books)
        {
            Console.Clear();
            Console.WriteLine("Enter username");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter name of book");
            string bookName = Console.ReadLine();

            Console.WriteLine("Length of loan period in months:");
            string loanPeriod = Console.ReadLine();
            var parsedLoanPeriod = int.Parse(loanPeriod);

            books.TakeBook(userName, bookName, parsedLoanPeriod);
           
        }
    }

    class ReturnBook : IFunction
    {
        public string Description => "Return a book";
        public void Run(Library books)
        {
            Console.Clear();

            Console.WriteLine("Enter name of book");
            string bookName = Console.ReadLine();

            books.ReturnBook(bookName);
            Console.WriteLine("Book returned");
        }
    }

    // add filter
    class ListBooks : IFunction
    {
        public string Description => "List books in library";
        public void Run(Library books)
        {
            var filters = new string[]
           {
                "Author",
                "Category",
                "Language",
                "ISBN",
                "Book name",
                "Taken books",
                "Available books",
                "No filter"
           };

            Console.WriteLine("Filters:");
            for (int i = 0; i < filters.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, filters[i]);
            }

            int filterIndex;
            string choice;
            do
            {
                choice = Console.ReadLine();
            }
            while (!int.TryParse(choice, out filterIndex) || filterIndex > filters.Length);  
            books.PrintBooks(filterIndex);
        }
    }
    class DeleteBook : IFunction
    {
        public string Description => "Delete a book";
        public void Run(Library books)
        {
            Console.Clear();
            Console.WriteLine("Input exact name of book to delete");
            string Name = Console.ReadLine();
            books.DeleteBook(Name);
         }
    }

    class Exit : IFunction
    {
        public string Description => "Exit application";
        public void Run(Library books)
        {
            var json = JsonSerializer.Serialize(books.Books);
            File.WriteAllText("library.json", json);
            Environment.Exit(0);
        }
    }

    public class CLI 
    {
        public void Start()
        {
            var books = new Library(new List<Book>());

            if (File.Exists("library.json"))
            {
                var json = File.ReadAllText("library.json");
                var bookList = JsonSerializer.Deserialize<List<Book>>(json);
                books = new Library(bookList);
            }
            
            this.Main(books);
        }

        public void Main(Library books)
        {
            var commands = new IFunction[]
            {
                new AddBook(),
                new TakeBook(),
                new ReturnBook(),
                new ListBooks(),
                new DeleteBook(),
                new Exit()
            };
            
            while (true)
            {

                Console.WriteLine("Commands:");
                for (int i = 0; i < commands.Length; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, commands[i].Description);
                }

                int commandIndex;
                string choice;
                do
                {
                    choice = Console.ReadLine();
                }
                while (!int.TryParse(choice, out commandIndex) || commandIndex > commands.Length);

                commands[commandIndex - 1].Run(books);
                Console.WriteLine("*******************");
            }
        }
    }
}
