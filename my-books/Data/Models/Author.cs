using System.Collections.Generic;

namespace my_books.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        
        //Navigation properties Book ve Author arasındaki ilişki Many to Many olduğu için Join Model eklememiz GEREKLİ !!!!

        //Burada yapacağımız işlemin sebebi Book tablosu ile Author tablosu arasındaki ilişki Book_Author tablosunda depolanacak.
        //Book_Author navigation properties olarak eklenecek.

        public  List<Book_Author> Book_Authors { get; set; } // List tipinde olmasının sebebi artık Book modeli artık  book authors diye bir listeye sahip.
    }
}
