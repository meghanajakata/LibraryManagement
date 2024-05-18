using LibraryManagement.Models;

namespace LibraryManagement.Repository
{
    public interface IBookRepository
    {
        public List<Book> GetAllBooks();
        public Book GetBookById(int id);
        public void UpdateBook(Book book);
        public void DeleteBook(Book book);
        public void AddBook(Book book);

    }
}
