using System.Data;
using FluentValidation;

namespace WebApi.BookOperations.GetBookDetail
{
     // Validation class containing rules for the GetBookDetailQuery class
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query=> query.BookId).GreaterThan(0);  // BookId in the query must be greater than 0

            
        }
    }
}