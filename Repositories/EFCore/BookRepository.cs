﻿using Entities.Models;
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

        public IQueryable<Book> GetAllBooks(bool trackChanges) => GetAll(trackChanges).OrderBy(b => b.Id);

        public Book GetBook(int id, bool trackChanges) => GetByCondition(b => b.Id == id, trackChanges).SingleOrDefault();

        public void UpdateBook(Book book) => Update(book);
        
    }
}
