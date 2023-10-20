using System;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        // Model containing data for creating a new author.
        public CreateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        private readonly IMapper _mapper;


        // Constructor to initialize the command with dependencies.
        public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // Method to handle the creation of a new author.
        public void Handle()
        {
             // Check if an author with the same first and last name already exists.                  
            var author = _dbContext.Authors.SingleOrDefault(
                x =>
                    x.FName.ToLower() == Model.FirstName.ToLower()
                    && x.LName.ToLower() == Model.LastName.ToLower()
            );

            // If an author with the same name exists, throw an exception.
            if (author is not null)
                throw new InvalidOperationException("Already exists Author!");

              // If the author does not exist, create a new Author object and add it to the database.
            author = new Author();
            author.FName = Model.FirstName;
            author.LName = Model.LastName;
            author.BirthDate = Model.BirthDate;
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }
    }


    // Model class representing the data required to create a new author.
    public class CreateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}