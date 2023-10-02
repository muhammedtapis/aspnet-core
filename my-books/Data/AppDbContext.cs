
using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;

namespace my_books.Data
{
    //c# sınıfları ve sql db arasındaki köprü sınıf
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {                                                //BookAuthor Book ve Author arasındaki Join Table-Model!!!
            modelBuilder.Entity<Book_Author>()           //BookAuthorla Book arasındaki ilişkiyi belirliyoruz.
                .HasOne(b => b.Book)                     //bookAuthor tek book sahip.
                .WithMany(ba => ba.Book_Authors)         //Book birkaç tane  book_Authors sahip olabilir.
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Book_Author>()           //BookAuthorla Author arasındaki ilişkiyi belirliyoruz.
                .HasOne(b => b.Author)                   //bookAuthor tek Author sahip.
                .WithMany(ba => ba.Book_Authors)         //Book birkaç tane  book_Authors sahip olabilir.
                .HasForeignKey(bi => bi.AuthorId);       //Foreign key belirle.

        }
        //c# modelleri için tablo isimlerini belirle.
        public DbSet<Book> Books { get; set; } // "Books" kullanarak db get set edebileceğiz. Dbset Book türünde olacağı için. tek bu kod yeterli.
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book_Author> Books_Authors { get; set; } //Join table-model
        public DbSet<Publisher> Publishers { get; set; } // bu işlemlerden sonra migration ekleyince Publishers,Authors,Books_Authors adında tablolar oluşturulacak.
    }
}
