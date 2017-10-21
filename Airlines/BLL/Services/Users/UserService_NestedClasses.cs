using BLL.Services.ServiceBase;
using Contracts.DomainEntities.Users;
using DAL;

namespace BLL.Services
{
    public partial class UserService
    {
        public class UserMicroMicroService : MicroServiceBase<User>
        {
            public UserMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.UsersRepository)
            { }
        }
        public class RolesMicroMicroService : MicroServiceBase<Role>
        {
            public RolesMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.RolesRepository)
            { }
        }
        public class RequestMicroMicroService : MicroServiceBase<UserRequest>
        {
            public RequestMicroMicroService(UnitOfWork unitOfWork) : base(unitOfWork.RequestsRepository)
            { }
        }
    }
}
