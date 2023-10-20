using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteAuthor
{

    // This class represents the command for deleting an author.
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int AuthorId { get; set; }

        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Method to handle the deletion of an author.
        public void Handle()
        {
             // Find the author in the database based on the provided author ID.
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Not found this Author!");

            var hasBooks = _dbContext.Books.Any(x => x.Id == AuthorId);
            if (hasBooks)
                throw new InvalidOperationException("Can not be deleted!");

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}