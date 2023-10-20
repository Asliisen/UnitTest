using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenresDetails;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.Application.GenreOperations.UpdateGenre;
using WebApi.Application.GenreOperations.DeleteGenre;



namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase 
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController (IBookStoreDbContext context, IMapper mapper)
        { 
            _context = context;
            _mapper = mapper;
        }



         [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var obj = query.Handle();

            return Ok(obj);
        }


        [HttpGet("{id}")]
        public ActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper, id);
            var validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }


	    [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper, newGenre);
            var validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
        {
        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.GenreId = id;
		UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
		validator.ValidateAndThrow(command);
		
		command.Handle();
		
		return Ok();
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;
            var validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

    }
}