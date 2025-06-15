namespace SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs
{
    public class DisplayBorrowedBook
    {
        public string BookName { get; set; }
        public string BorrowerName { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
