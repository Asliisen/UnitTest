using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenresDetails
{
    public class GetGenreDetailQuery
    {
        public int GenreId {get; set;}
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        private object value1;
        private object value2;

        public GetGenreDetailQuery (IBookStoreDbContext context, IMapper mapper, int id)
        {
            _context = context;
            _mapper =mapper;
        }

        public GetGenreDetailQuery(object value1, object value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }

        public GenreDetailViewModel   Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=> x.IsActive && x.Id == GenreId); 
            if (genre is null )
            throw new InvalidOperationException("not found the type of book!");
              return _mapper.Map<GenreDetailViewModel>(genre);
        }
        
    }

    public class GenreDetailViewModel
    {
        public int Id {get; set;}
        public string? Name {get; set;}
    }
}

