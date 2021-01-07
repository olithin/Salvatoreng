namespace Server
{
    public class Query
    {
        public Book GetBook() => new Book {Title = "C# in De", Author = "Jon Skeet"};

    }
}