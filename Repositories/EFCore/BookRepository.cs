using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {
            
        }

        public void CreateBook(Book book) => Create(book);

        public void DeleteBook(Book book) => Delete(book);

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) => await GetAll(trackChanges).OrderBy(b => b.Id).ToListAsync();

        public async Task<Book> GetBookAsync(int id, bool trackChanges) => await GetByCondition(b => b.Id == id, trackChanges).SingleOrDefaultAsync();

        public void UpdateBook(Book book) => Update(book);
        
    }
}
