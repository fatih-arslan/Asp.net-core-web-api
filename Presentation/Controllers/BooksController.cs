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
            try
            {
                var books = _manager.BookService.GetAllBooks(false);
                return Ok(books); // 200
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute] int id)
        {
            try
            {
                var book = _manager.BookService.GetBookById(id, false);
                if (book == null)
                {
                    return NotFound(); // 404
                }
                return Ok(book); // 200
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            try
            {
                if (newBook is null)
                {
                    return BadRequest(); // 400
                }
                _manager.BookService.CreateBook(newBook);
                return StatusCode(201, newBook);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] Book newBook)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook([FromRoute] int id)
        {
            try
            {
                _manager.BookService.DeleteBook(id, true);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                var book = _manager.BookService.GetBookById(id, true);
                if (book == null)
                {
                    return NotFound(); // 404
                }
                bookPatch.ApplyTo(book);
                _manager.BookService.UpdateBook(id, book, true);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
