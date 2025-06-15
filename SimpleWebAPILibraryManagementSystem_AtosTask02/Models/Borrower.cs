namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Models
{
    public class Borrower : BaseEntity
    {
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
