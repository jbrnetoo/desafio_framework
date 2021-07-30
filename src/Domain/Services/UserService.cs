using Domain.Interfaces;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        public bool CheckUser(string username, string password)
        {
            return username.Equals("JbFramework") && password.Equals("1234");
        }
    }
}
