using System.Collections.Generic;

namespace my_books.Data.Models
{

    //burada belirleyeceğimiz özellikler publisher tablosundaki sütunları temsil edecek.
    public class Publisher
    {
        public int id { get; set; }
        public string Name { get; set; }

        //Navigation Properties ilişkileri tanımlamak için olan özellikler
        //ilişki One To Many olacak Kitap sadece Bir publishera sahip olabilir ama publisher bir sürü kitaba sahip olabilir.
        public List<Book> Books { get; set; } //Yanlış anlamadıysam publisherların Kitapları için eklendi bu liste. Mesela Id = 2 olan publisherin kitaplarını listelemek için bu ilişki.
    }
}
