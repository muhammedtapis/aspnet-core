using my_books.Data.Models;
using System;

namespace my_books.Data.ViewModels
{
    public class CustomActionResultVM
    {

        public Exception Exception { get; set; }
      //  public object Data { get; set; } //genel olarak dönmek istediğin data olunca böyle 

        public Publisher Publisher {  get; set; } //Spesifik return type.
    }
}

