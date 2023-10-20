using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand      // Command class responsible for updating a book in the database
    {
        private readonly IBookStoreDbContext _context;
        public int BookId {get; set;}
        public UpdateBookModel Model {get; set;}
        public UpdateBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()         // Handles the update operation for the specified book
        {
            var book =_context.Books.SingleOrDefault(x=> x.Id==BookId);
            if(book is null)
            {
                throw new InvalidOperationException("No books to update were found.");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title=Model.Title != default ? Model.Title : book.Title;
            _context.SaveChanges();
        }
    }

    // Model class representing the data needed for updating a book
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}