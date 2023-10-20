using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;



namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase 
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController (IBookStoreDbContext context, IMapper mapper)
        { 
            _context = context;
            _mapper = mapper;
        }

// Endpoint used to fetch books
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

// Endpoint used to fetch details of a particular book
        [HttpGet("{id}")] 
            public IActionResult GetById (int id)
            {
                BookDetailViewModel result;
                    GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                    query.BookId = id;
                    GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                    validator.ValidateAndThrow(query);
                    result = query.Handle();
                    return Ok(result);
            }

// Endpoint used to add a new book
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel model)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
          
                command.Model=model;

                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                FluentValidation.Results.ValidationResult result = validator.Validate(command);
                validator.ValidateAndThrow(command);
                  command.Handle();

                // if(!result.IsValid)
                // foreach(var item in result.Errors)
                // {
                //     Console.WriteLine("Özellik: "+ item.PropertyName + "error message: " + item.ErrorMessage);
                // }    
                // else{
                //      command.Handle();
                // }            
         
            return Ok();
        }


// Endpoint used to update a book
        [HttpPut("{id}")]
            public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId=id;
                command.Model=updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);                
                command.Handle();
                return Ok();
                
            }

// Endpoint used to delete a book
        [HttpDelete("{id}")]
            public IActionResult DeleteBook(int id)
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId=id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok();
            }
    }
}