using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BLL;
using BLL.Services.Users;
using Contracts.DomainEntities.Users;
using Contracts.Enums;
using Grey_Airlines.Models.UserModels;

namespace Grey_Airlines.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private const int  AdminRoleId=1;
        private readonly BllUnit _bllUnit;
        private readonly UserService _service;

        public UserController()
        {
            _bllUnit = new BllUnit();
            _service = _bllUnit.UserService;
        }
        // GET: User
        [Authorize(Roles = "System administrator")]
        public ActionResult IndexUser()
        {
            var model = new List<UserModel>();
            foreach (var user in _service.Users.GetAll())
            {
                model.Add(Mapper.Map<User,UserModel>(user));
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "System administrator")]
        public ActionResult CreateUser()
        {
            var administrators = _service.Users.GetAll()
                    .Where(u => u.Role.Id == AdminRoleId)
                    .Select(u => new UserModel()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
            administrators.Add(new UserModel()
            {
                Id = -1,
                Name = "Without administrator"
            });
            ViewBag.Administrators = new SelectList(administrators, "Id", "Name");
            ViewBag.Roles = new SelectList(
                _service.Roles.GetAll()
                    .Select(u => new RoleModel()
                    {
                        Id = u.Id,
                        Title = u.Title
                    }), "Id", "Title");
            return View(new CreateUserModel());
        }
        [HttpPost]
        [Authorize(Roles = "System administrator")]
        public ActionResult CreateUser(CreateUserModel user)
        {
            if (!user.Password.Equals(user.ConfirmPassword))
            {
                ModelState.AddModelError("Password","Password and password confirmation are not the same");
            }
            if (user.ChiefId == -1 && user.RoleId == 2)
            {
                ModelState.AddModelError("ChiefId","Dispatcher must have a chief administrator. Change role of new user, or set an administrator");
            }
            if (!ModelState.IsValid)
            {
                var administrators = _service.Users.GetAll()
                    .Where(u => u.Role.Id == AdminRoleId)
                    .Select(u => new UserModel()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList();
                administrators.Add(new UserModel()
                {
                    Id = -1,
                    Name = "Without administrator"
                });
                ViewBag.Administrators = new SelectList(administrators, "Id", "Name");
                ViewBag.Roles = new SelectList(
                    _service.Roles.GetAll()
                        .Select(u => new RoleModel()
                        {
                            Id = u.Id,
                            Title = u.Title
                        }), "Id", "Title");
                return View(user);
            }
            var chiefAdmin = user.ChiefId == -1 || user.RoleId == 1 ?
                null : _service.Users.GetById(user.ChiefId);
            var role = _service.Roles.GetById(user.RoleId);
            var entity = new User()
            {
                Name = user.Name,
                Login = user.Login,
                Password = user.Password.GetHashCode(),
                ChiefAdmin = chiefAdmin,
                Role = role
            };
            _service.Users.Insert(entity);
            return RedirectToAction("IndexUser");
        }

        [HttpGet]
        [Authorize(Roles = "System administrator")]
        public ActionResult EditUser(int id)
        {
            var administrators = _service.Users.GetAll()
                   .Where(u => u.Role.Id == AdminRoleId)
                   .Select(u => new UserModel()
                   {
                       Id = u.Id,
                       Name = u.Name
                   }).ToList();
            administrators.Add(new UserModel()
            {
                Id = -1,
                Name = "Without administrator"
            });
            ViewBag.Administrators = new SelectList(administrators, "Id", "Name");
            ViewBag.Roles = new SelectList(
                _service.Roles.GetAll()
                    .Select(u => new RoleModel()
                    {
                        Id = u.Id,
                        Title = u.Title
                    }), "Id", "Title");
            var model = Mapper.Map<User, UserModel>(_service.Users.GetById(id));
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "System administrator")]
        public ActionResult EditUser(UserModel user)
        {
            var entity = _service.Users.GetById(user.Id);

            entity.Name = user.Name;
            entity.ChiefAdmin = _service.Users.GetById(user.ChiefAdminId);
            entity.Login = user.Login;
            entity.Role = _service.Roles.GetById(user.RoleId);

            _service.Users.Update(entity);
            _bllUnit.Save();
            return RedirectToAction("IndexUser");
        }

        [Authorize(Roles = "System administrator")]
        public ActionResult DeleteUser(int id)
        {
            _service.Users.Delete(id);
            _bllUnit.Save();
            return RedirectToAction("IndexUser");
        }

        [Authorize(Roles = "Dispatcher")]
        public ActionResult IndexRequest()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new HttpUnauthorizedResult();
            }
            var model = new List<UserRequestModel>();
            var user = _service.GetUserByLogin(HttpContext.User.Identity.Name);
            foreach (var request in _service.Requests.GetAll().Where(r => r.Creator == user&& r.Status<RequestStatus.Closed))
            {
                model.Add(Mapper.Map<UserRequest, UserRequestModel>(request));
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult IndexRequestAdmin()
        {
            var model = new List<UserRequestModel>();
            var user = _service.GetUserByLogin(HttpContext.User.Identity.Name);
            foreach (var request in _service.Requests.GetAll().Where(r => r.AssignedTo == user && r.Status==RequestStatus.Created))
            {
                model.Add(Mapper.Map<UserRequest, UserRequestModel>(request));
            }
            return View("IndexRequest",model);
        }

        [Authorize]
        public ActionResult DetailsRequest(int id)
        {
            var model = Mapper.Map<UserRequest, UserRequestModel>(_service.Requests.GetById(id));
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Dispatcher")]
        public ActionResult CreateRequest()
        {
            return View(new UserRequestModel());
        }
        [HttpPost]
        [Authorize(Roles = "Dispatcher")]
        public ActionResult CreateRequest(UserRequestModel newRequest)
        {
            var currentUser = _service.GetUserByLogin(HttpContext.User.Identity.Name);
            var entity = new UserRequest()
            {
                Creator = currentUser,
                AssignedTo = currentUser.ChiefAdmin,
                CreateTime = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Status = RequestStatus.Created,
                Title = newRequest.Title,
                Text = newRequest.Text
            };
            _service.Requests.Insert(entity);
            return RedirectToAction("IndexRequest");
        }

        [Authorize(Roles = "Administrator,Dispatcher")]
        public ActionResult ResolveRequest(int id, RequestStatus requestStatus)
        {
            var request = _service.Requests.GetById(id);
            request.Status = requestStatus;
            request.LastModified = DateTime.UtcNow;
            _service.Requests.Update(request);
            _bllUnit.Save();
            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("IndexRequestAdmin");
            }
            return RedirectToAction("IndexRequest");
        }

        [Authorize(Roles = "System administrator")]
        public ActionResult IndexRoles()
        {
            var model = _service.Roles.GetAll().Select(a => new RoleModel()
            {
                Id = a.Id,
                Title = a.Title,
                AccessLevel = a.AccessLevel
            }).ToList();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "System administrator")]
        public ActionResult CreateRole()
        {
            return View(new RoleModel());
        }
        [HttpPost]
        [Authorize(Roles = "System administrator")]
        public ActionResult CreateRole(RoleModel role)
        {
            var entity = new Role
            {
                Id = role.Id,
                AccessLevel = role.AccessLevel,
                Title = role.Title
            };
            _service.Roles.Insert(entity);
            return RedirectToAction("IndexRoles");
        }

        [HttpGet]
        [Authorize(Roles = "System administrator")]
        public ActionResult EditRole(int id)
        {
            var model = Mapper.Map<Role,RoleModel>(_service.Roles.GetById(id));
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "System administrator")]
        public ActionResult EditRole(RoleModel role)
        {
            var entity = _service.Roles.GetById(role.Id);
            entity.Title = role.Title;
            entity.AccessLevel = role.AccessLevel;
            _service.Roles.Update(entity);
            _bllUnit.Save();
            return RedirectToAction("IndexRoles");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new AccountModel());
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AccountModel model, string returnUrl)
        {
            if (ModelState.IsValid)
                if (_service.Authenticate(model.Login, model.Password))
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index","Search");
                }
            ModelState.AddModelError("", "Wrong login or password.Please, try again");
            return View(model);
        }
        [Authorize]
        public ActionResult Logout()
        {
            _service.Logout();
            return RedirectToAction("Login");
        }
    }
}