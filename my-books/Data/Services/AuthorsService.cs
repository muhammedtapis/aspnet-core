using my_books.Data.Models;
using my_books.Data.ViewModels;
using System.Linq;

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


        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(n => n.Id == authorId).Select(n => new AuthorWithBooksVM //burada seçilen idye sahip olan authoru yeni bi AuthorWithBooksVM nesneye atadık işlem yapmak için
            { 
                FullName = n.FullName,
                BookTitles = n.Book_Authors.Select(n=> n.Book.Title).ToList() //Soldaki Booktitles method tipi olan AuthorWithBooksVM nesnesinden geliyor atama yapıyoruz.
                                                                              //sağdaki ise seçilen id ye sahip olan nesneyi n e atamıştık title özelliğine erişiö Authors.cs taki Book_Author türündeki Book_Authors listesini istiyoruz.
                                                                              //Bu listede de birsürü BookAuthors var istediğimiz olan da "n" instance lı Book_Author nesnesinde tutuluyor
                                                                              //Book_Author nesnesnde Book nesnesi türünde kitaplar tutuluyor o kitaptan kitabın title erişim sağlıyoruz.
            }).FirstOrDefault();

            return _author;
        }
    }
}
