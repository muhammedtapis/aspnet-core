using my_books.Data.Models;
using my_books.Data.ViewModels;

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
    }
}
