using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository
{
    /// <summary>
    /// Represents CRUD operations on Book Model
    /// </summary>
    public class BookRepository : IBookRepository
    {
        private readonly LibraryManagementContext _context;
        public BookRepository(LibraryManagementContext context) 
        {
            _context = context;
        }

        /// <summary>
        /// Adds the book to the database
        /// </summary>
        /// <param name="book"></param>
        public void AddBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Error while adding data to the database");
            }
        }

        /// <summary>
        /// Deletes the book from the database
        /// </summary>
        /// <param name="book"></param>
        public void DeleteBook(Book book)
        {
            try
            {
                book.IsDeleted = 1;
                _context.Entry(book).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("Error while deleting the user from the database");
            }
            
        }

        /// <summary>
        /// Retrieves the book from the database with the given ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Book GetBookById(int id)
        {
            try
            {
                return _context.Books.Where(x => x.Id == id && x.IsDeleted == 0).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw new Exception("Error when fetching the book details of id "+ id + "from the database");
            }
        }

        /// <summary>
        /// Get all book details from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Book> GetAllBooks()
        {
            try
            {
                return _context.Books.Where(x => x.IsDeleted == 0).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception("Error when fetching the book details from database");
            }
        }

        /// <summary>
        /// Update Book details to the database
        /// </summary>
        /// <param name="book"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateBook(Book book)
        {
            try
            {
                _context.Entry(book).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when updating the book details of id " + book.Id + "from the database");
            }
        }


    }
}
