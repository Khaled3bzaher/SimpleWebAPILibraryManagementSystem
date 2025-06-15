namespace SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs
{
    public class DisplayBookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string AutherName { get; set; }
        public DateOnly PublicationDate { get; set; }
    }
}
