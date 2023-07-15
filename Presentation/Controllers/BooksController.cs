﻿using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
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

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> AddBookAsync([FromBody] BookDtoForInsertion bookDto)
        {            
            var book = await _manager.BookService.CreateBookAsync(bookDto);
            return StatusCode(201, book);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] int id, [FromBody] BookDtoForUpdate bookDto)
        {            
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
