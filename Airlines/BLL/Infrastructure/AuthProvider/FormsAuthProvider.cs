using System.Linq;
using System.Web.Security;
using DAL;

namespace BLL.Infrastructure.AuthProvider
{
    class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string login, string password)
        {
            var users =
                new UnitOfWork().UsersRepository.GetAll().ToList();

            var encPass = password.GetHashCode();

            var user = users.FirstOrDefault(
                u => u.Login.ToLower().Equals(login.ToLower()) &&
                u.Password == encPass);
            if (user == null)
            {
                return false;
            }
            FormsAuthentication.SetAuthCookie(login, false);
            return true;
        }

        public void Deauthenticate()
        {
            FormsAuthentication.SignOut();
        }
    }
}
