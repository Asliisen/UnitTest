using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.CreateBook;
using Xunit;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lean Startup",0,0)]
        [InlineData("Lean Startup",0,1)]
        [InlineData("Lean Startup",100,0)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("",0,1)]
        [InlineData("Lea",100,1)]
        [InlineData("Lean",100,0)]
        [InlineData("Lean",0,1)]
        [InlineData(" ",100,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int pagecount, int genreid) 
        {
            
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model=new CreateBookModel()
            {
                Title=title,
                PageCount=pagecount,
                PublishDate=System.DateTime.Now.Date.AddYears(-1),
                GenreId=genreid
            };
            //act
            CreateBookCommandValidator  validations= new CreateBookCommandValidator();
            var result = validations.Validate(command);
   
            result.Errors.Count.Should().BeGreaterThan(0);   
        }

        [Fact] 
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError() 
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model=new CreateBookModel()
            {
                Title="Lean Startup",
                PageCount=100,
                PublishDate=System.DateTime.Now.Date, //
                GenreId=1
            };

             CreateBookCommandValidator  validations= new CreateBookCommandValidator();
            var result = validations.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError() 
        {
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model=new CreateBookModel()
            {
                Title="Lean Startup",
                PageCount=100,
                PublishDate=System.DateTime.Now.Date.AddYears(-2),
                GenreId=1
            };

            CreateBookCommandValidator  validations= new CreateBookCommandValidator();
            var result = validations.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}