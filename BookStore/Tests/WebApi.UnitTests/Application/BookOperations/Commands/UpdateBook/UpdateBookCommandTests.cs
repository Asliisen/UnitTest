using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenBookIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId=0;

            
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not found fot updating!");

        }

        [Fact]
        public void WhenGivenBookIdinDB_Book_ShouldBeUpdate()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            UpdateBookModel model = new UpdateBookModel(){Title="WhenGivenBookIdinDBBookShouldBeUpdate", GenreId=1};            
            command.Model = model;
            command.BookId=1;
            
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            var book=_context.Books.SingleOrDefault(book=>book.Id == command.BookId);
            book.Should().NotBeNull();
            
        }
    }
}