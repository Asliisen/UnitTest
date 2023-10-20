using System;
using FluentValidation;

namespace WebApi.BookOperations.CreateBook
{
// Validation class containing the validation rules of the CreateBookCommand class
public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
   public CreateBookCommandValidator()
   {
     RuleFor(command => command.Model.GenreId).GreaterThan(0);
     RuleFor(command=> command.Model.PageCount).GreaterThan(0);
     RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
      RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
   }

// A method that is not yet implemented and is used when a validation error occurs
// Implementation of this method allows a different action to be taken when a validation error occurs
        internal void ValidationThrow(CreateBookCommand command)
        {
            throw new NotImplementedException();
        }
    }

}