using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        
        [Fact]
        public void WhenGivenBookIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId=0;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not found for deleting!");
        }

        [Fact]
        public void WhenGivenBookIdinDB_Book_ShouldBeRemove()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId=1;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var book=_context.Books.SingleOrDefault(book=>book.Id == command.BookId);
            book.Should().Be(null);
        }
    }
}