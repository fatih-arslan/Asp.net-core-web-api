using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
            
        }

        public void CreateBook(Book book) => Create(book);

        public void DeleteBook(Book book) => Delete(book);

        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {            
            var books = await GetAll(trackChanges)
                .FilterBooks(bookParameters.MinPrice, bookParameters.MaxPrice) // extension method
                .Search(bookParameters.SearchTerm) // extension method               
                .Sort(bookParameters.OrderBy) // extension method
                .ToListAsync();

            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }

        public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
        {
            return await GetAll(trackChanges)
                .OrderBy(b => b.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges)
        {
            return await _context.Books.Include(b => b.Category).OrderBy(b => b.Id).ToListAsync();
        }

        public async Task<Book> GetBookAsync(int id, bool trackChanges) => await GetByCondition(b => b.Id == id, trackChanges).SingleOrDefaultAsync();

        public void UpdateBook(Book book) => Update(book);
        
    }
}
