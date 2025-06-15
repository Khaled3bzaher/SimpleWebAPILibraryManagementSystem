namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Models
{
    public class Author : BaseEntity
    {
        public DateOnly? BirthDate { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
