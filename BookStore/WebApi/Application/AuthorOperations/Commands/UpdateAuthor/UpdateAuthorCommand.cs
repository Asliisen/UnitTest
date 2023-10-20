using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthor
{
        // This class represents the command for updating an existing author.
    public class UpdateAuthorCommand
    {

        // Identifier of the author to be updated and the model containing updated data.
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // Method to handle the update of an author.
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Not found this Author!");

            if (
                _dbContext.Authors.Any(
                    x =>
                        x.FName.ToLower() == Model.FirstName.ToLower()
                        && x.LName.ToLower() == Model.LastName.ToLower()
                        && x.Id != AuthorId
                )
            )
                throw new InvalidOperationException("Already exists with same name!");

            author.FName = Model.FirstName;
            author.LName = Model.LastName;
            author.BirthDate = Model.BirthDate;
            _dbContext.SaveChanges();
        }
    }


    // Model class representing the data required to update an author.
    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}