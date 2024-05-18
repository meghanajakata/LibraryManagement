using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryManagementContext _context;
        public UserRepository(LibraryManagementContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all users from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<UserInfo> GetAllUsers()
        {
            try
            {
                return _context.UserInfos.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception("Error when fetching the Users from database");
            }
        }

        /// <summary>
        /// Get the user with the given Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public UserInfo GetUserById(int id)
        {
            try
            {
                return _context.UserInfos.Where(x => x.Id == id).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// Get the user based on name and password 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public UserInfo GetUserByNameAndPassword(string userName, string password)
        {
            try
            {
                return _context.UserInfos.FirstOrDefault(u => u.Name == userName && u.Password == password);
            }
            catch(Exception ex)
            {
                throw new Exception("Error while fetching data from the database");
            }
        }

        /// <summary>
        /// Update the user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateUser(UserInfo user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when updating the user details of id " + user.Id + "from the database");
            }
        }

        /// <summary>
        /// Delete the user from the database
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteUser(UserInfo user)
        {
            try
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be added to database");
            }
        }

        /// <summary>
        /// Add the user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="Exception"></exception>
        public void AddUser(UserInfo user)
        {
            try
            {
                _context.Add(user);
                _context.SaveChanges();
            }
            catch (Exception Ex)
            {

                throw new Exception("Error while adding data to the database");
            }
        }

        
    }
}
