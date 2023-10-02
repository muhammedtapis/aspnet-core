using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace my_books.Data.Services
{
    public class BooksService
    {
        //sadece bir method kullanacağız database e books eklemek için.
        //database e books eklemek için appdbcontext e referans alman gerekiyor.ÖNEMLİ!! çünkü SQL ve c# arasındaki ilişkiyi o kuruyor.
        //Servis sınıfında AppDbContext sınıfından gelen List(Books listesi) üzerinden işlem yapıyoruz ekleme çıkarma silme gösterme.
        //Bu tarz methodların hepsi Servis methodunun içinde yazılır ve Controller sınıfından çağrılıp kullanılır.
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
                
        }

        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,//book zaten okunmuş mu onu kontrol ettik önce eğer okunmamışsa null olcak.
                Rate = book.IsRead ? book.Rate : null, //aynı şekilde puanlama yapabilmek için okunmuş olması gerek, eğer okunnmamışsa 0 verilcek.
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,  
                PublisherId = book.PublisherId,   //sol taraftakiler modelden geliyo sağdakiler view modelden SWAGGER dan.
      
            };
            _context.Books.Add(_book);//oluşturulan _book instance Books içine ekle.
            _context.SaveChanges(); //değişiklikleri kaydet.

            //database book eklendikten sonra BookAuthors tablosunda Book ve BookAuthors arasındaki ilişkiyi oluşturmamız gerekiyor.

            foreach (var id in book.AuthorIds)
            {
                //Bookauthor oluştur.

                var _book_author = new Book_Author()
                {
                    BookId = _book.Id, //burdaki _book.id kitap eklendikten sonra oluşan id.!!!!
                    AuthorId = id, //sağdaki author id ViewModelden geliyor. sanırım Authorları listeleyip seçince onun idsini alıyor.

                };
                _context.Books_Authors.Add(_book_author);
                _context.SaveChanges();
            }

        }

        //bu servisi kullanabilmek için startup.cs dosyasında konfigüre etmemiz gerek.


     // aynı işi yapıyor aşağıdaki kodla   public List<Book> GetAllBooks() => _context.Books.ToList();
        public List <Book> GetAllBooks() {
        
            return _context.Books.ToList(); //AppDbContext içinden gelen Books liste olarak döndürüldü.

        }

        public Book GetBookById(int bookId) {

            return _context.Books.FirstOrDefault(n => n.Id == bookId); //FirstOrDefault methodundaki n.id bookId parametresine eşitledik.
            //firstorDEfault methodu entity frameworkten geliyor.
        }

        public Book UpdateBookById(int bookId, BookVM book) { //BookVM olmasının sebebi sadece kullanıcının değişebeliceği özellikleri
                                                               //değiştirmek istiyoruz id değiştiremeyiz mesela.             
             var _book = _context.Books.FirstOrDefault(n => n.Id == bookId); // id ye sahip olan kitap databasede var mı onu kontrol ettik.

            if(_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;//book zaten okunmuş mu onu kontrol ettik önce eğer okunmamışsa null olcak.
                _book.Rate = book.IsRead ? book.Rate : null; //aynı şekilde puanlama yapabilmek için okunmuş olması gerek, eğer okunnmamışsa 0 verilcek.
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
            }

            return _book;
        }

        public void DeleteBookById(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId); //bu kitap db de var mı yok mu onu dönüyor true ya da false.
            if( _book != null)
            {
                _context.Books.Remove( _book );
                _context.SaveChanges();
            }
        }
    }
}
