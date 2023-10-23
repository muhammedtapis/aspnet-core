using System;

namespace my_books.Exceptions
{
    public class PublisherNameException :Exception  //exception classı oluşturabilmen için parenttan inherit alman gerek.
    {
        public string PublisherName { get; set; }

        public PublisherNameException()
        {
                
        }

        public PublisherNameException(string message):base(message)
        {
                
        }

        public PublisherNameException(string message,Exception inner):base(message, inner) 
        {
                
        }

        public PublisherNameException(string message ,string publisherName):this(message)
        {
            PublisherName = publisherName;  
        }
    }
}
