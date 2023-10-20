using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Entities;
using WebApi.DBOperations;
using WebApi.Applications.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Applications.AuthorOperations.Queries.GetAuthor;



namespace WebApi.Controllers
{
    // This attribute indicates that this class is a controller that responds to HTTP requests.
    [ApiController]
    [Route("api/[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

    // Constructor to initialize the controller with the required dependencies.
        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    // HTTP GET method to retrieve all authors.
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query =new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

    // HTTP GET method to retrieve a specific author by ID.
        [HttpGet("{id}")]
        public IActionResult GetAuthor(int id)
        {
            AuthorDetailViewModel result;
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;

             // Validation of the query parameters using a custom validator.
            GetAuthorDetailQueryValidator validator= new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

    // HTTP POST method to add a new author.
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateAuthorModel newBook)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newBook;

            // Validation of the command parameters using a custom validator.
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

    // HTTP PUT method to update an existing author by ID.
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = id;
            command.Model = updatedAuthor;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

    // HTTP DELETE method to delete an author by ID.
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command =new DeleteAuthorCommand(_context);
            command.AuthorId = id;

            // Validation of the command parameters using a custom validator.
            DeleteAuthorCommandValidator validator= new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}