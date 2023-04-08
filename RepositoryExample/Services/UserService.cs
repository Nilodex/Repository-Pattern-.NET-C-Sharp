using RepositoryExample.Interfaces;
using RepositoryExample.Models;

namespace RepositoryExample.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }
        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
        }
        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }
    }
}
