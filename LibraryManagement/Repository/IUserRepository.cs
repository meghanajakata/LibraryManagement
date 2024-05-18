using LibraryManagement.Models;

namespace LibraryManagement.Repository
{
    public interface IUserRepository
    {
        public List<UserInfo> GetAllUsers();
        public UserInfo GetUserById(int id);
        public UserInfo GetUserByNameAndPassword(string userName, string password);
        public void UpdateUser(UserInfo user);
        public void DeleteUser(UserInfo user);
        public void AddUser(UserInfo user);
    }
}
