using my_books.Data.Models;
using my_books.Data.ViewModels;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        //AppDBContext sınıfını inject ediyoruz çünkü veritabanına veri eklemek için entity framework core kullanıyoruz.

        private AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;

        }

        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
                //sol taraftaki modelden gelen fullname sağ taraftaki kullanıcıdan yani AuthorVM den gelen FullName.
            };
            _context.Authors.Add(_author);//oluşturulan _author instance Authors içine ekle.
            _context.SaveChanges(); //değişiklikleri kaydet.
        }
    }
}
