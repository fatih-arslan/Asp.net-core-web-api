﻿using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
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
                .FilterBooks(bookParameters.MinPrice, bookParameters.MaxPrice)
                .OrderBy(b => b.Id)
                .ToListAsync();

            return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
        }            

        public async Task<Book> GetBookAsync(int id, bool trackChanges) => await GetByCondition(b => b.Id == id, trackChanges).SingleOrDefaultAsync();

        public void UpdateBook(Book book) => Update(book);
        
    }
}
