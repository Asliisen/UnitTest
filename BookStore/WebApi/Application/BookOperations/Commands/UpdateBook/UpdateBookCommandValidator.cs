
using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
    // Validation class containing the validation rules of the UpdateBookCommand class
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {

        public UpdateBookCommandValidator(){
            RuleFor(command => command.BookId).GreaterThan(0);  // Book ID (BookId) must have a positive value
            RuleFor(command => command.Model.GenreId).GreaterThan(0);  // Book type (GenreId) must have a positive value
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);  //Book title (Title) should not be empty and must be at least 4 characters long

        }
    }
    
    
}