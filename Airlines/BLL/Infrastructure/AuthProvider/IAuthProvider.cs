using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.AuthProvider
{
    /// <summary>
    /// Interface for authentication user
    /// </summary>
    interface IAuthProvider
    {
        bool Authenticate(string login, string password);

        void Deauthenticate();
    }
}
