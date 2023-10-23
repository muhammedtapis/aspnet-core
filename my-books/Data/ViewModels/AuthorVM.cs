using System.Collections.Generic;

namespace my_books.Data.ViewModels
{
    public class AuthorVM
    {
        public string FullName { get; set; }
    }
    //muhammet mensur
    public class AuthorWithBooksVM
    {
        public string FullName { get; set;}
        public List<string> BookTitles { get; set; }
    }
}
