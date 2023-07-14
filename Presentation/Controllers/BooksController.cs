using Entities.DataTransferObjects;
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
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _manager.BookService.GetAllBooksAsync(false);
            return Ok(books); // 200                        
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookAsync([FromRoute] int id)
        {                           
            var book = await _manager.BookService.GetBookByIdAsync(id, false);            
            return Ok(book); // 200
            
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync([FromBody] BookDtoForInsertion bookDto)
        {
            if (bookDto is null)
            {
                return BadRequest(); // 400
            }
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState); // 422
            }
            var book = await _manager.BookService.CreateBookAsync(bookDto);
            return StatusCode(201, book);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            if (bookDto is null)
            {
                return BadRequest(); // 400
            }                
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState); // 422
            }
            await _manager.BookService.UpdateBookAsync(id, bookDto, false);
            return NoContent();  // 204          
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] int id)
        {
            await _manager.BookService.DeleteBookAsync(id, true);
            return NoContent(); // 204            
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateBookAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
            if(bookPatch is null)
            {
                return BadRequest(); // 400
            }
            var result = await _manager.BookService.GetBookForPatchAsync(id, false);   
            
            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState); // 422
            }

            await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);

            return NoContent(); // 204
            
        }
    }
}
