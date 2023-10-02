using System;
using System.Collections.Generic;

namespace my_books.Data.ViewModels
{
    //Bu viewmodeli sadece kullanıcıdan isteyeceğimiz veya kullanıcını güncelleyeceği bilgiler için kullanacağız o yüzden oluşturduk.
    //Bookservice sınıfında kullanacağız.
    public class BookVM
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; } // dateread opsiyonel olacak "?" işaretiyle, kitap olmazsa okuma süresi de olmaz.

        public int? Rate { get; set; }

        public string Genre { get; set; }

        public string CoverUrl { get; set; }


        //Publisher, Author modelleri eklendikten sonraki yapılan güncelleme.!!!!!

        public int PublisherId { get; set; }  //Book sadece bir publishera sahip olabilir o yüzden sadece Publisher Id

        public List<int>  AuthorIds { get; set; }  // Book birden fazl authora sahip olabilir , bu sebeple author Id lerini listede tutuyoruz.
    }
}
