using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;


namespace WebApi.Application.GenreOperations.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model {get; set;}
        private readonly IBookStoreDbContext _context;
        private object value;

        public CreateGenreCommand(IBookStoreDbContext context, AutoMapper.IMapper _mapper, CreateGenreModel newGenre)
        {
            _context = context;
        }

        public CreateGenreCommand(object value)
        {
            this.value = value;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=> x.Name == Model.Name);
            if(genre is not null)
            {
                throw new InvalidOperationException("already exist.");
            }

            genre = new Genre();
            genre.Name= Model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();
            
        }
    }

    public class CreateGenreModel
    {
        public string? Name {get; set;}
    }
}