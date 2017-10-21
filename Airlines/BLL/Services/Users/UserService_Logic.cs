using System;
using System.Linq;
using System.Net;
using BLL.Infrastructure.AuthProvider;
using BLL.Services.ServiceBase;
using Contracts.DomainEntities.Users;
using DAL;

namespace BLL.Services
{
    public partial class UserService 
    {
        /// <summary>
        /// Search for the current user by his login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public User GetUserByLogin(string login)
        {
            return _unitOfWork.UsersRepository.GetAll().FirstOrDefault(u => u.Login.Equals(login));
        }


        /// <summary>
        /// Try to authenticate user by login and password
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Authenticate(string login, string password)
        {
            FormsAuthProvider authProvider = new FormsAuthProvider();
            return authProvider.Authenticate(login, password); 
        }

        /// <summary>
        /// Deauthentiate current user
        /// </summary>
        public void Logout()
        {
            FormsAuthProvider authProvider = new FormsAuthProvider();
            authProvider.Deauthenticate();
        }
    }
}
