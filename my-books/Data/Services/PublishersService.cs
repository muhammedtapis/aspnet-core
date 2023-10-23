using my_books.Data.Models;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

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

        public Publisher AddPublisher(PublisherVM publisher)  // return type  publisher olarak değiştik çünkü HTTP endpointte status değişti
                                                              // OK değil Created oldu sebebi ise bir kanyak oluşturduk publisher ekleyerek.
        {
            if(StringStartWithNumber(publisher.Name)) //hata kontrolü servis sınıfında yapıldı publisher.name alındı kontrol methoduna parametre olarak verildi.
            {
                throw new PublisherNameException("name starts with number",publisher.Name); // string kontrolü sırasında hata varsa exceptionu burada throw ediyoruz.
                                                                                            //daha sonra controllerda catch edeceğiz.
            }
            var _publisher = new Publisher()
            {
                Name = publisher.Name
                //sol taraftaki publisher modelden gelen name sağ taraftaki kullanıcıdan yani PublisherVM den gelen Name.
            };
            _context.Publishers.Add(_publisher);//oluşturulan _publisher instance Publishers içine ekle.
            _context.SaveChanges(); //değişiklikleri kaydet.

            return _publisher;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n=> n.id == id);
        // finding publisher method 
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
            else
            {
                throw new Exception($"The publisher with id :{id} does not exist. ");
            }

            
        }

        private bool StringStartWithNumber(string name) // bu methodu AddPublisher methodunda string kontrolü için kullancaz
        {
            return (Regex.IsMatch(name, @"^\d")); // parametreden gelen string değeri sayı ile başlıyor mu onu kontrol edip bool değer dönüo d = digit.
        }
    }
}
