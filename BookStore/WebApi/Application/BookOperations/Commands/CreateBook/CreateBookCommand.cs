using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.BookOperations.CreateBook
{

    // Command class responsible for creating a new book in the database
    public class CreateBookCommand
    {
        public CreateBookModel Model {get; set;}
        private readonly IBookStoreDbContext dbContext;

        private readonly IMapper _mapper;
        public CreateBookCommand(IBookStoreDbContext _dbContext, IMapper mapper)
        {
            dbContext=_dbContext;
            _mapper = mapper;
        }
        
         // Handles the creation of a new book
        public void Handle()
        {
            var book =  dbContext.Books.SingleOrDefault(x=> x.Title == Model.Title);

            if (book is not null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut");
            }
            book =  _mapper.Map<Book>(Model);  // new Book();
           // book.Title=Model.Title;
           // book.PublishDate=Model.PublishDate;
            //book.PageCount=Model.PageCount;
           // book.GenreId=Model.GenreId;
            
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}