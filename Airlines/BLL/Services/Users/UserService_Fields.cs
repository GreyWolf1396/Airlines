using BLL.Services.ServiceBase;
using DAL;

namespace BLL.Services
{
    /// <summary>
    /// Service for work with entities: Users, Roles, Requests
    /// </summary>
    public partial class UserService 
    {
        private readonly BllUnit _bllUnit;
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork, BllUnit bllUnit)
        {
            _bllUnit = bllUnit;
            _unitOfWork = unitOfWork;
        }

        private UserMicroMicroService _users;
        private RolesMicroMicroService _roles;
        private RequestMicroMicroService _requests;

        public UserMicroMicroService Users 
            => _users ?? (_users = new UserMicroMicroService(_unitOfWork));
        public RolesMicroMicroService Roles 
            => _roles ?? (_roles = new RolesMicroMicroService(_unitOfWork));
        public RequestMicroMicroService Requests 
            => _requests ?? (_requests = new RequestMicroMicroService(_unitOfWork));
    }
}
