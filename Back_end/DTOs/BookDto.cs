namespace ProductAPI.DTOs
{
    public class BookDTO
    {
        public string BookName { get; set; }
        public int ISBN { get; set; }
        public string Publisher { get; set; }
        public int NoOfCopies { get; set; }
        public string ImageURL { get; set; }
        public string Genre { get; set; }
    }
}