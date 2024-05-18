using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using LibraryManagement.Repository;

namespace LibraryManagement.Controllers
{
    [Authorize]
    [Route("api/book")]
    [ApiController]
    public class BooksController : Controller
    {

        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {

            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<List<Book>> Get()
        {
            var books = await Task.FromResult(_bookRepository.GetAllBooks());
            return books;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await Task.FromResult(_bookRepository.GetBookById(id));
            if (book != null)
                return book;
            else
                return NotFound("The resource does not exist");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            _bookRepository.AddBook(book);
            return await Task.FromResult(book);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            _bookRepository.UpdateBook(book);
            return await Task.FromResult(book);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> Delete(int id)
        {

            Book? book = _bookRepository.GetBookById(id);

            if (book != null)
            {
                _bookRepository.DeleteBook(book);
                return book;
            }
            else
            {
                return NotFound("Book does not exist");
            }

        }


    }
}
