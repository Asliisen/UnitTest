using System.Data;
using FluentValidation;

namespace WebApi.Application.GenreOperations.Queries.GetGenresDetails
{
    public class GetGenreDetailQueryValidator: AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }

    }
}