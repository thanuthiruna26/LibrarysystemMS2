
namespace ProductAPI.Models
{
    public class Book
    {
       
        public string BookName { get; set; }
        public int ISBNNumber { get; set; }
        public string Publisher { get; set; }
        public int NoOfCopies { get; set; }
        public string ImageURL { get; set; }
        public string Genre { get; set; }

        internal static void Add(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
