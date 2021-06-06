using System;

namespace LibraryConsoleApp.Models
{
    public class Book
    {
        public string Name { get; init; }
        public string Author { get; init; }
        public string Category { get; init; }
        public string Language { get; init; }
        public DateTime PublicationDate { get; init; }
        public string ISBN { get; init; }

        public bool OnLoan = false;

        public string OnLoanToWhom { get; set; }
        public DateTime OnLoanUntil { get; set; }

        public Book(string name, string author, string category, string language, DateTime publicationDate, string isbn)
        {
            Name = name;
            Author = author;
            Category = category;
            Language = language;
            PublicationDate = publicationDate;
            ISBN = isbn;
        }
    }
}
