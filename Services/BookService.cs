using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<BookDto> _shaper;

        public BookService(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<BookDto> shaper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _shaper = shaper;
        }

        public async Task<BookDto> CreateBookAsync(BookDtoForInsertion bookDto)
        {
            var entity = _mapper.Map<Book>(bookDto);
            _manager.BookRepository.Create(entity);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteBookAsync(int id, bool trackChanges)
        {
            var entity = await GetBookAndCheckExistenceAsync(id, trackChanges);
            _manager.BookRepository.DeleteBook(entity);
            await _manager.SaveAsync();
        }

        public async Task<(IEnumerable<ExpandoObject> books, MetaData metadata)> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
            if(!bookParameters.ValidPriceRange)
            {
                throw new PriceOutOfRangeBadRequestException();
            }
            var booksWithMetaData = await _manager.BookRepository.GetAllBooksAsync(bookParameters, trackChanges);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);

            var shapedData = _shaper.ShapeData(booksDto, bookParameters.Fields);
            return (books: shapedData, metadata: booksWithMetaData.MetaData);
        }

        public async Task<BookDto> GetBookByIdAsync(int id, bool trackChanges)
        {
            var book = await GetBookAndCheckExistenceAsync(id, trackChanges);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetBookAndCheckExistenceAsync(id, trackChanges);
            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);

            return (bookDtoForUpdate, book);
        }

        public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate, book);
            await _manager.SaveAsync();
        }

        public async Task UpdateBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            var entity = await GetBookAndCheckExistenceAsync(id, trackChanges);
            if (bookDto is null) { throw new ArgumentNullException(nameof(bookDto)); }
            entity = _mapper.Map<Book>(bookDto);
            _manager.BookRepository.Update(entity);
            await _manager.SaveAsync();
        }

        private async Task<Book> GetBookAndCheckExistenceAsync(int id, bool trackChanges)
        {
            var entity = await _manager.BookRepository.GetBookAsync(id, trackChanges);

            if(entity is null)
            {
                throw new BookNotFoundException(id);
            }
            return entity;
        }
    }
}
