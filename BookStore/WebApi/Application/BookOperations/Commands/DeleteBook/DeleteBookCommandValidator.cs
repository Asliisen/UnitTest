using System;
using System.Linq;
using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    // Validation class containing the validation rules of the DeleteBookCommand class
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            // Book ID (BookId) must have a positive value
            RuleFor(command => command.BookId).GreaterThan(0);
            
        }
    }

}