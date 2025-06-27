using UserOrderSystem.Interfaces;
using UserOrderSystem.Models;

namespace UserOrderSystem.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User? GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }
    }
}
