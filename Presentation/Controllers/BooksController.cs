using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books); // 200                        
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute] int id)
        {                           
            var book = _manager.BookService.GetBookById(id, false);            
            return Ok(book); // 200
            
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            if (newBook is null)
            {
                return BadRequest(); // 400
            }
            _manager.BookService.CreateBook(newBook);
            return StatusCode(201, newBook);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] Book newBook)
        {
            if (newBook is null)
            {
                return BadRequest(); // 400
            }                
            if (id != newBook.Id)
            {
                return BadRequest(); // 400
            }
            _manager.BookService.UpdateBook(id, newBook, true);
            return NoContent();            
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            _manager.BookService.DeleteBook(id, true);
            return NoContent(); // 204            
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var book = _manager.BookService.GetBookById(id, true);            
            bookPatch.ApplyTo(book);
            _manager.BookService.UpdateBook(id, book, true);
            return NoContent(); // 204
            
        }
    }
}
