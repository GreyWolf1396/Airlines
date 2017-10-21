using System.Linq;
using System.Web.Security;
using DAL;

namespace BLL.Infrastructure.AuthProvider
{
    class FormsAuthProvider:IAuthProvider
    {
        public bool Authenticate(string login, string password)
        {
            var user =
                new UnitOfWork().UsersRepository.GetAll()
                    .FirstOrDefault(
                        u => u.Login.ToLower().Equals(login.ToLower()) && 
                        u.Password == password.GetHashCode());
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
