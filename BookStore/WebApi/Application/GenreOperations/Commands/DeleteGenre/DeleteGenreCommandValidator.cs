using System.Data;
using FluentValidation;

namespace WebApi.Application.GenreOperations.DeleteGenre
{
    public class DeleteGenreCommandValidator: AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
        }

        internal void ValidationAndThrow(DeleteGenreCommand command)
        {
            throw new NotImplementedException();
        }
    }
}