using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests :IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
        }


        [Fact]
        public void WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre(){Name="WhenAlreadyExistGenreTitleIsGivenInvalidOperationExceptionShouldBeReturn"};
            _context.Genres.Add(genre);
            _context.SaveChanges();
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model=new CreateGenreModel(){Name=genre.Name};
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("This book already exists.");
        }

        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model=new CreateGenreModel(){Name="WhenValidInputIsGivenGenreShouldBeCreated"};
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            var genre=_context.Genres.SingleOrDefault(genre=>genre.Name == command.Model.Name);
            genre.Should().NotBeNull();
        }
    }
}