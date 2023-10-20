using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Common;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }


                context.Genres.AddRange(
				new Genre
				{
					//Id = 1,
					Name = "Personal Growth"
				},
				new Genre
				{
					//Id = 2,
					Name = "Science Fiction"
				},
				new Genre
				{
					//Id = 3,
					Name = "Philosophy"
				}
                );


                context.Books.AddRange(
                    new Book{
                        //Id=1,
                        Title="Lean Startup",
                        GenreId=1, 
                        PageCount=200,
                        PublishDate= new DateTime(2001,06,12)
                        },
                    new Book{
                        //Id=2,
                        Title="Herland",
                        GenreId=2,
                        PageCount=250,
                        PublishDate= new DateTime(2002,05,19)
                    },
                    new Book{
                        //Id=3,
                        Title="Dune",
                        GenreId=2, 
                        PageCount=540,
                        PublishDate= new DateTime(2002,12,21)
                    }
                );
                

                    context.Authors.AddRange(
                new Author
                {
                    FName = "Asli",
                    LName = "Sen",
                    BirthDate = new DateTime(1999, 02, 15)
                },
                new Author
                {
                    FName = "Basak",
                    LName = "Oz",
                    BirthDate = new DateTime(1990, 10, 23)
                },
                new Author
                {
                    FName = "Fatma",
                    LName = "Ozturk",
                    BirthDate = new DateTime(2002, 12, 21)
                }
            );

                context.SaveChanges();
            }
        }
    }
}