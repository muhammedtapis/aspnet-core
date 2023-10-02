namespace my_books.Data.Models
{
    public class Book_Author
    {
        public int Id { get; set; }

        //Navigation Properties

        //Book ve Author arasındaki join model olduğu için iki modelin de Id si ve modeli getirildi.
        public int BookId { get; set; }  
        public Book Book { get; set; }


        public int AuthorId { get; set; }
        public Author Author { get; set; }


    }
}
