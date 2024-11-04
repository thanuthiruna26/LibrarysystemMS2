public class BookBorrowingResponseDto
{
    public int Id { get; set; }
    public string BookId { get; set; }
    public string MemberNic { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime? ReturnedDate { get; set; }
}