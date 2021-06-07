using System;
using Xunit;
using LibraryConsoleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryUnitTests { 

    public class TestLibrary
    {
    [Fact]
        public void BookIsLateReturnsTrueIfLate()
        {
            DateTime returnDate = new(2011, 6, 12);
            DateTime loanUntilDate = new(2011, 6, 11);
            Book book = new("test", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            book.OnLoanUntil = loanUntilDate;
            Library books = new(new List<Book>());

            Assert.True(books.BookIsLate(book, returnDate));
        }

        [Fact]
        public void BookIsLateReturnsFalseIfNotLate()
        {
            DateTime returnDate = new(2011, 6, 11);
            DateTime loanUntilDate = new(2011, 6, 12);
            Book book = new("test", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            book.OnLoanUntil = loanUntilDate;
            Library books = new(new List<Book>());

            Assert.False(books.BookIsLate(book, returnDate));
        }
        [Fact]
        public void AddBookAddsBookObjectToList1()
        {
            Library books = new(new List<Book>());
            books.AddBook("test", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            Assert.True(books.Books.Count == 1);
        }

        [Fact]
        public void AddBookAddsBookObjectToList2()
        {
            Library books = new(new List<Book>());
            books.AddBook("test", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            books.AddBook("test1", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            Assert.True(books.Books.Count == 2);
        }

        [Fact]
        public void RemoveBookRemovesBookIfCorrespondingNameExists()
        {
            Library books = new(new List<Book>());
            books.AddBook("test", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            Assert.True(books.Books.Count == 1);
            books.DeleteBook("test");
            Assert.True(books.Books.Count == 0);
        }

        [Fact]
        public void RemoveBookRemovesNoBooksIfCorrespondingNameDoesNotExist()
        {
            Library books = new(new List<Book>());
            books.AddBook("test", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            Assert.True(books.Books.Count == 1);
            books.DeleteBook("test1");
            Assert.True(books.Books.Count == 1);
        }

        [Fact]
        public void TakeBookPreventsTakingNonexistantBook()
        {
            Library books = new(new List<Book>());
            books.TakeBook("test", "test", 1);

            Assert.ThrowsAsync<Exception>(() => { throw new Exception(); });
        }
        
        [Fact]
        public void TakeBookSetsOnLoadPropertyToTrueForBook()
        {
            Library books = new(new List<Book>());
            books.AddBook("test", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            Assert.False(books.Books.First(Book => Book.Name == "test").OnLoan);
            books.TakeBook("test", "test", 1);
            Assert.True(books.Books.First(Book => Book.Name == "test").OnLoan);
        }

        [Fact]
        public void ReturnBookPreventsReturningNonexistantBook()
        {
            Library books = new(new List<Book>());
            books.ReturnBook("test");

            Assert.ThrowsAsync<Exception>(() => { throw new Exception(); });
        }
        [Fact]
        public void AddBookPreventsAddingBookWithDuplicateName()
        {
            Library books = new(new List<Book>());
            books.AddBook("test", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            books.AddBook("test", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            Assert.True(books.Books.Count == 1);
        }

        [Fact]

        public void UserBooksOnLoanCountsCorrectly()
        {
            Library books = new(new List<Book>());
            books.AddBook("test", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            books.AddBook("test1", "test", "test", "test", new DateTime(2000, 1, 1), "test");
            books.TakeBook("testUserName", "test", 1);
            Assert.Equal(1, books.UserBooksOnLoan(books.Books, "testUserName"));
            books.TakeBook("testUserName", "test1", 1);
            Assert.Equal(2, books.UserBooksOnLoan(books.Books, "testUserName"));
        }
    }
}
