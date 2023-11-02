using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books.Data.Paging
{
    public class PaginatedList<T> : List<T> //generic class oluşturduk ki başka yerlerde de kullanabilelim.
    {
        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items,int count,int pageIndex,int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count/(double)pageSize); //bu işlem toplam sayfa sayısını hesaplıyor sayfadaki veri sayısı pageSize sayfada göstermek istediğin veri sayısı

            this.AddRange(items);      //bu işlem verileri listenin sonuna ekler.
        }

        public bool HasPreviousPage //kullanıcı eğer ilk sayfada ise önceki sayfayı görmemesi gerek çünkü öyle bir sayfa yok bunun için oluşturduk.
        {  
            get 
            {
                return PageIndex > 1;
            }
                
        }

        public bool HasNextPage  //kullanıcı eğer son sayfada ise sonraki sayfayı görmemesi gerek çünkü öyle bir sayfa yok bunun için oluşturduk.
        {
            get
            {
                return PageIndex < TotalPages;
            }
        }

        public static PaginatedList<T> Create(IQueryable<T> source,int pageIndex,int pageSize)
        {
            var count = source.Count(); //burdaki source sorgudan gelecek veri count u oluyor.
            var items = source.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList(); //açıklaması : diyelim ki 12 itemin var pagesize ise 5 yani sayfada gösterilcek veri sayısı
                                                                                     // sen de 2. sayfayı görmek istiyosun (2-1)*5 = 5 yani ilk 5 itemi skip edip sonraki 5 itemi gösteriyor.

            return new PaginatedList<T>(items, count, pageIndex, pageSize); 
        }
    }
}
