using System;
using System.Collections.Generic;

namespace my_books.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; } // dateread opsiyonel olacak "?" işaretiyle, kitap olmazsa okuma süresi de olmaz.

        public int? Rate { get; set; }

        public string Genre { get; set; }

        public string CoverUrl { get; set; }     

        public DateTime DateAdded { get; set;}

        //Navigation Properties Book sadece bir tane Publishera(yayımevi) sahip olabilir.

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; } // bu işlemden sonra entity framework PublisherId Publisher modelinde primary key olduğunu bilecek. ama Book modelinde foreign key olacak !!!!
                                                 // yanlış anlamadıysam bu propu koymamızın sebebi Books tablo-modelinde her bir kitap için bir publisher id gerekli bu on sağlayacak.
          
        public List<Book_Author> Book_Authors { get; set;} //aynısını author modelinde de yaptık çünkü Book_Author iki model arasındaki Join Model.
                                                           //yani Book_Authors tablosunda Authors ve Books  adında iki sütun olacak ve bu sütunlar iki
                                                           //modelden gelen id ler ile eşlenecek. Book id1 Author id3
                                                           //                                     Book id1 Author id2 olacak mesela çünkü ilişki many to many.



    }
}
