using System.Text.RegularExpressions;
using UserOrderSystem.Interfaces;

namespace UserOrderSystem.Services
{
    public class AuthService
    {
        private readonly IUserRepository _repository;

        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            var user = await _repository.GetUserByEmailAsync(email);
            return user != null;
        }

        public bool IsValidPassword(string password)
        {
            if (password.Length < 8) return false;
            if (!Regex.IsMatch(password, "[A-Z]")) return false;
            if (!Regex.IsMatch(password, "[0-9]")) return false;

            return true;
        }
    }
}
