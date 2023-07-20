using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    //[ApiVersion("1.0")] 
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/books")]
    [ResponseCache(CacheProfileName = "5mins")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpHead]
        [HttpGet(Name ="GetAllBooksAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [ResponseCache(Duration = 60)]
        [Authorize(Roles ="User, Editor, Admin")]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters)
        {
            var linkParameters = new LinkParameters
            {
                BookParameters = bookParameters,
                HttpContext = HttpContext
            };
            var result = await _manager.BookService.GetAllBooksAsync(linkParameters, false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metadata));

            return result.linkResponse.HasLinks ?
                Ok(result.linkResponse.LinkedEntities) :  // 200
                Ok(result.linkResponse.ShapedEntities);  // 200
        }

        [Authorize]
        [HttpGet("details")] // api/books/details
        public async Task<IActionResult> GetAllBooksWithDetails()
        {
            return Ok(await _manager.BookService.GetAllBooksWithDetailsAsync(false));
        }

        [Authorize(Roles = "User, Editor, Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookAsync([FromRoute] int id)
        {                           
            var book = await _manager.BookService.GetBookByIdAsync(id, false);            
            return Ok(book); // 200
            
        }

        [Authorize(Roles = "Editor, Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost(Name ="AddBookAsync")]
        public async Task<IActionResult> AddBookAsync([FromBody] BookDtoForInsertion bookDto)
        {            
            var book = await _manager.BookService.CreateBookAsync(bookDto);
            return StatusCode(201, book);
        }

        [Authorize(Roles = "Editor, Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] int id, [FromBody] BookDtoForUpdate bookDto)
        {            
            await _manager.BookService.UpdateBookAsync(id, bookDto, false);
            return NoContent();  // 204          
        }

        [Authorize(Roles = "Editor, Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] int id)
        {
            await _manager.BookService.DeleteBookAsync(id, true);
            return NoContent(); // 204            
        }

        [Authorize(Roles = "Editor, Admin")]
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

        [Authorize(Roles = "Editor, Admin")]
        [HttpOptions]
        public IActionResult GetBookOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
