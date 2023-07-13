using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public BookService(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Book CreateBook(Book book)
        {
            _manager.BookRepository.Create(book);
            _manager.Save();
            return book;
        }

        public void DeleteBook(int id, bool trackChanges)
        {
            var entity = _manager.BookRepository.GetBook(id, trackChanges);
            if(entity is null)
            {
                throw new BookNotFoundException(id);
            }
            _manager.BookRepository.DeleteBook(entity);
            _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _manager.BookRepository.GetAllBooks(trackChanges);
        }

        public Book GetBookById(int id, bool trackChanges)
        {
            var book = _manager.BookRepository.GetBook(id, trackChanges);
            if (book == null)
            {
                throw new BookNotFoundException(id);
            }
            return book;
        }

        public void UpdateBook(int id, Book book, bool trackChanges)
        {
            var entity = _manager.BookRepository.GetBook(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }
            if (book is null) { throw new ArgumentNullException(nameof(book)); }
            entity.Title = book.Title;
            entity.Price = book.Price;
            _manager.BookRepository.Update(entity);
            _manager.Save();
        }
    }
}
