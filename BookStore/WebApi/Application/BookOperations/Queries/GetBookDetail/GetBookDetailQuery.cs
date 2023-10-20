using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId {get; set;}
        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
           _mapper = mapper;
        }



        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x=> x.Genre).Where(book=> book.Id == BookId).SingleOrDefault();
            if(book is null)
            {
                throw new InvalidOperationException("Kitap mevcut değil.");
            }
            BookDetailViewModel vm =  _mapper.Map<BookDetailViewModel>(book);  //new BookDetailViewModel();
            //vm.Title=book.Title;
            //vm.PageCount=book.PageCount;
            //vm.Genre=((GenreEnum)book.GenreId).ToString();
            //vm.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyy");
          
            return vm;
        }
    }




    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}