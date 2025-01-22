using BookstoreAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookstoreAPI.Data
{
    public class BookRepository
    {
        private readonly List<Book> _books;

        public BookRepository()
        {
            _books = new List<Book>
            {
                new Book { Id = 1, Stock = 10, Title = "Book 1", Author = "Author 1", Price = 9.99m },
                new Book { Id = 2, Stock = 5, Title = "Book 2", Author = "Author 2", Price = 19.99m },
                new Book { Id = 3, Stock = 2, Title = "Book 3", Author = "Author 3", Price = 29.99m }
            };
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(book => book.Id == id);
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Price = book.Price;
                existingBook.Stock = book.Stock;
            }
        }

        public void DeleteBook(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }
    }
}