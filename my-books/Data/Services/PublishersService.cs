using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Linq;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        //AppDBContext sınıfını inject ediyoruz çünkü veritabanına veri eklemek için entity framework core kullanıyoruz.
        //önce servisi oluşturuyorsun ardından controllerda kullanıyosun.

        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;

        }

        public void AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
                //sol taraftaki publisher modelden gelen name sağ taraftaki kullanıcıdan yani PublisherVM den gelen Name.
            };
            _context.Publishers.Add(_publisher);//oluşturulan _publisher instance Publishers içine ekle.
            _context.SaveChanges(); //değişiklikleri kaydet.
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()  //gelen id eşit olan publisheri VM atadık.
                {
                    Name = n.Name,  //sol taraf VM sağ taraf Publisher class idye sahip olanı vm ye eşitledik.
                    BookAuthors = n.Books.Select(n => new BookAuthorVM() 
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()  //bookauthors listeye atamayı unutma
                }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.id == id);

            if (_publisher != null) 
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }

            
        }
    }
}
