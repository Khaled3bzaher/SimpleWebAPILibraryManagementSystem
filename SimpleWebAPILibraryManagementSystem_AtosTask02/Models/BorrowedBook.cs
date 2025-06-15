namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Models
{
    public class BorrowedBook
    {
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public Book Book { get; set; }
        public Borrower Borrower { get; set; }
    }
}
