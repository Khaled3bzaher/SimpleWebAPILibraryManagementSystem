namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Models
{
    public class Category : BaseEntity
    {
        public ICollection<Book> Books { get; set; }
    }
}
